﻿using System;
using WebService.Models;

namespace WebService.Helpers.Attributes
{
    public class AuthLevelAttribute : Attribute
    {
        public const EAuthLevel Default = EAuthLevel.User;

        public EAuthLevel[] AuthLevels { get; set; }
        
        public AuthLevelAttribute(params EAuthLevel[] authLevels)
        {
            AuthLevels = authLevels;
        }
    }
}