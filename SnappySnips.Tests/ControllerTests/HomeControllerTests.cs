using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SnappySnips.Controllers;
using SnappySnips.Models;

namespace SnappySnips.Tests
{
  [TestClass]
  public class HomeControllerTest
  {
    [TestMethod]
    public void Index_ReturnsCorrectView_True()
    {
      //Arrange
      HomeController controller = new HomeController();
      //Act
      IActionResult indexView = controller.Index();
      ViewResult result = indexView as ViewResult;
      //Assert
      Assert.IsInstanceOfType(result, typeof(ViewResult));
    }
  }
}
