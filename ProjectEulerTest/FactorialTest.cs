using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;

[TestClass]
public class FactorialTest
{
  [TestMethod]
  public void TestSequence()
  {
    Assert.AreEqual(1L, Factorial.Get(0));
    long fact = 1L;
    for (int n = 1; n < 20; ++n)
    {
      fact *= n;
      Assert.AreEqual(fact, Factorial.Get(n));
    }
  }
}
