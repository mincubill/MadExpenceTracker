﻿using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Server.Model;

namespace MadExpenceTracker.Server.Mapper
{
    public static class ConfigurationMapper
    {
        public static ConfigurationApi MapToApi(Configuration input)
        {
            return new ConfigurationApi()
            {
                SavingsRate = input.SavingsRate,
                AditionalExpencesRate = input.AditionalExpencesRate,
                BaseExpencesRate = input.BaseExpencesRate
            };
        }

        public static Configuration MapToModel(ConfigurationApi input)
        {
            return new Configuration()
            {
                SavingsRate = input.SavingsRate,
                AditionalExpencesRate = input.AditionalExpencesRate,
                BaseExpencesRate = input.BaseExpencesRate
            };
        }
    }
}
