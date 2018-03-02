using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SnappySnips.Models;

namespace SnappySnips.Tests
{
  [TestClass]
  public class SpecialtyTest : IDisposable
  {
    public void SpecialtyTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=jasun_feddema_test;";
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
      Specialty.DeleteAll();
      clients_stylists.DeleteAll();
      specialtys_stylists.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Specialty.GetAll().Count;

      //
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesSpecialtyToDatabase_SpecialtyList()
    {
      //Arrange
      string name = "Kim";
      Specialty newSpecialty = new Specialty(name);

      //Act
      newSpecialty.Save();
      List<Specialty> result = Specialty.GetAll();
      List<Specialty> testList = new List<Specialty>{newSpecialty};

      //Assert
      CollectionAssert.AreEqual(result, testList);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Specialty()
    {
      //Arrange
      Specialty firstSpecialty = new Specialty("Kim");
      Specialty secondSpecialty = new Specialty("Kim");

      //Act
      firstSpecialty.Save();
      secondSpecialty.Save();

      //Assert
      Assert.AreEqual(true, firstSpecialty.GetName().Equals(secondSpecialty.GetName()));
    }

    [TestMethod]
    public void GetAll_ReturnsSpecialtys_SpecialtyList()
    {
      //Arrange
      string name1 = "Kim";
      string name2 = "John";
      Specialty specialty1 = new Specialty(name1);
      Specialty specialty2 = new Specialty(name2);
      List<Specialty> newList = new List<Specialty> {specialty1, specialty2};

      //Act
      specialty1.Save();
      specialty2.Save();
      List<Specialty> result = Specialty.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToObject_Id()
    {
      //Arrange
      string name = "Kim";
      Specialty newSpecialty = new Specialty(name);
      newSpecialty.Save();

      //Act
      Specialty savedSpecialty = Specialty.GetAll()[0];
      int result = savedSpecialty.GetId();
      int testId = newSpecialty.GetId();

      //Assert
      Assert.AreEqual(testId, result);

    }

    [TestMethod]
    public void Find_FindsSpecialtyInDatabase_Specialty()
    {
      //Arrange
      Specialty newSpecialty = new Specialty("Kim");
      newSpecialty.Save();

      //Act
      Specialty foundSpecialty = Specialty.Find(newSpecialty.GetId());

      //Assert
      Assert.AreEqual(true, newSpecialty.Equals(foundSpecialty));
    }

    [TestMethod]
    public void Edit_UpdatesSpecialtyName_Specialty()
    {
      //Arrange
      Specialty newSpecialty = new Specialty("Kim");
      newSpecialty.Save();
      //Act
      newSpecialty.Edit("John");
      //Assert
      Assert.AreEqual("John", newSpecialty.GetName());
    }

    [TestMethod]
    public void Delete_DeleteOneSpecialtyInDatabase_True()
    {
      //Arrange
      Specialty firstSpecialty = new Specialty("Wu");
      Specialty secondSpecialty = new Specialty("Yu");
      List<Specialty> testList = new List<Specialty>{secondSpecialty};
      firstSpecialty.Save();
      secondSpecialty.Save();

      //Act
      int firstId = firstSpecialty.GetId();
      firstSpecialty.Delete(firstId);
      List<Specialty> compareList = Specialty.GetAll();
      Console.WriteLine(compareList.Count);
      //Assert
      Assert.AreEqual(testList.Count, compareList.Count);
    }

    [TestMethod]
      public void AddStylist_AddsStylistToSpecialty_StylistList()
      {
        //Arrange
        Specialty testSpecialty1 = new Specialty("Wu");
        testSpecialty1.Save();

        System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
        Stylist firstStylist = new Stylist("Kim", hireDate);
        firstStylist.Save();


        //Act
        testSpecialty1.AddStylist(firstStylist);
        List<Stylist> testList = new List<Stylist>{firstStylist};
        List<Stylist> result = testSpecialty1.GetStylists();

        //Assert
        Assert.AreEqual(testList.Count, result.Count);
      }

      [TestMethod]
      public void GetStylists_ReturnsAllSpecialtyStylists_StylistList()
      {
        //Arrange
        Specialty testSpecialty1 = new Specialty("Wu");
        testSpecialty1.Save();

        System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
        Stylist firstStylist = new Stylist("Kim", hireDate);
        firstStylist.Save();

        //Act
        testSpecialty1.AddStylist(firstStylist);
        List<Stylist> result = testSpecialty1.GetStylists();
        List<Stylist> testList = new List<Stylist> {firstStylist};

        //Assert
        Assert.AreEqual(testList.Count, result.Count);
        // CollectionAssert.AreEqual(testList, result);
      }
  }
}
