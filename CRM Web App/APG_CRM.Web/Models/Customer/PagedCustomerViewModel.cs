using APG_CRM.Data.Entities;

namespace APG_CRM.Web.Models
{
    public class PagedCustomerViewModel : Paged<CustomerViewModel>
    {
        // inherit properties from CustomerViewModel to PagedCustomerViewModel to be a paginated list of View. the Paged<T> mapping is in CustomerController

        public IList<Customer> Customers { get; set; }
        public string Check { get; set; }
        public CustomerScanViewModel SearchModel { get; set; }
        public string SearchTerm { get; set; }

    }
}
