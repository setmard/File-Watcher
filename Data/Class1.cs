using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Data
{
    public class CapaData
    {
        DataProductsEntities BD = new DataProductsEntities();

        public void InsertProduct(Producto p)
        {
            BD.Products.Add(
                new Products()
                {
                    IdType = p.idType,
                    IdColor = p.idColor,
                    IdBrand = p.idBrand,
                    IdProvider = p.idProvider,
                    IdCatalog = p.idCatalog,
                    Title = p.title,
                    Nombre = p.nombre,
                    Description = p.description,
                    Observations = p.observations,
                    PriceDistributor = p.priceDistributor,
                    PriceClient = p.priceClient,
                    PriceMember = p.priceMember,
                    IsEnabled = p.isEnabled,
                    DateUpdate = p.dateUpdate
                });
            BD.SaveChanges();
        }
        public void DeletedProduct(int id)
        {
            BD.Products.Remove(
                BD.Products.FirstOrDefault(p => p.Id == id)
                );
            BD.SaveChanges();
        }
        public void UpdateProduct(Producto p)
        {
            if (p.Id.HasValue)
            {
                var producto = (from o in BD.Products where o.Id == p.Id select o).FirstOrDefault();
                producto.IdBrand = p.idBrand;
                producto.IdCatalog = p.idCatalog;
                producto.IdProvider = p.idProvider;
                producto.IdType = p.idType;
                producto.Title = p.title;
                producto.Nombre = p.nombre;
                producto.PriceDistributor = p.priceDistributor;
                producto.PriceClient = p.priceClient;
                producto.PriceMember = p.priceMember;
                producto.Observations = p.observations;
                producto.Description = p.description;
                producto.IsEnabled = p.isEnabled;
                producto.DateUpdate = p.dateUpdate;
                BD.SaveChanges();
            }
            else
            {
                InsertProduct(p);
            }
        }

        public OnProducts GetLastRecordOn(string tableName)
        {
            if (tableName == Common.Strings.ChangesOnProductTableName)
            {
                int id = BD.ChangesOnProduct.OrderByDescending(p => p.IdLog).Select(p => p.IdLog).First();
                ChangesOnProduct last = BD.ChangesOnProduct.Where(p => p.IdLog == id).FirstOrDefault();
                return new OnProducts()
                {
                    IdLog = last.IdLog,
                    IdProduct = last.IdProduct,
                    ActionMade = last.ActionMade
                };
            }
            return new OnProducts();
        }


        public List<OnProducts> ReturnChangesOnTable(string tableName, int id)
        {
            if (tableName == Common.Strings.ChangesOnProductTableName)
            {
                List<OnProducts> a = new List<OnProducts>();
                var list = (from o in BD.ChangesOnProduct where o.IdLog >= id select o).ToList();
                foreach (var item in list)
                {
                    a.Add(new OnProducts
                    {
                        IdLog = item.IdLog,
                        IdProduct = item.IdProduct,
                        ActionMade = item.ActionMade
                    });
                }
                return a;
            }
            return new List<OnProducts>();
        }
        
        public Producto GetProductById(int id)
        {
            
            var producto = (from o in BD.Products where o.Id == id select o).ToList();
            producto.Add(new Products
                {
                    Id = producto[0].Id,
                    IdType = producto[0].IdType,
                    IdBrand = producto[0].IdBrand,
                    IdCatalog = producto[0].IdCatalog,
                    IdColor = producto[0].IdColor,
                    IdProvider = producto[0].IdProvider,
                    Nombre = producto[0].Nombre,
                    Title = producto[0].Title,
                    Description = producto[0].Description,
                    Observations = producto[0].Observations,
                    PriceDistributor = producto[0].PriceDistributor,
                    PriceClient = producto[0].PriceClient,
                    PriceMember = producto[0].PriceMember,
                    IsEnabled = producto[0].IsEnabled,
                    Keywords = producto[0].Keywords,
                    DateUpdate = producto[0].DateUpdate,
                });

            return new Producto(); 
            
        }
    }
}
