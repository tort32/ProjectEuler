using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectEuler.Utils;

namespace ProjectEulerTest
{
  [TestClass]
  public class Problem12Test
  {
    [TestMethod]
    public void CalculateNumberOfDevidersTest()
    {
      Assert.AreEqual(2u, Dividers.getNumberOfDeviders(3));
      Assert.AreEqual(4u, Dividers.getNumberOfDeviders(6));
      Assert.AreEqual(4u, Dividers.getNumberOfDeviders(10));
      Assert.AreEqual(4u, Dividers.getNumberOfDeviders(15));
      Assert.AreEqual(4u, Dividers.getNumberOfDeviders(21));
      Assert.AreEqual(6u, Dividers.getNumberOfDeviders(28));
      Assert.AreEqual(9u, Dividers.getNumberOfDeviders(36));
      Assert.AreEqual(6u, Dividers.getNumberOfDeviders(45));
      Assert.AreEqual(4u, Dividers.getNumberOfDeviders(55));
      Assert.AreEqual(8u, Dividers.getNumberOfDeviders(66));
      Assert.AreEqual(8u, Dividers.getNumberOfDeviders(78));
      Assert.AreEqual(4u, Dividers.getNumberOfDeviders(91));
      Assert.AreEqual(8u, Dividers.getNumberOfDeviders(105));
      Assert.AreEqual(16u, Dividers.getNumberOfDeviders(120));
      Assert.AreEqual(8u, Dividers.getNumberOfDeviders(136));
      Assert.AreEqual(6u, Dividers.getNumberOfDeviders(153));
      Assert.AreEqual(6u, Dividers.getNumberOfDeviders(171));
      Assert.AreEqual(8u, Dividers.getNumberOfDeviders(190));
      Assert.AreEqual(16u, Dividers.getNumberOfDeviders(210));
    }
  }
}
