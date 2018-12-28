using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Problems
{
  class Problem26 : ProblemBase
  {
    public uint digitsCount = 1024 * 2;

    public ulong Solve()
    {
      uint maxPeriod = 0;
      uint devider = 0;
      for (uint i = 3; i < 1000; ++i)
      {
        uint period = CalculateFractionPeriod(i, digitsCount);
        if(period > maxPeriod)
        {
          maxPeriod = period;
          devider = i;
        }
      }

      return devider;
    }

    private uint CalculateFractionPeriod(uint devider, uint digitsCount)
    {
      byte[] data = new byte[digitsCount];
      uint index = 0; // digit index
      uint reminder = 1;
      uint periodLenght = 0;

      if (DebugResult)
      {
        Console.Out.Write(reminder);
        Console.Out.Write("/");
        Console.Out.Write(devider);
        Console.Out.WriteLine("=");
      }

      while (reminder != 0 && index < digitsCount)
      {
        byte digit = 0;
        while (reminder >= devider)
        {
          reminder -= devider;
          ++digit;
        }

        data[index] = digit;
        if (DebugResult)
        {
          if (index == 1)
          {
            Console.Out.Write(".");
          }
          Console.Out.Write(digit);
        }

        if (reminder != 0)
        {
          reminder *= 10;
          ++index;
        }
      }

      if (reminder > 0)
      {
        // Search for a period
        uint period = 0;
        uint offset = 0;
        for (offset = 1; offset < digitsCount; ++offset)
        {
          for (period = 1; period < digitsCount / 2; ++period)
          {
            bool isPeriodic = CheckPeriod(data, digitsCount, offset, period);
            if (isPeriodic)
              goto finish;
          }
        }
      finish:
        if (period < digitsCount / 2)
        {
          if (DebugResult)
          {
            Console.Out.WriteLine();
            Console.Out.Write(">");
            for (uint i = 0; i < offset; ++i)
              Console.Out.Write(" ");
            for (uint i = 0; i < period; ++i)
              Console.Out.Write("^");

            Console.Out.Write(" = _" + period + "_    offset = " + offset);
          }
          periodLenght = period;
        }
      }

      if (DebugResult)
      {
        Console.Out.WriteLine();
        if (reminder == 0)
          Console.Out.WriteLine("INTEGRAL, no reminder");
        else
          Console.Out.WriteLine("fractional, reminder = " + reminder);
      }
      return periodLenght;
    }

    private bool CheckPeriod(byte[] digits, uint digitsCount, uint offset, uint period)
    {
      uint count = (digitsCount - offset) / period;
      if (count < 2)
        return false;
      for (uint i = 1; i < count; ++i)
      {
        for (uint j = 0; j < period; ++j)
        {
          uint index = i * period + j + offset;
          if (digits[index] != digits[index - period])
            return false;
        }
      }
      return true;
    }

    private bool DebugResult { get; set; } = false;
  }
}
