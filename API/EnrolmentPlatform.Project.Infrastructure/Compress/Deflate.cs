using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.Compress
{
   public class Deflate
    {
        /// <summary>
        /// Deflate压缩
        /// </summary>
        /// <param name="buffer">字节流</param>
        public static byte[] DeflateCompress(byte[] data)
        {
            if (data == null || data.Length < 1)
                return data;
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    using (DeflateStream gZipStream = new DeflateStream(stream, CompressionMode.Compress))
                    {
                        gZipStream.Write(data, 0, data.Length);
                        gZipStream.Close();
                    }
                    return stream.ToArray();
                }
            }
            catch (Exception)
            {
                return data;
            }
        }
    }
}
