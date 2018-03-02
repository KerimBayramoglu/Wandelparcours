﻿using System.Collections.Generic;

namespace WebService.Models.Bases
{
    public class AUpdater<T>
    {
        public T Value { get; set; }
        public IEnumerable<string> PropertiesToUpdate { get; set; }
    }
}
