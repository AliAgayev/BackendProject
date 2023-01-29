using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCProjectDAL.Data;
using MVCProjectDAL.Model;

namespace MVCProjectAdmin.Controllers
{

        public class OrdersController : Controller
        {
            private readonly AppDBContext _context;

            public OrdersController(AppDBContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Index()
            {
                var appDBContext = _context.Orders.Include(o => o.Customer);
                return View(await appDBContext.ToListAsync());
            }

            public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.Orders == null)
                {
                    return NotFound();
                }

                var order = await _context.Orders
                    .Include(o => o.Customer)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (order == null)
                {
                    return NotFound();
                }

                return View(order);
            }

            public IActionResult Create()
            {
                ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
                return View();
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Order order)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(order);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", order.CustomerId);
                return View(order);
            }

            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || _context.Orders == null)
                {
                    return NotFound();
                }

                var order = await _context.Orders.FindAsync(id);
                if (order == null)
                {
                    return NotFound();
                }
                ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", order.CustomerId);
                return View(order);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, Order order)
            {
                if (id != order.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        order.UpdatedDate = DateTime.Now;
                        _context.Update(order);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!OrderExists(order.Id))
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
                ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", order.CustomerId);
                return View(order);
            }

            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || _context.Orders == null)
                {
                    return NotFound();
                }

                var order = await _context.Orders
                    .Include(o => o.Customer)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (order == null)
                {
                    return NotFound();
                }

                return View(order);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.Orders == null)
                {
                    return Problem("Entity set 'AppDBContext.Orders'  is null.");
                }
                var order = await _context.Orders.FindAsync(id);
                if (order != null)
                {
                    order.DeletedDate = DateTime.Now;
                    _context.Orders.Remove(order);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool OrderExists(int id)
            {
                return _context.Orders.Any(e => e.Id == id);
            }
        }
    }
