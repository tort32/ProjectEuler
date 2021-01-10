using Utils;
class Problem20 : ProblemBase
{
  private const int DIGITS_COUNT = 160;

  public ulong Solve()
  {
    BigNumber n = new BigNumber(DIGITS_COUNT, 1);
    for (int i = 2; i <= 100; ++i)
    {
      BigNumber m = new BigNumber(n);
      for (int j = 1; j < i; ++j)
      {
        n.Add(m);
      }
    }
    uint sum = n.GetSumOfDigits();
    return sum;
  }
}
