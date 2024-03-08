using Microsoft.AspNetCore.Mvc;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Services;
using APG_CRM.Data.Entities;
using APG_CRM.Web.Models;

using Microsoft.AspNetCore.Authorization;  // Need to add this in so i can add authorization to these actions also 
using System;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using APG_CRM.Web.Views.Shared.Components;
using System.Linq;

using Entities = APG_CRM.Data.Entities;
using Models = APG_CRM.Web.Models;



namespace APG_CRM.Controllers
{
    [Authorize]//all actions within this controller would require authorization.
    public class GlassController : Controller
    {
        private readonly IGlassService _glassService;    //prefixed with _ to differentiate them from local variables.
        private readonly ISupplierService _supplierService; // Suppliers is needed for glass items. 
        private readonly DatabaseContext dbContext;
        private readonly ILogger<GlassController> _logger;

        public GlassController(DatabaseContext dbContext, IGlassService glassService, ISupplierService supplierService, ILogger<GlassController> logger)
        {
            this.dbContext = dbContext;
            this._glassService = glassService;
            this._supplierService = supplierService;
            _logger = logger;
        }

        // GET: Glass/index
        public IActionResult Index(int pageNumber = 1, string searchTerm = "", string sortBy = "Name")
        {
            var glasses = dbContext.Glasses
                .Include(g => g.Supplier)
                .AsQueryable();

            var glassesQuery = _glassService.GetAllGlass().Include(g => g.Supplier).AsQueryable();
            // Fetching all glasses and including their Supplier details


            // Filtering
            if (!string.IsNullOrEmpty(searchTerm))
            {
                glassesQuery = glassesQuery.Where(g => g.Name.Contains(searchTerm));
            }

            // Sorting - You can keep adding more conditions based on your requirements
            switch (sortBy.ToLower())
            {
                case "type":
                    glassesQuery = glassesQuery.OrderBy(g => g.Type);
                    break;
                case "name":
                    glassesQuery = glassesQuery.OrderBy(g => g.Name);
                    break;
                case "thickness":
                    glassesQuery = glassesQuery.OrderBy(g => g.Thickness);
                    break;
                case "sheetSizeL":
                    glassesQuery = glassesQuery.OrderBy(g => g.SheetSizeL);
                    break;
                case "sheetSizeH":
                    glassesQuery = glassesQuery.OrderBy(g => g.SheetSizeH);
                    break;
                case "pricePerSheet ":
                    glassesQuery = glassesQuery.OrderBy(g => g.PricePerSheet);
                    break;
                case "boxSheetQuantity":
                    glassesQuery = glassesQuery.OrderBy(g => g.BoxSheetQuantity);
                    break;
            }
            // Using the ToPaged extension method to handle pagination and sorting
            var paginatedGlasses = glassesQuery.ToPaged(pageNumber, 10, sortBy);

            //string path= Server.MapPath ("~/Images/GlassIMG/"); /// get the server path for images folder
            return View(paginatedGlasses);


        }

        // //GET: Glass/index
        // public IActionResult Index(int pageNumber = 1, string searchTerm = "", string sortBy = "Name")
        // {
        //     var glasses = dbContext.Glasses
        //         .Include(g => g.Supplier)
        //         .AsQueryable();

        //     var glassesQuery = _glassService.GetAllGlass().Include(g => g.Supplier).AsQueryable();
        //     // Fetching all glasses and including their Supplier details


        //     // Filtering
        //     if (!string.IsNullOrEmpty(searchTerm))
        //     {
        //         glassesQuery = glassesQuery.Where(g => g.Name.Contains(searchTerm));
        //     }

        //     // Sorting - You can keep adding more conditions based on your requirements

        //      glassesQuery = sortBy.ToLower() switch
        //     {
        //         "id" => glassesQuery.OrderBy(g => g.Id),
        //         "type" => glassesQuery.OrderBy(g => g.Type),
        //         "name" => glassesQuery.OrderBy(g => g.Name),
        //         "thickness" => glassesQuery.OrderBy(g => g.Thickness),
        //         "sheetsizel" => glassesQuery.OrderBy(g => g.SheetSizeL),
        //         "sheetsizeh" => glassesQuery.OrderBy(g => g.SheetSizeH),
        //         "pricepersheet" => glassesQuery.OrderBy(g => g.PricePerSheet),
        //         "boxsheetquantity" => glassesQuery.OrderBy(g => g.BoxSheetQuantity),
        //         "supplierid" => glassesQuery.OrderBy(g => g.SupplierId),
        //         _ => glassesQuery.OrderBy(g => g.Name) // Default sort
        //     };

        //     // Using the ToPaged extension method to handle pagination and sorting
        //     var GlassViewModel = glassesQuery.ToPaged(pageNumber, 10, sortBy);

        //     //string path= Server.MapPath ("~/Images/GlassIMG/"); /// get the server path for images folder
        //     return View(glasses);
        //  }

        // public IActionResult Index()
        // {
        //     var glasses = _glassService.GetAllGlass();
        //     return View(glasses);
        // }

        // GET: Glass/Details/5
        // public IActionResult Details(int id)
        // {
        //     var glass = _glassService.GetGlassById(id);
        //     if (glass == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(glass);
        // }

        // GET: Glass/Details/5
        // public IActionResult Details(int id)
        // {
        //     var glass = _glassService.GetGlassById(id);
        //     if (glass == null)
        //     {
        //         return NotFound();
        //     }

        //     // Fetching previous and next glass IDs
        //     var previousGlassId = dbContext.Glasses
        //         .Where(g => g.Id < id)
        //         .OrderByDescending(g => g.Id)
        //         .Select(g => g.Id)
        //         .FirstOrDefault();

        //     var nextGlassId = dbContext.Glasses
        //         .Where(g => g.Id > id)
        //         .OrderBy(g => g.Id)
        //         .Select(g => g.Id)
        //         .FirstOrDefault();

        //     // Pass these IDs to the view
        //     ViewBag.PreviousGlassId = previousGlassId;
        //     ViewBag.NextGlassId = nextGlassId;

        //     return View(glass);
        // }

        // GET: Glass/Details/
        // public IActionResult Details(int id)
        // {
        //     var glass = _glassService.GetGlassById(id);
        //     if (glass == null)
        //     {
        //         return NotFound();
        //     }

        //     // Get Previous and Next Glass IDs
        //     glass.PreviousGlassId = dbContext.Glasses
        //         .Where(g => g.Id < id)
        //         .OrderByDescending(g => g.Id)
        //         .Select(g => (int?)g.Id)
        //         .FirstOrDefault();

        //     glass.NextGlassId = dbContext.Glasses
        //         .Where(g => g.Id > id)
        //         .OrderBy(g => g.Id)
        //         .Select(g => (int?)g.Id)
        //         .FirstOrDefault();

        //     return View(glass);
        // }

        public IActionResult Details(int id)
        {
            // Fetch the Glass entity and include the Supplier entity
            var glass = dbContext.Glasses
                                 .Include(g => g.Supplier)
                                 .FirstOrDefault(g => g.Id == id);

            if (glass == null)
            {
                return NotFound();
            }

            // Get Previous and Next Glass IDs
            glass.PreviousGlassId = dbContext.Glasses
                                            .Where(g => g.Id < id)
                                            .OrderByDescending(g => g.Id)
                                            .Select(g => (int?)g.Id)
                                            .FirstOrDefault();

            glass.NextGlassId = dbContext.Glasses
                                          .Where(g => g.Id > id)
                                          .OrderBy(g => g.Id)
                                          .Select(g => (int?)g.Id)
                                          .FirstOrDefault();

            return View(glass);
        }


        // GET: Glass/Create
        [Authorize(Roles = "admin, manager")] //this means only admin, manager, users can create, not customer. 
        public IActionResult Create()
        {
            return View();  // display the blank form used to create a glass object to Db.
        }

        // POST: Glass/Create
        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        [ValidateAntiForgeryToken]
        //public IActionResult Create(Glass glass)
        public IActionResult Create([Bind("Type, Name, Thickness, SheetSizeL, SheetSizeH, PricePerSheet, BoxSheetQuantity, SupplierId")] Entities.Glass glass)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _glassService.AddGlass(glass);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An unexpected error occurred while creating glass.");
                    ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
                }
            }

            // If we reach this point, there was a problem, and we should redisplay the form.
            return View(glass);
        }

        // GET: Glass/Edit/
        public IActionResult Edit(int id)
        {
            var glass = _glassService.GetGlassById(id);
            if (glass == null)
            {
                return NotFound();
            }
            return View(glass);
        }

        // POST: Glass/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Type, Name, Thickness, SheetSizeL, SheetSizeH, PricePerSheet, BoxSheetQuantity, SupplierId")] Entities.Glass glass)
        {
            if (id != glass.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _glassService.UpdateGlass(glass);
                return RedirectToAction(nameof(Index));
            }
            return View(glass);
        }

        // GET: Glass/Delete/5
        public IActionResult Delete(int id)
        {
            var glass = _glassService.GetGlassById(id);
            if (glass == null)
            {
                return NotFound();
            }
            return View(glass);
        }

        // POST: Glass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _glassService.DeleteGlass(id);

            TempData["isDeleted"] = true; // Set this to indicate a successful deletion to the next GET request.

            return RedirectToAction(nameof(Index));
        }

        // public IActionResult GetAllGlass()
        // {
        //     var glasses = dbContext.Glasses.ToList();

        //     // Convert each Glass entity to a GlassViewModel
        //     var glassViewModels = glasses.Select(g => MapToViewModel(g)).ToList();

        //     return View(glassViewModels);
        // }

        // public GlassViewModel MapToViewModel(Glass glass)
        // {
        //     return new GlassViewModel
        //     {
        //         Id = glass.Id,
        //         Type = glass.Type,
        //         Name = glass.Name,
        //         // ... other properties ...

        //         DisplaySheetSize = $"{glass.SheetSizeL} x {glass.SheetSizeH}" // example of custom mapping
        //     };
        // }

//         public async Task<IActionResult> SendEmailToSupplier(int id)
// {
//     var glass = await dbContext.Glasses.Include(g => g.Supplier).FirstOrDefaultAsync(g => g.Id == id);
//     if (glass == null)
//     {
//         return NotFound();
//     }

//     var supplierEmail = glass.Supplier.Email;
//     // Construct your email message
//     var emailMessage = "..."; 

//     // Send email
//     await _emailService.SendEmailAsync(supplierEmail, "Subject Here", emailMessage);

//     return RedirectToAction(nameof(Index)); // Or wherever you want to redirect
// }



    }
}
