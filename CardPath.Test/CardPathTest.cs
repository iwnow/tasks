using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardPath.Test
{
    [TestClass]
    public class CardPathTest
    {
        [TestMethod]
        [Description("Уникальность объектов City")]
        public void TestCity()
        {
            var c1 = new City("city1");
            var c2 = new City("city2");

            Assert.AreNotEqual(c1.Id, c2.Id);
        }

        [TestMethod]
        [Description("Передача пустого параметра в метод сортировки")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PassNullToLinkMethod_ShouldException()
        {
            CardPath.LinkCard(null);
        }

        [TestMethod]
        [Description("Проверка на выброс исключения, если невозможно определить начальный путь")]
        [ExpectedException(typeof(ApplicationException))]
        public void MultipleSameCityHead() {
            var c1 = new CardPath(new City("1"), new City("2"));
            var c2 = new CardPath(new City("2"), new City("3"));
            var c3 = new CardPath(new City("3"), new City("1"));

            CardPath.LinkCard(new List<CardPath>{c1, c2, c3});
        }

        [TestMethod]
        [Description("Проверка корректности алгоритма сортировки карточек")]
        public void CheckSort() {
            var c1 = new CardPath(new City("1"), new City("2"));
            var c2 = new CardPath(new City("2"), new City("3"));
            var c3 = new CardPath(new City("3"), new City("4"));

            var arr = CardPath.LinkCard(new List<CardPath> { c1, c3, c2 }).ToArray();

            Assert.IsTrue(arr[0].Equals(c1));
            Assert.IsTrue(arr[1].Equals(c2));
            Assert.IsTrue(arr[2].Equals(c3));
        }

        [TestMethod]
        [Description("Проверка на выброс исключения, если есть цикл")]
        [ExpectedException(typeof(ApplicationException))]
        public void CheckForCircle() {
            var c1 = new CardPath(new City("1"), new City("2"));
            var c2 = new CardPath(new City("2"), new City("1"));
            var c3 = new CardPath(new City("2"), new City("3"));
            var c4 = new CardPath(new City("3"), new City("4"));
            var c5 = new CardPath(new City("4"), new City("2"));

            CardPath.LinkCard(new List<CardPath> { c1, c2 });
        }
    }
}
