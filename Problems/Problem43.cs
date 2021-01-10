using System.Linq;
using Utils;

class Problem43 : ProblemBase
{
  public ulong Solve()
  {
    DecimalPermutations perm = new DecimalPermutations(10);
    byte[][] perms = perm.GetPermutations(10);
    long[] magicNumbers = perms.Where((digits) => IsMagic(digits)).Select((digits) => (long)perm.GetPermutationNumber(digits)).ToArray();
    ulong sum = (ulong)magicNumbers.Sum();
    return sum;
  }

  private bool IsMagic(byte[] d)
  {
    // d[0] - first digit
    if (d[3] % 2 != 0)
      return false;
    if (d[5] % 5 != 0)
      return false;
    if (GetNum(d[7], d[8], d[9]) % 17 != 0)
      return false;
    if (GetNum(d[6], d[7], d[8]) % 13 != 0)
      return false;
    if (GetNum(d[5], d[6], d[7]) % 11 != 0)
      return false;
    if (GetNum(d[4], d[5], d[6]) % 7 != 0)
      return false;
    if (GetNum(d[2], d[3], d[4]) % 3 != 0)
      return false;
    return true;
  }

  private static uint GetNum(byte d2, byte d1, byte d0)
  {
    return (uint)d2 * 100 + (uint)d1 * 10 + (uint)d0;
  }
}
