using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ProjectEuler.Utils;

namespace ProjectEuler.Problems
{
  class Problem54 : ProblemBase
  {
    public ulong Solve()
    {
      ulong wins = 0;
      foreach (Poker.Session s in GetSessions())
      {
        Poker.Hand hand1 = new Poker.Hand(s.player1);
        Poker.Hand hand2 = new Poker.Hand(s.player2);
        //if (hand1.getRank() > hand2.getRank())
        //  ++wins;
        try
        {
          int score = hand1.CompareTo(hand2);
          Debug.Assert(score != 0, "Tie session" + s);
          if (score > 0)
            ++wins;
        }
        catch (NotImplementedException e)
        {
          throw new ApplicationException("Unresolved session" + s, e);
        }
      }
      return wins;
    }

    private static ulong GetRank(Cards.Set hand)
    {
      Poker.Hand pocker = new Poker.Hand(hand);
      return pocker.GetRank();
    }

    private IEnumerable<Poker.Session> GetSessions()
    {
      Assembly assembly = Assembly.GetExecutingAssembly();
      using (Stream stream = assembly.GetManifestResourceStream("ProjectEuler.Resources.p054_poker.txt"))
      {
        using (StreamReader reader = new StreamReader(stream))
        {
          string result = reader.ReadToEnd();
          foreach (string line in result.Split('\n'))
          {
            if (line.Equals(String.Empty))
              continue;
            var deal = line.Split(' ').Select(s => Cards.Card.Parse(s));
            yield return new Poker.Session(deal);
          }
        }
      }
    }
  }
}
