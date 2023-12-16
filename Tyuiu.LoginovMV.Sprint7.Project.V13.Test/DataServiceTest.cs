using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tyuiu.LoginovMV.Sprint7.Project.V13.Lib;

namespace Tyuiu.LoginovMV.Sprint7.Project.V13.Test
{
    [TestClass]
    public class DataServiceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var manager = new CountryManager();
            manager.LoadDataFromCSV("countries.csv");

            manager.SaveDataToCSV("countries_updated.csv");

            manager.AddCountry("Россия", "Москва", 146599183);

            manager.RemoveCountry("Франция");

            var info = manager.GetCountryInfo("Германия");
            Console.WriteLine(info);
        }
    }
}
