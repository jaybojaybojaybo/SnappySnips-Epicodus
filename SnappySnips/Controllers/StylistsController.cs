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

    [HttpGet("/stylists/{id}/specialtys/create")]
    public ActionResult CreateSpecialty(int id)
    {
      Stylist currentStylist = Stylist.Find(id);

      Dictionary<string, object> model = new Dictionary<string, object> {};
      List<Specialty> allSpecialtys = Specialty.GetAll();
      model.Add("currentStylist", currentStylist);
      model.Add("allSpecialtys", allSpecialtys);
      return View(model);
    }

    [HttpPost("/stylists/{id}/specialtys/create")]
    public ActionResult SpecialtyCreate(int id)
    {
      Stylist currentStylist = Stylist.Find(id);
      int specialtyId = Convert.ToInt32(Request.Form["specialty"]);
      Specialty newSpecialty = Specialty.Find(specialtyId);

      newSpecialty.AddStylist(currentStylist);

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
      clients_stylists.DeleteAll();
      specialtys_stylists.DeleteAll();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }

    [HttpGet("/stylists/{id}/edit")]
    public ActionResult EditForm(int id)
    {
      Stylist currentStylist = Stylist.Find(id);
      return View(currentStylist);
    }

    [HttpPost("/stylists/{id}/edit")]
    public ActionResult Edit(int id)
    {
      Stylist currentStylist = Stylist.Find(id);
      string newName = Request.Form["newName"];
      DateTime newHireDate = DateTime.Parse(Request.Form["newHireDate"]);
      currentStylist.EditName(newName);
      currentStylist.EditHireDate(newHireDate);

      Dictionary<string, object> model = new Dictionary<string, object> {};
      List<Client> stylistClients = currentStylist.GetClients();
      List<Specialty> stylistSpecialtys = currentStylist.GetSpecialtys();
      model.Add("currentStylist", currentStylist);
      model.Add("stylistClients", stylistClients);
      model.Add("stylistSpecialtys", stylistSpecialtys);
      return View("Details", model);
    }

    [HttpGet("/stylists/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Stylist currentStylist = Stylist.Find(id);
      List<Client> deletedClients = currentStylist.GetClients();
      foreach(var client in deletedClients)
      {
        client.Delete(client.GetId());
      }
      currentStylist.Delete(currentStylist.GetId());

      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }


  }
}
