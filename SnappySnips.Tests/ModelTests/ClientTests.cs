using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SnappySnips.Models;

namespace SnappySnips.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
  {
    public void ClientTests()
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
      int result = Client.GetAll().Count;

      //
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesClientToDatabase_ClientList()
    {
      //Arrange
      string name = "Kim";
      Client newClient = new Client(name);

      //Act
      newClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{newClient};

      //Assert
      CollectionAssert.AreEqual(result, testList);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Client()
    {
      //Arrange
      Client firstClient = new Client("Kim");
      Client secondClient = new Client("Kim");

      //Act
      firstClient.Save();
      secondClient.Save();

      //Assert
      Assert.AreEqual(true, firstClient.GetName().Equals(secondClient.GetName()));
    }

    [TestMethod]
    public void GetAll_ReturnsClients_ClientList()
    {
      //Arrange
      string name1 = "Kim";
      string name2 = "John";
      Client client1 = new Client(name1);
      Client client2 = new Client(name2);
      List<Client> newList = new List<Client> {client1, client2};

      //Act
      client1.Save();
      client2.Save();
      List<Client> result = Client.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToObject_Id()
    {
      //Arrange
      string name = "Kim";
      Client newClient = new Client(name);
      newClient.Save();

      //Act
      Client savedClient = Client.GetAll()[0];
      int result = savedClient.GetId();
      int testId = newClient.GetId();

      //Assert
      Assert.AreEqual(testId, result);

    }

    [TestMethod]
    public void Find_FindsClientInDatabase_Client()
    {
      //Arrange
      Client newClient = new Client("Kim");
      newClient.Save();

      //Act
      Client foundClient = Client.Find(newClient.GetId());

      //Assert
      Assert.AreEqual(true, newClient.Equals(foundClient));
    }

    [TestMethod]
    public void Edit_UpdatesClientName_Client()
    {
      //Arrange
      Client newClient = new Client("Kim");
      newClient.Save();
      //Act
      newClient.Edit("John");
      //Assert
      Assert.AreEqual("John", newClient.GetName());
    }

    [TestMethod]
    public void Delete_DeleteOneClientInDatabase_True()
    {
      //Arrange
      Client firstClient = new Client("Wu");
      Client secondClient = new Client("Yu");
      List<Client> testList = new List<Client>{secondClient};
      firstClient.Save();
      secondClient.Save();

      //Act
      int firstId = firstClient.GetId();
      firstClient.Delete(firstId);
      List<Client> compareList = Client.GetAll();
      Console.WriteLine(compareList.Count);
      //Assert
      Assert.AreEqual(testList.Count, compareList.Count);
    }

    [TestMethod]
      public void AddStylist_AddsStylistToClient_StylistList()
      {
        //Arrange
        Client testClient1 = new Client("Wu");
        testClient1.Save();

        System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
        Stylist firstStylist = new Stylist("Kim", hireDate);
        firstStylist.Save();

        //Act
        testClient1.AddStylist(firstStylist);
        List<Stylist> testList = new List<Stylist>{firstStylist};
        string result = testClient1.GetStylist();

        //Assert
        Assert.AreEqual(testList[0].GetName(), result);
      }

      [TestMethod]
      public void GetStylist_ReturnsAllClientStylists_StylistList()
      {
        //Arrange
        Client testClient1 = new Client("Wu");
        testClient1.Save();

        System.DateTime hireDate = System.DateTime.Parse("03/01/2018");
        Stylist firstStylist = new Stylist("Kim", hireDate);
        firstStylist.Save();

        //Act
        testClient1.AddStylist(firstStylist);
        string result = testClient1.GetStylist();
        List<Stylist> testList = new List<Stylist> {firstStylist};

        //Assert
        Assert.AreEqual(testList[0].GetName(), result);
        // CollectionAssert.AreEqual(testList, result);
      }
  }
}
