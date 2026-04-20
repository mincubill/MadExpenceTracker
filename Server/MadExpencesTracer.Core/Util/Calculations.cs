using MadExpenceTracker.Core.Exceptions;
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
            long totalSavings = expences.Expence.Where(e => e.ExpenceType == ExpenceType.Saving).Sum(e => e.Amount);
            long totalIncomes = incomes.Income.Sum(i => i.Amount);
            byte savingRate = configuration.SavingsRate;
            byte baseRate = configuration.BaseExpencesRate;
            byte aditionalRate = configuration.AditionalExpencesRate;
            long sugestedBaseExpences = Convert.ToInt64((totalIncomes - totalSavings) * (baseRate / 100f));
            long sugestedAditionalExpences = Convert.ToInt64((totalIncomes - totalSavings) * (aditionalRate / 100f));
            long sugestedSaving = Convert.ToInt64(totalIncomes * (savingRate / 100f));
            long remainingBaseExpences = sugestedBaseExpences - totalBaseExpences;
            long remainingAdditionalExpences = 
                CalculateRemainingAditionalExpences( sugestedBaseExpences, 
                    totalBaseExpences, 
                    sugestedAditionalExpences, 
                    totalAditionalExpences, 
                    sugestedSaving);
            long remainingSaving = sugestedSaving;
            return new Amount
            {
                Id = Guid.NewGuid(),
                TotalBaseExpences = totalBaseExpences,
                TotalAditionalExpences = totalAditionalExpences,
                TotalIncomes = totalIncomes,
                TotalSavings = totalSavings,
                SugestedSavings = remainingSaving,
                SugestedBaseExpences = sugestedBaseExpences,
                SugestedAditionalExpences = sugestedAditionalExpences,
                // TODO: hacer que que si el utilizado es mayor haga descuento de la siguiente seccion
                RemainingBaseExpences = remainingBaseExpences,
                RemainingAditionalExpences = remainingAdditionalExpences,
            };
        }

        private static long CalculateRemainingAditionalExpences( long sugestedBaseExpences, 
                                                                 long totalBaseExpences, 
                                                                 long sugestedAditionalExpences, 
                                                                 long totalAditionalExpences, 
                                                                 long sugestedSaving)
        {
            if(sugestedBaseExpences - totalBaseExpences < 0)
            {
                long exceedingAmount = (sugestedBaseExpences - totalBaseExpences) -
                                       (totalAditionalExpences - sugestedAditionalExpences);
                return exceedingAmount;
            }
            return sugestedAditionalExpences - totalAditionalExpences;
        }
    }
}
