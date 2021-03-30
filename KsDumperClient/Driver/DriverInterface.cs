using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using KsDumperClient.Utility;

using static KsDumperClient.Driver.Operations;

namespace KsDumperClient.Driver
{
    public class DriverInterface
    {
        private readonly IntPtr driverHandle;

        public DriverInterface(string registryPath)
        {
            driverHandle = WinApi.CreateFileA(registryPath, FileAccess.ReadWrite, 
                FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
        }

        public bool HasValidHandle()
        {
            return driverHandle != WinApi.INVALID_HANDLE_VALUE;
        }

        public int GetModuleSummaryList(int pid, out ModuleSummary[] result)
        {
            result = new ModuleSummary[0];

            if (driverHandle != WinApi.INVALID_HANDLE_VALUE)
            {
                // Im lazy as fuck, so I just open a LARGE buffer
                int bufferSize = 1000 * 533;

                IntPtr bufferPointer = MarshalUtility.AllocZeroFilled(bufferSize);

                KERNEL_QUERY_PROCESS_INFO_OPERATION operation = new KERNEL_QUERY_PROCESS_INFO_OPERATION
                {
                    targetProcessId = pid,
                    bufferSize = bufferSize,
                    bufferAddress = (ulong)bufferPointer.ToInt64()
                };

                IntPtr operationPointer = MarshalUtility.CopyStructToMemory(operation);
                int operationSize = Marshal.SizeOf<KERNEL_QUERY_PROCESS_INFO_OPERATION>();

                if (WinApi.DeviceIoControl(driverHandle, IO_QUERY_PROCESS_INFO, operationPointer, operationSize, operationPointer, operationSize, IntPtr.Zero, IntPtr.Zero))
                {
                    operation = MarshalUtility.GetStructFromMemory<KERNEL_QUERY_PROCESS_INFO_OPERATION>(operationPointer);

                    if(operation.moduleCount > 0)
                    {
                        byte[] managedBuffer = new byte[bufferSize];
                        Marshal.Copy(bufferPointer, managedBuffer, 0, bufferSize);
                        Marshal.FreeHGlobal(bufferPointer);

                        result = new ModuleSummary[operation.moduleCount];

                        using (BinaryReader reader = new BinaryReader(new MemoryStream(managedBuffer)))
                        {
                            for (int i = 0; i < result.Length; i++)
                            {
                                result[i] = ModuleSummary.FromStream(reader);
                            }
                        }

                        return result.Length;
                    }
                }
                else
                {
                    int errCode = Marshal.GetLastWin32Error();

                    IntPtr tempptr = IntPtr.Zero;
                    string msg = null;
                    WinApi.FormatMessage(0x1300, ref tempptr, errCode, 0, ref msg, 255, ref tempptr);

                    MessageBox.Show(msg);
                }
            }

            return 0;
        }

        public bool GetProcessSummaryList(out ProcessSummary[] result)
        {
            result = new ProcessSummary[0];

            if (driverHandle != WinApi.INVALID_HANDLE_VALUE)
            {
                int requiredBufferSize = GetProcessListRequiredBufferSize();

                if (requiredBufferSize > 0)
                {
                    IntPtr bufferPointer = MarshalUtility.AllocZeroFilled(requiredBufferSize);
                    KERNEL_PROCESS_LIST_OPERATION operation = new KERNEL_PROCESS_LIST_OPERATION
                    {
                        bufferAddress = (ulong)bufferPointer.ToInt64(),
                        bufferSize = requiredBufferSize
                    };
                    IntPtr operationPointer = MarshalUtility.CopyStructToMemory(operation);
                    int operationSize = Marshal.SizeOf<KERNEL_PROCESS_LIST_OPERATION>();

                    if (WinApi.DeviceIoControl(driverHandle, IO_GET_PROCESS_LIST, operationPointer, operationSize, operationPointer, operationSize, IntPtr.Zero, IntPtr.Zero))
                    {
                        operation = MarshalUtility.GetStructFromMemory<KERNEL_PROCESS_LIST_OPERATION>(operationPointer);

                        if (operation.processCount > 0)
                        {
                            byte[] managedBuffer = new byte[requiredBufferSize];
                            Marshal.Copy(bufferPointer, managedBuffer, 0, requiredBufferSize);
                            Marshal.FreeHGlobal(bufferPointer);

                            result = new ProcessSummary[operation.processCount];

                            using (BinaryReader reader = new BinaryReader(new MemoryStream(managedBuffer)))
                            {
                                for (int i = 0; i < result.Length; i++)
                                {
                                    result[i] = ProcessSummary.FromStream(reader);
                                }
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private int GetProcessListRequiredBufferSize()
        {
            IntPtr operationPointer = MarshalUtility.AllocEmptyStruct<KERNEL_PROCESS_LIST_OPERATION>();
            int operationSize = Marshal.SizeOf<KERNEL_PROCESS_LIST_OPERATION>();

            if (WinApi.DeviceIoControl(driverHandle, IO_GET_PROCESS_LIST, operationPointer, operationSize, operationPointer, operationSize, IntPtr.Zero, IntPtr.Zero))
            {
                KERNEL_PROCESS_LIST_OPERATION operation = MarshalUtility.GetStructFromMemory<KERNEL_PROCESS_LIST_OPERATION>(operationPointer);

                if (operation.processCount == 0 && operation.bufferSize > 0)
                {
                    return operation.bufferSize;
                }
            }
            return 0;
        }

        public bool CopyVirtualMemory(int targetProcessId, IntPtr targetAddress, IntPtr bufferAddress, int bufferSize)
        {
            if (driverHandle != WinApi.INVALID_HANDLE_VALUE)
            {
                KERNEL_COPY_MEMORY_OPERATION operation = new KERNEL_COPY_MEMORY_OPERATION
                {
                    targetProcessId = targetProcessId,
                    targetAddress = (ulong)targetAddress.ToInt64(),
                    bufferAddress = (ulong)bufferAddress.ToInt64(),
                    bufferSize = bufferSize
                };

                IntPtr operationPointer = MarshalUtility.CopyStructToMemory(operation);

                bool result = WinApi.DeviceIoControl(driverHandle, IO_COPY_MEMORY, operationPointer, Marshal.SizeOf<KERNEL_COPY_MEMORY_OPERATION>(), IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero);
                Marshal.FreeHGlobal(operationPointer);

                return result;
            }
            return false;
        }
    }
}
