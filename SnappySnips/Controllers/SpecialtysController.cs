using Microsoft.AspNetCore.Mvc;
using SnappySnips.Models;
using System;
using System.Collections.Generic;

namespace SnappySnips.Controllers
{
  public class SpecialtysController : Controller
  {
    [HttpGet("/specialtys")]
    public ActionResult Index()
    {
      List<Specialty> allSpecialtys = Specialty.GetAll();
      return View(allSpecialtys);
    }

    [HttpGet("/specialtys/create")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/specialtys/create")]
    public ActionResult Create()
    {
      Specialty newSpecialty = new Specialty(Request.Form["name"]);
      newSpecialty.Save();
      List<Specialty> allSpecialtys = Specialty.GetAll();
      return View("Index", allSpecialtys);
    }

    [HttpPost("/specialtys/delete")]
    public ActionResult DeleteAll()
    {
      Specialty.DeleteAll();
      specialtys_stylists.DeleteAll();
      List<Specialty> allSpecialtys = Specialty.GetAll();
      return View("Index", allSpecialtys);
    }
  }
}
