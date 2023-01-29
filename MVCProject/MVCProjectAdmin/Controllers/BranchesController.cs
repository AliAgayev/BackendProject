using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCProjectDAL.Data;
using MVCProjectDAL.Model;
using System.Data;

namespace MVCProjectAdmin.Controllers
    {
        [Authorize(Roles = "SuperAdmin")]

        public class BranchesController : Controller
        {
            private readonly AppDBContext _context;

            public BranchesController(AppDBContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Index()
            {
                var appDBContext = _context.Branches.Include(b => b.Store);
                return View(await appDBContext.ToListAsync());
            }

            public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.Branches == null)
                {
                    return NotFound();
                }

                var branch = await _context.Branches
                    .Include(b => b.Store)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (branch == null)
                {
                    return NotFound();
                }

                return View(branch);
            }

            public IActionResult Create()
            {
                ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Name");
                return View();
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Branch branch)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(branch);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Name", branch.StoreId);
                return View(branch);
            }

            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || _context.Branches == null)
                {
                    return NotFound();
                }

                var branch = await _context.Branches.FindAsync(id);
                if (branch == null)
                {
                    return NotFound();
                }
                ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Name", branch.StoreId);
                return View(branch);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, Branch branch)
            {
                if (id != branch.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        branch.UpdatedDate = DateTime.Now;
                        _context.Update(branch);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BranchExists(branch.Id))
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
                ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Name", branch.StoreId);
                return View(branch);
            }

            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || _context.Branches == null)
                {
                    return NotFound();
                }

                var branch = await _context.Branches
                    .Include(b => b.Store)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (branch == null)
                {
                    return NotFound();
                }

                return View(branch);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.Branches == null)
                {
                    return Problem("Entity set 'AppDBContext.Branches'  is null.");
                }
                var branch = await _context.Branches.FindAsync(id);
                if (branch != null)
                {
                    branch.DeletedDate = DateTime.Now;
                    _context.Branches.Remove(branch);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool BranchExists(int id)
            {
                return _context.Branches.Any(e => e.Id == id);
            }
        }
    }
