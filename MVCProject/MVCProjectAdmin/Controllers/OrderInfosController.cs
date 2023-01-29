using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCProjectDAL.Data;
using MVCProjectDAL.Model;

namespace MVCProjectAdmin.Controllers
{
    public class OrderInfosController : Controller
    {

        public class OrderitemsController : Controller
        {
            private readonly AppDBContext _context;

            public OrderitemsController(AppDBContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Index()
            {
                var appDBContext = _context.OrderInfos.Include(o => o.Order).Include(o => o.Product);
                return View(await appDBContext.ToListAsync());
            }

            public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.OrderInfos == null)
                {
                    return NotFound();
                }

                var orderInf = await _context.OrderInfos
                    .Include(o => o.Order)
                    .Include(o => o.Product)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (orderInf == null)
                {
                    return NotFound();
                }

                return View(orderInf);
            }

            public IActionResult Create()
            {
                ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Name");
                ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
                return View();
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(OrderInf orderInf)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(orderInf);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Name", orderInf.OrderId);
                ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orderInf.ProductId);
                return View(orderInf);
            }

            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || _context.OrderInfos == null)
                {
                    return NotFound();
                }

                var orderInf = await _context.OrderInfos.FindAsync(id);
                if (orderInf == null)
                {
                    return NotFound();
                }
                ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Name", orderInf.OrderId);
                ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orderInf.ProductId);
                return View(orderInf);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, OrderInf orderInf)
            {
                if (id != orderInf.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        orderInf.UpdatedDate = DateTime.Now;
                        _context.Update(orderInf);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!OrderitemExists(orderInf.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Name", orderInf.OrderId);
                ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orderInf.ProductId);
                return View(orderInf);
            }

            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || _context.OrderInfos == null)
                {
                    return NotFound();
                }

                var orderInf = await _context.OrderInfos
                    .Include(o => o.Order)
                    .Include(o => o.Product)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (orderInf == null)
                {
                    return NotFound();
                }

                return View(orderInf);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.OrderInfos == null)
                {
                    return Problem("Entity set 'AppDBContext.Orderitems'  is null.");
                }
                var orderInf = await _context.OrderInfos.FindAsync(id);
                if (orderInf != null)
                {
                    orderInf.DeletedDate = DateTime.Now;
                    _context.OrderInfos.Remove(orderInf);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool OrderitemExists(int id)
            {
                return _context.OrderInfos.Any(e => e.Id == id);
            }
        }
    }
}
