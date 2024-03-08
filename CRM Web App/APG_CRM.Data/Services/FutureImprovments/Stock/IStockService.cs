using Microsoft.EntityFrameworkCore;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;

namespace APG_CRM.Data.Services;

public interface IStockService
{
    void Initialise();

    // ==================== Stock Entity Services Management =========================

    Stock AddStock(Stock st);
    Stock DeleteStock(int id);
    List<Stock> GetAllStocks();
    Stock GetStockById(int id);

    bool GlassOutOfStock(int GlassId);// Instock= 0

    bool IsGlassStockLow(int GlassId);// Instock= Less than 50

  
    Supplier UpdateSupplier(Supplier updated);

    // ====================Stock Entity Services Search Functions =========================

    Stock GetStockByGlassName(string Name);


    // ====================Glass Entity Services Search Functions =========================
    Glass GetStockByName(string Name);

}


