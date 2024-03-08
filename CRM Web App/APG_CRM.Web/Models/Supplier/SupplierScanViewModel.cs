using Microsoft.EntityFrameworkCore;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;
using APG_CRM.Web.Models;

namespace APG_CRM.Web.Models;

public class SupplierScanViewModel
{
    public IList<Supplier> Suppliers { get; set; }

    public string Check { get; set; }
}