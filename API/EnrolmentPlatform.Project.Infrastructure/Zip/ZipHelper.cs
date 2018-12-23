using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

namespace EnrolmentPlatform.Project.Infrastructure.Zip
{
    public class ZipHelper
    {
        #region 压缩目录

        /// <summary>
        /// 压缩目录
        /// </summary>
        /// <param name="dirPath">需要压缩的目录</param>
        /// <param name="targetFullFileName">解压的目录完整文件</param>
        /// <param name="compressionLevel">压缩比(0-9)</param>
        /// <param name="password">密码</param>
        /// <param name="comment">备注信息</param>
        /// <returns></returns>
        public static void ZipFile(string dirPath, string targetFullFileName, int compressionLevel,
            string password, string comment)
        {
            DirectoryInfo di = new DirectoryInfo(dirPath);
            if (di.Exists)
            {
                List<FileInfo> files = new List<FileInfo>();
                GetChildFile(di, files);
                if (files.Count > 0)
                {
                    string[] fileArr = new string[files.Count];
                    for (int a = 0; a < files.Count; a++)
                    {
                        fileArr[a] = files[a].FullName;
                    }
                    ZipFile(fileArr, targetFullFileName, compressionLevel, password, comment, dirPath);
                }
            }
        }

        /// <summary>
        /// 递归获得某个文件夹下所有文件，级所有子目录下的所有文件
        /// </summary>
        /// <param name="di">文件夹根目录</param>
        /// <param name="list">文件集合</param>
        private static void GetChildFile(DirectoryInfo di, List<FileInfo> list)
        {
            FileInfo[] fis = di.GetFiles();
            foreach (FileInfo fi in fis)
            {
                list.Add(fi);
            }

            foreach (DirectoryInfo cd in di.GetDirectories())
            {
                GetChildFile(cd, list);
            }
        }

        #endregion

        #region 压缩文件

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileNameToZip">要压缩文件(绝对文件路径)</param>
        /// <param name="targetFullFileName">压缩(绝对文件路径)</param>
        /// <param name="compressionLevel">压缩比(0-9)</param>
        /// <param name="password">加密密码</param>
        /// <param name="comment">压缩文件描述</param>
        /// <param name="dirPath">目录名称，如果为空所有文件都在根目录下，不为空则有文件层级</param>
        /// <returns>异常信息</returns>
        public static string ZipFile(string[] fileNameToZip, string targetFullFileName, int compressionLevel = 6,
            string password = null, string comment = null, string dirPath = null)
        {
            try
            {
                foreach (string name in fileNameToZip)
                {
                    if (!File.Exists(name))
                    {
                        throw new Exception("文件[" + name + "]不存在");
                    }
                }

                //创建ZipFileOutPutStream
                using (ZipOutputStream newzipstream = new ZipOutputStream(File.Open(targetFullFileName,
                    FileMode.OpenOrCreate)))
                {

                    //判断Password
                    if (password != null && password.Length > 0)
                    {
                        newzipstream.Password = password;
                    }
                    if (comment != null && comment.Length > 0)
                    {
                        newzipstream.SetComment(comment);
                    }
                    //设置CompressionLevel
                    newzipstream.SetLevel(compressionLevel); //-查看0 - means store only to 9 - means best compression 

                    //执行压缩
                    foreach (string filename in fileNameToZip)
                    {
                        FileStream newstream = File.OpenRead(filename);//打开预压缩文件
                        byte[] setbuffer = new byte[newstream.Length];
                        newstream.Read(setbuffer, 0, setbuffer.Length);//读入文件

                        //新建ZipEntrity
                        ZipEntry newEntry = null;
                        //如果给定的目录不是空，则压缩的文件有相对路径
                        if (!string.IsNullOrEmpty(dirPath))
                        {
                            string newFileName = Path.GetFullPath(filename);
                            newEntry = new ZipEntry(newFileName.Replace(dirPath, "").TrimStart('\\'));
                        }
                        else
                        {
                            //如果给定的目录为空，则所有文件都在根目录
                            newEntry = new ZipEntry(Path.GetFileName(filename));
                        }

                        //设置时间-长度
                        newEntry.DateTime = DateTime.Now;
                        newEntry.Size = newstream.Length;
                        newstream.Close();
                        newzipstream.PutNextEntry(newEntry);//压入
                        newzipstream.Write(setbuffer, 0, setbuffer.Length);

                    }
                    //重复压入操作
                    newzipstream.Finish();
                    newzipstream.Close();
                }
            }
            catch (Exception e)
            {
                //出现异常
                File.Delete(targetFullFileName);
                return e.Message.ToString();
            }

            return "";
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="dic">Dictionary<文件夹, Dictionary<文件名称,文件地址>></param>
        /// <param name="targetFullFileName">压缩目标文件(绝对文件路径)</param>
        /// <param name="compressionLevel">压缩比(0-9)</param>
        /// <param name="password">加密密码</param>
        /// <param name="comment">压缩文件描述</param>
        /// <returns>异常信息</returns>
        public static string ZipFile(Dictionary<string, Dictionary<string, string>> dic, string targetFullFileName, int compressionLevel = 6,
            string password = null, string comment = null)
        {
            try
            {
                //创建ZipFileOutPutStream
                using (ZipOutputStream newzipstream = new ZipOutputStream(File.Open(targetFullFileName,
                    FileMode.OpenOrCreate)))
                {
                    //判断Password
                    if (password != null && password.Length > 0)
                    {
                        newzipstream.Password = password;
                    }
                    if (comment != null && comment.Length > 0)
                    {
                        newzipstream.SetComment(comment);
                    }
                    //设置CompressionLevel
                    newzipstream.SetLevel(compressionLevel); //-查看0 - means store only to 9 - means best compression 

                    //执行压缩
                    foreach (var item in dic)
                    {
                        foreach (var file in item.Value)
                        {
                            if (File.Exists(file.Value) == false)
                            {
                                continue;
                            }

                            FileStream newstream = File.OpenRead(file.Value);//打开预压缩文件
                            byte[] setbuffer = new byte[newstream.Length];
                            newstream.Read(setbuffer, 0, setbuffer.Length);//读入文件

                            //新建ZipEntrity
                            ZipEntry newEntry = newEntry = new ZipEntry(item.Key + "/" + file.Key);

                            //设置时间-长度
                            newEntry.DateTime = DateTime.Now;
                            newEntry.Size = newstream.Length;
                            newstream.Close();
                            newzipstream.PutNextEntry(newEntry);//压入
                            newzipstream.Write(setbuffer, 0, setbuffer.Length);
                        }

                    }
                    //重复压入操作
                    newzipstream.Finish();
                    newzipstream.Close();
                }
            }
            catch (Exception e)
            {
                //出现异常
                File.Delete(targetFullFileName);
                return e.Message;
            }
            return "";
        }

        #endregion

        #region 解压文件

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="zipfilename">要解压文件Zip(物理路径)</param>
        /// <param name="unZipDir">解压目的路径(物理路径)</param>
        /// <param name="password">解压密码</param>
        /// <returns>异常信息</returns>
        public static string UnZipFile(string zipfilename, string unZipDir, string password)
        {
            //判断待解压文件路径
            if (!File.Exists(zipfilename))
            {
                File.Delete(unZipDir);
                return "待解压文件路径不存在!";
            }

            //执行解压操作
            try
            {
                //创建ZipInputStream
                using (ZipInputStream newinStream = new ZipInputStream(File.OpenRead(zipfilename)))
                {

                    //判断Password
                    if (password != null && password.Length > 0)
                    {
                        newinStream.Password = password;
                    }

                    ZipEntry theEntry;
                    //获取Zip中单个File
                    while ((theEntry = newinStream.GetNextEntry()) != null)
                    {
                        //判断目的路径
                        if (!Directory.Exists(unZipDir))
                        {
                            Directory.CreateDirectory(unZipDir);//创建目的目录
                        }
                        //获得目的目录信息
                        string pathname = Path.GetDirectoryName(theEntry.Name);//获得子级目录
                        string filename = Path.GetFileName(theEntry.Name);//获得子集文件名
                        //处理文件盘符问题
                        pathname = pathname.Replace(":", "$");//处理当前压缩出现盘符问题
                        string driectoryname = Path.Combine(unZipDir, pathname);

                        //目录处理
                        if (!Directory.Exists(driectoryname))
                        {
                            Directory.CreateDirectory(driectoryname);
                        }

                        //解压指定子目录
                        if (filename != string.Empty)
                        {
                            FileStream newstream = File.Create(driectoryname + "\\" + filename);
                            int size = 2048;
                            byte[] newbyte = new byte[size];
                            while (true)
                            {
                                size = newinStream.Read(newbyte, 0, newbyte.Length);
                                if (size > 0)
                                {
                                    //写入数据
                                    newstream.Write(newbyte, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            newstream.Close();
                        }
                    }
                    newinStream.Close();
                }
            }
            catch (Exception se)
            {
                return se.Message.ToString();
            }
            return "";
        }

        #endregion
    }
}
