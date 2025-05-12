using System;
using System.IO;
using System.Security.Cryptography;
using Gml.Web.Api.Domains.System;

namespace Gml.Common
{
    public static class SystemHelper
    {
       public static string CalculateFileHash(string filePath, HashAlgorithm algorithm)
        {
            var fileInfo = new FileInfo(filePath);
            
            // Combine file attributes that can be obtained quickly
            string fileName = Path.GetFileName(filePath);
            long fileLength = fileInfo.Length;
            long lastWriteTimeTicks = fileInfo.LastWriteTimeUtc.Ticks;
            
            // Create a unique identifier from these attributes
            string uniqueId = $"{fileName}_{fileLength}_{lastWriteTimeTicks}";
            
            // Convert to a format similar to a hash (hex string)
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(uniqueId);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }
        public static string GetStringOsType(OsType osType)
        {
            switch (osType)
            {
                case OsType.Undefined:
                    throw new PlatformNotSupportedException();
                case OsType.Linux:
                    return "linux";
                case OsType.OsX:
                    return "osx";
                case OsType.Windows:
                    return "windows";
                default:
                    throw new ArgumentOutOfRangeException(nameof(osType), osType, null);
            }
        }
    }
}
