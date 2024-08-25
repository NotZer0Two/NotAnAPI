using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotAnAPI.API.Extensions
{
    public static class HashsetExtension
    {

        public static bool TryGetValue<T>(this HashSet<T> set, T equalValue, out T actualValue)
        {
            if (set.Contains(equalValue))
            {
                actualValue = equalValue;
                return true;
            }
            actualValue = default;
            return false;
        }

    }
}
