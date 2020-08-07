using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http.Formatting;
using System.IO;

namespace ltweb.Helper
{
    public static class MD5Helper
    {
        public static string Calculate(HttpPostedFileBase data)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = new MemoryStream())
                {
                    // Clone stream 
                    data.InputStream.CopyTo(stream);
                    stream.Position = 0;

                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            while (true)
            {
                int read = input.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                    return;
                output.Write(buffer, 0, read);
            }
        }
    }
}