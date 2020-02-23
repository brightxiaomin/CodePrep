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
            root = new Folder();
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
                    current.subFolders.Add(name, new Folder());

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
                current.files.Add(fileName, content);
            else
            {
                current.files[fileName] = current.files[fileName] + content;
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
            return current.files[fileName];

        }

        public List<string> ReadFolder(Folder node)
        {
            var folders = node.subFolders.Keys;
            var files = node.files.Keys;
            var res = folders.Concat(files).ToList();
            res.Sort();
            return res;
        }
    }

    public class Folder
    {
        //public string name;
        //public List<Folder> subFolders; 

        public Dictionary<string, Folder> subFolders = new Dictionary<string, Folder>();
        public Dictionary<string, string> files = new Dictionary<string, string>(); // store filename maps to fileContent
        //public List<File> files;

       // constructor is not needed
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
