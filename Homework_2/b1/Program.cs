using System;

class Program
{
    static void Main()
    {
        // 打印表头
        Console.WriteLine("{0,-10} {1,-10} {2,-30} {3,-30}", "类型", "字节数", "最小值", "最大值");
        Console.WriteLine(new string('-', 90));  // 画一条分隔线

        // 依次输出各个数据类型的信息
        PrintTypeInfo("sbyte", sizeof(sbyte), sbyte.MinValue, sbyte.MaxValue);
        PrintTypeInfo("byte", sizeof(byte), byte.MinValue, byte.MaxValue);
        PrintTypeInfo("short", sizeof(short), short.MinValue, short.MaxValue);
        PrintTypeInfo("ushort", sizeof(ushort), ushort.MinValue, ushort.MaxValue);
        PrintTypeInfo("int", sizeof(int), int.MinValue, int.MaxValue);
        PrintTypeInfo("uint", sizeof(uint), uint.MinValue, uint.MaxValue);
        PrintTypeInfo("long", sizeof(long), long.MinValue, long.MaxValue);
        PrintTypeInfo("ulong", sizeof(ulong), ulong.MinValue, ulong.MaxValue);
        PrintTypeInfo("float", sizeof(float), float.MinValue, float.MaxValue);
        PrintTypeInfo("double", sizeof(double), double.MinValue, double.MaxValue);
        PrintTypeInfo("decimal", sizeof(decimal), decimal.MinValue, decimal.MaxValue);
    }

    // 辅助方法：格式化输出
    static void PrintTypeInfo<T>(string typeName, int size, T minValue, T maxValue)
    {
        Console.WriteLine("{0,-10} {1,-10} {2,-30} {3,-30}", typeName, size, minValue, maxValue);
    }
}
