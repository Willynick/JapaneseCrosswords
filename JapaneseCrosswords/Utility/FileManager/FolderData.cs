using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JapaneseCrosswords.Utility.FileManager
{
    public enum ElementType
    {
        Folder,
        File
    }

    public class FolderElement
    {
        public string Name { get; set; }
        public string Information { get; set; }
        public string Path { get; set; }
        public ElementType Type { get; set; }
    }

    public class FolderData
    {
        private readonly string _folderpath;

        public FolderData(string folderpath)
        {
            _folderpath = folderpath;
        }

        public IEnumerable<FolderElement> GetFiles()
        {
            return Directory.GetFiles(_folderpath).Select(x => new FolderElement { Name = GetName(x), Information = GetInfromation(GetName(x)), Path = x, Type = ElementType.File });
        }

        public IEnumerable<FolderElement> GetFolders()
        {
            return Directory.GetDirectories(_folderpath).Select(x => new FolderElement { Name = GetName(x), Information = GetInfromation(GetName(x)), Path = x, Type = ElementType.Folder });
        }

        public IEnumerable<FolderElement> GetData()
        {
            return GetFiles().Union(GetFolders());
        }

        public string GetName(string x)
        {
            List<string> temp = new List<string>();
            temp = x.Split('\\', '.').ToList();
            return temp[temp.Count() - 2];
        }

        public string GetInfromation(string Name)
        {
            string fileName = _folderpath + "\\Information\\" + Name + ".txt";
            if (System.IO.File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    return sr.ReadToEnd();
                }
            }
            return "";
        }
    }
}
