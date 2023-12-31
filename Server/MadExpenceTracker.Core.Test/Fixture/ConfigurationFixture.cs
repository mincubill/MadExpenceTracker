﻿using MadExpenceTracker.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.Test.Fixture
{
    public static class ConfigurationFixture
    {
        public static Configuration GetConfiguration()
        {
            return new Configuration() 
            { 
                SavingsRate = 20,
                AditionalExpencesRate = 30,
                BaseExpencesRate = 50
            };
        }
    }
}
