﻿using System.Collections.Generic;
using System.Linq;

namespace DatabaseImporter.Helpers.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> This)
            => This == null || !This.Any();
    }
}