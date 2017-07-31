using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ProjectEuler.Problems;

namespace ProjectEuler.Utils
{
  class BigNumber
  {
    private byte[] digits;

    public BigNumber(BigNumber other)
    {
      digits = new byte[other.digits.Length];
      for (int i = 0; i < digits.Length; ++i)
      {
        digits[i] = other.digits[i];
      }
    }

    public BigNumber(uint digitLimit)
    {
      digits = new byte[digitLimit];
    }

    public BigNumber(uint digitLimit, string strValue)
      : this(digitLimit)
    {
      for (int i = 0; i < strValue.Length; ++i)
      {
        digits[i] = charToDigit(strValue[strValue.Length - 1 - i]);
      }
    }

    public BigNumber(uint digitLimit, uint value) : this(digitLimit, Convert.ToString(value)) {}

    public void Add(BigNumber other)
    {
      if (digits.Length != other.digits.Length)
        throw new ArgumentException("Argument should be the same limit");
      for (int i = 0; i < digits.Length; ++i)
      {
        digits[i] += other.digits[i];
        if (digits[i] > 9)
        {
          if (i == digits.Length - 1)
            throw new InternalBufferOverflowException("Value Digits buffer");
          digits[i] -= 10;
          digits[i + 1] += 1;
        }
      }
    }

    public byte GetDigitAt(int i)
    {
      return digits[i];
    }

    private void Normalize()
    {
      for (int i = 0; i < digits.Length; ++i)
      {
        while (digits[i] > 9)
        {
          if (i == digits.Length - 1)
            throw new InternalBufferOverflowException("Value Digits buffer");
          digits[i] -= 10;
          digits[i] += 1;
        }
      }
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder(digits.Length);
      for (int i = 0; i < digits.Length; ++i)
      {
        sb.Append(digitToChar(digits[digits.Length - 1 - i]));
      }
      return sb.ToString();
    }

    private byte charToDigit(char ch)
    {
      int d = (byte) ch - (byte) '0';
      if (d < 0 || d > 9)
        throw new ArgumentException("Char should represent a decimal digit");
      return (byte) d;
    }

    private char digitToChar(byte digit)
    {
      if (digit > 9)
        throw new ArgumentException("Digit value should be decimal ranged");
      char c = (char) (digit + (byte) '0');
      return c;
    }
  }
}
