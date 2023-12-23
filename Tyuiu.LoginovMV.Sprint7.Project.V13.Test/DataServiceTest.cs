using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tyuiu.LoginovMV.Sprint7.Project.V13.Lib;

namespace Tyuiu.LoginovMV.Sprint7.Project.V13.Test
{
    [TestClass]
    public class DataServiceTest
    {
        DataService ds = new DataService();
        [TestMethod]
        public void TestGetMatrix()
        {
            string path = @"C:\Users\Валерий\source\repos\Tyuiu.LoginovMV.Sprint7\Tyuiu.LoginovMV.Sprint7.Project.V13\bin\Debug\Countries\Австралия.csv";
            string[,] mas = ds.GetMatrix(path);
            string res = mas[0, 0];
            string wait = "Австралия";
            Assert.AreEqual(wait, res);
        }
        [TestMethod]
        public void TestGDPperCapita()
        {
            double x = 100;
            double y = 10;
            double res = ds.GDPperCapita(x, y);
            double wait = 10.0;
            Assert.AreEqual(wait, res);
        }
        
    }
}
