using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServiceStation.Data.Context;
using ServiceStation.Data.Entities;
using ServiceStation.Data.API;
using ServiceStation.Data.Repositories;

namespace ServiceStation.Web.Controllers
{
    public class CarController : Controller
    {
        private ICarRepository carRepository;
        private IClientRepository clientRepository;

        public CarController()
        {
            var context = new ServiceStationDbContext();
            this.carRepository = new CarRepository(context);
            this.clientRepository = new ClientRepository(context);
        }

        public CarController(ICarRepository carRepository, IClientRepository clientRepository)
        {
            this.carRepository = carRepository;
            this.clientRepository = clientRepository;
        }

        public ActionResult Index()
        {
            return View(carRepository.GetCars());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = carRepository.FindCarById(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        public ActionResult Create(int? clientId)
        {
            if (clientId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.ClientId = new SelectList(clientRepository.GetClients().Where(c => c.ClientId == clientId), "ClientId", "FullName", clientId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarId,Make,Model,Year,VIN,ClientId")] Car car)
        {
            if (ModelState.IsValid)
            {
                carRepository.AddCar(car);
                carRepository.Save();
                return RedirectToAction("Details", "Client", new { id = car.ClientId });
            }

            ViewBag.ClientId = new SelectList(clientRepository.GetClients(), "ClientId", "FullName", car.ClientId);
            return View(car);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = carRepository.FindCarById(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(clientRepository.GetClients().Where(c => c.ClientId == car.ClientId), "ClientId", "FullName", car.ClientId);
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarId,Make,Model,Year,VIN,ClientId")] Car car, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                carRepository.UpdateCar(car);
                carRepository.Save();
                return Redirect(returnUrl);
            }
            ViewBag.ClientId = new SelectList(clientRepository.GetClients(), "ClientId", "FullName", car.ClientId);
            return View(car);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = carRepository.FindCarById(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = carRepository.FindCarById(id);
            if (car.Orders.Count() != 0)
            {
                TempData["message"] = "This car can't be deleted until it has related orders";
                return View(car);
            }
            int clientId = car.ClientId;
            carRepository.DeleteCar(car);
            carRepository.Save();
            return RedirectToAction("Details", "Client", new { id = clientId });
        }

        protected override void Dispose(bool disposing)
        {
            carRepository.Dispose();
            clientRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
