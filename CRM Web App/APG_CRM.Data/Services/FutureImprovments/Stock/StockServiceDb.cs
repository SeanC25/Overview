using Microsoft.EntityFrameworkCore;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;

namespace APG_CRM.Data.Services
{
    public class StockServiceDb : IStockService

    {
        private readonly DatabaseContext db;
        public StockServiceDb(DatabaseContext context)
        {
            db = context;
        }

        public void Initialise()
        {
            db.Initialise(); // recreate database
        }

        public Stock AddStock(Stock st)
        {
            // perform any business logic around adding stock here - duplicate check etc.
            db.Stock.Add(st);
            db.SaveChanges();
            return st;
        }

        public List<Stock> GetAllStocks()
        {
            throw new NotImplementedException();
        }

        public Stock GetStockById(int id)
        {
            throw new NotImplementedException();
        }


        public bool GlassOutOfStock(int GlassId)
        {
            var stockItem = db.Stock.FirstOrDefault(s => s.GlassId == GlassId);
            if (stockItem == null)
                return true; // No stock record found for the given GlassId, assuming it's out of stock.

            return stockItem.InStock == 0;
        }


        public bool IsGlassStockLow(int GlassId)
        {
            var stockItem = db.Stock.FirstOrDefault(s => s.GlassId == GlassId);
            if (stockItem == null)
                return false; // No stock record found for the given GlassId,

            return stockItem.InStock < 50;
        }

        Supplier GetSupplieryBySupplierEmail(Supplier Email)
        {
            throw new NotImplementedException();
        }


        public Supplier UpdateSupplier(Supplier updated)
        {
            throw new NotImplementedException();
        }

        public Stock DeleteStock(int id)
        {
            throw new NotImplementedException();
        }

        // ====================Stock Entity Services Search Functions =========================

        public Stock GetStockByGlassName(string Namee)
        {
            throw new NotImplementedException();
        }
        
        public List<string> GetStockNamesByEmail(string Email)
        {
            var supplier = db.Suppliers.Include(s => s.Glasses) // or .Include(s => s.Stocks) if there's a direct Stocks property
            .FirstOrDefault(s => s.Email == Email);

            if (supplier == null)
                return new List<string>();

            //Glass has "Name" that refers to the stock name
            return supplier.Glasses.Select(g => g.Name).ToList();
        }


        public Supplier GetSupplieryByEmail(Supplier Email)
        {
            throw new NotImplementedException();
        }

        public Glass GetStockByName(string Name)
        {
            return db.Glasses.FirstOrDefault(g => g.Name == Name);
        }

    }

}