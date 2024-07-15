using Common.Enums;
using EndpointProtector.Models.Ram;

namespace Common.Extensions
{
    internal static class LongExtensions
    {
        private static (double, UnitRepresentation) FormatMemorySize(long bytes)
        {
            string[] suffix = ["B", "KB", "MB", "GB", "TB"];

            double dblSByte = bytes;
            int i;

            for (i = 0; i < suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return (dblSByte, (UnitRepresentation)i);
        }

        public static Storage ConvertToStorage(this long bytes)
        {
            var (size, unit) = FormatMemorySize(bytes);
            return new Storage(size, unit);
        }

        public static string ConvertToDiskSize(this long bytes)
        {
            var (size, unit) = FormatMemorySize(bytes);
            return string.Format("{0:0.##} {1}", size, unit);
        }
    }
}
