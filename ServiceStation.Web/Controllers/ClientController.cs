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
    public class ClientController : Controller
    {
        private IClientRepository clientRepository;

        public ClientController()
        {
            this.clientRepository = new ClientRepository(new ServiceStationDbContext());
        }

        public ClientController(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public ActionResult Index(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return View(clientRepository.GetClients());

            if (searchString.Length < 3)
            {
                TempData["message"] = "Search string should contain at least 3 symbols";
                return View(clientRepository.GetClients());
            }

            var searchResult = clientRepository.GetClients().Where(p => p.FullName.Contains(searchString)).ToList();
            
            if (searchResult.Count() == 0)
            {
                TempData["message"] = "Client not found";
                return View(clientRepository.GetClients());
            }

            if (searchResult.Count() == 1)
            {
                return RedirectToAction("Details", "Client", new { id = searchResult.First().ClientId });
            }

            return View(searchResult);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = clientRepository.FindClientById(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,LastName,FullName,DateOfBirth,Address,Phone,Email")] Client client)
        {
            if (ModelState.IsValid)
            {
                client.FullName = client.FirstName + " " + client.LastName;
                clientRepository.AddClient(client);
                clientRepository.Save();
                return RedirectToAction("Details", "Client", new { id = client.ClientId});
            }
            return View(client);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = clientRepository.FindClientById(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,FirstName,LastName,FullName,DateOfBirth,Address,Phone,Email")] Client client, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                client.FullName = client.FirstName + " " + client.LastName;
                clientRepository.UpdateClient(client);
                clientRepository.Save();
                return Redirect(returnUrl);
            }
            return View(client);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = clientRepository.FindClientById(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = clientRepository.FindClientById(id);
            if (client.Cars.Count() != 0)
            {
                TempData["message"] = "This client can't be deleted until he has related cars";
                return View(client);
            }
            clientRepository.DeleteClient(client);
            clientRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            clientRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
