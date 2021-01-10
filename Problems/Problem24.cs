using Utils;

class Problem24 : ProblemBase
{
  public ulong Solve()
  {
    DecimalPermutations perm = new DecimalPermutations(10);
    byte[] numberDigits = perm.GetPermutation(10, 1000000);
    ulong result = perm.GetPermutationNumber(numberDigits);
    return result;
  }
}
