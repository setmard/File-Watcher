using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace FileWatcher
{
    public class Observer
    {
        private FileSystemWatcher watcher;
        private Data.CapaData D;
        public Observer(string path)
        {
            watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.Deleted += DeletedFile;
            watcher.Changed += SavedFile;
            watcher.EnableRaisingEvents = true;
            var common = new Common.XMLFile();
            D = new Data.CapaData();
        }
        public void DeletedFile(object source, FileSystemEventArgs e)
        {
            if (!Program.lockedO)
            {
                Program.lockedTM = true;
                D.DeletedProduct(int.Parse(e.Name.Split('.')[0]));
                Program.lockedTM = false;
            }
            Console.WriteLine("Eliminado");
        }
        public void SavedFile(object source, FileSystemEventArgs e)
        {
            if (!Program.lockedO)
            {
                Program.lockedTM = true;
                D.UpdateProduct(Common.XMLFile.DeserializeList<Producto>(e.Name));
                Program.lockedTM = false;
            }
            Console.WriteLine("Archivo Guardado");
        }
    }
}
