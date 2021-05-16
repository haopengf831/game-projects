using System;

/// <summary>
/// 线程安全随机数类
/// </summary>
public static class ThreadSafeRandom
{
    [ThreadStatic] private static Random m_Local;

    private static Random GetLocal()
    {
        return m_Local ?? (m_Local = new Random(Guid.NewGuid().GetHashCode()));
    }

    public static int Next()
    {
        return GetLocal().Next();
    }

    public static int Next(int maxValue)
    {
        return GetLocal().Next(maxValue);
    }

    public static int Next(int minValue, int maxValue)
    {
        return GetLocal().Next(minValue, maxValue);
    }

    public static float NextFloat(float minValue, float maxValue)
    {
        return (float) GetLocal().NextDouble() * (maxValue - minValue) + minValue;
    }

    public static double NextDouble(double minValue, double maxValue)
    {
        return GetLocal().NextDouble() * (maxValue - minValue) + minValue;
    }

    public static double NextDouble()
    {
        return GetLocal().NextDouble();
    }

    public static void NextBytes(byte[] buffer)
    {
        GetLocal().NextBytes(buffer);
    }
}