using System;
using System.Collections.Generic;

namespace MobileHubClient.Misc
{
    public static class Utilities
    {
         public static IEnumerable<string> SplitByLength(this string value, int maxLength)
        {
            for (int index = 0; index < value.Length; index += maxLength)
                yield return value.Substring(index, Math.Min(maxLength, value.Length - index));
        }
    }
}
