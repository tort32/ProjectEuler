using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;

[TestClass]
public class Problem12Test
{
  [TestMethod]
  public void CalculateNumberOfDevidersTest()
  {
    Assert.AreEqual(2u, Dividers.GetNumberOfDeviders(3));
    Assert.AreEqual(4u, Dividers.GetNumberOfDeviders(6));
    Assert.AreEqual(4u, Dividers.GetNumberOfDeviders(10));
    Assert.AreEqual(4u, Dividers.GetNumberOfDeviders(15));
    Assert.AreEqual(4u, Dividers.GetNumberOfDeviders(21));
    Assert.AreEqual(6u, Dividers.GetNumberOfDeviders(28));
    Assert.AreEqual(9u, Dividers.GetNumberOfDeviders(36));
    Assert.AreEqual(6u, Dividers.GetNumberOfDeviders(45));
    Assert.AreEqual(4u, Dividers.GetNumberOfDeviders(55));
    Assert.AreEqual(8u, Dividers.GetNumberOfDeviders(66));
    Assert.AreEqual(8u, Dividers.GetNumberOfDeviders(78));
    Assert.AreEqual(4u, Dividers.GetNumberOfDeviders(91));
    Assert.AreEqual(8u, Dividers.GetNumberOfDeviders(105));
    Assert.AreEqual(16u, Dividers.GetNumberOfDeviders(120));
    Assert.AreEqual(8u, Dividers.GetNumberOfDeviders(136));
    Assert.AreEqual(6u, Dividers.GetNumberOfDeviders(153));
    Assert.AreEqual(6u, Dividers.GetNumberOfDeviders(171));
    Assert.AreEqual(8u, Dividers.GetNumberOfDeviders(190));
    Assert.AreEqual(16u, Dividers.GetNumberOfDeviders(210));
  }
}
