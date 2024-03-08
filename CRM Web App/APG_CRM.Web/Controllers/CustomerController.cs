using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;
using APG_CRM.Web.Models;
using APG_CRM.Data.Services;
using System.Globalization;
using APG_CRM.Web.Views.Shared.Components;
using System.Linq;

namespace APG_CRM.Web.Controllers;

public class CustomerController : BaseController
{
    private ICustomerService svc;
    private IQuotationService svcQ;

    private readonly DatabaseContext dbContext;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(DatabaseContext dbContext, ILogger<CustomerController> logger, ICustomerService CustomerService, IQuotationService QuotationService)
    {
        this.dbContext = dbContext;
        _logger = logger;
        this.svc = CustomerService; // Initialize svc
        this.svcQ = QuotationService;
    }


    //===================Paginated Index page ==========

    [Authorize]
    public IActionResult Index(int page = 1, int size = 10, string orderBy = "id", string direction = "asc", string searchTerm = "")
    {
        var customerQuery = GetCustomersQuery(searchTerm);

        ApplySorting(ref customerQuery, orderBy, direction);

        var pagedCustomerEntity = customerQuery.ToPaged(page, size, orderBy, direction);

        var pagedCustomerViewModel = ConvertToViewModel(pagedCustomerEntity);

        pagedCustomerViewModel.SearchTerm = searchTerm; // Set the searchTerm here

        return View("Index", pagedCustomerViewModel);
    }

    private IQueryable<Customer> GetCustomersQuery(string searchTerm)
    {
        var customerQuery = svc.GetCustomers().AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            customerQuery = customerQuery.Where(c =>
                (c.Name != null && c.Name.Contains(searchTerm)) ||
                (c.Street != null && c.Street.Contains(searchTerm)) ||
                (c.City != null && c.City.Contains(searchTerm)) ||
                (c.County != null && c.County.Contains(searchTerm)) ||
                (c.PostCode != null && c.PostCode.Contains(searchTerm)) ||
                (c.Phone != null && c.Phone.Contains(searchTerm)) ||
                (c.Email != null && c.Email.Contains(searchTerm)) ||
                (c.Description != null && c.Description.Contains(searchTerm)) ||
                (c.PaymentTerms != null && c.PaymentTerms.Contains(searchTerm))
            );
        }

        return customerQuery.Include(q => q.Quotations);
    }

    private void ApplySorting(ref IQueryable<Customer> customerQuery, string orderBy, string direction)
    {
        switch (orderBy.ToLower())
        {
            case "id":
                customerQuery = direction == "asc"
                    ? customerQuery.OrderBy(g => g.Id)
                    : customerQuery.OrderByDescending(g => g.Id);
                break;

            case "dateestablished":
                customerQuery = direction == "asc"
                    ? customerQuery.OrderBy(g => g.DateEstablished)
                    : customerQuery.OrderByDescending(g => g.DateEstablished);
                break;

            case "name":
                customerQuery = direction == "asc"
                    ? customerQuery.OrderBy(g => g.Name)
                    : customerQuery.OrderByDescending(g => g.Name);
                break;

            case "type":
                customerQuery = direction == "asc"
                    ? customerQuery.OrderBy(g => g.Type)
                    : customerQuery.OrderByDescending(g => g.Type);
                break;

            case "phone":
                customerQuery = direction == "asc"
                    ? customerQuery.OrderBy(g => g.Phone)
                    : customerQuery.OrderByDescending(g => g.Phone);
                break;

            case "address":
                //sorting based on Street.
                customerQuery = direction == "asc"
                    ? customerQuery.OrderBy(g => g.Street)
                    : customerQuery.OrderByDescending(g => g.Street);
                break;

            case "City":
                //sorting based on Street.
                customerQuery = direction == "asc"
                    ? customerQuery.OrderBy(g => g.City)
                    : customerQuery.OrderByDescending(g => g.City);
                break;

            case "County":
                customerQuery = direction == "asc"
                    ? customerQuery.OrderBy(g => g.County)
                    : customerQuery.OrderByDescending(g => g.County);
                break;

            case "postcode":
                customerQuery = direction == "asc"
                    ? customerQuery.OrderBy(g => g.PostCode)
                    : customerQuery.OrderByDescending(g => g.PostCode);
                break;

            case "email":
                customerQuery = direction == "asc"
                    ? customerQuery.OrderBy(g => g.Email)
                    : customerQuery.OrderByDescending(g => g.Email);
                break;

            case "description":
                customerQuery = direction == "asc"
                    ? customerQuery.OrderBy(g => g.Description)
                    : customerQuery.OrderByDescending(g => g.Description);
                break;

            case "paymentterms":
                customerQuery = direction == "asc"
                    ? customerQuery.OrderBy(g => g.PaymentTerms)
                    : customerQuery.OrderByDescending(g => g.PaymentTerms);
                break;

            default:
                // If no order is specified or if an unknown order is specified, you might want to default to a standard ordering.
                // For instance, by ID:
                customerQuery = customerQuery.OrderBy(g => g.Id);
                break;
        }
    }

    private PagedCustomerViewModel ConvertToViewModel(Paged<Customer> pagedCustomerEntity)
    {
        return new PagedCustomerViewModel
        {
            Data = pagedCustomerEntity.Data.Select(MapToViewModel).ToList(),
            TotalRows = pagedCustomerEntity.TotalRows,
            CurrentPage = pagedCustomerEntity.CurrentPage,
            PageSize = pagedCustomerEntity.PageSize,
            OrderBy = pagedCustomerEntity.OrderBy,
            Direction = pagedCustomerEntity.Direction
        };
    }

    private CustomerViewModel MapToViewModel(Customer cu)
    {
        return new CustomerViewModel
        {
            Id = cu.Id,
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
    }


    public IActionResult ScanIndex(CustomerScanViewModel scan)
    {
        if (string.IsNullOrEmpty(scan.Check))
        {
            scan.Customers = svc.GetAllCustomers();
        }
        else
        {
            scan.Customers = svc.SearchCustomers(scan.Check);
        }
        return View(scan);
    }


    // GET /customer/create
    [Authorize(Roles = "admin, manager")]
    public IActionResult Create()
    {
        return View(new CreateCustomerViewModel()); // Correct as is
    }

    // POST /customer/create
    [HttpPost]
    [Authorize(Roles = "admin, manager")]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("DateEstablished, Type, Name, Street, City, County, PostCode, Phone, Email, Description, PaymentTerms")] CreateCustomerViewModel cucreate)
    {
        // validate customer e-mail is unique 
        var unique = svc.GetCustomerByEmail(cucreate.Email);

        if (unique != null)
        {
            ModelState.AddModelError(nameof(cucreate.Email), "Customer email already used");
        }

        if (ModelState.IsValid)
        {
            var customer = new Customer
            {
                // Map properties from cucreate to Customer
                DateEstablished = cucreate.DateEstablished,
                Type = cucreate.Type,
                Name = cucreate.Name,
                Street = cucreate.Street,
                City = cucreate.City,
                County = cucreate.County,
                PostCode = cucreate.PostCode,
                Phone = cucreate.Phone,
                Email = cucreate.Email,
                Description = cucreate.Description,
                PaymentTerms = cucreate.PaymentTerms
                
            };

            var cucreate1 = svc.AddCustomer(customer);
            if (cucreate1 == null)
            {
                Alert($"Customer could not be created, ambiguous error", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }
            Alert($"Customer created successfully", AlertType.success);
            return RedirectToAction(nameof(Details), new { Id = cucreate1.Id });
        }
        return View(cucreate); // Return the view with the same model
    }


    //GET - a single Customer scanning for from its Id
    public IActionResult Details(int id)
    {
        var customer = svc.GetCustomerById(id);

        if (customer is null)
        {
            Alert("customer not found", AlertType.warning);
            return RedirectToAction(nameof(Index));
        }
        // Get Previous and Next customer IDs
        customer.PreviousId = dbContext.Customers
            .Where(cu => cu.Id < id)
            .OrderByDescending(cu => cu.Id)
            .Select(cu => (int?)cu.Id)
            .FirstOrDefault();

        customer.NextId = dbContext.Customers
             .Where(cu => cu.Id > id)
             .OrderBy(cu => cu.Id)
             .Select(cu => (int?)cu.Id)
             .FirstOrDefault();

        // Sort quotations by the nearest QuoteFollowDate first
        var sortedQuotations = customer.Quotations
                                       .OrderBy(quotation => quotation.QuoteFollowDate)
                                       .ToList();

        var customerViewModel = new CustomerViewModel
        {
            Id = customer.Id,
            DateEstablished = customer.DateEstablished,
            Type = customer.Type,
            Name = customer.Name,
            Street = customer.Street,
            City = customer.City,
            County = customer.County,
            PostCode = customer.PostCode,
            Phone = customer.Phone,
            Email = customer.Email,
            Description = customer.Description,
            PaymentTerms = customer.PaymentTerms,
            PreviousId = customer.PreviousId,
            NextId = customer.NextId,
            Quotations = sortedQuotations

            //Quotations = customer.Quotations // Ensure this line is correct according to your data model

        };

        return View(customerViewModel);

    }

    
    // GET /customer/delete
    [Authorize(Roles = "admin, manager ")] //only admin, manager users can Delete a customer- guest, customer users can not Delete customer, to prevent bad behavviour. 
    public IActionResult Delete(int id)
    {
        var customer = svc.GetCustomerById(id);// scan to get the Customer to be deleted.

        if (customer == null)
        {
            //return NotFound();
            Alert($"Customer with id of {id} not found", AlertType.info);
            return RedirectToAction(nameof(Index));
        }//check to make sure customer/Id is found- alert user to try again from index area.

        // return Customer to view, to athorize deletion action
        return View(customer);
    }

    // POST /customer/delete
    [HttpPost]
    [Authorize(Roles = "admin, manager")] // Ensure there are no trailing spaces in role names
    public IActionResult DeleteConfirm(int id)
    {
        try
        {
            var success = svc.DeleteCustomer(id);
            if (!success)
            {
                Alert($"Customer with id {id} could not be deleted.", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            Alert($"Customer Deleted Successfully", AlertType.success);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting customer with ID {CustomerId}", id);
            Alert($"Error occurred while deleting the customer.", AlertType.warning);
        }

        return RedirectToAction(nameof(Index));
    }


    
    // GET /customer/edit/
    [Authorize(Roles = "admin, manager ")] //only admin, manager, users can edita customer .guest and customer users cant edit, to prevent bad behavviour. 
    public IActionResult Edit(int id)
    {
        var customer = svc.GetCustomerById(id);

        if (customer is null)  //Check if Customer is Null
        {
            Alert($"No such customer with the id of {id}", AlertType.info);
            return RedirectToAction(nameof(Index));
        }
        // if not null then pass/return to view to be editied
        return View(customer);
    }

    // POST customer/edit/
    [HttpPost]
    [Authorize(Roles = "admin, manager ")] //this means only admin, manager, users can create a customer- guest, customer cant edit, to prevent bad behavviour. 
    public IActionResult Edit(int id, Customer cuedit)
    {
        var exists = svc.GetCustomerByEmail(cuedit.Email);
        if (exists != null && exists.Id != cuedit.Id)
        {
            ModelState.AddModelError(nameof(cuedit.Email), "This Email is already being used");
        }//email is unique so good item to validate customer with not used/unique, continue with Customer edit with UpdateCustomer

        if (ModelState.IsValid)
        {
            var customer = svc.UpdateCustomer(cuedit);
            Alert($"customer edits made successfully", AlertType.success); // Method Success alert.
            return RedirectToAction(nameof(Details), new { Id = cuedit.Id }); //User returned to the customer view which has been ededit..
        }
        Alert($"customer edits made successfully", AlertType.success); // Method Success alert.
        return View(cuedit); //User returned to the new added customer view.
    }

}
