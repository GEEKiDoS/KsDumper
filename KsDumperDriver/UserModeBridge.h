#pragma once
#include <ntddk.h>

#define IO_GET_PROCESS_LIST CTL_CODE(FILE_DEVICE_UNKNOWN, 0x1724, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)

#define IO_COPY_MEMORY CTL_CODE(FILE_DEVICE_UNKNOWN, 0x1725, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)

#define IO_QUERY_PROCESS_INFO CTL_CODE(FILE_DEVICE_UNKNOWN, 0x1726, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)

typedef struct _KERNEL_PROCESS_LIST_OPERATION
{
	PVOID bufferAddress;
	INT32 bufferSize;
	INT32 processCount;
} KERNEL_PROCESS_LIST_OPERATION, *PKERNEL_PROCESS_LIST_OPERATION;

typedef struct _KERNEL_COPY_MEMORY_OPERATION
{
	INT32 targetProcessId;
	PVOID targetAddress;
	PVOID bufferAddress;
	INT32 bufferSize;
} KERNEL_COPY_MEMORY_OPERATION, *PKERNEL_COPY_MEMORY_OPERATION;

typedef struct _KERNEL_QUERY_PROCESS_INFO_OPERATION
{
	INT32 targetProcessId;
	INT32 moduleCount;
	INT32 bufferSize;
	PVOID bufferAddress;
} KERNEL_QUERY_PROCESS_INFO_OPERATION, *PKERNEL_QUERY_PROCESS_INFO_OPERATION;
