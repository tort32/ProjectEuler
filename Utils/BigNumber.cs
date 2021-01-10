using System;
using System.Text;

namespace Utils
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

    public BigNumber(int digitLimit)
    {
      digits = new byte[digitLimit];
    }

    public BigNumber(int digitLimit, string strValue)
      : this(digitLimit)
    {
      for (int i = 0; i < strValue.Length; ++i)
      {
        digits[i] = charToDigit(strValue[strValue.Length - 1 - i]);
      }
    }

    public BigNumber(int digitLimit, uint value) : this(digitLimit, Convert.ToString(value)) {}

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
            throw new OverflowException("Value Digits buffer");
          digits[i] -= 10;
          digits[i + 1] += 1;
        }
      }
    }

    public BigNumber Multiply(BigNumber other)
    {
      return Multiply(other, new BigNumber(digits.Length));
    }

    public BigNumber Multiply(BigNumber other, BigNumber store)
    {
      if (digits.Length != store.digits.Length || other.digits.Length != store.digits.Length)
        throw new ArgumentException("Argument should be the same limit");
      store.SetZero();
      int thisLenght = this.GetLenght();
      int otherLenght = other.GetLenght();
      if(digits.Length < thisLenght + otherLenght)
        throw new OverflowException("Value Digits buffer");
      for (int i = 0; i < otherLenght; ++i)
      {
        byte m = other.digits[i];
        for(int j = 0; j < thisLenght; ++j)
        {
          store.digits[i + j] += (byte) (digits[j] * m);
        }
        store.Normalize();
      }
      return store;
    }

    public BigNumber Power(uint power)
    {
      if (power == 0) {
        return new BigNumber(digits.Length, 1);
      }
      else if(power == 1)
      {
        return new BigNumber(this);
      }
      else
      {
        BigNumber mult = new BigNumber(this);
        BigNumber tmp = new BigNumber(digits.Length);
        for (int i = 2; i <= power; ++i)
        {
          mult.Multiply(this, tmp);
          mult.Set(tmp);
        }
        return mult;
      }
    }

    public BigNumber SetZero()
    {
      for (int i = 0; i < digits.Length; ++i)
      {
        digits[i] = 0;
      }
      return this;
    }

    public BigNumber Set(BigNumber other)
    {
      if (digits.Length != other.digits.Length)
        throw new ArgumentException("Argument should be the same limit");
      Array.Copy(other.digits, digits, digits.Length);
      return this;
    }

    public byte GetDigitAt(int i)
    {
      return digits[i];
    }

    public uint GetSumOfDigits()
    {
      uint sum = 0;
      for (int i = 0; i < digits.Length; ++i)
      {
        sum += digits[i];
      }
      return sum;
    }

    private void Normalize()
    {
      for (int i = 0; i < digits.Length; ++i)
      {
        while (digits[i] > 9)
        {
          if (i == digits.Length - 1)
            throw new OverflowException("Value Digits buffer");
          digits[i] -= 10;
          digits[i + 1] += 1;
        }
      }
    }

    public int GetLenght()
    {
      for (int i = digits.Length - 1; i >= 0; --i)
      {
        if (digits[i] != 0)
          return (i + 1);
      }
      return 0;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder(digits.Length);
      for (int i = GetLenght() - 1; i >= 0; --i)
      {
        sb.Append(digitToChar(digits[i]));
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
