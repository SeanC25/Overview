using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;
using APG_CRM.Data.Services;
using APG_CRM.Web.Models;
using System.Linq;

namespace APG_CRM.Web.Controllers
{

    [Authorize]
    public class QuotationController : BaseController
    //inherit from this BaseController. This way, the Alert method will be available in all controllers that inherit from BaseController.

    {
        // Services injection
        private IQuotationService svcQ;
        private readonly IQuotationService _quotationService;
        private ICustomerService svc;
        private ISurveyService ssvc;
        private readonly DatabaseContext dbContext;
        private readonly ILogger<QuotationController> _logger;

        // constructor injection
        public QuotationController(DatabaseContext dbContext,
                                   ILogger<QuotationController> logger,
                                   IQuotationService quotationService,
                                   ICustomerService customerService,
                                   ISurveyService surveyService)
        {
            this.dbContext = dbContext;
            _logger = logger;
            this.svcQ = quotationService;
            this.svc = customerService;
            this.ssvc = surveyService;
            _quotationService = quotationService;
        }


        //===================Paginated Index page ==========

        public IActionResult Index(int page = 1, int size = 20, string orderBy = "id", string direction = "asc", string searchTerm = "")
        {
            orderBy = orderBy.ToLower();

            var quotationQuery = GetQuotationsQuery(searchTerm);

            ApplyQuotationSorting(ref quotationQuery, orderBy, direction);

            var pagedQuotations = quotationQuery.ToPaged(page, size);

            var viewModelList = pagedQuotations.Data.Select(q => MapToViewModel(q)).ToList();

            var pagedQuotationViewModel = new PagedQuotationViewModel
            {
                Data = viewModelList,
                TotalRows = pagedQuotations.TotalRows,
                CurrentPage = pagedQuotations.CurrentPage,
                PageSize = pagedQuotations.PageSize,
                OrderBy = orderBy,
                Direction = direction,
                // TotalPages is a calculated property and should not be set here
            };

            ViewBag.CurrentSortOrder = orderBy;
            ViewBag.CurrentSortDirection = direction;

            return View(pagedQuotationViewModel);
        }


        /// creating the search tool 

        private IQueryable<Quotation> GetQuotationsQuery(string searchTerm)
        {
            var quotationQuery = dbContext.Quotations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var lowerCaseSearchTerm = searchTerm.ToLower();

                quotationQuery = quotationQuery.Where(q =>
                    q.Title != null && q.Title.ToLower().Contains(lowerCaseSearchTerm) ||
                    q.ContactName != null && q.ContactName.ToLower().Contains(lowerCaseSearchTerm) ||
                    q.ContactPhone != null && q.ContactPhone.Contains(lowerCaseSearchTerm) ||
                    q.ContactEmail != null && q.ContactEmail.ToLower().Contains(lowerCaseSearchTerm) ||
                    q.WorkType != null && q.WorkType.ToLower().Contains(lowerCaseSearchTerm) ||
                    q.DeliveryAddress != null && q.DeliveryAddress.ToLower().Contains(lowerCaseSearchTerm) ||
                    q.Response != null && q.Response.ToLower().Contains(lowerCaseSearchTerm)
                );
            }

            return quotationQuery.Include(cu => cu.Customer);
        }


        ///Mapping Logic: a method  to map a Quotation entity to a QuotationViewModel. this is for Pagination of Quotation data. 

        private QuotationViewModel MapToViewModel(Quotation quotation)
        {
            return new QuotationViewModel
            {
                CustomerId = quotation.CustomerId,
                Id = quotation.Id,
                Date = quotation.Date,
                Title = quotation.Title,
                ContactName = quotation.ContactName,
                ContactPhone = quotation.ContactPhone,
                ContactEmail = quotation.ContactEmail,
                Description = quotation.Description,
                RequiresSurvey = quotation.RequiresSurvey,
                WorkType = quotation.WorkType,
                DeliveryRequired = quotation.DeliveryRequired,
                DeliveryAddress = quotation.DeliveryAddress,
                Price = quotation.Price,
                QuoteSentDate = quotation.QuoteSentDate,
                QuoteFollowDate = quotation.QuoteFollowDate,
                Status = (QuotationViewModel.Quotestatus)quotation.Status,  //both enums in the enitiy and in the viewmodel have the same options in the same order.
                Response = quotation.Response,
                Accepted = quotation.Accepted,
                Urgency = quotation.Urgency
            };
        }

        private void ApplyQuotationSorting(ref IQueryable<Quotation> quotationQuery, string orderBy, string direction)
        {
            switch (orderBy.ToLower())
            {
                case "id":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.Id)
                        : quotationQuery.OrderByDescending(q => q.Id);
                    break;

                case "date ":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.Date)
                        : quotationQuery.OrderByDescending(q => q.Date);
                    break;
                case "title":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.Title)
                        : quotationQuery.OrderByDescending(q => q.Title);
                    break;
                case "contactname":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.ContactName)
                        : quotationQuery.OrderByDescending(q => q.ContactName);
                    break;
                case "contactphone":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.ContactPhone)
                        : quotationQuery.OrderByDescending(q => q.ContactPhone);
                    break;
                case "contactemail":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.ContactEmail)
                        : quotationQuery.OrderByDescending(q => q.ContactEmail);
                    break;
                case "description":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.Description)
                        : quotationQuery.OrderByDescending(q => q.Description);
                    break;
                case "requiresSurvey":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.RequiresSurvey)
                        : quotationQuery.OrderByDescending(q => q.RequiresSurvey);
                    break;
                case "worktype":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.WorkType)
                        : quotationQuery.OrderByDescending(q => q.WorkType);
                    break;
                case "deliveryRequired":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.DeliveryRequired)
                        : quotationQuery.OrderByDescending(q => q.DeliveryRequired);
                    break;
                case "price":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.Price)
                        : quotationQuery.OrderByDescending(q => q.Price);
                    break;
                case "quotesentdate":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.QuoteSentDate)
                        : quotationQuery.OrderByDescending(q => q.QuoteSentDate);
                    break;
                case "quotefollowdate":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.QuoteFollowDate)
                        : quotationQuery.OrderByDescending(q => q.QuoteFollowDate);
                    break;
                case "status":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.Status)
                        : quotationQuery.OrderByDescending(q => q.Status);
                    break;
                case "response":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.Response)
                        : quotationQuery.OrderByDescending(q => q.Response);
                    break;
                case "urgency":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.Urgency)
                        : quotationQuery.OrderByDescending(q => q.Urgency);
                    break;
                case "accepted ":
                    quotationQuery = direction == "asc"
                        ? quotationQuery.OrderBy(q => q.Accepted)
                        : quotationQuery.OrderByDescending(q => q.Accepted);
                    break;
                default:
                    quotationQuery = quotationQuery.OrderBy(q => q.Id);
                    break;
            }
        }


        private Quotation MapToEntity(QuotationViewModel model)
        {
            return new Quotation
            {
                // Map properties from QuotationViewModel to Quotation
                CustomerId = model.CustomerId,
                Id = model.Id,
                Date = model.Date,
                Title = model.Title,
                ContactName = model.ContactName,
                ContactPhone = model.ContactPhone,
                ContactEmail = model.ContactEmail,
                Description = model.Description,
                RequiresSurvey = model.RequiresSurvey,
                WorkType = model.WorkType,
                DeliveryRequired = model.DeliveryRequired,
                DeliveryAddress = model.DeliveryAddress,
                Price = model.Price,
                QuoteSentDate = model.QuoteSentDate,
                QuoteFollowDate = model.QuoteFollowDate,
                Status = (APG_CRM.Data.Entities.Quotestatus)model.Status,
                Response = model.Response,
                Accepted = model.Accepted,
                Urgency = model.Urgency
            };
        }

        //=========  QuotationScanViewModel =======

        // will be used in the Scan/Search 
        public IActionResult ScanIndex(QuotationScanViewModel scan)
        {
            if (string.IsNullOrEmpty(scan.Check))
            {
                scan.Quotations = svcQ.GetAllQuotations();
            }
            else
            {
                scan.Quotations = svcQ.SearchQuotations(scan.Check);
            }
            return View(scan);
        }
        //=========  CRUD (Create, Read, Update, Delete) operations =======
        // GET: Quotation/Create
        [HttpGet]
        [Route("Quotation/Create")]

        //QuotationViewModel always has a non-null Customer property when creating a new quotation, - as i want quotations to only be created on an existing customer.
        public IActionResult Create()
        {
            var model = new QuotationViewModel
            {
                // Initialize any default values or fetch them from a service
                Customers = new SelectList(svc.GetAllCustomers(), "Id", "Name"),

                // Initialize the Customer property to avoid null reference issues
                Customer = new QuotationViewModel.CustomerViewModel(),

                // Add any other default values or initialization logic here
            };

            return View(model);
        }

        // GET: Quotation/Create with a customerId parameter
        [HttpGet]
        [Route("Quotation/Create/{customerId:int}")]
        public IActionResult Create(int customerId)
        {
            var customer = svc.GetCustomerById(customerId); // Replace with actual method to fetch customer
            if (customer == null)
            {
                // Handle the scenario where customer is not found
                return NotFound();
            }
            var model = new QuotationViewModel
            {
                CustomerId = customerId,
                Customer = new QuotationViewModel.CustomerViewModel
                {
                    // Map properties from QuotationViewModel to Quotation
                    Name = customer.Name,
                    Street = customer.Street,
                    City = customer.City,
                    County = customer.County,
                    PostCode = customer.PostCode,
                    Phone = customer.Phone,
                    Email = customer.Email,
                }

            };
            return View(model);
        }

        // POST: Quotation/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // Protect against CSRF attacks
        public IActionResult Create(QuotationViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Map properties from QuotationViewModel to Quotation entity
                    var quotation = new Quotation
                    {
                        CustomerId = model.CustomerId,
                        Date = model.Date, // Ensure this is correctly set
                        Title = model.Title,
                        ContactName = model.ContactName,
                        ContactPhone = model.ContactPhone,
                        ContactEmail = model.ContactEmail,
                        Description = model.Description,
                        RequiresSurvey = model.RequiresSurvey,
                        WorkType = model.WorkType,
                        DeliveryRequired = model.DeliveryRequired,
                        DeliveryAddress = model.DeliveryAddress,
                        Price = model.Price,
                        QuoteSentDate = model.QuoteSentDate,
                        QuoteFollowDate = model.QuoteFollowDate,
                        Status = (APG_CRM.Data.Entities.Quotestatus)model.Status,
                        Response = model.Response,
                        Accepted = model.Accepted,
                        Urgency = model.Urgency
                    };

                    // Call the service to add the new Quotation to the database
                    _quotationService.CreateQuotation(
                        model.CustomerId,
                        model.Date,
                        model.Title,
                        model.ContactName,
                        model.ContactPhone,
                        model.ContactEmail,
                        model.Description,
                        model.RequiresSurvey,
                        model.WorkType,
                        model.DeliveryRequired,
                        model.DeliveryAddress,
                        model.Price,
                        model.QuoteSentDate,
                        model.QuoteFollowDate,
                        (APG_CRM.Data.Entities.Quotestatus)model.Status,
                        model.Response,
                        model.Accepted,
                        model.Urgency
                    );

                    // Optionally, redirect to another page after successful creation
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while adding a new quotation.");
                    ModelState.AddModelError("", "An error occurred while creating the quotation. Please try again.");
                }
            }

            // If we reach here, something went wrong, redisplay form
            // Repopulate any necessary data (e.g., dropdowns) for the view
            model.Customers = new SelectList(svc.GetAllCustomers(), "Id", "Name");
            return View(model);
        }



        // Method to fetch customer details - Replace with your actual data access method
        private Customer GetCustomerDetails(int customerId)
        {
            // Example of fetching a customer from a data source
            // Replace this with your actual data access logic

            // Assuming you have some data source (like a database) from which to fetch the customer
            var customer = dbContext.Customers.FirstOrDefault(c => c.Id == customerId);

            // If a customer is found, return the customer object
            if (customer != null)
            {
                return customer;
            }
            // If no customer is found, return null
            return null;
        }


        //GET - a single Quotation scanning for from its Id
        public IActionResult Details(int id)
        {
            var quotation = svcQ.GetQuotationById(id);

            if (quotation is null)
            {
                Alert("Quotation not found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // Get Previous and Next customer IDs
            quotation.PreviousId = dbContext.Quotations
                .Where(q => q.Id < id)
                .OrderByDescending(q => q.Id)
                .Select(q => (int?)q.Id)
                .FirstOrDefault();

            quotation.NextId = dbContext.Quotations
                 .Where(q => q.Id > id)
                 .OrderBy(q => q.Id)
                 .Select(q => (int?)q.Id)
                 .FirstOrDefault();

            // Additional details for the partial view
            ViewBag.AdditionalDetails = "Some additional details here.";

            // Fetch survey data (replace with your actual method)
            var surveyData = svcQ.GetSurveysForQuotation(id);

            // If you need to pass data to the partial view
            ViewBag.YourSurveyPagedModel = surveyData;

            return View(quotation);
        }

        // GET /Quotation/Edit
        [HttpGet]
        [Authorize(Roles = "admin, manager")]
        public IActionResult Edit(int id)
        {
            var quotation = dbContext.Quotations.Find(id);
            if (quotation == null)
            {
                return NotFound();
            }

            var model = MapToViewModel(quotation);
            return View(model);
        }


        // POST /Quotation/Edit
        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        public IActionResult Edit(QuotationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var quotation = dbContext.Quotations.Find(model.Id);
                if (quotation == null)
                {
                    return NotFound();
                }

                // Map the updated values from the model to the entity
                quotation.Date = model.Date;
                quotation.Title = model.Title;
                quotation.ContactName = model.ContactName;
                quotation.ContactPhone = model.ContactPhone;
                quotation.ContactEmail = model.ContactEmail;
                quotation.Description = model.Description;
                quotation.RequiresSurvey = model.RequiresSurvey;
                quotation.WorkType = model.WorkType;
                quotation.DeliveryRequired = model.DeliveryRequired;
                quotation.DeliveryAddress = model.DeliveryAddress;
                quotation.Price = model.Price;
                quotation.QuoteSentDate = model.QuoteSentDate;
                quotation.QuoteFollowDate = model.QuoteFollowDate;
                // Explicitly cast the enum
                quotation.Status = (APG_CRM.Data.Entities.Quotestatus)model.Status;
                quotation.Response = model.Response;
                quotation.Accepted = model.Accepted;
                quotation.Urgency = model.Urgency;

                // Save changes in the database
                dbContext.Update(quotation);
                dbContext.SaveChanges();

                // Redirect to Index
                return RedirectToAction("Index");
            }

            // If ModelState is not valid, show the form again with validation messages
            return View(model);
        }


        // GET: Quotation/Delete/id
        [HttpGet]
        [Authorize(Roles = "admin, manager")]
        public IActionResult Delete(int id)
        {
            var quotationEntity = svcQ.GetQuotationById(id);
            if (quotationEntity == null)
            {
                Alert($"Quotation with id of {id} not found", AlertType.info);
                return RedirectToAction(nameof(Index));
            }

            var quotationViewModel = MapToViewModel(quotationEntity); // Convert to ViewModel
            return View(quotationViewModel);
        }

        // POST: Quotation/Delete/id
        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        public IActionResult DeleteConfirm(int id)
        {
            try
            {
                var success = svcQ.DeleteQuotation(id); // Attempt to delete the quotation
                if (!success)
                {
                    Alert($"Quotation with id {id} could not be deleted.", AlertType.warning);
                    return RedirectToAction(nameof(Index));
                }

                Alert($"Quotation Deleted Successfully", AlertType.success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting quotation with ID {QuotationId}", id);
                Alert($"Error occurred while deleting the quotation.", AlertType.warning);
            }

            return RedirectToAction(nameof(Index)); // Redirect to index after deletion
        }
    }
}
