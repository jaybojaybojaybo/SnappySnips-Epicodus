using Microsoft.AspNetCore.Mvc;
using SnappySnips.Models;
using System;
using System.Collections.Generic;

namespace SnappySnips.Controllers
{
  public class ClientsController : Controller
  {
    [HttpGet("/clients")]
    public ActionResult Index()
    {
      List<Client> allClients = Client.GetAll();
      return View(allClients);
    }

    [HttpGet("/clients/create")]
    public ActionResult CreateForm()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpPost("/clients/create")]
    public ActionResult Create(int id)
    {
      Client newClient = new Client(Request.Form["name"]);
      newClient.Save();
      int stylistId = Convert.ToInt32(Request.Form["stylist"]);
      Stylist assignedStylist = Stylist.Find(stylistId);
      newClient.AddStylist(assignedStylist);
      List<Client> allClients = Client.GetAll();
      return View("Index", allClients);
    }

    [HttpGet("/clients/delete")]
    public ActionResult DeleteAll()
    {
      Client.DeleteAll();
      clients_stylists.DeleteAll();
      List<Client> allClients = Client.GetAll();
      return View("Index", allClients);
    }

    [HttpGet("/clients/{id}/edit")]
    public ActionResult EditForm(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object> {};
      Client currentClient = Client.Find(id);
      List<Stylist> allStylists = Stylist.GetAll();
      model.Add("currentClient", currentClient);
      model.Add("allStylists", allStylists);
      return View(model);
    }

    [HttpPost("/Clients/{id}/edit")]
    public ActionResult Edit(int id)
    {
      Client currentClient = Client.Find(id);
      string newName = Request.Form["newName"];
      currentClient.Edit(newName);
      int newStylistId = Convert.ToInt32(Request.Form["newStylist"]);
      Stylist arrangedStylist = Stylist.Find(newStylistId);
      currentClient.AddStylist(arrangedStylist);

      List<Client> allClients = Client.GetAll();
      return View("Index", allClients);
    }

    [HttpGet("/clients/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Client currentClient = Client.Find(id);
      currentClient.Delete(currentClient.GetId());

      List<Client> allClients = Client.GetAll();
      return View("Index", allClients);
    }
  }
}
