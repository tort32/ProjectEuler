using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectEuler.Utils
{
  class Poker {
    public struct Session
    {
      public readonly Cards.Set player1;
      public readonly Cards.Set player2;

      public Session(IEnumerable<Cards.Card> deal)
      {
        this.player1 = new Cards.Set(deal.Take(5).ToArray());
        this.player2 = new Cards.Set(deal.Skip(5).Take(5).ToArray());
      }

      public Session(Cards.Set player1, Cards.Set player2)
      {
        this.player1 = player1;
        this.player2 = player2;
      }

      public override string ToString()
      {
        return String.Format("({0} | {1})", player1, player2);
      }
    }

    public class Hand : IComparable
    {
      protected readonly Cards.Set hand;
      protected readonly bool isFlush;
      protected readonly int straightIdx;
      protected readonly int fourIdx;
      protected readonly int threeIdx;
      protected readonly int[] pairIdx;
      protected readonly int[] highestIdx;

      public Hand(Cards.Set hand)
      {
        this.hand = hand;
        this.straightIdx = -1;
        this.fourIdx = -1;
        this.threeIdx = -1;
        this.pairIdx = new int[2] { -1, -1 };
        this.highestIdx = new int[5] { -1, -1, -1, -1, -1 };
        this.isFlush = FLUSH_INCLUSIVE.Has(hand);
        for (int i = 0; i < STRAIGHT_EXCLUSIVE.Length; ++i)
        {
          if (STRAIGHT_EXCLUSIVE[i].None(hand))
          {
            // Check all cards are differ
            int differCount = SINGLE_KIND.Select((single) => single.Fits(hand)).Count();
            if (differCount == 5)
            {
              this.straightIdx = i;
              return; // Nothing left
            }
          }
        }
        for (int i = 0; i < FOUR_KIND.Length; ++i)
        {
          Cards.Combination four = FOUR_KIND[i];
          if (four.Fits(hand))
          {
            this.fourIdx = i;
            hand = four.Filter(hand);
            break;
          }
        }
        if (this.fourIdx == -1)
        {
          for (int i = 0; i < THREE_KIND.Length; ++i)
          {
            Cards.Combination three = THREE_KIND[i];
            if (three.Fits(hand))
            {
              this.threeIdx = i;
              hand = three.Filter(hand);
              break;
            }
          }
          // Search a pair
          for (int i = 0; i < PAIR_KIND.Length; ++i)
          {
            Cards.Combination pair = PAIR_KIND[i];
            if (pair.Fits(hand))
            {
              this.pairIdx[0] = i;
              hand = pair.Filter(hand);
              break;
            }
          }
          if (this.threeIdx == -1 || this.pairIdx[0] != -1)
          {
            // Search the second pair
            int prevIdx = (this.pairIdx[0] == -1) ? 0 : this.pairIdx[0];
            for (int i = prevIdx; i < PAIR_KIND.Length; ++i)
            {
              Cards.Combination pair = PAIR_KIND[i];
              if (pair.Fits(hand))
              {
                this.pairIdx[1] = i;
                hand = pair.Filter(hand);
                break;
              }
            }
          }
        }
        int singleNumber = 0;
        while (!hand.IsEmpty())
        {
          // Search higest card
          int prevIdx = (singleNumber == 0) ? 0 : this.highestIdx[singleNumber - 1];
          for (int i = prevIdx; i < SINGLE_KIND.Length; ++i)
          {
            Cards.Combination single = SINGLE_KIND[i];
            if (single.Fits(hand))
            {
              this.highestIdx[singleNumber++] = i;
              hand = single.Filter(hand);
              break;
            }
          }
        }
      }

      public bool IsStraightFlush()
      {
        return isFlush && straightIdx != -1;
      }

      public bool IsFlush()
      {
        return isFlush;
      }

      public bool IsStraight()
      {
        return straightIdx != -1;
      }

      public bool HasFour()
      {
        return fourIdx != -1;
      }

      public bool IsFullhouse()
      {
        return threeIdx != -1 && pairIdx[0] != -1;
      }

      public bool HasThree()
      {
        return threeIdx != -1;
      }

      public bool HasTwoPairs()
      {
        return pairIdx[0] != -1 && pairIdx[1] != -1;
      }

      public bool HasPair()
      {
        return pairIdx[0] != -1;
      }

      public Cards.Value GetStraightHighestValue()
      {
        Debug.Assert(straightIdx != -1, "Has no Straight");
        return Cards.Value.Ace - fourIdx;
      }

      public Cards.Value GetFourValue()
      {
        Debug.Assert(fourIdx != -1, "Has no Four");
        return Cards.Value.Ace - fourIdx;
      }

      public Cards.Value GetThreeValue()
      {
        Debug.Assert(threeIdx != -1, "Has no Three");
        return Cards.Value.Ace - threeIdx;
      }

      public Cards.Value GetFirstPairValue()
      {
        Debug.Assert(pairIdx[0] != -1, "Has no Pair");
        return Cards.Value.Ace - pairIdx[0];
      }

      public Cards.Value GetSecondPairValue()
      {
        Debug.Assert(pairIdx[1] != -1, "Has no second Pair");
        return Cards.Value.Ace - pairIdx[1];
      }

      public Cards.Value GetHighestCardValue()
      {
        Debug.Assert(highestIdx[0] != -1);
        return Cards.Value.Ace - highestIdx[0];
      }

      public int StraightCompareTo(Hand other)
      {
        Debug.Assert(this.straightIdx != -1 && other.straightIdx != -1, "Invalid comparision");
        int straightCmp = other.straightIdx - this.straightIdx;
        return straightCmp;
      }

      public int FourCompareTo(Hand other)
      {
        Debug.Assert(this.fourIdx != -1 && other.fourIdx != -1, "Invalid comparision");
        int fourCmp = other.fourIdx - this.fourIdx;
        return fourCmp;
      }

      public int ThreeCompareTo(Hand other)
      {
        Debug.Assert(this.threeIdx != -1 && other.threeIdx != -1, "Invalid comparision");
        int threeCmp = other.threeIdx - this.threeIdx;
        return threeCmp;
      }

      public int FirstPairCompareTo(Hand other)
      {
        Debug.Assert(this.pairIdx[0] != -1 && other.pairIdx[0] != -1, "Invalid comparision");
        int pairCmp = other.pairIdx[0] - this.pairIdx[0];
        return pairCmp;
      }

      public int SecondPairCompareTo(Hand other)
      {
        Debug.Assert(this.pairIdx[1] != -1 && other.pairIdx[1] != -1, "Invalid comparision");
        int pairCmp = other.pairIdx[1] - this.pairIdx[1];
        return pairCmp;
      }

      public int HighestCompareTo(Hand other)
      {
        Debug.Assert(this.highestIdx[0] != -1 && other.highestIdx[0] != -1, "Invalid comparision");
        int highestCmp = other.highestIdx[0] - this.highestIdx[0];
        return highestCmp;
      }

      public int CompareTo(object obj)
      {
        if (obj == null) return 1;
        if (!(obj is Hand other))
          throw new ArgumentException();

        #region Straight Flush + Royal Flush
        // Royal Flush: Ten, Jack, Queen, King, Ace, in same suit.
        // Straight Flush: All cards are consecutive values of same suit.
        if (this.IsStraightFlush() && !other.IsStraightFlush())
          return 1;
        else if (!this.IsStraightFlush() && other.IsStraightFlush())
          return -1;
        else if (this.IsStraightFlush() && other.IsStraightFlush())
          return StraightCompareTo(other);
        #endregion
        #region Four of a Kind
        // Four of a Kind: Four cards of the same value.
        if (this.HasFour() && !other.HasFour())
          return 1;
        else if (!this.HasFour() && other.HasFour())
          return -1;
        else if (this.HasFour() && other.HasFour())
        {
          int fourCmp = FourCompareTo(other);
          if (fourCmp != 0)
            return fourCmp;
          else
            return HighestCompareTo(other);
        }
        #endregion
        #region Full House
        // Full House: Three of a kind and a pair.
        if (this.IsFullhouse() && !other.IsFullhouse())
          return 1;
        else if (!this.IsFullhouse() && other.IsFullhouse())
          return -1;
        else if (this.IsFullhouse() && other.IsFullhouse())
        {
          int threeCmp = ThreeCompareTo(other);
          if (threeCmp != 0)
            return threeCmp;
          else
            return FirstPairCompareTo(other);
        }
        #endregion
        #region Flush
        // Flush: All cards of the same suit.
        if (this.IsFlush() && !other.IsFlush())
          return 1;
        else if (!this.IsFlush() && other.IsFlush())
          return -1;
        else if (this.IsFlush() && other.IsFlush())
        {
          throw new NotImplementedException();
        }
        #endregion
        #region Straight
        // Straight: All cards are consecutive values.
        if (this.IsStraight() && !other.IsStraight())
          return 1;
        else if (!this.IsStraight() && other.IsStraight())
          return -1;
        else if (this.IsStraight() && other.IsStraight())
          return StraightCompareTo(other);
        #endregion
        #region Three of a Kind
        // Three of a Kind: Three cards of the same value.
        if (this.HasThree() && !other.HasThree())
          return 1;
        else if (!this.HasThree() && other.HasThree())
          return -1;
        else if (this.HasThree() && other.HasThree())
        {
          int threeCmp = ThreeCompareTo(other);
          if (threeCmp != 0)
            return threeCmp;
          else
          {
            Debug.Assert(!(HasPair() || other.HasPair()), "Should be handled as Fullhouse");
            return HighestCompareTo(other);
          }
        }
        #endregion
        #region Two Pairs
        // Two Pairs: Two different pairs.
        if (this.HasTwoPairs() && !other.HasTwoPairs())
          return 1;
        else if (!this.HasTwoPairs() && other.HasTwoPairs())
          return -1;
        else if (this.HasTwoPairs() && other.HasTwoPairs())
        {
          int firstPairCmp = FirstPairCompareTo(other);
          if (firstPairCmp != 0)
            return firstPairCmp;
          else
          {
            int secondPairCmp = SecondPairCompareTo(other);
            if (secondPairCmp != 0)
              return secondPairCmp;
            else
              throw new NotImplementedException();
          }
        }
        #endregion
        #region One Pair
        // One Pair: Two cards of the same value.
        if (this.HasPair() && !other.HasPair())
          return 1;
        else if (!this.HasPair() && other.HasPair())
          return -1;
        else if (this.HasPair() && other.HasPair())
        {
          int firstPairCmp = FirstPairCompareTo(other);
          if (firstPairCmp != 0)
            return firstPairCmp;
          else
          {
            Debug.Assert(this.pairIdx[1] == -1 && other.pairIdx[1] == -1, "Should be handled as two pairs");
            int highestCmp = HighestCompareTo(other);
            if (highestCmp != 0)
              return highestCmp;
            else
              throw new NotImplementedException();
          }
        }
        #endregion
        #region High Card
        // High Card: Highest value card.
        int firstHighestCmp = HighestCompareTo(other);
        if (firstHighestCmp != 0)
          return firstHighestCmp;
        else
        {
          Debug.Assert(this.highestIdx[1] != -1 && other.highestIdx[1] != -1, "Invalid comparision");
          int secondHighestCmp = other.highestIdx[1] - this.highestIdx[1];
          if (secondHighestCmp != 0)
            return secondHighestCmp;
          else
            throw new NotImplementedException();
        }
        #endregion
        throw new InvalidOperationException("Unexpected comparision");
      }

      public static bool operator >(Hand hand1, Hand hand2)
      {
        return hand1.CompareTo(hand2) > 0;
      }

      public static bool operator <(Hand hand1, Hand hand2)
      {
        return hand1.CompareTo(hand2) < 0;
      }

      public static Hand Parse(string cardsLine)
      {
        var deal = cardsLine.Split(' ').Select(s => Cards.Card.Parse(s));
        Debug.Assert(deal.Count() == 5, "Pocker hand should contains 5 cards");
        return new Hand(new Cards.Set(deal.ToArray()));
      }

      const int STRAIGHT_COUNT = 9;
      const int VALUES_COUNT = 13;
      const ulong Rank_HighCard = 1; // 13
      const ulong Rank_OnePair = Rank_HighCard * 14; // 13
      const ulong Rank_TwoPairs = Rank_OnePair * 14; // 13
      const ulong Rank_ThreeOfAKind = Rank_TwoPairs * 14; // 13
      const ulong Rank_Straight = Rank_ThreeOfAKind * 14; // 9
      const ulong Rank_Flush = Rank_Straight * 10; // 1
      const ulong Rank_FullHouse = Rank_Flush * 2; // 1
      const ulong Rank_FourOfAKind = Rank_FullHouse * 2; // 13
      const ulong Rank_StraightFlush = Rank_FourOfAKind * 14; // 9
      const ulong Rank_RoyalFlush = Rank_StraightFlush * 10;

      public ulong GetRank()
      {
        ulong rank = 0;
        if (isFlush)
          rank += Rank_Flush;
        if (straightIdx != -1)
          rank += Rank_Straight * (ulong)(STRAIGHT_COUNT - straightIdx);
        if (isFlush && straightIdx != -1)
        {
          if (straightIdx == 0)
            rank += Rank_RoyalFlush;
          else
            rank += Rank_StraightFlush * (ulong)(STRAIGHT_COUNT - straightIdx);
        }
        if (fourIdx != -1)
          rank += Rank_FourOfAKind * (ulong)(VALUES_COUNT - fourIdx);
        if (threeIdx != -1 && pairIdx[0] != -1)
          rank += Rank_FullHouse;
        if (threeIdx != -1)
          rank += Rank_ThreeOfAKind * (ulong)(VALUES_COUNT - threeIdx);
        if (pairIdx[1] != -1)
          rank += Rank_TwoPairs * (ulong)(VALUES_COUNT - pairIdx[1]);
        if (pairIdx[0] != -1)
          rank += Rank_OnePair * (ulong)(VALUES_COUNT - pairIdx[0]);
        if (highestIdx[0] != -1)
          rank += Rank_HighCard * (ulong)(VALUES_COUNT - highestIdx[0]);
        return rank;
      }

      // Flush = All cards of the same suit.
      public static readonly Cards.Combination FLUSH_INCLUSIVE =
        Cards.Combination.Parse('2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A');

      // Straight: All cards are consecutive values.
      public static readonly Cards.Combination[] STRAIGHT_EXCLUSIVE = new Cards.Combination[STRAIGHT_COUNT] {
          Cards.Combination.Parse('2', '3', '4', '5', '6', '7', '8', '9'), // TJQKA
          Cards.Combination.Parse('2', '3', '4', '5', '6', '7', '8', 'A'), // 9TJQK
          Cards.Combination.Parse('2', '3', '4', '5', '6', '7', 'K', 'A'), // 89TJQ
          Cards.Combination.Parse('2', '3', '4', '5', '6', 'Q', 'K', 'A'), // 789TJ
          Cards.Combination.Parse('2', '3', '4', '5', 'J', 'Q', 'K', 'A'), // 6789T
          Cards.Combination.Parse('2', '3', '4', 'T', 'J', 'Q', 'K', 'A'), // 56789
          Cards.Combination.Parse('2', '3', '9', 'T', 'J', 'Q', 'K', 'A'), // 45678
          Cards.Combination.Parse('2', '8', '9', 'T', 'J', 'Q', 'K', 'A'), // 34567
          Cards.Combination.Parse('7', '8', '9', 'T', 'J', 'Q', 'K', 'A'), // 23456
        };

      // Four of a Kind = Four cards of the same value.
      public static readonly Cards.Combination[] FOUR_KIND = new Cards.Combination[VALUES_COUNT] {
          Cards.Combination.Parse(4, 'A'),
          Cards.Combination.Parse(4, 'K'),
          Cards.Combination.Parse(4, 'Q'),
          Cards.Combination.Parse(4, 'J'),
          Cards.Combination.Parse(4, 'T'),
          Cards.Combination.Parse(4, '9'),
          Cards.Combination.Parse(4, '8'),
          Cards.Combination.Parse(4, '7'),
          Cards.Combination.Parse(4, '6'),
          Cards.Combination.Parse(4, '5'),
          Cards.Combination.Parse(4, '4'),
          Cards.Combination.Parse(4, '3'),
          Cards.Combination.Parse(4, '2')
        };

      // Three of a Kind = Three cards of the same value.
      public static readonly Cards.Combination[] THREE_KIND = new Cards.Combination[VALUES_COUNT] {
          Cards.Combination.Parse(3, 'A'),
          Cards.Combination.Parse(3, 'K'),
          Cards.Combination.Parse(3, 'Q'),
          Cards.Combination.Parse(3, 'J'),
          Cards.Combination.Parse(3, 'T'),
          Cards.Combination.Parse(3, '9'),
          Cards.Combination.Parse(3, '8'),
          Cards.Combination.Parse(3, '7'),
          Cards.Combination.Parse(3, '6'),
          Cards.Combination.Parse(3, '5'),
          Cards.Combination.Parse(3, '4'),
          Cards.Combination.Parse(3, '3'),
          Cards.Combination.Parse(3, '2')
        };

      // Two Pairs: Two different pairs.
      // One Pair = Two cards of the same value.
      public static readonly Cards.Combination[] PAIR_KIND = new Cards.Combination[VALUES_COUNT] {
          Cards.Combination.Parse(2, 'A'),
          Cards.Combination.Parse(2, 'K'),
          Cards.Combination.Parse(2, 'Q'),
          Cards.Combination.Parse(2, 'J'),
          Cards.Combination.Parse(2, 'T'),
          Cards.Combination.Parse(2, '9'),
          Cards.Combination.Parse(2, '8'),
          Cards.Combination.Parse(2, '7'),
          Cards.Combination.Parse(2, '6'),
          Cards.Combination.Parse(2, '5'),
          Cards.Combination.Parse(2, '4'),
          Cards.Combination.Parse(2, '3'),
          Cards.Combination.Parse(2, '2')
        };

      // High Card: Highest value card.
      public static readonly Cards.Combination[] SINGLE_KIND = new Cards.Combination[VALUES_COUNT] {
          Cards.Combination.Parse(1, 'A'),
          Cards.Combination.Parse(1, 'K'),
          Cards.Combination.Parse(1, 'Q'),
          Cards.Combination.Parse(1, 'J'),
          Cards.Combination.Parse(1, 'T'),
          Cards.Combination.Parse(1, '9'),
          Cards.Combination.Parse(1, '8'),
          Cards.Combination.Parse(1, '7'),
          Cards.Combination.Parse(1, '6'),
          Cards.Combination.Parse(1, '5'),
          Cards.Combination.Parse(1, '4'),
          Cards.Combination.Parse(1, '3'),
          Cards.Combination.Parse(1, '2')
        };
    }
  }
}
