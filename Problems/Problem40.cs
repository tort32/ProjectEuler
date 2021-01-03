using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Problems
{
  class Problem40 : ProblemBase
  {
    public ulong Solve()
    {
      int d1 = digitByIndex(1);
      Console.Out.WriteLine("d1 = " + d1);
      int d2 = digitByIndex(10);
      Console.Out.WriteLine("d10 = " + d2);
      int d3 = digitByIndex(100);
      Console.Out.WriteLine("d100 = " + d3);
      int d4 = digitByIndex(1_000);
      Console.Out.WriteLine("d1_000 = " + d4);
      int d5 = digitByIndex(10_000);
      Console.Out.WriteLine("d10_000 = " + d5);
      int d6 = digitByIndex(100_000);
      Console.Out.WriteLine("d100_000 = " + d6);
      int d7 = digitByIndex(1_000_000);
      Console.Out.WriteLine("d1_000_000 = " + d7);
      return (ulong)(d1 * d2 * d3 * d4 * d5 * d6 * d7);
    }

    private int digitByIndex(int index)
    {
      int row = 0;
      int rowOrder = 1;
      int subRowLenght = 1;
      int rowLenght = 1;
      do
      {
        index -= rowLenght;
        rowOrder = (int)Math.Pow(10, row);
        ++row;
        subRowLenght = row * rowOrder;
        rowLenght = 9 * subRowLenght;
      } while (index >= rowLenght);
      int rowIndex = index / subRowLenght; // Index of a decade in the row [0-8]
      index -= rowIndex * subRowLenght;
      int subRowIndex = index / row; // Index of a number in the decade series
      index -= subRowIndex * row; // Index of a digit in the number
      int number = (rowIndex + 1) * rowOrder + subRowIndex;
      string strNumber = number.ToString();
      char chDigit = strNumber[index];
      int digit = (chDigit - '0');
      return digit;
    }
  }
}
