using KsDumperClient.Utility;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace KsDumperClient
{
    public class ModuleSummary
    {
        public ulong ModuleEntryPoint { get; private set; }
        public ulong ModuleBase { get; private set; }
        public string ModuleFileName { get; private set; }
        public uint ModuleImageSize { get; private set; }
        public bool IsWOW64 { get; private set; }

        private ModuleSummary(ulong moduleBase, string moduleFileName, uint moduleImageSize, ulong moduleEntryPoint, bool isWow64)
        {
            ModuleBase = moduleBase;
            ModuleFileName = FixFileName(moduleFileName);
            ModuleImageSize = moduleImageSize;
            ModuleEntryPoint = moduleEntryPoint;
            IsWOW64 = isWow64;
        }

        private string FixFileName(string fileName)
        {
            if (fileName.StartsWith(@"\"))
            {
                return fileName;
            }

            StringBuilder sb = new StringBuilder(256);
            int length = WinApi.GetLongPathName(fileName, sb, sb.Capacity);

            if (length > sb.Capacity)
            {
                sb.Capacity = length;
                length = WinApi.GetLongPathName(fileName, sb, sb.Capacity);
            }
            return sb.ToString();
        }

        public static ModuleSummary FromStream(BinaryReader reader)
        {
            return new ModuleSummary 
            (
                reader.ReadUInt64(),
                Encoding.Unicode.GetString(reader.ReadBytes(512)).Split('\0')[0],
                reader.ReadUInt32(),
                reader.ReadUInt64(),
                reader.ReadBoolean()
            );
        }
    }
}
