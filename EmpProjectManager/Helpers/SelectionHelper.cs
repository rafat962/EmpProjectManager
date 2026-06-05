using System;
using System.Collections.Generic;

public static class SelectionHelper
{
    public static Dictionary<int, int> DisplayAndGetMenu<T>(IEnumerable<T> items, Func<T, string> displaySelector, Func<T, int> idSelector)
    {
        var mapping = new Dictionary<int, int>();
        int serial = 1;

        foreach (var item in items)
        {
            Console.WriteLine($"[{serial}] - {displaySelector(item)}");
            mapping.Add(serial, idSelector(item));
            serial++;
        }

        return mapping;
    }
}