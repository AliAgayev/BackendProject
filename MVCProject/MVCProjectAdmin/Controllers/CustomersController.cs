using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProjectDAL.Data;
using MVCProjectDAL.Model;

namespace MVCProjectAdmin.Controllers
{
        [Area("Admin")]
        public class CustomersController : Controller
        {
            private readonly AppDBContext _context;

            public CustomersController(AppDBContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Index()
            {
                return View(await _context.Customers.ToListAsync());
            }

            public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.Customers == null)
                {
                    return NotFound();
                }

                var customer = await _context.Customers
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (customer == null)
                {
                    return NotFound();
                }

                return View(customer);
            }

            public IActionResult Create()
            {
                return View();
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Customer customer)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }

            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || _context.Customers == null)
                {
                    return NotFound();
                }

                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, Customer customer)
            {
                if (id != customer.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    customer.UpdatedDate = DateTime.Now;
                    try
                    {

                        _context.Update(customer);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CustomerExists(customer.Id))
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
                return View(customer);
            }

            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || _context.Customers == null)
                {
                    return NotFound();
                }

                var customer = await _context.Customers
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (customer == null)
                {
                    return NotFound();
                }

                return View(customer);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.Customers == null)
                {
                    return Problem("Entity set 'AppDBContext.Customers'  is null.");
                }
                var customer = await _context.Customers.FindAsync(id);
                if (customer != null)
                {
                    customer.DeletedDate = DateTime.Now;
                    _context.Customers.Remove(customer);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool CustomerExists(int id)
            {
                return _context.Customers.Any(e => e.Id == id);
            }
        }
    }