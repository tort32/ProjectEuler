using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Problem14 : ProblemBase
{
  const int SEARCH_LIMIT = 1000000;

  Sequence sequence = new Sequence(SEARCH_LIMIT * 10);
  int itemsAdded;
  private int maxQueque;

  public ulong Solve()
  {
    sequence.Put(1, 1);
    Console.WriteLine("N\tadd\tmax");
    for (uint i = 1; i < SEARCH_LIMIT; ++i)
    {
      FindSequenceDepth(i);
      if (i % 100000 == 0)
      {
        Console.WriteLine("{0}\t{1}\t{2}", i, itemsAdded, maxQueque);
        maxQueque = 0;
        itemsAdded = 0;
      }
    }

    ulong number = 0;
    uint maxDepth = 0;
    for (uint i = 1; i < SEARCH_LIMIT; ++i)
    {
      uint depth = sequence.Get(i);
      if (depth > maxDepth)
      {
        maxDepth = depth;
        number = i;
      }
    }
    return number;
  }

  private void FindSequenceDepth(uint i)
  {
    if (sequence.Get(i) != 0)
      return;

    Stack<ulong> queqe = new Stack<ulong>();
    queqe.Push(i);

    do
    {
      ulong current = queqe.Peek();
      ulong next = (current % 2 == 0) ? (current / 2) : (3 * current + 1);

      if (queqe.Contains(next))
        throw new PathTooLongException("Infinite sequence occured");

      uint nextDepth = sequence.Get(next);
      if (nextDepth != 0)
      {
        if (queqe.Count > maxQueque)
          maxQueque = queqe.Count;

        uint depth = nextDepth;
        do
        {
          current = queqe.Pop();
          sequence.Put(current, ++depth);
          ++itemsAdded;
        } while (queqe.Count != 0);
        break;
      }

      queqe.Push(next);
    } while (queqe.Count != 0);
  }

  private class Sequence
  {
    uint[] array;
    SortedDictionary<ulong, uint> tail;

    public Sequence(int arraySize)
    {
      array = new uint[arraySize];
      tail = new SortedDictionary<ulong, uint>();
    }

    public uint Get(ulong i)
    {
      if (i < (ulong)array.Length)
        return array[i];

      uint value;
      return tail.TryGetValue(i, out value) ? value : 0;
    }

    public void Put(ulong i, uint value)
    {
      if (i < (ulong)array.Length)
      {
        array[i] = value;
      }
      else
      {
        tail.Add(i, value);
      }
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      for (int i = 1; i < array.Length; ++i)
      {
        if (array[i] != 0)
          sb.Append(i).Append(", ");
      }
      sb.Append(Environment.NewLine);
      foreach (ulong key in tail.Keys)
      {
        sb.Append(key).Append(", ");
      }
      return sb.ToString();
    }
  }
}
