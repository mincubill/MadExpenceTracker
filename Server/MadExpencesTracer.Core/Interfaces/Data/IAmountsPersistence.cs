using MadExpenceTracker.Core.Model;

namespace MadExpenceTracker.Core.Persistence
{
    public interface IAmountsPersistence
    {
        IEnumerable<Amounts> GetAmounts();
        Amounts GetAmounts(Guid id);
        Amounts AddAmount(Amount amountToCreate);
    }
}
