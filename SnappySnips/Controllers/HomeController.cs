using Microsoft.AspNetCore.Mvc;
using SnappySnips.Models;
using System;
using System.Collections.Generic;

namespace SnappySnips.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }
  }
}
