using System;
using Xunit;
using APG_CRM.Data.Entities;
using APG_CRM.Data.Services;
using Microsoft.EntityFrameworkCore;
using APG_CRM.Data.Repositories;


namespace APG_CRM.Test
{
    [Collection("Sequential")]
    public class SupplierServiceTest
    {
        private readonly ISupplierService service;
        private readonly IGlassService GlassService;

        public SupplierServiceTest()
        {
            // configure the data context options to use sqlite for testing
            var options = DatabaseContext.OptionsBuilder
                            .UseSqlite("Filename=test.db")
                            //.LogTo(Console.WriteLine)
                            .Options;

            // create service with new context
            service = new SupplierServiceDb(new DatabaseContext(options));
            GlassService = new GlassServiceDb(new DatabaseContext(options));


            service.Initialise();

        }// test enviroment

        // ==================== Supplier UpdateSupplier(Supplier s) Test =========================
        [Fact]
        public void CreateSupplier_AddNewSupplier()
        {
            // arrange
            // Adding supplier 
            var data = service.AddSupplier(new Supplier
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

            //act
            var supplier = service.GetSupplierById(data.Id);
            //assert
            Assert.NotNull(supplier);
            Assert.Equal(new DateTime(2023, 07, 14), supplier.DateAdded);
            Assert.Equal("Pillkys Glass", supplier.Name);
            Assert.Equal("joe Gallagher", supplier.MainContactName);
            Assert.Equal("453 Wigan Way", supplier.Address);
            Assert.Equal("Wigan", supplier.City);
            Assert.Equal("WN5 0UZ", supplier.PostCode);
            Assert.Equal("Pillkys.com", supplier.Website);
            Assert.Equal("01952246934", supplier.Phone);
            Assert.Equal("mail@Pillkys.com", supplier.Email);
            Assert.Equal("supplier of Pillys made glass- Uk leading supplier.", supplier.Notes);
        }

        // ====================  Supplier GetSupplier(int id); //read method Test ======================

        [Fact]
        public void GetSupplierById_ReturnsTheSupplierWithCorrectId()
        {
            // Arrange- create origninal Supplier- supplier can be made even if they dont supply a glass.
            var originalSupplier = new Supplier
            {
                DateAdded = new DateTime(2023, 07, 14),
                Name = "5 star Glass",
                MainContactName = "joe Gallagher",
                Address = "2 Birmingham Way",
                City = "Birmingham",
                PostCode = "BR1 0UZ",
                Website = "5StarGlass.com",
                Phone = "01952246934",
                Email = "mail@5StarGlass.com",
                Notes = "supplier of English made glass- Uk leading supplier."
            };

            service.AddSupplier(originalSupplier);
            Assert.True(originalSupplier.Id > 0); // Ensure Supplier has an Id.

            // Act
            var testSupplier = service.GetSupplierById(originalSupplier.Id);

            // Assert
            Assert.NotNull(testSupplier);
            Assert.Equal(originalSupplier.Id, testSupplier.Id);
            Assert.Equal("5 star Glass", testSupplier.Name);
        }

        // ====================  Supplier UpdateSupplier(Supplier s); //update method Test ================

        [Fact]
        public void UpdateSupplier_ShouldUpdate()
        {

            // Arrange- create origninal Supplier- supplier can be made even if they dont supply a glass.
            var originalSupplier = new Supplier
            {
                DateAdded = new DateTime(2023, 07, 14),
                Name = "5 star Glass",
                MainContactName = "joe Gallagher",
                Address = "2 Birmingham Way",
                City = "Birmingham",
                PostCode = "BR1 0UZ",
                Website = "5StarGlass.com",
                Phone = "01952246934",
                Email = "mail@5StarGlass.com",
                Notes = "supplier of English made glass- Uk leading supplier."
            };

            service.AddSupplier(originalSupplier);
            Assert.True(originalSupplier.Id > 0); // Ensure Supplier has an Id.

            // Update properties of the original supplier
            originalSupplier.DateAdded = new DateTime(2023, 08, 10);
            originalSupplier.Name = "1star glass";
            originalSupplier.MainContactName = "joe Gallagher";
            originalSupplier.Address = "1 Birmingham Way";
            originalSupplier.City = "Birmingham";
            originalSupplier.PostCode = "BR1 0UZ";
            originalSupplier.Website = "1StarGlass.com";
            originalSupplier.Phone = "01952246934";
            originalSupplier.Email = "mail@1StarGlass.com";
            originalSupplier.Notes = "supplier of English made glass- Uk leading supplier.";

            // Act
            var updatedSupplier = service.UpdateSupplier(originalSupplier);

            // Retrieve the updated supplier 
            var retrievedSupplier = service.GetSupplierById(updatedSupplier.Id);

            // Assert
            Assert.NotNull(retrievedSupplier);
            Assert.Equal(originalSupplier.Id, retrievedSupplier.Id); // Ensure the ID matches and that it's the same supplier.
            Assert.Equal("1star glass", retrievedSupplier.Name);
            Assert.Equal(new DateTime(2023, 08, 10), retrievedSupplier.DateAdded);
            Assert.Equal("1StarGlass.com", retrievedSupplier.Website);
            Assert.Equal("mail@1StarGlass.com", retrievedSupplier.Email);
        }

    }
}