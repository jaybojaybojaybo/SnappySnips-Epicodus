using Microsoft.AspNetCore.Mvc;
using SnappySnips.Models;
using System;
using System.Collections.Generic;

namespace SnappySnips.Controllers
{
  public class StylistsController : Controller
  {
    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/create")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/stylists/create")]
    public ActionResult Create()
    {
      DateTime hireDate = DateTime.Parse(Request.Form["hireDate"]);
      Stylist newStylist = new Stylist(Request.Form["name"], hireDate);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }

    [HttpGet("/stylists/{id}/details")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object> {};
      Stylist currentStylist = Stylist.Find(id);
      List<Client> stylistClients = currentStylist.GetClients();
      List<Specialty> stylistSpecialtys = currentStylist.GetSpecialtys();
      model.Add("currentStylist", currentStylist);
      model.Add("stylistClients", stylistClients);
      model.Add("stylistSpecialtys", stylistSpecialtys);
      return View(model);
    }

    [HttpGet("/stylists/{id}/clients/create")]
    public ActionResult CreateClient(int id)
    {
      Stylist currentStylist = Stylist.Find(id);
      return View(currentStylist);
    }

    [HttpPost("/stylists/{id}/clients/create")]
    public ActionResult ClientCreate(int id)
    {
      Stylist currentStylist = Stylist.Find(id);
      Client newClient = new Client(Request.Form["name"]);
      newClient.Save();

      newClient.AddStylist(currentStylist);

      Dictionary<string, object> model = new Dictionary<string, object> {};
      List<Client> stylistClients = currentStylist.GetClients();
      List<Specialty> stylistSpecialtys = currentStylist.GetSpecialtys();
      model.Add("currentStylist", currentStylist);
      model.Add("stylistClients", stylistClients);
      model.Add("stylistSpecialtys", stylistSpecialtys);
      return View("Details", model);
    }

    [HttpPost("/stylists/delete")]
    public ActionResult DeleteAll()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }
  }
}
