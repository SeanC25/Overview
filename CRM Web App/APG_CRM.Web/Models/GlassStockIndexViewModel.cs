using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using APG_CRM.Data.Entities;
namespace APG_CRM.Web.Models;
public class GlassStockIndexViewModel
{
    public List<Stock> Stock { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
