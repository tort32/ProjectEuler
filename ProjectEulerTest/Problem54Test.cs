using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;

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

  [TestMethod]
  public void TestStraightFlush()
  {
    Poker.Hand hand = Poker.Hand.Parse("3C 5C 6C 4C 7C");
    Assert.IsTrue(hand.IsStraightFlush());
    Assert.IsTrue(hand.IsStraight());
    Assert.IsTrue(hand.IsFlush());
    Assert.IsFalse(hand.HasFour());
    Assert.IsFalse(hand.IsFullhouse());
    Assert.IsFalse(hand.HasThree());
    Assert.IsFalse(hand.HasTwoPairs());
    Assert.IsFalse(hand.HasPair());
    Assert.AreEqual(Cards.Value.Seven, hand.GetStraightHighestValue());
  }

  [TestMethod]
  public void TestFourKind()
  {
    Poker.Hand hand = Poker.Hand.Parse("QC QD 3C QH QS");
    Assert.IsFalse(hand.IsStraightFlush());
    Assert.IsFalse(hand.IsStraight());
    Assert.IsFalse(hand.IsFlush());
    Assert.IsTrue(hand.HasFour());
    Assert.IsFalse(hand.IsFullhouse());
    Assert.IsFalse(hand.HasThree());
    Assert.IsFalse(hand.HasTwoPairs());
    Assert.IsFalse(hand.HasPair());
    Assert.AreEqual(Cards.Value.Queen, hand.GetFourValue());
    Assert.AreEqual(Cards.Value.Three, hand.GetHighestCardValue());
  }

  [TestMethod]
  public void TestFullHouse()
  {
    Poker.Hand hand = Poker.Hand.Parse("KC KD 3C KH 3S");
    Assert.IsFalse(hand.IsStraightFlush());
    Assert.IsFalse(hand.IsStraight());
    Assert.IsFalse(hand.IsFlush());
    Assert.IsFalse(hand.HasFour());
    Assert.IsTrue(hand.IsFullhouse());
    Assert.IsTrue(hand.HasThree());
    Assert.IsFalse(hand.HasTwoPairs());
    Assert.IsTrue(hand.HasPair());
    Assert.AreEqual(Cards.Value.King, hand.GetThreeValue());
    Assert.AreEqual(Cards.Value.Three, hand.GetFirstPairValue());
  }

  [TestMethod]
  public void TestFlush()
  {
    Poker.Hand hand = Poker.Hand.Parse("5H JH 3H TH 8H");
    Assert.IsFalse(hand.IsStraightFlush());
    Assert.IsFalse(hand.IsStraight());
    Assert.IsFalse(hand.HasFour());
    Assert.IsFalse(hand.IsFullhouse());
    Assert.IsTrue(hand.IsFlush());
    Assert.IsFalse(hand.HasThree());
    Assert.IsFalse(hand.HasTwoPairs());
    Assert.IsFalse(hand.HasPair());
    Assert.AreEqual(Cards.Value.Jack, hand.GetHighestCardValue());
  }

  [TestMethod]
  public void TestStraight()
  {
    Poker.Hand hand = Poker.Hand.Parse("6C TH 8S 7D 9H");
    Assert.IsFalse(hand.IsStraightFlush());
    Assert.IsTrue(hand.IsStraight());
    Assert.IsFalse(hand.IsFlush());
    Assert.IsFalse(hand.HasFour());
    Assert.IsFalse(hand.IsFullhouse());
    Assert.IsFalse(hand.HasThree());
    Assert.IsFalse(hand.HasTwoPairs());
    Assert.IsFalse(hand.HasPair());
    Assert.AreEqual(Cards.Value.Ten, hand.GetStraightHighestValue());
  }

  [TestMethod]
  public void TestThreeKind()
  {
    Poker.Hand hand = Poker.Hand.Parse("KD 4C 8H 4D 4S");
    Assert.IsFalse(hand.IsStraightFlush());
    Assert.IsFalse(hand.IsStraight());
    Assert.IsFalse(hand.IsFlush());
    Assert.IsFalse(hand.HasFour());
    Assert.IsFalse(hand.IsFullhouse());
    Assert.IsTrue(hand.HasThree());
    Assert.IsFalse(hand.HasTwoPairs());
    Assert.IsFalse(hand.HasPair());
    Assert.AreEqual(Cards.Value.Four, hand.GetThreeValue());
    Assert.AreEqual(Cards.Value.King, hand.GetHighestCardValue());
  }

  [TestMethod]
  public void TestTwoPairs()
  {
    Poker.Hand hand = Poker.Hand.Parse("KD KC 4H 3D 4S");
    Assert.IsFalse(hand.IsStraightFlush());
    Assert.IsFalse(hand.IsStraight());
    Assert.IsFalse(hand.IsFlush());
    Assert.IsFalse(hand.HasFour());
    Assert.IsFalse(hand.IsFullhouse());
    Assert.IsFalse(hand.HasThree());
    Assert.IsTrue(hand.HasTwoPairs());
    Assert.IsTrue(hand.HasPair());
    Assert.AreEqual(Cards.Value.King, hand.GetFirstPairValue());
    Assert.AreEqual(Cards.Value.Four, hand.GetSecondPairValue());
    Assert.AreEqual(Cards.Value.Three, hand.GetHighestCardValue());
  }

  [TestMethod]
  public void TestPair()
  {
    Poker.Hand hand = Poker.Hand.Parse("8D 2C 3H 4D 3S");
    Assert.IsFalse(hand.IsStraightFlush());
    Assert.IsFalse(hand.IsStraight());
    Assert.IsFalse(hand.IsFlush());
    Assert.IsFalse(hand.HasFour());
    Assert.IsFalse(hand.IsFullhouse());
    Assert.IsFalse(hand.HasThree());
    Assert.IsFalse(hand.HasTwoPairs());
    Assert.IsTrue(hand.HasPair());
    Assert.AreEqual(Cards.Value.Three, hand.GetFirstPairValue());
    Assert.AreEqual(Cards.Value.Eight, hand.GetHighestCardValue());
  }

  [TestMethod]
  public void TestHighest()
  {
    Poker.Hand hand = Poker.Hand.Parse("8H 2H 3H 4H JS");
    Assert.IsFalse(hand.IsStraightFlush());
    Assert.IsFalse(hand.IsStraight());
    Assert.IsFalse(hand.IsFlush());
    Assert.IsFalse(hand.HasFour());
    Assert.IsFalse(hand.IsFullhouse());
    Assert.IsFalse(hand.HasThree());
    Assert.IsFalse(hand.HasTwoPairs());
    Assert.IsFalse(hand.HasPair());
    Assert.AreEqual(Cards.Value.Jack, hand.GetHighestCardValue());
  }
}
