using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Utils
{
  /*
   * Lexicographic permutations table:
   * [0] = {0}
   * [1] = {01, 10}
   * [2] = {012, 021, 102, 120, 201, 210}
   * [3] = {0123, 0132, 0213, 0231, 0312, 0321, ... , 3012, 3021, 3102, 3120, 3201, 3210}
   * ...
   * [9] = {0123456789, ... , 9876543210} for 10 digits
   */
  class DecimalPermutations
  {
    private readonly byte[][][] table;

    public DecimalPermutations(int digitsCount)
    {
      table = new byte[digitsCount][][];
      FillPermutationTable(digitsCount);
    }

    private void FillPermutationTable(int n)
    {
      if (n == 1)
      {
        table[0] = new byte[1][];
        table[0][0] = new byte[1];
        return;
      }

      if (table[n - 2] == null)
      {
        FillPermutationTable(n - 1);
      }
      long permutCount = Factorial.Get(n);
      long permutCountPrev = Factorial.Get(n - 1);
      table[n - 1] = new byte[permutCount][];
      for (int l = 0; l < permutCount; l++)
      {
        table[n - 1][l] = new byte[n];
      }
      for (int k = 0; k < n; k++)
      {
        for (int j = 0; j < permutCountPrev; j++)
        {
          table[n - 1][k * permutCountPrev + j][0] = (byte)k;
          for (int i = 0; i < n - 1; i++)
          {
            int permutIndexPrev = table[n - 2][j][i];
            int permutIndex = permutIndexPrev + ((permutIndexPrev >= k) ? 1 : 0);
            table[n - 1][k * permutCountPrev + j][i + 1] = (byte)permutIndex;
          }
        }
      }
    }

    public void PrintPermutations()
    {
      int n = table.Length;
      int permutCount = table[n - 1].Count();
      List<string> res = new List<string>();
      for (int j = 0; j < permutCount; j++)
      {
        StringBuilder sb = new StringBuilder(n);
        for (int i = 0; i < n; i++)
        {
          char ch = (char)(table[n - 1][j][i] + (byte)'0');
          sb.Append(ch);
        }
        res.Add(sb.ToString());
      }
      Console.WriteLine(string.Join(", ", res));
    }

    /*
     * Returns all n-digit permutations
     */
    public byte[][] GetPermutations(int n)
    {
      return table[n - 1];
    }

    /*
     * Returns n-digit permutation from lexicographic ordered list by positional index (one-based).
     */
    public byte[] GetPermutation(int n, int number)
    {
      return GetPermutations(n)[number - 1];
    }

    public ulong GetPermutationNumber(byte[] numberDigits)
    {
      ulong res = 0;
      for (int i = 0; i < numberDigits.Length; i++)
      {
        res = res * 10 + numberDigits[i];
      }
      return res;
    }
  }
}
