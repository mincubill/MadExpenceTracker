﻿using MadExpenceTracker.Core.Exceptions;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.Util
{
    public static class Calculations
    {
        public static Amount CalculateAmounts(Expences expences, Incomes incomes, Configuration configuration)
        {
            long totalBaseExpences = expences.Expence.Where(e => e.ExpenceType == ExpenceType.Base).Sum(e => e.Amount);
            long totalAditionalExpences = expences.Expence.Where(e => e.ExpenceType == ExpenceType.Aditional).Sum(e => e.Amount);
            long totalIncomes = incomes.Income.Sum(i => i.Amount);
            byte savingRate = configuration.SavingsRate;
            byte baseRate = configuration.BaseExpencesRate;
            byte aditionalRate = configuration.AditionalExpencesRate;
            return new Amount
            {
                Id = Guid.NewGuid(),
                TotalBaseExpences = totalBaseExpences,
                TotalAditionalExpences = totalAditionalExpences,
                TotalIncomes = totalIncomes,
                Savings = Convert.ToInt64(totalIncomes * (savingRate / 100f)),
                SugestedBaseExpences = Convert.ToInt64(totalIncomes * (baseRate / 100f)),
                SugestedAditionalExpences = Convert.ToInt64(totalIncomes * (aditionalRate / 100f)),
            };
        }
    }
}