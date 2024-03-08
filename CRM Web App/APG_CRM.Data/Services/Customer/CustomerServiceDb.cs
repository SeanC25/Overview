using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace APG_CRM.Data.Services
{
    public class CustomerServiceDb : ICustomerService
    {
        private readonly DatabaseContext db;

        public CustomerServiceDb(DatabaseContext context)
        {
            db = context;

        }

        public void Initialise()
        {
            db.Initialise();
        }

        // ***==================== Customer Entity Services Management CRUD operations (Create, Read, Update, Delete), =========================

        // ==================== AddCustomer =========================

        public Customer AddCustomer(Customer cu)
        {
            if (cu == null)
            {
                throw new ArgumentNullException(nameof(cu), "Customer cannot be null");
            }

            // Validate the Customer entity
            var validationContext = new ValidationContext(cu);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(cu, validationContext, validationResults, true))
            {
                var errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new InvalidOperationException($"The Customer entity is not valid. Errors: {errorMessages}");
            }

            // Check if a customer with the same email already exists
            if (GetCustomerByEmail(cu.Email) != null)
            {
                // If a customer with the same email is found, return null
                return null;
            }

            {
                var customer = new Customer
                {
                    DateEstablished = cu.DateEstablished,
                    Type = cu.Type,
                    Name = cu.Name,
                    Street = cu.Street,
                    City = cu.City,
                    County = cu.County,
                    PostCode = cu.PostCode,
                    Phone = cu.Phone,
                    Email = cu.Email,
                    Description = cu.Description,
                    PaymentTerms = cu.PaymentTerms
                };

                db.Customers.Add(customer);
                db.SaveChanges();
                return customer;
            }
        }

        public Customer GetCustomerById(int id)  //read method
        {
            return db.Customers
                  .Include(c => c.Quotations) // Include the Quotations navigation property- for the partial view
                  .FirstOrDefault(cu => cu.Id == id);
        }//read method


        public Customer UpdateCustomer(Customer update) //Update method
        {
            var customer = GetCustomerById(update.Id);
            if (customer == null) return null; //checks that customer exists


            var existingCustomerByEmail = GetCustomerByEmail(update.Email);
            if (existingCustomerByEmail != null && existingCustomerByEmail.Id != update.Id) return null;   //checkts that email exits and is also unique, if not unique will return null- we want to update the correct user with this e-mail address. so good to check this.

            // Update properties
            customer.DateEstablished = update.DateEstablished;
            customer.Type = update.Type;
            customer.Name = update.Name;
            customer.Street = update.Street;
            customer.City = update.City;
            customer.County = update.County;
            customer.PostCode = update.PostCode;
            customer.Phone = update.Phone;
            customer.Email = update.Email;
            customer.Description = update.Description;
            customer.PaymentTerms = update.PaymentTerms;

            db.SaveChanges(); // save upate
            return customer; // return updates on this customer
        } //Update method

        public bool DeleteCustomer(int id)
        {
            var customer = GetCustomerById(id);
            if (customer == null) return false;

            // Handling dependent data
            var quotations = db.Quotations.Where(q => q.CustomerId == id).ToList();
            if (quotations.Any())
            {
                // Either delete these or handle them as per your business logic
                db.Quotations.RemoveRange(quotations);
            }

            var surveys = db.Surveys.Where(s => s.CustomerId == id).ToList();
            if (surveys.Any())
            {
                // Similar handling for surveys
                db.Surveys.RemoveRange(surveys);
            }

            db.Customers.Remove(customer);
            db.SaveChanges();
            return true;
        }


        // ==================== Customer Entity Services Search Functions=========================

        public IList<Customer> SearchCustomers(string scan)
        {
            scan = (scan ?? "").ToLower();  //if the scanner bar is left empty it shows all recipes

            // otherwise this is where the search takes place

            return db.Customers
                     .Where(cu => cu.Name.ToLower().Contains(scan) || cu.Email.ToLower().Contains(scan))
                     .ToList();

            // this scans the database for customer name customer email, email will be unique so find correct customer, the method will return a list of all customers in the database.
        }

        // ***==================== Customer Entity Services  helpter methods/Search Functions=========================

        public List<Customer> GetAllCustomers()
        {
            return db.Customers
                //.Include(customer => customer.Surveys)  // Include the Surveys navigation property TBC
                .ToList();
        }

        public Supplier GetSupplierByPostCode(string PostCode)
        {
            return db.Suppliers.FirstOrDefault(s => s.PostCode == PostCode);
        }


        public Customer GetCustomerByCustomerName(string Name)
        {
            return db.Customers.FirstOrDefault(cu => cu.Name == Name);
        }

        public Customer GetCustomerByEmail(string Email)
        {
            return db.Customers.FirstOrDefault(cu => cu.Email == Email);
        }

        public Customer GetCustomerByPostCode(string PostCode)
        {
            return db.Customers.FirstOrDefault(cu => cu.PostCode == PostCode);
        }

        //Pagination
        public Paged<Customer> GetCustomersPaged(string searchTerm = "", string sortBy = "Id", string direction = "asc", int page = 1, int size = 20)
        {
            var query = db.Customers.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.Name.Contains(searchTerm));
            }

            // Sorting
            query = (sortBy.ToLower(), direction.ToLower()) switch
            {
                ("id", "asc") => query.OrderBy(c => c.Id),
                ("id", "desc") => query.OrderByDescending(c => c.Id),
                ("Date Established", "asc") => query.OrderBy(c => c.DateEstablished),
                ("Date Established", "desc") => query.OrderByDescending(c => c.DateEstablished),
                ("Type", "asc") => query.OrderBy(c => c.Type),
                ("Type", "desc") => query.OrderByDescending(c => c.Type),
                ("Name", "asc") => query.OrderBy(c => c.Name),
                ("Name", "desc") => query.OrderByDescending(c => c.Name),
                ("County", "asc") => query.OrderBy(c => c.County),
                ("County", "desc") => query.OrderByDescending(c => c.County),
                ("Post Code", "asc") => query.OrderBy(c => c.PostCode),
                ("Post Code", "desc") => query.OrderByDescending(c => c.PostCode),
                ("PaymentTerms", "asc") => query.OrderBy(c => c.PaymentTerms),
                ("PaymentTerms", "desc") => query.OrderByDescending(c => c.PaymentTerms),
                _ => query.OrderBy(c => c.Id) // Default sorting
            };

            return query.ToPaged(page, size, sortBy, direction);
        }

        public IQueryable<Customer> GetCustomers()
        {
            return db.Customers.Include(q => q.Quotations);
        }

    }
}
