using Common;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher
{
    class TableMonitor
    {
        private Data.CapaData d;
        private int currentId;
        private int newId;
        private string tableName;
        


        public TableMonitor(string tableName)
        {
            this.tableName = tableName;
            d = new CapaData();
            currentId = d.GetLastRecordOn(Strings.ChangesOnProductTableName).IdLog;
            

            Start();
        }

        private async Task Start()
        {
            while (true)
            {
                await StartAsync();
            }
        }
            
        private async Task StartAsync()
        {
                if (!Program.lockedTM)
                {
                    newId = d.GetLastRecordOn(Strings.ChangesOnProductTableName).IdLog;
                    if (currentId != newId)
                    {
                        List<OnProducts> newRows = d.ReturnChangesOnTable(tableName, currentId + 1);
                        foreach (OnProducts row in newRows)
                        {
                            switch (row.ActionMade)
                            {
                                case 1:
                                    InsertWasMAde(row.IdProduct);
                                Console.WriteLine("Se insertó");
                                    break;
                                case 2:
                                    DeleteWasMade(row.IdProduct);
                                Console.WriteLine("Se borró");
                                    break;
                                case 3:
                                    UpdateWasMade(row.IdProduct);
                                Console.WriteLine("Se actualizó");
                                    break;
                                default:
                                    Console.WriteLine("Error");
                                    break;

                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine("No hay cambios");
                    }
                }
            
        }
            
    


        public void DeleteWasMade(int IdProduct)
        {
            Program.lockedO = true;
            Common.XMLFile.Delete(IdProduct + ".xml");
            currentId = d.GetLastRecordOn(tableName).IdLog;
            Program.lockedO = false;
        }

        public void UpdateWasMade(int IdProduct)
        {
            Program.lockedO = true;
            var UpdateProduct = d.GetProductById(IdProduct);
            Common.XMLFile.Delete(IdProduct + ".xml");
            Common.XMLFile.SerializeList(UpdateProduct, IdProduct + ".xml");
            currentId = d.GetLastRecordOn(tableName).IdLog;
            Program.lockedO = false;
            
        }

        private void InsertWasMAde(int IdProduct)
        {
            Program.lockedO = true;
            Producto p = d.GetProductById(IdProduct);
            currentId = d.GetLastRecordOn(tableName).IdLog;
            Program.lockedO = false;
        }

        
    }

}
