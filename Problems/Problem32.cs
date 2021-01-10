using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class Problem32 : ProblemBase
{
  public ulong Solve()
  {
    traverseMultipliers(4, 2);
    ulong sum = mProducts.Aggregate((a, b) => a + b);
    return sum;
  }

  private void traverseMultipliers(uint maxLen1, uint maxLen2)
  {
    BitArray digitsUsed = new BitArray(10);
    for (uint len1 = 1; len1 <= maxLen1; ++len1)
      permutateFirst(0, len1, 0, maxLen2, digitsUsed);
  }

  private void permutateFirst(uint digit, uint len1, ulong mult1, uint maxLen2, BitArray digitsUsed)
  {
    if (digit == len1)
    {
      for (uint len2 = 1; len2 <= maxLen2; ++len2)
        permutateSecond(0, len1, len2, mult1, 0, digitsUsed);
      return;
    }
    for (int i = 1; i <= 9; ++i)
    {
      if (digitsUsed[i] == false)
      {
        digitsUsed[i] = true;
        ulong nextMult1 = mult1 * 10 + (uint)i;
        permutateFirst(digit + 1, len1, nextMult1, maxLen2, digitsUsed);
        digitsUsed[i] = false;
      }
    }
  }

  private void permutateSecond(uint digit, uint len1, uint len2, ulong mult1, ulong mult2, BitArray digitsUsed)
  {
    if (digit == len2)
    {
      checkMultiplication(len1, len2, mult1, mult2, digitsUsed);
      return;
    }
    for (int i = 1; i <= 9; ++i)
    {
      if (digitsUsed[i] == false)
      {
        digitsUsed[i] = true;
        ulong nextMult2 = mult2 * 10 + (uint)i;
        permutateSecond(digit + 1, len1, len2, mult1, nextMult2, digitsUsed);
        digitsUsed[i] = false;
      }
    }
  }

  private void checkMultiplication(uint len1, uint len2, ulong mult1, ulong mult2, BitArray digitsUsed)
  {
    ulong prod = mult1 * mult2;
    string strProd = prod.ToString();
    int lenProd = strProd.Length;
    if (len1 + len2 + lenProd == 9)
    {
      if (checkProdDigits(0, lenProd, strProd, digitsUsed))
      {
        mProducts.Add(prod);
        Console.Out.WriteLine("{0} * {1} = {2}", mult1, mult2, prod);
      }
    }
  }

  static bool checkProdDigits(int digit, int len, string strN, BitArray digitsUsed)
  {
    if (digit == len)
      return true;

    int d = (byte)strN[digit] - (byte)'0';
    if (d == 0 || digitsUsed[d])
      return false;

    digitsUsed[d] = true;
    bool result = checkProdDigits(digit + 1, len, strN, digitsUsed);
    digitsUsed[d] = false;

    return result;
  }

  private SortedSet<ulong> mProducts = new SortedSet<ulong>();
}
