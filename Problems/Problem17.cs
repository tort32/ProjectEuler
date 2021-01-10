using System;

class Problem17 : ProblemBase
{
  public ulong Solve()
  {
    uint result = 0;
    for (uint i = 1; i < 1000; ++i)
    {
      result += countLettersLessThousand(i);
    }
    result += countLettersLessDecade(1) + countLettersThousand();
    return result;
  }

  public uint countLettersThousand()
  {
    return 8; // thousand
  }

  public uint countLettersLessThousand(uint x)
  {
    if (x > 999)
      throw new ArgumentException("Should be less then thousand");
    uint hundreds = x / 100;
    uint rest = x % 100;
    uint result = countLettersLessDecade(hundreds);
    uint result2 = countLettersLessHundred(rest);
    if (result != 0)
    {
      result += 7; // hundred
      if (result2 != 0)
      {
        result += 3; // and
        result += result2;
      }
    }
    else
    {
      result = result2;
    }
    return result;
  }

  public uint countLettersLessHundred(uint x)
  {
    if (x > 99)
      throw new ArgumentException("Should be less then hundred");
    uint ones = x % 10;
    uint result = countLettersLessDecade(ones);
    uint decades = x / 10;
    switch (decades)
    {
      case 1:
        switch (ones)
        {
          case 0: // ten
            return 3;
          case 1: // eleven
          case 2: // twelve
            return 6;
          case 5: // fifteen
          case 6: // sixteen
            return 7;
          case 3: // thirteen
          case 4: // fourteen
          case 8: // eighteen
          case 9: // nineteen
            return 8;
          case 7: // seventeen
            return 9;
        }
        break;
      case 2: // twenty
      case 3: // thirty
      case 8: // eighty
      case 9: // ninety
        return 6 + result;
      case 4: // forty
      case 5: // fifty
      case 6: // sixty
        return 5 + result;
      case 7: // seventy
        return 7 + result;
    }
    return result;
  }

  public uint countLettersLessDecade(uint x)
  {
    if (x > 9)
      throw new ArgumentException("Should be less then ten");
    switch (x)
    {
      case 1: // one
      case 2: // two
      case 6: // six
        return 3;
      case 4: // four
      case 5: // five
      case 9: // nine
        return 4;
      case 3: // three
      case 7: // seven
      case 8: // eight
        return 5;
    }
    return 0;
  }
}
