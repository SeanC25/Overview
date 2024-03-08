using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace APG_CRM.Data.Services
{
    public class GlassServiceDb : IGlassService
    {
        private readonly DatabaseContext db; //the service is public but the access to this db is private readonly within this service.  //Dependency Injection: injecting it through the constructor. This will make unit testing easier and is a better practice for maintainability and scalability.
        public GlassServiceDb(DatabaseContext context)
        {
            db = context;
        }
        public void Initialise()
        {
            db.Initialise(); // recreate database
        }

        //*** =================== Glass Entity Services Management CRUD operations (Create, Read, Update, Delete),=========================

        public Glass AddGlass(Glass g)
        {
            if (g == null)
            {
                throw new ArgumentNullException(nameof(g), "Glass cannot be null");
            }

            // Validate the glass entity
            var validationContext = new ValidationContext(g);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(g, validationContext, validationResults, true))
            {
                // Combine all validation errors into a single message.
                var errorMessages = validationResults.Select(r => r.ErrorMessage);
                var combinedErrorMessage = string.Join("; ", errorMessages);
                throw new InvalidOperationException($"The glass entity is not valid: {combinedErrorMessage}");
            }

            // Check if a glass with the same name already exists
            if (GetGlassByName(g.Name) != null)
            {
                throw new InvalidOperationException("A glass with the same name already exists.");
            }

            try
            {
                // Create New Glass
                var glass = new Glass
                {
                    Type = g.Type,
                    Name = g.Name,
                    Thickness = g.Thickness,
                    SheetSizeL = g.SheetSizeL,
                    SheetSizeH = g.SheetSizeH,
                    PricePerSheet = g.PricePerSheet,
                    BoxSheetQuantity = g.BoxSheetQuantity,
                    ImageUrl = g.ImageUrl,
                };

                // Directly add glass 'g' object to the database.
                db.Glasses.Add(g);
                db.SaveChanges();
                // Return the added entity with its Id. 
                // The entity 'g' should have its Id populated after the save.
                return g;
            }

            catch (DbUpdateException ex) // Catch database update exceptions specifically.
            {
                throw new InvalidOperationException("An error occurred while adding this glass.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred.", ex);
            }
        }

        public Glass GetGlassById(int id)  //Read Method
        {
            return db.Glasses.FirstOrDefault(g => g.Id == id);
        } //Read Method

        public Glass UpdateGlass(Glass g)
        {
            if (g == null)
            {
                throw new ArgumentNullException(nameof(g), "Glass cannot be null");
            }

            var existingGlass = db.Glasses.Find(g.Id);
            if (existingGlass == null)
            {
                throw new InvalidOperationException("The glass does not exist in the database.");
            }

            // Validate the glass entity
            var validationContext = new ValidationContext(g);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(g, validationContext, validationResults, true))
            {
                // Handle validation errors
                var errorMessages = validationResults.Select(r => r.ErrorMessage);
                var combinedErrorMessage = string.Join("; ", errorMessages);
                throw new InvalidOperationException($"The updated glass entity is not valid: {combinedErrorMessage}");
            }

            // Check if a glass with the same name already exists (excluding the current glass)
            var duplicateGlass = GetGlassByName(g.Name);
            if (duplicateGlass != null && duplicateGlass.Id != g.Id)
            {
                throw new InvalidOperationException("Another glass with the same name already exists.");
            }

            // Update glass details
            existingGlass.Type = g.Type;
            existingGlass.Name = g.Name;
            existingGlass.Thickness = g.Thickness;
            existingGlass.SheetSizeL = g.SheetSizeL;
            existingGlass.SheetSizeH = g.SheetSizeH;
            existingGlass.PricePerSheet = g.PricePerSheet;
            existingGlass.BoxSheetQuantity = g.BoxSheetQuantity;
            existingGlass.ImageUrl = g.ImageUrl;

            try
            {
                db.SaveChanges();
                return existingGlass;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating this glass.", ex);
            }
        }



        public bool DeleteGlass(int id)
        {
            var glassToDelete = db.Glasses.Find(id);

            if (glassToDelete == null)
            {
                throw new InvalidOperationException("The glass does not exist in the database.");
            }

            try
            {
                db.Glasses.Remove(glassToDelete);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while deleting this glass.", ex);
            }
        }


        //*** ==================== Glass Entity Services  helpter methods/Search Functions=========================

        // public List<Glass> GetAllGlass()
        // {
        //     return db.Glasses
        //         .Include(g => g.Supplier) // Include the Suppliers navigation property
        //         .ToList(); // Fetch the result and convert it to a list
        // }

        public IQueryable<Glass> GetAllGlass()
        {
            return db.Glasses.Include(g => g.Supplier);
        }

        public Glass GetGlassByName(string name)
        {
            return db.Glasses.FirstOrDefault(g => g.Name.Contains(name));
        }

        public Glass GetGlassByType(string Type)
        {
            // Attempt to parse the given string to a GlassType enum value
            if (Enum.TryParse(Type, true, out GlassType glassType))
            {
                // If parsing is successful, query the database
                return db.Glasses.FirstOrDefault(g => g.Type == glassType);
            }

            return null; // If parsing fails or no matching record is found
        }

        //helper methods, Find Glass
        // public Glass GetGlassByName(string Name) => db.Glass.FirstOrDefault(g => g.Name == Name);

        //*** ====================Stock Entity Services Search Functions =========================

        public Stock GetStockByGlassName(string Namee)
        {
            throw new NotImplementedException();
        }

        public Stock GetStockBySupplier_Name(string Name)
        {
            throw new NotImplementedException();
        }

        // public List<string> GetStockNamesByEmail(string Email)
        // {
        //     var supplier = db.Suppliers.Include(s => s.Glasses) // or .Include(s => s.Stocks) if there's a direct Stocks property
        //                                     .FirstOrDefault(s => s.Email == Email);

        //     if (supplier == null)
        //         return new List<string>();

        //     //Glass has "Name" that refers to the stock name
        //     return supplier.Glasses.Select(g => g.Name).ToList();
        // }

        public Supplier GetSupplieryByEmail(Supplier Email)
        {
            throw new NotImplementedException();
        }

        public Glass GetStockByName(string Name)
        {
            return db.Glasses.FirstOrDefault(g => g.Name == Name);
        }

        //Pagination
        public Paged<Glass> GetAllGlassPaged(string searchTerm = "", string sortBy = "Id", string direction = "asc", int page = 1, int size = 20)
        {
            var query = db.Glasses.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(g => g.Name.Contains(searchTerm));
            }

            // Sorting
            query = (sortBy.ToLower(), direction.ToLower()) switch
            {
                ("id", "asc") => query.OrderBy(g => g.Id),
                ("id", "desc") => query.OrderByDescending(g => g.Id),
                ("name", "asc") => query.OrderBy(g => g.Name),
                ("name", "desc") => query.OrderByDescending(g => g.Name),
                ("type", "asc") => query.OrderBy(g => g.Type),
                ("type", "desc") => query.OrderByDescending(g => g.Type),
                ("thickness", "asc") => query.OrderBy(g => g.Thickness),
                ("thickness", "desc") => query.OrderByDescending(g => g.Thickness),
                ("pricepersheet", "asc") => query.OrderBy(g => g.PricePerSheet),
                ("pricepersheet", "desc") => query.OrderByDescending(g => g.PricePerSheet),
                ("supplierid", "asc") => query.OrderBy(g => g.SupplierId),
                ("supplierid", "desc") => query.OrderByDescending(g => g.SupplierId),
                _ => query.OrderBy(g => g.Id) // Default sorting
            };

            return query.ToPaged(page, size, sortBy, direction);
        }


        // public IQueryable<Glass> GetAllGlassPaged(string searchTerm = "", string sortBy = "Name", int pageNumber = 1, int pageSize = 10)
        // {
        //     var glasses = db.Glasses.AsQueryable();

        //     // Filtering
        //     if (!string.IsNullOrEmpty(searchTerm))
        //     {
        //         glasses = glasses.Where(g => g.Name.Contains(searchTerm));
        //     }

        //     // Sorting
        //     switch (sortBy.ToLower())
        //     {
        //         case "type":
        //             glasses = glasses.OrderBy(g => g.Type);
        //             break;
        //         case "name":
        //             glasses = glasses.OrderBy(g => g.Name);
        //             break;
        //         case "thickness":
        //             glasses = glasses.OrderBy(g => g.Thickness);
        //             break;
        //         case "pricepersheet":
        //             glasses = glasses.OrderBy(g => g.PricePerSheet);
        //             break;
        //         case "supplierid":
        //             glasses = glasses.OrderBy(g => g.SupplierId);
        //             break;
        //         // You can add more sorting options if needed
        //         default:
        //             throw new ArgumentException($"Invalid sortBy value: {sortBy}");
        //     }

        //     // Pagination
        //     return glasses.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        // }

        // public Paged<Glass> GetAllGlassPaged(int page = 1, int size = 20, string orderBy = "id", string direction = "asc")
        // {
        //     var query = (orderBy.ToLower(), direction.ToLower()) switch
        //     {
        //         ("id", "asc") => db.Glasses.OrderBy(g => g.Id),
        //         ("id", "desc") => db.Glasses.OrderByDescending(g => g.Id),
        //         ("name", "asc") => db.Glasses.OrderBy(g => g.Name),
        //         ("name", "desc") => db.Glasses.OrderByDescending(g => g.Name),
        //         ("Type", "asc") => db.Glasses.OrderBy(g => g.Type),
        //         ("Type", "desc") => db.Glasses.OrderByDescending(g => g.Type),
        //         ("Thickness", "asc") => db.Glasses.OrderBy(g => g.Thickness),
        //         ("Thickness", "desc") => db.Glasses.OrderByDescending(g => g.Thickness),
        //         ("PricePerSheet", "asc") => db.Glasses.OrderBy(g => g.PricePerSheet),
        //         ("PricePerSheet", "desc") => db.Glasses.OrderByDescending(g => g.PricePerSheet),
        //         ("SupplierId", "asc") => db.Glasses.OrderBy(g => g.SupplierId),
        //         ("SupplierId", "desc") => db.Glasses.OrderByDescending(g => g.SupplierId),
        //         _ => db.Glasses.OrderBy(g => g.Id)
        //     };

        //     return query.ToPaged(page, size, orderBy, direction);
        // }

    }

}
