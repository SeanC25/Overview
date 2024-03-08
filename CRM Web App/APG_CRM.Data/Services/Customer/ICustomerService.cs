using APG_CRM.Data.Entities;


namespace APG_CRM.Data.Services;

// This interface describes the functions that a ContactService class will implement
public interface ICustomerService
{
    void Initialise();

    //*** ==================== Customer Entity Services Management CRUD operations (Create, Read, Update, Delete),=========================
    Customer AddCustomer(Customer cu); //Create Method
    Customer GetCustomerById(int id); //read method

    Customer UpdateCustomer(Customer update); //Update method
    bool DeleteCustomer(int id); //Delete method

    //*** ==================== Customer Entity Services  helpter methods/Search Functions=========================

    List<Customer> GetAllCustomers();
    IList<Customer> SearchCustomers(string scan); //Search method
    Customer GetCustomerByCustomerName(string Name);
    Customer GetCustomerByEmail(string Email);
    Customer GetCustomerByPostCode(string PostCode);

    //Pagination
    Paged<Customer> GetCustomersPaged(string searchTerm = "", string sortBy = "Id", string direction = "asc", int page = 1, int size = 20);
    // Paged<Customer> GetCustomersPaged(int page = 1, int size = 20, string orderBy = "id", string direction = "asc");

    IQueryable<Customer> GetCustomers();

}
