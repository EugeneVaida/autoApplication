using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using autoApp.Models;
using autoApp.Models.DB;
using autoApp.Models.Converter;

namespace autoApp.Controllers
{
    public class CarsController : Controller
    {
        private CarContext db = new CarContext();



        // GET: Cars
        public ActionResult Index(string price, int? manufacturer, int? model)
        {
            IQueryable<Car> cars;

            cars = db.Cars.Include(c => c.Model).Include(c => c.Model.Manufacturer);

            if (manufacturer != null)
            {
                cars = cars.Where(x => x.Model.ManufacturerId == manufacturer);
            }
            if (model != null)
            {
                cars = cars.Where(x => x.ModelId == model);
            }


            if (price == "")
            {
                ViewBag.Price = "Low";
            }
            else
            {
                ViewBag.Price = price;
            }
            
            var eql = (price == "High") ?
                cars = cars.OrderByDescending(x => x.Price)
                :
                cars = cars.OrderBy(x => x.Price);

            ViewBag.Manufacturer = manufacturer;
            ViewBag.Model = model;
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "Id", "Name", manufacturer);
            ViewBag.ModelId = new SelectList(db.Models.Where(x => x.ManufacturerId == manufacturer), "Id", "Name", model);
            return View(cars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "Id", "Name");
            ViewBag.ModelId = new SelectList(db.Models, "Id", "Name");
            return View();
        }

        // POST: Cars/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "Id", "Name", car.Model.ManufacturerId);
            ViewBag.ModelId = new SelectList(db.Models.Where(x => x.ManufacturerId == car.Model.ManufacturerId), "Id", "Name", car.ModelId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "Id", "Name", car.Model.ManufacturerId);
            ViewBag.ModelId = new SelectList(db.Models, "Id", "Name", car.ModelId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        #region HelperMethods
        [HttpGet]
        public ActionResult GetModelsByManufacturerId(int id)
        {
            var data = db.Models.Where(x => x.ManufacturerId == id).ToList();
            var result = Json(ToModelItemList(data), JsonRequestBehavior.AllowGet);
            return result;
        }


        public List<ModelItem> ToModelItemList(List<Model> models)
        {
            List<ModelItem> modelItems = new List<ModelItem>();

            foreach (var model in models)
            {
                modelItems.Add(ToModelItem(model));
            }

            return modelItems;
        }

        public ModelItem ToModelItem(Model model)
        {
            ModelItem modelItem = new ModelItem
            {
                Id = model.Id,
                Name = model.Name
            };

            return modelItem;
        }

        #endregion

    }
}
