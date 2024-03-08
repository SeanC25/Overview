using Microsoft.EntityFrameworkCore;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;

namespace APG_CRM.Web.Models;

public class QuotationScanViewModel
{
    public IList<Quotation> Quotations { get; set; }

    public string Check { get; set; }
}