using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    public class FileSystem
    {
        private Folder root;

        public FileSystem()
        {
            root = new Folder("/");
        }

        public IList<string> Ls(string path)
        {
            if (path == "/") return ReadFolder(root);
            char[] delimiter = { '/' };
            string[] list = path.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            Folder current = root;
            for(int i  = 0; i < list.Length - 1; i++)
            {
                current = current.subFolders[list[i]];
            }

            string lastName = list[list.Length - 1];
            if (current.files.ContainsKey(lastName))
            {
              return  new List<string> { lastName };
            }
            current = current.subFolders[lastName];
            return ReadFolder(current);
        }

        public void Mkdir(string path)
        {
            char[] delimiter = { '/' };
            string[] list = path.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            Folder current = root;
            for (int i = 0; i < list.Length; i++)
            {
                string name = list[i];
                if (!current.subFolders.ContainsKey(name))
                    current.subFolders.Add(name, new Folder(name));

                current = current.subFolders[name];
            }
        }

        public void AddContentToFile(string filePath, string content)
        {
            char[] delimiter = { '/' };
            string[] list = filePath.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            Folder current = root;
            for (int i = 0; i < list.Length - 1; i++)
                current = current.subFolders[list[i]];
            string fileName = list[list.Length - 1];
            if (!current.files.ContainsKey(fileName))
                current.files.Add(fileName, new File(fileName, content));
            else
            {
                current.files[fileName].content = current.files[fileName].content + content;
            }

        }

        public string ReadContentFromFile(string filePath)
        {
            char[] delimiter = { '/' };
            string[] list = filePath.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            Folder current = root;
            for (int i = 0; i < list.Length - 1; i++)
                current = current.subFolders[list[i]];
            string fileName = list[list.Length - 1];
            return current.files[fileName].content;

        }

        public List<string> ReadFolder(Folder node)
        {
            var folders = node.subFolders.Keys;
            var files = node.files.Keys;
            var res = folders.Concat(files).ToList();
            res.Sort();
            return res;
        }

        public Folder GetLastFolder(string path)
        {
            char[] delimiter = { '/' };
            string[] list = path.Split(delimiter);
            Folder current = root;
            for (int i = 0; i < list.Length - 1; i++)
                current = current.subFolders[list[i]];
            return current;
        }
    }

    public class Folder
    {
        public string name;
        //public List<Folder> subFolders; 

        public Dictionary<string, Folder> subFolders;
        public Dictionary<string, File> files;
        //public List<File> files;
        public Folder(string name)
        {
            this.name = name;
            subFolders = new Dictionary<string, Folder>();
            files = new Dictionary<string, File>();
        }
    }

    public class File
    {
        public string name;
        public string content;
        public File(string name, string content)
        {
            this.name = name;
            this.content = content;
        }
    }

}
