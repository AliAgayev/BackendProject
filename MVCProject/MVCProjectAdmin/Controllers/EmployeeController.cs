using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProjectDAL.Data;
using MVCProjectDAL.Model;

namespace MVCProjectAdmin.Controllers
{
    public class EmployeeController : Controller
    {

        public class EmployeesController : Controller
        {
            private readonly AppDBContext _context;

            public EmployeesController(AppDBContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Index()
            {
                return View(await _context.Employees.ToListAsync());
            }

            public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.Employees == null)
                {
                    return NotFound();
                }

                var employee = await _context.Employees
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (employee == null)
                {
                    return NotFound();
                }

                return View(employee);
            }

            public IActionResult Create()
            {
                return View();
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Employee employee)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(employee);
            }

            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || _context.Employees == null)
                {
                    return NotFound();
                }

                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, Employee employee)
            {
                if (id != employee.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        employee.UpdatedDate = DateTime.Now;
                        _context.Update(employee);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeExists(employee.Id))
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
                return View(employee);
            }

            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || _context.Employees == null)
                {
                    return NotFound();
                }

                var employee = await _context.Employees
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (employee == null)
                {
                    return NotFound();
                }

                return View(employee);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.Employees == null)
                {
                    return Problem("Entity set 'AppDBContext.Employees'  is null.");
                }
                var employee = await _context.Employees.FindAsync(id);
                if (employee != null)
                {
                    employee.DeletedDate = DateTime.Now;
                    _context.Employees.Remove(employee);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool EmployeeExists(int id)
            {
                return _context.Employees.Any(e => e.Id == id);
            }
        }
    }
}
