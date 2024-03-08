// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc;
// using APG_CRM.Data.Repositories;
// using APG_CRM.Data.Entities;
// using APG_CRM.Web.Models;
// using System;
// using System.Linq;

// namespace APG_CRM.Web.Controllers
// {
//     public class GlassStockController : Controller
//     {
//         private IStockService svc;
//         private readonly DatabaseContext dbContext;

//         private readonly ILogger<GlassStockController> _logger;

//         public GlassStockController(DatabaseContext dbContext, ILogger<GlassStockController> logger, IStockService StockService)
//         {
//             this.dbContext = dbContext;
//             _logger = logger;
//             this.svc = StockService;
//         }
//         //CRUD/ GET: /GlassStock

//         private const int PageSize = 30;

//         public IActionResult Index(int pageNumber = 1)
//         {
//             var totalItems = svc.Stocks.Count();
//             var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

//             var stocks = svc.GlassStocks
//                                 .Skip((pageNumber - 1) * PageSize)
//                                 .Take(PageSize)
//                                 .ToList();

//             var viewModel = new GlassStockIndexViewModel
//             {
//                 GlassStocks = stocks,
//                 CurrentPage = pageNumber,
//                 TotalPages = totalPages
//             };

//             return View(viewModel);
//         }
//         public IActionResult AllGlassDetails()
//         {
//             return View();
//         }
//         public IActionResult Details(int id)
//         {
//             var stock = svc.GlassStocks.FirstOrDefault(s => s.Id == id);
//             if (stock == null)
//             {
//                 return NotFound();
//             }
//             return View(stock);
//         }
//         public IActionResult Create()
//         {
//             return View();
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public IActionResult Create(Stock stock)
//         {
//             if (ModelState.IsValid)
//             {
//                 svc.GlassStocks.Add(stock);
//                 svc.SaveChanges();
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(stock);
//         }

//         public IActionResult Edit(int id)
//         {
//             var stock = svc.GlassStocks.FirstOrDefault(s => s.Id == id);
//             if (stock == null)
//             {
//                 return NotFound();
//             }
//             return View(stock);
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public IActionResult Edit(int id, Stock stock)
//         {
//             if (id != stock.Id)
//             {
//                 return NotFound();
//             }

//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     svc.Update(stock);
//                     svc.SaveChanges();
//                     return RedirectToAction(nameof(Index));
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     // This is for handling situations where the record might have been 
//                     // modified or deleted by another user since it was loaded.
//                     if (!svc.GlassStocks.Any(e => e.Id == id))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//             }
//             return View(stock);
//         }

//         public IActionResult Delete(int id)
//         {
//             var stock = svc.GlassStocks.FirstOrDefault(s => s.Id == id);
//             if (stock == null)
//             {
//                 return NotFound();
//             }
//             return View(stock);
//         }

//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public IActionResult DeleteConfirmed(int id)
//         {
//             var stock = svc.Stock.FirstOrDefault(s => s.Id == id);
//             svc.Stock.Remove(stock);
//             svc.SaveChanges();
//             return RedirectToAction(nameof(Index));
//         }


//     }
// }
