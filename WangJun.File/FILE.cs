using System;
using System.Collections.Generic;
using System.IO;

namespace WangJun.Yun
{
    public class FILE
    {
        protected Stack<string> folderStack = new Stack<string>();
        public int StackDeep {
            get {
                return this.folderStack.Count;
            }
        }
        public void AddFolder(string folderPath)
        {
            this.folderStack.Push(folderPath);
        }
        public RES Traverse(string rootPath, EventHandler<CallbackEventAgrs> fileCallback, EventHandler<CallbackEventAgrs> foldlerCallback)
        {
            if (!string.IsNullOrWhiteSpace(rootPath))
            {
                if (Directory.Exists(rootPath))
                {

                    this.AddFolder(rootPath);
                    while (0 < this.folderStack.Count)
                    {
                        try
                        {
                            var cur = this.folderStack.Pop();
                            var files = Directory.EnumerateFiles(cur);


                            foreach (var file in files)
                            {
                                if (null != fileCallback)
                                {
                                    fileCallback(this, new CallbackEventAgrs { DATA = file });
                                }
                            }

                            var subFolders = Directory.EnumerateDirectories(cur);

                            foreach (var folder in subFolders)
                            {
                                if (null != foldlerCallback)
                                {
                                    foldlerCallback(this, new CallbackEventAgrs { DATA = folder });
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine($"{ex.Message}");
                        }

                    }

                }
            }
            return RES.OK();
        }
    }
}
