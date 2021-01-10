using Utils;

class Problem16 : ProblemBase
{
  private const int DIGITS_COUNT = 302;

  public ulong Solve()
  {
    BigNumber n = new BigNumber(DIGITS_COUNT, 2);
    for (int i = 1; i < 1000; ++i)
    {
      BigNumber m = new BigNumber(n);
      n.Add(m); // n + n = 2 * n
    }
    uint sum = n.GetSumOfDigits();
    return sum;
  }
}
