#include "NTUndocumented.h"
#include "ProcessLister.h"
#include "Utility.h"

static PSYSTEM_PROCESS_INFORMATION GetRawProcessList()
{
	ULONG bufferSize = 0;
	PVOID bufferPtr = NULL;

	if (ZwQuerySystemInformation(SystemProcessInformation, 0, bufferSize, &bufferSize) == STATUS_INFO_LENGTH_MISMATCH)
	{
		bufferPtr = ExAllocatePool(NonPagedPool, bufferSize);

		if (bufferPtr != NULL)
		{
			ZwQuerySystemInformation(SystemProcessInformation, bufferPtr, bufferSize, &bufferSize);
		}
	}
	return (PSYSTEM_PROCESS_INFORMATION)bufferPtr;
}

static ULONG CalculateProcessListOutputSize(PSYSTEM_PROCESS_INFORMATION rawProcessList)
{
	int size = 0;

	while (rawProcessList->NextEntryOffset)
	{
		size += sizeof(PROCESS_SUMMARY);
		rawProcessList = (PSYSTEM_PROCESS_INFORMATION)(((CHAR*)rawProcessList) + rawProcessList->NextEntryOffset);
	}
	return size;
}

static PLDR_DATA_TABLE_ENTRY GetMainModuleDataTableEntry(PPEB64 peb)
{
	if (SanitizeUserPointer(peb, sizeof(PEB64)))
	{
		if (peb->Ldr)
		{
			if (SanitizeUserPointer(peb->Ldr, sizeof(PEB_LDR_DATA)))
			{
				if (!peb->Ldr->Initialized)
				{
					int initLoadCount = 0;

					while (!peb->Ldr->Initialized && initLoadCount++ < 4)
					{
						DriverSleep(250);
					}
				}

				if (peb->Ldr->Initialized)
				{
					return CONTAINING_RECORD(peb->Ldr->InLoadOrderModuleList.Flink, LDR_DATA_TABLE_ENTRY, InLoadOrderLinks);
				}
			}
		}
	}
	return NULL;
}

NTSTATUS GetProcessList(PVOID listedProcessBuffer, INT32 bufferSize, PINT32 requiredBufferSize, PINT32 processCount)
{
	PPROCESS_SUMMARY processSummary = (PPROCESS_SUMMARY)listedProcessBuffer;
	PSYSTEM_PROCESS_INFORMATION rawProcessList = GetRawProcessList();
	PVOID listHeadPointer = rawProcessList;
	*processCount = 0;

	if (rawProcessList)
	{
		int expectedBufferSize = CalculateProcessListOutputSize(rawProcessList);

		if (!listedProcessBuffer || bufferSize < expectedBufferSize)
		{
			*requiredBufferSize = expectedBufferSize;
			return STATUS_INFO_LENGTH_MISMATCH;
		}

		while (rawProcessList->NextEntryOffset)
		{
			PEPROCESS targetProcess;
			KAPC_STATE state;

			if (NT_SUCCESS(PsLookupProcessByProcessId(rawProcessList->UniqueProcessId, &targetProcess)))
			{
				PVOID mainModuleBase = NULL;
				PVOID mainModuleEntryPoint = NULL;
				UINT32 mainModuleImageSize = 0;
				PWCHAR mainModuleFileName = NULL;
				BOOLEAN isWow64 = 0;

				__try
				{
					KeStackAttachProcess(targetProcess, &state);

					__try
					{
						mainModuleBase = PsGetProcessSectionBaseAddress(targetProcess);

						if (mainModuleBase)
						{
							PPEB64 peb = (PPEB64)PsGetProcessPeb(targetProcess);

							if (peb)
							{
								PLDR_DATA_TABLE_ENTRY mainModuleEntry = GetMainModuleDataTableEntry(peb);
								mainModuleEntry = SanitizeUserPointer(mainModuleEntry, sizeof(LDR_DATA_TABLE_ENTRY));

								if (mainModuleEntry)
								{
									mainModuleEntryPoint = mainModuleEntry->EntryPoint;
									mainModuleImageSize = mainModuleEntry->SizeOfImage;
									isWow64 = IS_WOW64_PE(mainModuleBase);

									mainModuleFileName = ExAllocatePool(NonPagedPool, 256 * sizeof(WCHAR));
									RtlZeroMemory(mainModuleFileName, 256 * sizeof(WCHAR));
									RtlCopyMemory(mainModuleFileName, mainModuleEntry->FullDllName.Buffer, 256 * sizeof(WCHAR));
								}
							}
						}
					}
					__except (GetExceptionCode())
					{
						DbgPrintEx(0, 0, "Peb Interaction Failed.\n");
					}
				}
				__finally
				{
					KeUnstackDetachProcess(&state);
				}

				if (mainModuleFileName)
				{
					RtlCopyMemory(processSummary->MainModuleFileName, mainModuleFileName, 256 * sizeof(WCHAR));
					ExFreePool(mainModuleFileName);

					processSummary->ProcessId = rawProcessList->UniqueProcessId;
					processSummary->MainModuleBase = mainModuleBase;
					processSummary->MainModuleEntryPoint = mainModuleEntryPoint;
					processSummary->MainModuleImageSize = mainModuleImageSize;
					processSummary->WOW64 = isWow64;

					processSummary++;
					(*processCount)++;
				}

				ObDereferenceObject(targetProcess);
			}

			rawProcessList = (PSYSTEM_PROCESS_INFORMATION)(((CHAR*)rawProcessList) + rawProcessList->NextEntryOffset);
		}

		ExFreePool(listHeadPointer);
		return STATUS_SUCCESS;
	}
}

NTSTATUS QueryProcessInfo(INT32 pid, PVOID buffer, INT32 bufferSize, PINT32 moduleCount)
{
	PEPROCESS targetProcess;
	KAPC_STATE state;
	PMODULE_SUMMARY summary = (PMODULE_SUMMARY)buffer;

	if (NT_SUCCESS(PsLookupProcessByProcessId(pid, &targetProcess)))
	{
		PPEB64 peb = (PPEB64)PsGetProcessPeb(targetProcess);

		int count = 0;
		PVOID ModuleBase = NULL;
		PVOID ModuleEntryPoint = NULL;
		INT32 ModuleImageSize = 0;
		PWCHAR ModuleFileName = ExAllocatePool(NonPagedPool, 256 * sizeof(WCHAR));
		BOOLEAN isWow64 = 0;

		KeStackAttachProcess(targetProcess, &state);

		if (peb->Ldr->Initialized)
		{
			PLIST_ENTRY pEntry = NULL;
			PLIST_ENTRY pHeadEntry = &peb->Ldr->InLoadOrderModuleList;

			for (pEntry = pHeadEntry->Flink; pEntry != pHeadEntry; pEntry = pEntry->Flink)
			{
				if (++count * sizeof(MODULE_SUMMARY) > bufferSize)
				{
					return STATUS_INFO_LENGTH_MISMATCH;
				}

				PLDR_DATA_TABLE_ENTRY moduleEntry = SanitizeUserPointer(CONTAINING_RECORD(pEntry, LDR_DATA_TABLE_ENTRY, InLoadOrderLinks), sizeof(LDR_DATA_TABLE_ENTRY));

				if (moduleEntry)
				{
					RtlZeroMemory(ModuleFileName, 256 * sizeof(WCHAR));
					RtlCopyMemory(ModuleFileName, moduleEntry->FullDllName.Buffer, moduleEntry->FullDllName.Length * sizeof(WCHAR));

					ModuleBase = moduleEntry->DllBase;
					ModuleImageSize = moduleEntry->SizeOfImage;
					ModuleEntryPoint = moduleEntry->EntryPoint;
					isWow64 = IS_WOW64_PE(ModuleBase);

					KeUnstackDetachProcess(&state);

					RtlCopyMemory(summary->ModuleFileName, ModuleFileName, 256 * sizeof(WCHAR));
					summary->ModuleBase = ModuleBase;
					summary->ModuleEntryPoint = ModuleEntryPoint;
					summary->ModuleImageSize = ModuleImageSize;
					summary->WOW64 = isWow64;

					summary++;

					KeStackAttachProcess(targetProcess, &state);
				}
			}
		}

		KeUnstackDetachProcess(&state);

		ExFreePool(ModuleFileName);

		*moduleCount = count;
	}

	return STATUS_SUCCESS;
}
