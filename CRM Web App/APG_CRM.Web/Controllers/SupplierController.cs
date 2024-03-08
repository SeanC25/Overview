using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;
using APG_CRM.Web.Models;
using APG_CRM.Data.Services;
using System.Linq;

namespace APG_CRM.Web.Controllers
{
    public class SupplierController : BaseController
    {
        private readonly ISupplierService _supplierService;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(ISupplierService supplierService, ILogger<SupplierController> logger)
        {
            _supplierService = supplierService;
            _logger = logger;
        }


        // public IActionResult Index(string searchTerm = "", int page = 1, int pageSize = 10, string orderBy = "Name", string direction = "asc")
        // {

        //     // search term logic...
        //     IQueryable<Supplier> suppliers = _supplierService.GetAllSuppliers();

        //     if (!string.IsNullOrEmpty(searchTerm))
        //     {
        //         suppliers = suppliers.Where(s => s.Name.Contains(searchTerm)
        //                                         || s.MainContactName.Contains(searchTerm)
        //                                         || s.Address.Contains(searchTerm)
        //                                         || s.City.Contains(searchTerm)
        //                                         // ... add other fields as necessary
        //                                         );
        //     }

        //     ApplySorting(ref suppliers, orderBy, direction);

        //     // Apply pagination
        //     var paginatedSuppliers = Paginate(suppliers, page, pageSize);
        //     var viewModel = new PaginatedSuppliersViewModel
        //     {
        //         Suppliers = paginatedSuppliers,

        //     };

        //     return View(viewModel);
        // }

        // public IActionResult Index(string searchTerm = "", int page = 1, int pageSize = 10, string orderBy = "Name", string direction = "asc")
        // {
        //     IQueryable<Supplier> suppliers = _supplierService.GetAllSuppliers();

        //     if (!string.IsNullOrEmpty(searchTerm))
        //     {
        //         suppliers = suppliers.Where(s => s.Name.Contains(searchTerm)
        //                                         || s.MainContactName.Contains(searchTerm)
        //                                         || s.Address.Contains(searchTerm)
        //                                         || s.City.Contains(searchTerm)
        //                                         // ... add other fields as necessary
        //                                         );
        //     }

        //     suppliers = ApplySorting(suppliers, orderBy, direction);

        //     var paginatedSuppliers = Paginate(suppliers, page, pageSize);
        //     var viewModel = new PaginatedSuppliersViewModel
        //     {
        //         Suppliers = paginatedSuppliers,
        //     };

        //     return View(viewModel);
        // }

        public IActionResult Index(string searchTerm = "", int page = 1, int pageSize = 10, string orderBy = "Id", string direction = "asc")
        {
            IQueryable<Supplier> suppliers = _supplierService.GetAllSuppliers();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                suppliers = suppliers.Where(s => s.Name.Contains(searchTerm)
                                                || s.MainContactName.Contains(searchTerm)
                                                || s.Address.Contains(searchTerm)
                                                || s.City.Contains(searchTerm)
                                                || s.PostCode.Contains(searchTerm)
                                                );
            }

            suppliers = ApplySorting(suppliers, orderBy, direction);

            var paginatedSuppliers = Paginate(suppliers, page, pageSize);
            var viewModel = new PaginatedSuppliersViewModel
            {
                Suppliers = paginatedSuppliers,
            };

            return View(viewModel);
        }

        private Paged<Supplier> Paginate(IQueryable<Supplier> suppliers, int page, int pageSize)
        {
            var totalItems = suppliers.Count();
            var itemsOnPage = suppliers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new Paged<Supplier>
            {
                Data = itemsOnPage,
                TotalRows = totalItems,
                CurrentPage = page,
                PageSize = pageSize
                // Do not set TotalPages here, as it is a calculated property in your Paged<T> class in entities 
            };
        }

        public static IQueryable<Supplier> ApplySorting(IQueryable<Supplier> suppliers, string orderBy, string direction)
        {
            switch (orderBy)
            {
                case "Name":
                    return direction == "asc" ? suppliers.OrderBy(s => s.Name) : suppliers.OrderByDescending(s => s.Name);
                case "MainContactName":
                    return direction == "asc" ? suppliers.OrderBy(s => s.MainContactName) : suppliers.OrderByDescending(s => s.MainContactName);
                case "Address":
                    return direction == "asc" ? suppliers.OrderBy(s => s.Address) : suppliers.OrderByDescending(s => s.Address);
                case "City":
                    return direction == "asc" ? suppliers.OrderBy(s => s.City) : suppliers.OrderByDescending(s => s.City);
                case "PostCode":
                    return direction == "asc" ? suppliers.OrderBy(s => s.PostCode) : suppliers.OrderByDescending(s => s.PostCode);
                case "Website":
                    return direction == "asc" ? suppliers.OrderBy(s => s.Website) : suppliers.OrderByDescending(s => s.Website);
                case "Phone":
                    return direction == "asc" ? suppliers.OrderBy(s => s.Phone) : suppliers.OrderByDescending(s => s.Phone);
                case "Email":
                    return direction == "asc" ? suppliers.OrderBy(s => s.Email) : suppliers.OrderByDescending(s => s.Email);
                case "Notes":
                    return direction == "asc" ? suppliers.OrderBy(s => s.Notes) : suppliers.OrderByDescending(s => s.Notes);
                // Add other cases as needed for different supplier properties
                default:
                    // Default sort
                    return direction == "asc" ? suppliers.OrderBy(s => s.Id) : suppliers.OrderByDescending(s => s.Id);
            }
        }


        // GET /supplier/create
        [Authorize(Roles = "admin, manager")]
        public IActionResult Create()
        {
            return View(new CreateSupplierViewModel());
        }

        // POST /supplier/create
        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind(" DateAdded,Name, MainContactName, Address, City, PostCode, Website, Phone, Email, Notes")] CreateSupplierViewModel supplierCreate)
        {
            if (ModelState.IsValid)
            {
                var supplier = new APG_CRM.Data.Entities.Supplier
                {
                    DateAdded = supplierCreate.DateAdded,
                    Name = supplierCreate.Name,
                    MainContactName = supplierCreate.MainContactName,
                    Address = supplierCreate.Address,
                    City = supplierCreate.City,
                    PostCode = supplierCreate.PostCode,
                    Website = supplierCreate.Website,
                    Phone = supplierCreate.Phone,
                    Email = supplierCreate.Email,
                    Notes = supplierCreate.Notes
                };
                _supplierService.AddSupplier(supplier);
                Alert("Supplier created successfully", AlertType.success);
                return RedirectToAction(nameof(Details), new { id = supplier.Id });
            }

            return View(supplierCreate);
        }


        // GET: /supplier/details/{id}
        public IActionResult Details(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);
            if (supplier == null)
            {
                Alert("Supplier not found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            return View(supplier); // Assuming you have a view named 'Details' to display the supplier
        }


        // GET /supplier/edit/{id}
        [Authorize(Roles = "admin, manager")]
        public IActionResult Edit(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);

            if (supplier == null)
            {
                Alert("Supplier not found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // Directly passing the supplier model to the view
            return View(supplier);
        }

        // POST /supplier/edit/{id}
        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, APG_CRM.Data.Entities.Supplier supplierEdit)
        {
            if (!ModelState.IsValid)
            {
                return View(supplierEdit);
            }

            var supplier = _supplierService.GetSupplierById(id);
            if (supplier == null)
            {
                Alert("Supplier not found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // Map properties from supplierEdit to Supplier
            supplier.Name = supplierEdit.Name;
            supplier.MainContactName = supplierEdit.MainContactName;
            supplier.Address = supplierEdit.Address;
            supplier.City = supplierEdit.City;
            supplier.PostCode = supplierEdit.PostCode;
            supplier.Website = supplierEdit.Website;
            supplier.Phone = supplierEdit.Phone;
            supplier.Email = supplierEdit.Email;
            supplier.Notes = supplierEdit.Notes;

            _supplierService.UpdateSupplier(supplier);
            Alert("Supplier updated successfully", AlertType.success);
            return RedirectToAction(nameof(Details), new { id = supplier.Id });
        }

        // GET /supplier/delete/{id}
        [Authorize(Roles = "admin, manager")]
        public IActionResult Delete(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);

            if (supplier == null)
            {
                Alert("Supplier not found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            return View(supplier);
        }

        // POST /supplier/delete/{id}
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin, manager")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);

            if (supplier == null)
            {
                Alert("Supplier not found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            _supplierService.DeleteSupplier(id);
            Alert("Supplier deleted successfully", AlertType.success);
            return RedirectToAction(nameof(Index));
        }
    }
}
