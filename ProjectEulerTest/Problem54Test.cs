using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectEuler.Utils;

namespace ProjectEulerTest
{
  [TestClass]
  public class Problem54Test
  {

    [TestMethod]
    public void Sample1()
    {
      Poker.Hand hand1 = Poker.Hand.Parse("5H 5C 6S 7S KD");
      Assert.IsFalse(hand1.IsStraightFlush());
      Assert.IsFalse(hand1.HasFour());
      Assert.IsFalse(hand1.IsFullhouse());
      Assert.IsFalse(hand1.IsFlush());
      Assert.IsFalse(hand1.IsStraight());
      Assert.IsFalse(hand1.HasThree());
      Assert.IsFalse(hand1.HasTwoPairs());
      Assert.IsTrue(hand1.HasPair());
      Assert.AreEqual(Cards.Value.Five, hand1.GetFirstPairValue());

      Poker.Hand hand2 = Poker.Hand.Parse("2C 3S 8S 8D TD");
      Assert.IsFalse(hand2.IsStraightFlush());
      Assert.IsFalse(hand2.HasFour());
      Assert.IsFalse(hand2.IsFullhouse());
      Assert.IsFalse(hand2.IsFlush());
      Assert.IsFalse(hand2.IsStraight());
      Assert.IsFalse(hand2.HasThree());
      Assert.IsFalse(hand2.HasTwoPairs());
      Assert.IsTrue(hand2.HasPair());
      Assert.AreEqual(Cards.Value.Eight, hand2.GetFirstPairValue());

      Assert.IsTrue(hand2 > hand1);
    }

    [TestMethod]
    public void Sample2()
    {
      Poker.Hand hand1 = Poker.Hand.Parse("5D 8C 9S JS AC");
      Assert.IsFalse(hand1.IsStraightFlush());
      Assert.IsFalse(hand1.HasFour());
      Assert.IsFalse(hand1.IsFullhouse());
      Assert.IsFalse(hand1.IsFlush());
      Assert.IsFalse(hand1.IsStraight());
      Assert.IsFalse(hand1.HasThree());
      Assert.IsFalse(hand1.HasTwoPairs());
      Assert.IsFalse(hand1.HasPair());
      Assert.AreEqual(Cards.Value.Ace, hand1.GetHighestCardValue());

      Poker.Hand hand2 = Poker.Hand.Parse("2C 5C 7D 8S QH");
      Assert.IsFalse(hand2.IsStraightFlush());
      Assert.IsFalse(hand2.HasFour());
      Assert.IsFalse(hand2.IsFullhouse());
      Assert.IsFalse(hand2.IsFlush());
      Assert.IsFalse(hand2.IsStraight());
      Assert.IsFalse(hand2.HasThree());
      Assert.IsFalse(hand2.HasTwoPairs());
      Assert.IsFalse(hand2.HasPair());
      Assert.AreEqual(Cards.Value.Queen, hand2.GetHighestCardValue());

      Assert.IsTrue(hand1 > hand2);
    }

    [TestMethod]
    public void Sample3()
    {
      Poker.Hand hand1 = Poker.Hand.Parse("2D 9C AS AH AC");
      Assert.IsFalse(hand1.IsStraightFlush());
      Assert.IsFalse(hand1.HasFour());
      Assert.IsFalse(hand1.IsFullhouse());
      Assert.IsFalse(hand1.IsFlush());
      Assert.IsFalse(hand1.IsStraight());
      Assert.IsTrue(hand1.HasThree());
      Assert.AreEqual(Cards.Value.Ace, hand1.GetThreeValue());

      Poker.Hand hand2 = Poker.Hand.Parse("3D 6D 7D TD QD");
      Assert.IsFalse(hand2.IsStraightFlush());
      Assert.IsFalse(hand2.HasFour());
      Assert.IsFalse(hand2.IsFullhouse());
      Assert.IsTrue(hand2.IsFlush());

      Assert.IsTrue(hand2 > hand1);
    }

    [TestMethod]
    public void Sample4()
    {
      Poker.Hand hand1 = Poker.Hand.Parse("4D 6S 9H QH QC");
      Assert.IsFalse(hand1.IsStraightFlush());
      Assert.IsFalse(hand1.HasFour());
      Assert.IsFalse(hand1.IsFullhouse());
      Assert.IsFalse(hand1.IsFlush());
      Assert.IsFalse(hand1.IsStraight());
      Assert.IsFalse(hand1.HasThree());
      Assert.IsFalse(hand1.HasTwoPairs());
      Assert.IsTrue(hand1.HasPair());
      Assert.AreEqual(Cards.Value.Queen, hand1.GetFirstPairValue());
      Assert.AreEqual(Cards.Value.Nine, hand1.GetHighestCardValue());

      Poker.Hand hand2 = Poker.Hand.Parse("3D 6D 7H QD QS");
      Assert.IsFalse(hand2.IsStraightFlush());
      Assert.IsFalse(hand2.HasFour());
      Assert.IsFalse(hand2.IsFullhouse());
      Assert.IsFalse(hand2.IsFlush());
      Assert.IsFalse(hand2.IsStraight());
      Assert.IsFalse(hand2.HasThree());
      Assert.IsFalse(hand2.HasTwoPairs());
      Assert.IsTrue(hand2.HasPair());
      Assert.AreEqual(Cards.Value.Queen, hand2.GetFirstPairValue());
      Assert.AreEqual(Cards.Value.Seven, hand2.GetHighestCardValue());

      Assert.IsTrue(hand1 > hand2);
    }

    [TestMethod]
    public void Sample5()
    {
      Poker.Hand hand1 = Poker.Hand.Parse("2H 2D 4C 4D 4S");
      Assert.IsFalse(hand1.IsStraightFlush());
      Assert.IsFalse(hand1.HasFour());
      Assert.IsTrue(hand1.IsFullhouse());
      Assert.AreEqual(Cards.Value.Four, hand1.GetThreeValue());

      Poker.Hand hand2 = Poker.Hand.Parse("3C 3D 3S 9S 9D");
      Assert.IsFalse(hand2.IsStraightFlush());
      Assert.IsFalse(hand2.HasFour());
      Assert.IsTrue(hand2.IsFullhouse());
      Assert.AreEqual(Cards.Value.Three, hand2.GetThreeValue());

      Assert.IsTrue(hand1 > hand2);
    }
  }
}
