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
using System.ComponentModel.DataAnnotations;
using ServiceStation.Data.API;
using ServiceStation.Data.Repositories;

namespace ServiceStation.Web.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository orderRepository;
        private ICarRepository carRepository;

        public OrderController()
        {
            var context = new ServiceStationDbContext();
            this.orderRepository = new OrderRepository(context);
            this.carRepository = new CarRepository(context);
        }

        public OrderController(IOrderRepository orderRepository, ICarRepository carRepository)
        {
            this.orderRepository = orderRepository;
            this.carRepository = carRepository;
        }

        public ActionResult Index(string orderStatus)
        {
            ViewBag.StatusList = new List<SelectListItem>()
                {
                    new SelectListItem() { Value = string.Empty, Selected = true, Text = "All" },
                    new SelectListItem() { Value = OrderStatus.InProgress.ToString(), Selected = false, Text = "In progress" },
                    new SelectListItem() { Value = OrderStatus.Completed.ToString(), Selected = false, Text = "Completed" },
                    new SelectListItem() { Value = OrderStatus.Cancelled.ToString(), Selected = false, Text = "Cancelled" }
                };
            if (!string.IsNullOrEmpty(orderStatus))
            {
                return View(orderRepository.GetOrders().Where(o => o.Status.ToString().Equals(orderStatus)).OrderByDescending(o => o.Date).ToList());
            }
            return View(orderRepository.GetOrders().OrderByDescending(o => o.Date).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderRepository.FindOrderById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public ActionResult Create(int? carId)
        {
            if (carId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CarId = new SelectList(carRepository.GetCars().Where(c => c.CarId == carId), "CarId", "VIN", carId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,Date,Amount,Status,CarId")] Order order)
        {
            if (ModelState.IsValid)
            {
                orderRepository.AddOrder(order);
                orderRepository.Save();
                return RedirectToAction("Details", "Car", new { id = order.CarId });
            }

            ViewBag.CarId = new SelectList(carRepository.GetCars(), "CarId", "VIN", order.CarId);
            return View(order);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderRepository.FindOrderById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(carRepository.GetCars().Where(c => c.CarId == order.CarId), "CarId", "VIN", order.CarId);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,Date,Amount,Status,CarId")] Order order, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                orderRepository.UpdateOrder(order);
                orderRepository.Save();
                return Redirect(returnUrl);
            }
            ViewBag.CarId = new SelectList(carRepository.GetCars(), "CarId", "VIN", order.CarId);
            return View(order);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderRepository.FindOrderById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string returnUrl)
        {
            Order order = orderRepository.FindOrderById(id);
            int carId = order.CarId;
            orderRepository.DeleteOrder(order);
            orderRepository.Save();
            return Redirect(returnUrl);
        }

        protected override void Dispose(bool disposing)
        {
            orderRepository.Dispose();
            carRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
