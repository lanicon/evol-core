﻿using System;
using Evol.Common;

namespace Evol.Cinema.Domain.Models.AggregateRoots
{
    /// <summary>
    /// 演员
    /// </summary>
    public class Actor : BaseEntity
    {
        public string Name { get; set; }

        public string ImagePath { get; set; }
    }
}
