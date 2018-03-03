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
    public ActionResult Create()
    {
      Client newClient = new Client(Request.Form["name"]);
      newClient.Save();
      List<Client> allClients = Client.GetAll();
      return View("Index", allClients);
    }

    [HttpPost("/clients/delete")]
    public ActionResult DeleteAll()
    {
      Client.DeleteAll();
      List<Client> allClients = Client.GetAll();
      return View("Index", allClients);
    }
  }
}
