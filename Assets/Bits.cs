using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bits
{
    uint [] data;
    static int [] count;

    uint BitPattern(int bit)
    {
        return 0x1U << bit;
    }

    public Bits(int count, bool state)
    {
        int size = count / 32 + 1;
        int remainder = count % 32;
        data = new uint [size];
        int i;
        for (i = 0; i < size - 1; ++i)
        {
            data[i] = state? 0xFFFFFFFF : 0x0;
        }
        data[i] = (state? 0xFFFFFFFF : 0x0) & (BitPattern(remainder) - 1);
    }

    public int Intersect(Bits intersect)
    {
        int size = data.Length;
        int removed = 0;
        for (int i = 0; i < size; ++i)
        {
            uint mine = data[i];
            uint theirs = intersect.data[i];
            uint changes = mine & (mine ^ theirs);
            removed += CountBits(changes);
            data[i] = mine & theirs;
        }
        return removed;
    }

    void CreateBitPatternLookup()
    {
        count = new int [256];
        for (int i = 0; i < 256; ++i)
        {
            int bits = 0;
            for (int bit = 0; bit < 8; ++bit)
            {
                uint pattern = (uint)i;
                if (TestBit(pattern, bit))
                {
                    bits++;
                }
            }
            count[i] = bits;
        }
    }

    int CountBits(uint pattern)
    {
        if (count == null)
        {
            CreateBitPatternLookup();
        }

        int bits = 0;
        bits += count[pattern & 0xFFU];
        bits += count[(pattern >> 8) & 0xFFU];
        bits += count[(pattern >> 16) & 0xFFU];
        bits += count[(pattern >> 24) & 0xFFU];
        return bits;
    }

    bool TestBit(uint pattern, int bit)
    {
        return ((pattern >> bit) & 0x1) != 0;
    }

    uint SetBit(uint pattern, int bit)
    {
        return pattern | BitPattern(bit);
    }

    public bool SetBit(int index)
    {
        bool changed = false;
        int pos = index / 32;
        int bit = index % 32;

        if (!TestBit(data[pos], bit))
        {
            data[pos] = SetBit(data[pos], bit);
            changed = true;
        }
        return changed;
    }
}
