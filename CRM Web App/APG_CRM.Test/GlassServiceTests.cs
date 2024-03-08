using System;
using Xunit;
using APG_CRM.Data.Entities;
using APG_CRM.Data.Services;
using Microsoft.EntityFrameworkCore;
using APG_CRM.Data.Repositories;

namespace APG_CRM.Test
{
    [Collection("Sequential")]
    public class GlassServiceTests
    {
        private readonly IGlassService service;
        private readonly ISupplierService supplierService;

        public GlassServiceTests()
        {
            // configure the data context options to use sqlite for testing
            var options = DatabaseContext.OptionsBuilder
                            .UseSqlite("Filename=test.db")
                            //.LogTo(Console.WriteLine)
                            .Options;

            // create service with new context
            service = new GlassServiceDb(new DatabaseContext(options));
            supplierService = new SupplierServiceDb(new DatabaseContext(options));


            service.Initialise();

        }// test enviroment

        // ==================== Supplier AddSupplier(Supplier s);  //Create method Test =========================

        [Fact]
        public void CreateGlass_AddNewGlass_GlassAdded()
        {
            // arrange
            // Adding supplier to Db
            var supplier = supplierService.AddSupplier(new Supplier
            {
                DateAdded = new DateTime(2023, 07, 14),
                Name = "Pillkys Glass",
                MainContactName = "joe Gallagher",
                Address = "453 Wigan Way",
                City = "Wigan",
                PostCode = "WN5 0UZ",
                Website = "Pillkys.com",
                Phone = "01952246934",
                Email = "mail@Pillkys.com",
                Notes = "supplier of Pillys made glass- Uk leading supplier."
            });

            // Creating a new glass object with the supplier id added
            var glassToAdd = new Glass
            {
                Type = GlassType.STANDARD,
                Name = "4mm Float",
                Thickness = 4.0,
                SheetSizeL = 3210,
                SheetSizeH = 2250,
                PricePerSheet = 1200,
                BoxSheetQuantity = 40,
                SupplierId = supplier.Id,
            };

            // Adding the glass object using the service
            var addedGlass = service.AddGlass(glassToAdd);

            // Retrieving the added glass object using its ID
            var retrievedGlass = service.GetGlassById(addedGlass.Id);

            //assert
            Assert.NotNull(retrievedGlass);
            Assert.Equal(GlassType.STANDARD, retrievedGlass.Type);
            Assert.Equal("4mm Float", retrievedGlass.Name);
            Assert.Equal(4.0, retrievedGlass.Thickness);
            Assert.Equal(3210, retrievedGlass.SheetSizeL);
            Assert.Equal(2250, retrievedGlass.SheetSizeH);
            Assert.Equal(1200, retrievedGlass.PricePerSheet);
            Assert.Equal(40, retrievedGlass.BoxSheetQuantity);
        }

        // =============== GetGlassById(int id); //read method Test =========

        [Fact]
        public void GetGlassById_ReturnsTheGlassWithCorrectId()
        {
            // arrange
            // Adding supplier to Db- needed as a glass cant be added with out a ID for supplier added to system. 
            var supplier = supplierService.AddSupplier(new Supplier
            {
                DateAdded = new DateTime(2023, 07, 14),
                Name = "Pillkys Glass",
                MainContactName = "joe Gallagher",
                Address = "453 Wigan Way",
                City = "Wigan",
                PostCode = "WN5 0UZ",
                Website = "Pillkys.com",
                Phone = "01952246934",
                Email = "mail@Pillkys.com",
                Notes = "supplier of Pillys made glass- Uk leading supplier."
            });

            // Creating a new glass object with the supplier id added- 
            var glassToAdd = new Glass
            {
                Type = GlassType.STANDARD,
                Name = "4mm Float",
                Thickness = 4.0,
                SheetSizeL = 3210,
                SheetSizeH = 2250,
                PricePerSheet = 1200,
                BoxSheetQuantity = 40,
                SupplierId = supplier.Id,
            };

            service.AddGlass(glassToAdd);
            Assert.True(glassToAdd.Id > 0); // Ensure Supplier has an Id.

            // Act
            var testGlass = service.GetGlassById(glassToAdd.Id);

            // Assert
            Assert.NotNull(testGlass);
            Assert.Equal(glassToAdd.Id, testGlass.Id);
            Assert.Equal("4mm Float", testGlass.Name);
        }

        [Fact]
        public void GetGlass_WhenNoneExist_ExpectedNull()
        {
            // arrange
            // Adding supplier to Db- needed as a glass cant be added with out a ID for supplier added to system. 
            var supplier = supplierService.AddSupplier(new Supplier
            {
                DateAdded = new DateTime(2023, 07, 14),
                Name = "Pillkys Glass",
                MainContactName = "joe Gallagher",
                Address = "453 Wigan Way",
                City = "Wigan",
                PostCode = "WN5 0UZ",
                Website = "Pillkys.com",
                Phone = "01952246934",
                Email = "mail@Pillkys.com",
                Notes = "supplier of Pillys made glass- Uk leading supplier."
            });

            // Creating a new glass object with the supplier id added- 
            var glassToAdd = new Glass
            {
                Type = GlassType.STANDARD,
                Name = "4mm Float",
                Thickness = 4.0,
                SheetSizeL = 3210,
                SheetSizeH = 2250,
                PricePerSheet = 1200,
                BoxSheetQuantity = 40,
                SupplierId = supplier.Id,
            };

            service.AddGlass(glassToAdd);
            Assert.True(glassToAdd.Id > 0); // Ensure Supplier has an Id.

            // act
            var nonExistentId = 99999; //this glass is not there
            var glass = service.GetGlassById(nonExistentId);

            // assert
            Assert.Null(glass);
        }

        [Fact]
        public void GetCustomer_WhenAdded_ShouldReturnCustomer()
        {
            // arrange
            // Adding supplier to Db- needed as a glass cant be added with out a ID for supplier added to system. 
            var supplier = supplierService.AddSupplier(new Supplier
            {
                DateAdded = new DateTime(2023, 07, 14),
                Name = "Pillkys Glass",
                MainContactName = "joe Gallagher",
                Address = "453 Wigan Way",
                City = "Wigan",
                PostCode = "WN5 0UZ",
                Website = "Pillkys.com",
                Phone = "01952246934",
                Email = "mail@Pillkys.com",
                Notes = "supplier of Pillys made glass- Uk leading supplier."
            });

            // Creating a new glass object with the supplier id added- 
            var glassToAdd = new Glass
            {
                Type = GlassType.STANDARD,
                Name = "4mm Float",
                Thickness = 4.0,
                SheetSizeL = 3210,
                SheetSizeH = 2250,
                PricePerSheet = 1200,
                BoxSheetQuantity = 40,
                SupplierId = supplier.Id,
            };

            service.AddGlass(glassToAdd);
            Assert.True(glassToAdd.Id > 0); // Ensure glass has an Id.

            // act
            var testGlass = service.GetGlassById(glassToAdd.Id);
            // assert
            Assert.NotNull(testGlass);
            Assert.Equal(testGlass.Id, glassToAdd.Id);
        }

        // =============== Test: Glass UpdateGlass(Glass g); =========================

        [Fact]
        public void UpdateGlass_ShouldUpdate()
        {
            // Arrange
            //create  Supplier-  as a glass can not be added without a supplier.
            var supplier = new Supplier
            {
                DateAdded = new DateTime(2023, 07, 14),
                Name = "New Supplier of Glass",
                MainContactName = "joe Gallagher",
                Address = "new address 500 Wigan Way",
                City = "Wigan",
                PostCode = "WN5 0UZ",
                Website = "NewSupplier.com",
                Phone = "01952246934",
                Email = "mail@NewSupplier..com",
                Notes = "supplier of Pillys made glass- Uk leading supplier."
            };

            supplier = supplierService.AddSupplier(supplier);

            var originalGlass = new Glass
            {
                Type = GlassType.FIRE,
                Name = "OriginalGlass",
                Thickness = 6.0,
                SheetSizeL = 500,
                SheetSizeH = 800,
                PricePerSheet = 130,
                BoxSheetQuantity = 10,
                SupplierId = supplier.Id
            };

            service.AddGlass(originalGlass);
            Assert.True(originalGlass.Id > 0); // ensures the glass is assigned an Id.

            // Update properties of the original glass
            originalGlass.Type = GlassType.SAFETY;
            originalGlass.Name = "UpdatedGlass";
            originalGlass.Thickness = 6.0;
            originalGlass.SheetSizeL = 400;
            originalGlass.SheetSizeH = 900;
            originalGlass.PricePerSheet = 190;
            originalGlass.BoxSheetQuantity = 10;

            // Act
            var updatedGlass = service.UpdateGlass(originalGlass);
            // Assert
            Assert.NotNull(updatedGlass);
            Assert.Equal(GlassType.SAFETY, updatedGlass.Type);
            Assert.Equal("UpdatedGlass", updatedGlass.Name);
            Assert.Equal(6.0, updatedGlass.Thickness);
            Assert.Equal(400, updatedGlass.SheetSizeL);
            Assert.Equal(900, updatedGlass.SheetSizeH);
            Assert.Equal(190, updatedGlass.PricePerSheet);
            Assert.Equal(10, updatedGlass.BoxSheetQuantity);
        }

        // ==================== Test: lass GetGlassByType(string Type);  ================

        [Fact]
        public void GetGlassByType_ReturnsCorrectGlass()
        {
            // Arrange           
            var supplier = new Supplier
            {
                DateAdded = new DateTime(2023, 07, 14),
                Name = "Pillkys Glass",
                MainContactName = "joe Gallagher",
                Address = "453 Wigan Way",
                City = "Wigan",
                PostCode = "WN5 0UZ",
                Website = "Pillkys.com",
                Phone = "01952246934",
                Email = "mail@Pillkys.com",
                Notes = "supplier of Pillys made glass- Uk leading supplier."
            };


            supplier = supplierService.AddSupplier(supplier);

            var glassToAdd = new Glass { Id = 1, Type = GlassType.FIRE, Name = "Test", SupplierId = supplier.Id };
            service.AddGlass(glassToAdd);

            // Act
            var result = service.GetGlassByType("FIRE");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(GlassType.FIRE, result.Type);
        }

        [Fact]
        public void GetGlassByType_ReturnsNullForInvalidType()
        {
            // Act
            var result = service.GetGlassByType("INVALID");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteGlass_ShouldDelete()
        {
            // Arrange
            var supplier = new Supplier
            {
                DateAdded = new DateTime(2023, 07, 14),
                Name = "Pillkys Glass",
                MainContactName = "joe Gallagher",
                Address = "453 Wigan Way",
                City = "Wigan",
                PostCode = "WN5 0UZ",
                Website = "Pillkys.com",
                Phone = "01952246934",
                Email = "mail@Pillkys.com",
                Notes = "supplier of Pillys made glass- Uk leading supplier."
            };

            supplier = supplierService.AddSupplier(supplier);
            Assert.NotNull(supplier);  // Ensure the supplier was created

            var newGlass = new Glass
            {
                Type = GlassType.FIRE,
                Name = "TestGlass",
                Thickness = 6.0,
                SheetSizeL = 500,
                SheetSizeH = 500,
                PricePerSheet = 100,
                BoxSheetQuantity = 10,
                SupplierId = supplier.Id
            };

            var addedGlass = service.AddGlass(newGlass);
            Assert.NotNull(addedGlass);
            Assert.True(addedGlass.Id > 0); // Verifying the Glass has been added

            // Act
            var result = service.DeleteGlass(addedGlass.Id);

            // Assert
            Assert.True(result);
            Assert.Null(service.GetGlassById(addedGlass.Id)); // Confirm the glass no longer exists in the DB.
        }

    }
}