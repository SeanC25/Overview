using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;

namespace APG_CRM.Data.Services
{
    public class SupplierServiceDb : ISupplierService
    {
        private readonly DatabaseContext db;

        public SupplierServiceDb(DatabaseContext context)
        {
            db = context;
        }

        public void Initialise()
        {
            db.Initialise(); // recreate database
        }

        // ***==================== Suppliers Entity Services Management CRUD operations (Create, Read, Update, Delete), =========================

        public APG_CRM.Data.Entities.Supplier AddSupplier(APG_CRM.Data.Entities.Supplier s) //Create method
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s), "Supplier cannot be null");
            }

            // Validate the Supplier entity
            var validationContext = new ValidationContext(s);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(s, validationContext, validationResults, true))
            {
                // Join the validation error messages and throw an exception with the detailed messages.
                var errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new InvalidOperationException($"The Supplier entity is not valid. Errors: {errorMessages}");
            }

            // Check if a Supplier with the same name already exists
            if (GetSupplierByName(s.Name) != null)
            {
                throw new InvalidOperationException("A Supplier with the same name already exists.");
            }

            try
            {
                // Directly add Supplier 's' object to the database- instead of calling properities invividually.
                db.Suppliers.Add(s);
                db.SaveChanges();

                // Return the added entity with its Id. 
                // The entity 's' should have its Id populated after the save.
                return s;
            }


            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding this supplier.", ex);
            }
        } //create method

        public APG_CRM.Data.Entities.Supplier GetSupplierById(int id) //read method
        {
            return db.Suppliers.FirstOrDefault(s => s.Id == id);
        }//read method

        public APG_CRM.Data.Entities.Supplier UpdateSupplier(APG_CRM.Data.Entities.Supplier s) //Update method
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s), "Supplier cannot be null");
            }

            var existingSupplier = db.Suppliers.Find(s.Id);

            if (existingSupplier == null)
            {
                throw new InvalidOperationException("The Supplier does not exist in the database.");
            }

            // Validate theSupplier entity
            var validationContext = new ValidationContext(s);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(s, validationContext, validationResults, true))
            {
                // Handle validation errors
                throw new InvalidOperationException("The updated Supplier entity is not valid.");
            }

            // Check if a Supplier with the same name already exists (excluding the current Supplier)
            var duplicateSupplier = GetSupplierByName(s.Name);
            if (duplicateSupplier != null && duplicateSupplier.Id != s.Id)
            {
                throw new InvalidOperationException("Another Supplier with the same name already exists.");
            }

            try
            {
                db.Entry(existingSupplier).CurrentValues.SetValues(s);
                db.SaveChanges();
                return s;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating this glass.", ex);
            }
        } //Update method


        public bool DeleteSupplier(int id)  //Delete method
        {
            throw new NotImplementedException();
        }//Delete method


        // ***==================== Supplier Entity Services  helpter methods/Search Functions =========================
        // public List<Supplier> GetAllSuppliers()
        // {
        //     return db.Suppliers
        //         //.Include(supplier => supplier.Glasses)  // Include the glasses navigation property TBC if want to show this..
        //         .ToList();
        // }

        public IQueryable<APG_CRM.Data.Entities.Supplier> GetAllSuppliers()
        {
            return db.Suppliers;
        }

        public Supplier GetSupplierByName(string Name)
        {
            return db.Suppliers.FirstOrDefault(s => s.Name == Name);
        }

        public Supplier GetSupplieryByEmail(string Email)
        {
            return db.Suppliers.FirstOrDefault(s => s.Email == Email);
        }

        public Supplier GetSupplierByPostCode(string PostCode)
        {
            return db.Suppliers.FirstOrDefault(s => s.PostCode == PostCode);
        }

        public IList<Supplier> SearchSuppliers(string scan)
        {
            scan = (scan ?? "").ToLower();  //if the scanner bar is left empty it shows all recipes

            // otherwise this is where the search takes place

            return db.Suppliers
                     .Where(sp => sp.Name.ToLower().Contains(scan) || sp.Email.ToLower().Contains(scan))
                     .ToList();

            // this scans the database for Supplier name or Supplieremail, email will be unique so find correct Supplier, the method will return a list of all Suppliers in the database.
        }
        //Pagination
         public Paged<Supplier> GetAllSupplierPaged(string searchTerm = "", string sortBy = "Id", string direction = "asc", int page = 1, int size = 20)
        {
            var query = db.Suppliers.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(s => s.Name.Contains(searchTerm));
            }

            // Sorting
            query = (sortBy.ToLower(), direction.ToLower()) switch
            {
                ("id", "asc") => query.OrderBy(s => s.Id),
                ("id", "desc") => query.OrderByDescending(s => s.Id),
                ("Date Added", "asc") => query.OrderBy(s => s.DateAdded),
                ("Date Added", "desc") => query.OrderByDescending(s => s.DateAdded),
                ("Name", "asc") => query.OrderBy(s => s.Name),
                ("Name", "desc") => query.OrderByDescending(s => s.Name),
                ("MainContactName", "asc") => query.OrderBy(s => s.MainContactName),
                ("MainContactName", "desc") => query.OrderByDescending(s => s.MainContactName),
                ("Address", "asc") => query.OrderBy(s => s.Address),
                ("Address", "desc") => query.OrderByDescending(s => s.Address),
                ("City", "asc") => query.OrderBy(s => s.City),
                ("City", "desc") => query.OrderByDescending(s => s.City),
                ("PostCode", "asc") => query.OrderBy(s => s.PostCode),
                ("PostCode", "desc") => query.OrderByDescending(s => s.PostCode),
                _ => query.OrderBy(s => s.Id) // Default sorting
            };
            return query.ToPaged(page, size, sortBy, direction);
        }

    }
}
