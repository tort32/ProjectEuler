using ProjectEuler;
using ProjectEuler.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal class Problem24 : ProblemBase
{
    private byte[][][] table = new byte[10][][];

    public ulong Solve()
    {
        fillPermutationTable(10);
        ulong result = getPermutation(10, 1000000);

        return result;
    }

    private void fillPermutationTable(int n)
    {
        if (n == 1)
        {
            table[0] = new byte[1][];
            table[0][0] = new byte[1];
            return;
        }

        if (table[n - 2] == null)
        {
            fillPermutationTable(n - 1);
        }
        long permutCount = Factorial.Get(n);
        long permutCountPrev = Factorial.Get(n - 1);
        table[n - 1] = new byte[permutCount][];
        for (int l = 0; l < permutCount; l++)
        {
            table[n - 1][l] = new byte[n];
        }
        for (int k = 0; k < n; k++)
        {
            for (int j = 0; j < permutCountPrev; j++)
            {
                table[n - 1][k * permutCountPrev + j][0] = (byte)k;
                for (int i = 0; i < n - 1; i++)
                {
                    int permutIndexPrev = table[n - 2][j][i];
                    int permutIndex = permutIndexPrev + ((permutIndexPrev >= k) ? 1 : 0);
                    table[n - 1][k * permutCountPrev + j][i + 1] = (byte)permutIndex;
                }
            }
        }
    }

    private void printAllPermutations(int n)
    {
        fillPermutationTable(n);
        int permutCount = table[n - 1].Count();
        List<string> res = new List<string>();
        for (int j = 0; j < permutCount; j++)
        {
            StringBuilder sb = new StringBuilder(n);
            for (int i = 0; i < n; i++)
            {
                char ch = (char)(table[n - 1][j][i] + (byte)'0');
                sb.Append(ch);
            }
            res.Add(sb.ToString());
        }
        Console.WriteLine(string.Join(", ", res));
    }

    private ulong getPermutation(int n, int number)
    {
        byte[] permut = table[n - 1][number - 1];
        StringBuilder sb = new StringBuilder(n);
        for (int i = 0; i < n; i++)
        {
            char ch = (char)(permut[i] + 48);
            sb.Append(ch);
        }
        string str = sb.ToString();
        ulong res = ulong.Parse(str);
        return res;
    }
}
