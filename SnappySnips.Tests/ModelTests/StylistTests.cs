using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SnappySnips.Models;
using System.Globalization;

namespace SnappySnips.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public void StylistTests()
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
      int result = Stylist.GetAll().Count;

      //
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesStylistToDatabase_StylistList()
    {
      //Arrange
      string name = "Kim";
      System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
      Stylist newStylist = new Stylist(name, hireDate);
      newStylist.Save();

      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{newStylist};
      Console.WriteLine(testList.Count);

      //Assert
      Assert.AreEqual(result.Count, testList.Count);
      CollectionAssert.AreEqual(result, testList);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Stylist()
    {
      //Arrange
      System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
      Stylist firstStylist = new Stylist("Kim", hireDate);
      Stylist secondStylist = new Stylist("Kim", hireDate);

      //Act
      firstStylist.Save();
      secondStylist.Save();

      //Assert
      Assert.AreEqual(true, firstStylist.GetName().Equals(secondStylist.GetName()));
    }

    [TestMethod]
    public void GetAll_ReturnsStylists_StylistList()
    {
      //Arrange
      string name1 = "Kim";
      string name2 = "John";
      System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
      Stylist stylist1 = new Stylist(name1, hireDate);
      Stylist stylist2 = new Stylist(name2, hireDate);
      List<Stylist> newList = new List<Stylist> {stylist1, stylist2};

      //Act
      stylist1.Save();
      stylist2.Save();
      List<Stylist> result = Stylist.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToObject_Id()
    {
      //Arrange
      string name = "Kim";
      System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
      Stylist newStylist = new Stylist(name, hireDate);
      newStylist.Save();

      //Act
      Stylist savedStylist = Stylist.GetAll()[0];
      int result = savedStylist.GetId();
      int testId = newStylist.GetId();

      //Assert
      Assert.AreEqual(testId, result);

    }

    [TestMethod]
    public void Find_FindsStylistInDatabase_Stylist()
    {
      //Arrange
      System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
      Stylist newStylist = new Stylist("Kim", hireDate);
      newStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(newStylist.GetId());

      //Assert
      Assert.AreEqual(true, newStylist.Equals(foundStylist));
    }

    [TestMethod]
    public void EditName_UpdatesStylistName_Stylist()
    {
      //Arrange
      System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
      Stylist newStylist = new Stylist("Kim", hireDate);
      newStylist.Save();
      //Act
      newStylist.EditName("John");
      //Assert
      Assert.AreEqual("John", newStylist.GetName());
    }

    [TestMethod]
    public void EditHireDate_UpdatesStylistHireDate_Stylist()
    {
      //Arrange
      System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
      Stylist newStylist = new Stylist("Kim", hireDate);
      newStylist.Save();
      //Act
      newStylist.EditHireDate(System.DateTime.Parse("02/28/2018"));
      //Assert
      Assert.AreEqual(System.DateTime.Parse("02/28/2018"), newStylist.GetHireDate());
    }

    [TestMethod]
    public void Delete_DeleteOneStylistInDatabase_True()
    {
      //Arrange
      System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
      Stylist firstStylist = new Stylist("Kim", hireDate);
      Stylist secondStylist = new Stylist("John", hireDate);
      List<Stylist> testList = new List<Stylist>{secondStylist};
      firstStylist.Save();
      secondStylist.Save();

      //Act
      int firstId = firstStylist.GetId();
      firstStylist.Delete(firstId);
      List<Stylist> compareList = Stylist.GetAll();

      //Assert
      Assert.AreEqual(testList.Count, compareList.Count);
    }

    [TestMethod]
    public void AddClient_AddsClientToStylist_ClientList()
    {
      //Arrange
      Client testClient1 = new Client("Wu");
      testClient1.Save();
      Client testClient2 = new Client("Yu");
      testClient2.Save();

      System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
      Stylist firstStylist =  new Stylist("Kim", hireDate);
      firstStylist.Save();

      List<Client> testList = new List<Client>{testClient1, testClient2};

      //Act
      firstStylist.AddClient(testClient1);
      firstStylist.AddClient(testClient2);
      List<Client> result = firstStylist.GetClients();

      //Assert
      Assert.AreEqual(testList.Count, result.Count);
    }

    [TestMethod]
    public void GetClients_ReturnsAllClientStylists_ClientList()
    {
      //Arrange
      Client testClient1 = new Client("Wu");
      testClient1.Save();
      Client testClient2 = new Client("Yu");
      testClient2.Save();

      System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
      Stylist testStylist1 =  new Stylist("Kim", hireDate);
      testStylist1.Save();

      //Act
      testStylist1.AddClient(testClient1);
      testStylist1.AddClient(testClient2);
      List<Client> result = testStylist1.GetClients();
      List<Client> testList = new List<Client> {testClient1, testClient2};

      //Assert
      Assert.AreEqual(testList.Count, result.Count);
      // CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void AddSpecialty_AddsSpecialtyToStylist_SpecialtyList()
    {
      //Arrange
      Specialty testSpecialty1 = new Specialty("Wu");
      testSpecialty1.Save();
      Specialty testSpecialty2 = new Specialty("Yu");
      testSpecialty2.Save();

      System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
      Stylist firstStylist =  new Stylist("Kim", hireDate);
      firstStylist.Save();

      List<Specialty> testList = new List<Specialty>{testSpecialty1, testSpecialty2};

      //Act
      firstStylist.AddSpecialty(testSpecialty1);
      firstStylist.AddSpecialty(testSpecialty2);
      List<Specialty> result = firstStylist.GetSpecialtys();

      //Assert
      Assert.AreEqual(testList.Count, result.Count);
    }

    [TestMethod]
    public void GetSpecialtys_ReturnsAllSpecialtyStylists_SpecialtyList()
    {
      //Arrange
      Specialty testSpecialty1 = new Specialty("Wu");
      testSpecialty1.Save();
      Specialty testSpecialty2 = new Specialty("Yu");
      testSpecialty2.Save();

      System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
      Stylist testStylist1 =  new Stylist("Kim", hireDate);
      testStylist1.Save();

      //Act
      testStylist1.AddSpecialty(testSpecialty1);
      testStylist1.AddSpecialty(testSpecialty2);
      List<Specialty> result = testStylist1.GetSpecialtys();
      List<Specialty> testList = new List<Specialty> {testSpecialty1, testSpecialty2};

      //Assert
      Assert.AreEqual(testList.Count, result.Count);
      // CollectionAssert.AreEqual(testList, result);
    }
  }
}
