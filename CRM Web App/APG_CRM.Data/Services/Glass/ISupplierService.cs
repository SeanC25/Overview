using Microsoft.EntityFrameworkCore;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;

namespace APG_CRM.Data.Services;

// This interface describes the functions that a ContactService class will implement
public interface ISupplierService
{
    void Initialise();


    // ***==================== Suppliers Entity Services Management CRUD operations (Create, Read, Update, Delete), =========================

    Supplier AddSupplier(Supplier s);  //Create method
    Supplier GetSupplierById(int id); //read method
    Supplier UpdateSupplier(Supplier s); //update method
    bool DeleteSupplier(int id);  //Delete method

    // ***==================== Supplier Entity Services  helpter methods/Search Functions =========================

    //List<Supplier> GetAllSuppliers();
    IQueryable<Supplier> GetAllSuppliers();
    Supplier GetSupplierByName(string Name);
    Supplier GetSupplieryByEmail(string Email);
    Supplier GetSupplierByPostCode(string PostCode);

    IList<Supplier> SearchSuppliers(string scan);

    //Pagination
    Paged<Supplier> GetAllSupplierPaged(string searchTerm = "", string sortBy = "Id", string direction = "asc", int page = 1, int size = 20);

}
