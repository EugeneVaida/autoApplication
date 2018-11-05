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
using System.Text.RegularExpressions;
using System.IO;
using System.Web.Hosting;
using System.Configuration;

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
                if (Request.Files.Count != 0)
                {
                    car.Image = CreateImageLink(Request.Files[0]);
                }
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
                Car oldCar = db.Cars.Find(car.Id);
                if (Request.Files.Count != 0)
                {
                   car.Image = CreateImageLink(Request.Files[0]);
                }
                oldCar = ToNewCar(oldCar, car);
                //db.Entry(car).State = EntityState.Modified;
                
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
            DeleteImage(car);
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

        private string CreateImageLink(HttpPostedFileBase file)
        {
            Random rnd = new Random();
            if (file != null && file.ContentLength > 0)
            {
                string fileNameHash = Math.Abs(file.FileName.GetHashCode() * Math.Pow(2, rnd.Next(1, 10))) + Path.GetExtension(file.FileName);
                var serverUrl = "~/files/img/" + fileNameHash;
                Regex regex = new Regex(@"^.*\.(jpg|gif|png|bmp|jpeg)$", RegexOptions.IgnoreCase);
                if (!regex.IsMatch(Path.GetExtension(serverUrl)))
                {
                    return null;
                }
                file.SaveAs(HostingEnvironment.MapPath(serverUrl));
                
                return ConfigurationManager.AppSettings["Url"] + VirtualPathUtility.ToAbsolute(serverUrl);
            }
            return null;
        }

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

        public Car ToNewCar(Car oldCar, Car editCar)
        {
            oldCar.Id = editCar.Id;
            oldCar.FuelConsumption = editCar.FuelConsumption;
            oldCar.Description = editCar.Description;
            oldCar.ManufacturerDate = editCar.ManufacturerDate;
            oldCar.Power = editCar.Power;
            oldCar.Price = editCar.Price;
            oldCar.Color = editCar.Color;
            oldCar.Model = editCar.Model;
            oldCar.ModelId = editCar.ModelId;

            if (!string.Equals(oldCar.Image, editCar.Image) && editCar.Image != null)
            {
                DeleteImage(oldCar);
                oldCar.Image = editCar.Image;
            }
            return oldCar;
        }

        public void DeleteImage(Car oldCar)
        {
            if (oldCar.Image != null && !string.IsNullOrWhiteSpace(oldCar.Image))
            {
                var path = "~/files/img/" + Path.GetFileName(oldCar.Image);
                System.IO.File.Delete(HostingEnvironment.MapPath(path));
            }
        }

        #endregion

    }
}
