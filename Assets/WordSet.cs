using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSet
{
    uint [] set;
    int count;

    uint BitPattern(int bit)
    {
        return 0x1U << bit;
    }

    public WordSet(int count, bool state)
    {
        int size = count / 32 + 1;
        int remainder = count % 32;
        set = new uint [size];
        int i;
        for (i = 0; i < size - 1; ++i)
        {
            set[i] = state? 0xFFFFFFFF : 0x0;
        }
        set[i] = (state? 0xFFFFFFFF : 0x0) & (BitPattern(remainder) - 1);
        this.count = state? count : 0;
    }

    public void Intersect(WordSet intersect)
    {
        int size = set.Length;
        for (int i = 0; i < size; ++i)
        {
            uint removed = (set[i] ^ intersect.set[i]) & set[i];
            if (removed > 0)
            {
                count -= CountBits(removed);
            }
            set[i] = set[i] & intersect.set[i];
        }
    }

    bool TestBit(uint pattern, int bit)
    {
        return ((pattern >> bit) & 0x1) != 0;
    }

    uint SetBit(uint pattern, int bit)
    {
        return pattern | BitPattern(bit);
    }

    public void Add(int index)
    {
        int pos = index / 32;
        int bit = index % 32;

        if (!TestBit(set[pos], bit))
        {
            set[pos] = SetBit(set[pos], bit);
            count++;
        }
    }

    int CountBits(uint pattern)
    {
        int bits = 0;
        for (int bit = 0; bit < 32; ++bit)
        {
            if (TestBit(pattern, bit))
            {
                bits++;
            }
        }
        return bits;
    }

    public int Count()
    {
        return count;
    }
}
