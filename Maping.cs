using MapingFolders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MapingFolders
{
    public class Maping
    {
        public bool control { get; set; }
        public string path { get; set; }
        public List<string> Folders_string { get; set; } = new List<string>();
        public string logPath = "C:\\Users\\Playlist\\Documents\\MapingLogs";
        
        public string pathverification()
        {
            string folderPath = logPath;

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    return folderPath;
                }
                else
                {
                    return folderPath;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the folder: {ex.Message}");
                return null;
            }
        }
        public Maping(List<FolderOB> path)
        {

            foreach (var item in path)
            {
                Folders_string.Add(item.FolderPath);
            }
        }
        public void map()
        {
            foreach (var item in Folders_string)
            {
                var watcher = new FileSystemWatcher();
                watcher.Path = item;
                watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastAccess | NotifyFilters.LastWrite
                         | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                watcher.Deleted += new FileSystemEventHandler(OnDeleted);
                watcher.Created += new FileSystemEventHandler(OnCreated);
                watcher.Renamed += new RenamedEventHandler(OnRenamed);
                watcher.Filter = "*.*";
                watcher.EnableRaisingEvents = true;
            }
        }
        private readonly object logLock = new object();
        private void LogToFile(string message)
        {
            lock (logLock)
            {
                try
                {
                    string logPath = pathverification();
                    if (!File.Exists(logPath + @"\log.txt"))
                    {
                        File.Create(logPath + @"\log.txt").Dispose();
                        using (var writer = new StreamWriter(logPath + @"\log.txt", false))
                        {
                            writer.WriteLine("{0} - {1}", DateTime.Now, "Log criado com sucesso");
                            writer.WriteLine("{0} - {1}", DateTime.Now, message);
                        }
                    }
                    else
                    {
                        using (var writer = new StreamWriter(logPath + @"\log.txt", true))
                        {
                            writer.WriteLine("{0} - {1}", DateTime.Now, message);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
        private void OnRenamed(object source, RenamedEventArgs e)
        {
            string renamed = $"User: {Environment.UserName} Renamed the folder or file: {e.OldFullPath} for: {e.FullPath}";
            LogToFile(renamed);
        }
        private void OnCreated(object source, FileSystemEventArgs e)
        {
            if (File.Exists(e.FullPath))
            {
                string created = $"User: {Environment.UserName} Created the file: {e.FullPath}";
                LogToFile(created);
            }
            else if (Directory.Exists(e.FullPath))
            {
                if (Path.GetExtension(e.FullPath) == "")
                {
                    string created = $"User: {Environment.UserName} Created the folder: {e.FullPath}";
                    LogToFile(created);
                }
            }
        }
        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            string delete = $"User: {Environment.UserName} Deleted the folder or file: {e.FullPath}";
            LogToFile(delete);

        }
    }
}
