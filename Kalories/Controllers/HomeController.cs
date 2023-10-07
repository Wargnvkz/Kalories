using Kalories.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Kalories.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var data = new IndexViewModel();
            List<KaloriesDB.Food> lst = null;
            using (var db = new KaloriesDB.DB())
            {
                try
                {
                    lst = db.Foods.ToList();
                }
                catch(Exception ex)
                {
                    data.Text = $"{ex.Message}:{db.Database.Connection.ConnectionString}";
                }
            }
            data.Foods = lst;
            data.Food = new KaloriesDB.Food();
            //lst.Add(new KaloriesDB.Food() { FoodName = "греч сал", CaloriesPer100 = 87, Weight = 250 });
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            using (var db = new KaloriesDB.DB())
            {
                var customer = new KaloriesDB.Food() { FoodID = id };
                db.Foods.Attach(customer);
                db.Foods.Remove(customer);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Clear()
        {
            using (var db = new KaloriesDB.DB())
            {
                var foods = db.Foods.ToList();
                db.Foods.RemoveRange(foods);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Add(IndexViewModel data)
        {
            using (var db = new KaloriesDB.DB())
            {
                db.Foods.Add(data.Food);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
