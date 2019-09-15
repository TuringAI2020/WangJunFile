using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WangJun.Yun
{
    /// <summary>
    /// 文件存储服务
    /// </summary>
    public class FileService
    {

        protected string rootPath = "";
        public static FileService GetInst(string rootPath)
        {
            var inst = new FileService();
            inst.rootPath = rootPath;
            return inst;

        }
         
        public RES SaveTo(string folderPath,string fileName, byte[] data)
        {
            try
            {
                this.CreateFolder(folderPath);
                var filePath = $"{this.rootPath}\\{folderPath}\\{fileName}"; 
                File.WriteAllBytes(filePath, data);
                return RES.OK($"{folderPath}\\{fileName}");

            }
            catch (Exception ex)
            {
                return RES.FAIL($"{ex.Message}\t{((null != ex.InnerException) ? ex.InnerException.Message : string.Empty)}\t{folderPath}\\{fileName}");
            }
        } 
        public RES Delete(string path)
        {
            try
            {
                var filePath = $"{this.rootPath}\\{path}";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                return RES.FAIL("文件不存在");

            }
            catch (Exception ex)
            {
                return RES.FAIL($"{ex.Message}\t{((null != ex.InnerException) ? ex.InnerException.Message : string.Empty)}\t{path}");
            }
        } 
        public RES Query(string path)
        {
            try
            {
                var filePath = $"{this.rootPath}\\{path}";
                if (File.Exists(filePath))
                {
                    var fileInfo = new FileInfo(filePath);
                    return RES.OK(fileInfo);
                }
                return RES.FAIL("文件不存在");

            }
            catch (Exception ex)
            {
                return RES.FAIL($"{ex.Message}\t{((null != ex.InnerException) ? ex.InnerException.Message : string.Empty)}\t{path}");
            }
        } 
        public RES QueryDirectory(string path)
        {
            try
            {
                var dirPath = $"{this.rootPath}\\{path}";
                if (Directory.Exists(dirPath))
                {
                    var files = Directory.GetFiles(dirPath);
                    var folders = Directory.GetDirectories(dirPath);
                    var res = new List<string>(files);
                    res.AddRange(folders);
                    return RES.OK(res);
                }
                return RES.FAIL("目录不存在");

            }
            catch (Exception ex)
            {
                return RES.FAIL($"{ex.Message}\t{((null != ex.InnerException) ? ex.InnerException.Message : string.Empty)}\t{path}");
            }
        } 
        public RES Download(string path)
        {
            try
            {
                var filePath = $"{this.rootPath}\\{path}";
                if (File.Exists(filePath))
                {
                    var fileInfo = new FileInfo(filePath);
                    var fs = fileInfo.OpenRead();
                    byte[] buffer = new byte[(int)fs.Length];
                    fs.Read(buffer, 0, buffer.Length);

                    return RES.OK(buffer);
                }
                return RES.FAIL("文件不存在");
            }
            catch (Exception ex)
            {
                return RES.FAIL($"{ex.Message}\t{((null != ex.InnerException) ? ex.InnerException.Message : string.Empty)}\t{path}");
            }
        }

        public RES CreateFolder(string folderPath)
        {
            if (!string.IsNullOrWhiteSpace(folderPath)) {
                var absolutePath = this.rootPath;
                var arr = folderPath.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var queue = new Queue<string>(arr);
                while (0 < queue.Count)
                {
                    var folderName = queue.Dequeue();
                    var path = $"{absolutePath}\\{folderName}";
                    if (!Directory.Exists(path))
                    {
                        var dir = Directory.CreateDirectory(path); 
                        absolutePath = path;
                    }
                }
            }

            return RES.FAIL("路径不合法");

        }
    }
}
