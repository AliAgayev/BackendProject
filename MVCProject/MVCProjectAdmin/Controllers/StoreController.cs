using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProjectDAL.Data;
using MVCProjectDAL.Model;
using System.Data;

namespace MVCProjectAdmin.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class StoreController : Controller
    {
        private readonly AppDBContext _db;


        public StoreController(AppDBContext store)
        {
            _db = store;
        }
        public IActionResult Index()
        {
            List<Store> stores = _db.Stores.ToList();
            return View(stores);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Stores == null)
            {
                return NotFound();
            }

            var store = await _db.Stores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        public IActionResult Create()
        {
            Store store = new Store();
            return View(store);
        }

        [HttpPost]
        public IActionResult Create(Store store)
        {
            if (ModelState.IsValid)
            {
                _db.Stores.Add(store);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.Clear();
            return View(store);
        }

        public IActionResult Delete(int? id)
        {
            Store store = _db.Stores.Where(p => p.Id == id).FirstOrDefault();
            _db.Stores.Remove(store);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            Store store = _db.Stores.Where(p => p.Id == id).FirstOrDefault();
            return View(store);

        }
        [HttpPost]

        public IActionResult Edit([Bind("Id,Name,Description")] Store store)
        {
            store.UpdatedDate = DateTime.Now;
            _db.Stores.Update(store);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
