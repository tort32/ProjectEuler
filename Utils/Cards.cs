using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Utils
{
  class Cards
  {
    public struct Card
    {
      public readonly Value value;
      public readonly Suit suit;
      public readonly int index;

      public Card(Value value, Suit suit)
      {
        this.value = value;
        this.suit = suit;
        this.index = (int)suit * (int)Value.MaxValue + (int)value;
        Debug.Assert(index >= 0 && index < 64, "Card index should be less than 64");
      }

      public int GetIndex()
      {
        return this.index;
      }

      public ulong GetMask()
      {
        return (1UL << this.index);
      }

      public static Card Parse(string str)
      {
        if (str.Length != 2)
          throw new ArgumentException("Invalid card alias" + str);
        Debug.Assert(str.Length == 2, "Card alias should consist of 2 chars");
        int idx = (int)SUITS[str[1]] * (int)Value.MaxValue + (int)VALUES[str[0]];
        Card card = DECK[idx];
        return card;
      }

      public override string ToString()
      {
        Value value = this.value;
        Suit suit = this.suit;
        return String.Format("{0}{1}", VALUES.Single((e) => e.Value == value).Key, SUITS.Single((e) => e.Value == suit).Key);
      }
    }

    public struct Set
    {
      public readonly ulong mask;

      private Set(ulong mask)
      {
        this.mask = mask;
      }

      public Set(params Card[] cards)
      {
        Debug.Assert(cards.Length > 0, "Set should be not empty");
        mask = 0;
        foreach (Card card in cards)
        {
          mask |= card.GetMask();
        }
      }

      /*
       * Check this set has all cards from the other set.
       * Example to check the hand has a set of cards.
       */
      public bool Has(Set subset)
      {
        return (this.mask & subset.mask) == subset.mask;
      }

      /*
       * Remove the given subset of cards from this set.
       */
      public Set Remove(Set subset)
      {
        Debug.Assert(this.Has(subset), "Set should contain all cards to remove");
        ulong rest = this.mask & (~subset.mask);
        return new Set(rest);
      }

      /*
       * Remove this set of cards from the given set.
       */
      public Set Filter(Set set)
      {
        return set.Remove(this);
      }

      /*
       * Check the hand has no card from this set
       */
      public bool None(Set hand)
      {
        return (this.mask & hand.mask) == 0;
      }

      public bool IsEmpty()
      {
        return (mask == 0);
      }

      public ulong GetMask()
      {
        return this.mask;
      }

      public static Set Parse(params string[] cardsStr)
      {
        return new Set(cardsStr.Select((str) => Card.Parse(str)).ToArray());
      }

      public override string ToString()
      {
        ulong mask = this.mask;
        var cardsStr = DECK.Where((card) => (mask & card.GetMask()) != 0).Select((card) => card.ToString());
        return String.Join(" ", cardsStr);
      }
    }

    public struct Combination
    {
      public readonly Set[] sets;
      public readonly ulong mask;

      public Combination(params Set[] sets)
      {
        Debug.Assert(sets.Length > 0, "Sets should be not empty");
        this.sets = sets;
        mask = 0;
        foreach (Set set in sets)
        {
          mask |= set.GetMask();
        }
      }

      /*
       * Check this combination has at least one set with all cards from the hand
       */
      public bool Has(Set subset)
      {
        return sets.Any((set) => set.Has(subset));
      }

      /*
       * Check the hand has at least one whole set from the combination
       */
      public bool Fits(Set hand)
      {
        return sets.Any((set) => hand.Has(set));
      }

      /*
       * Remove this set of cards from the given set.
       */
      public Set Filter(Set hand)
      {
        foreach (Set set in sets)
        {
          if(hand.Has(set))
          {
            return hand.Remove(set);
          }
        }
        Debug.Fail("Sets should fits the collection");
        return new Set();
      }

      /*
       * Check hand has no card from the combination
       */
      public bool None(Set hand)
      {
        return (hand.mask & this.mask) == 0;
      }

      /*
       * Parse from values chars and makes a sets of all suits.
       */
      public static Combination Parse(params char[] cardsValues)
      {
        Set[] sets = new Set[4];
        for (int i = 0; i < 4; ++i)
          sets[i] = new Set(cardsValues.Select((ch) => new Card(VALUES[ch], (Suit)i)).ToArray());
        return new Combination(sets);
      }

      public static Combination Parse(int count, char value)
      {
        Value v = VALUES[value];
        Debug.Assert(count >= 1 && count <= 4, "Invalid suits count");
        if (count == 4)
        {
          return new Combination(new Set(SUITS.Select((e) => new Card(v, e.Value)).ToArray()));
        }
        else if(count == 3)
        {
          Set[] sets = new Set[4];
          sets[0] = new Set(new Card(v, Suit.Hearts), new Card(v, Suit.Diamonds), new Card(v, Suit.Clubs));
          sets[1] = new Set(new Card(v, Suit.Hearts), new Card(v, Suit.Diamonds), new Card(v, Suit.Spades));
          sets[2] = new Set(new Card(v, Suit.Hearts), new Card(v, Suit.Clubs), new Card(v, Suit.Spades));
          sets[3] = new Set(new Card(v, Suit.Diamonds), new Card(v, Suit.Clubs), new Card(v, Suit.Spades));
          return new Combination(sets);
        }
        else if (count == 2)
        {
          Set[] sets = new Set[6];
          sets[0] = new Set(new Card(v, Suit.Hearts), new Card(v, Suit.Diamonds));
          sets[1] = new Set(new Card(v, Suit.Hearts), new Card(v, Suit.Clubs));
          sets[2] = new Set(new Card(v, Suit.Hearts), new Card(v, Suit.Spades));
          sets[3] = new Set(new Card(v, Suit.Diamonds), new Card(v, Suit.Clubs));
          sets[4] = new Set(new Card(v, Suit.Diamonds), new Card(v, Suit.Spades));
          sets[5] = new Set(new Card(v, Suit.Clubs), new Card(v, Suit.Spades));
          return new Combination(sets);
        }
        else  if (count == 1)
        {
          return new Combination(SUITS.Select((e) => new Set(new Card(v, e.Value))).ToArray());
        }
        return new Combination();
      }
    }

    public enum Value
    {
      Two,
      Three,
      Four,
      Five,
      Six,
      Seven,
      Eight,
      Nine,
      Ten,
      Jack,
      Queen,
      King,
      Ace,
      MaxValue
    }

    public enum Suit
    {
      Hearts,
      Diamonds,
      Clubs,
      Spades,
      MaxValue
    }

    public static Dictionary<char, Value> VALUES;
    public static Dictionary<char, Suit> SUITS;
    public static Card[] DECK;

    static Cards()
    {
      VALUES = new Dictionary<char, Value>
      {
        { '2', Value.Two },
        { '3', Value.Three },
        { '4', Value.Four },
        { '5', Value.Five },
        { '6', Value.Six },
        { '7', Value.Seven },
        { '8', Value.Eight },
        { '9', Value.Nine },
        { 'T', Value.Ten },
        { 'J', Value.Jack },
        { 'Q', Value.Queen },
        { 'K', Value.King },
        { 'A', Value.Ace }
      };

      SUITS = new Dictionary<char, Suit>
      {
        { 'H', Suit.Hearts },
        { 'D', Suit.Diamonds },
        { 'C', Suit.Clubs },
        { 'S', Suit.Spades }
      };

      int suitsCount = (int)Suit.MaxValue;
      int valuesCount = (int)Value.MaxValue;
      DECK = new Card[suitsCount * valuesCount];
      for (int suitIdx = 0; suitIdx < suitsCount; ++suitIdx)
      {
        for (int valueIdx = 0; valueIdx < valuesCount; ++valueIdx)
        {
          Card card = new Card((Value)valueIdx, (Suit)suitIdx);
          DECK[card.GetIndex()] = card;
        }
      }
    }
  }
}
