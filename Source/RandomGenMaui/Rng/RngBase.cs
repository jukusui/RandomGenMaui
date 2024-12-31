using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Jukusui.RandomGen.Rng;
internal abstract class RngBase
{
    public abstract void NextBytes(Span<byte> data);

    public T NextT<T>() where T : struct
    {
        Span<byte> data = stackalloc byte[Marshal.SizeOf<T>()];
        NextBytes(data);
        return MemoryMarshal.Read<T>(data);
    }

    /// <summary>
    /// 0以上で1未満の数値を返却する
    /// </summary>
    /// <returns></returns>
    /// <remarks>53bitの精度がある</remarks>
    public double NextDouble()
        => (NextT<ulong>() >> 11) * (1.0 / (1ul << 53));


    // https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/Random.ImplBase.cs#L38
    // NextUInt32/64 algorithms based on https://arxiv.org/pdf/1805.10941.pdf and https://github.com/lemire/fastrange.
    // 0~(maxValue-1) = maxValue種類の値
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal uint NextUInt32(uint maxValue)
    {
        ulong randomProduct = (ulong)maxValue * NextT<uint>();
        uint lowPart = (uint)randomProduct;

        if (lowPart < maxValue)
        {
            uint remainder = (0u - maxValue) % maxValue;

            while (lowPart < remainder)
            {
                randomProduct = (ulong)maxValue * NextT<uint>();
                lowPart = (uint)randomProduct;
            }
        }

        return (uint)(randomProduct >> 32);
    }

    // https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/Random.ImplBase.cs#L38
    // NextUInt32/64 algorithms based on https://arxiv.org/pdf/1805.10941.pdf and https://github.com/lemire/fastrange.
    // 0~(maxValue-1) = maxValue種類の値
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ulong NextUInt64(ulong maxValue)
    {
        ulong randomProduct = Math.BigMul(maxValue, NextT<ulong>(), out ulong lowPart);

        if (lowPart < maxValue)
        {
            ulong remainder = (0ul - maxValue) % maxValue;

            while (lowPart < remainder)
            {
                randomProduct = Math.BigMul(maxValue, NextT<ulong>(), out lowPart);
            }
        }

        return randomProduct;
    }
}
