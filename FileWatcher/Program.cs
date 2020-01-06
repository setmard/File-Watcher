using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace FileWatcher
{
    class Program
    {
        public static bool lockedTM, lockedO;
        static void Main(string[] args)
        {
            Observer ws = new Observer(Common.XMLFile.PATH);
            TableMonitor tb = new TableMonitor(Strings.ChangesOnProductTableName);
            
            Console.ReadLine();
        }
    }
}
