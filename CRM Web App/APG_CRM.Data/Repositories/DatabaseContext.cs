using Microsoft.EntityFrameworkCore;
using APG_CRM.Data.Entities;

namespace APG_CRM.Data.Repositories;

public class DatabaseContext : DbContext  // if set as internal protects the Db from access from outside of the .data project.
{
    // The Context is How EntityFramework communicates with the database
    // We define DbSet properties for each table in the database

    //public class DatabaseContext : DbContext

    // Database store
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    // Business logic orientated types of actions by sales team
    public DbSet<Quotation> Quotations { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Invoice> Invoices { get; set; }

    // Business logic orientated types of managment review data 
    public DbSet<Glass> Glasses { get; set; }
    public DbSet<Stock> Stock { get; set; }
    public DbSet<GlassImage> GlassImages { get; set; } //setting up for image carousel

    // authentication store
    public DbSet<User> Users { get; set; }
    public DbSet<ForgotPassword> ForgotPasswords { get; set; }

    // Configure the context with logging - remove in production

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // configurations
        // Configure Customer-Quotation relationship with cascading delete
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Quotations)
            .WithOne(q => q.Customer)
            .HasForeignKey(q => q.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

              // Configure Quotation-Survey relationship with cascading delete
        modelBuilder.Entity<Quotation>()
            .HasMany(q => q.Surveys)
            .WithOne(s => s.Quotation)
            .HasForeignKey(s => s.QuotationId)   //SurveyId
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Customer-Quotation relationship with cascading delete
        modelBuilder.Entity<Survey>()
            .HasOne(s => s.Quotation)
            .WithMany(q => q.Surveys)
            .HasForeignKey(s => s.QuotationId)
            .OnDelete(DeleteBehavior.Cascade); // Add cascading delete for Surveys


      

        // Configuration for Glass and GlassImage
        modelBuilder.Entity<Glass>()
            .HasMany(g => g.Images)
            .WithOne(i => i.Glass)
            .HasForeignKey(i => i.GlassId);

    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
        .UseSqlite("Filename=data.db")
        //.LogTo(Console.WriteLine, LogLevel.Information)
        ;
        // remove in production - used to look for EF actions in development             
    }
    public static DbContextOptionsBuilder<DatabaseContext> OptionsBuilder => new();

    // Convenience method to recreate the database thus ensuring the new database takes 
    // account of any changes to Models or DatabaseContext. ONLY to be used in development
    public void Initialise()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}
