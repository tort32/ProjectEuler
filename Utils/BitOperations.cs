namespace Utils
{
  class BitOperations
  {
    /*
     * Software implementation from .NET sources
     */
    public static int PopCount(ulong value)
    {
      const ulong c1 = 0x_55555555_55555555ul;
      const ulong c2 = 0x_33333333_33333333ul;
      const ulong c3 = 0x_0F0F0F0F_0F0F0F0Ful;
      const ulong c4 = 0x_01010101_01010101ul;

      value -= (value >> 1) & c1;
      value = (value & c2) + ((value >> 2) & c2);
      value = (((value + (value >> 4)) & c3) * c4) >> 56;

      return (int)value;
    }
  }
}
