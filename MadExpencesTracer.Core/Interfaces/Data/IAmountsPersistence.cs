using MadExpenceTracker.Core.Model;

namespace MadExpenceTracker.Core.Persistence
{
    public interface IAmountsPersistence
    {
        IEnumerable<Amounts> GetAmounts();
        Amounts AddAmount(Amount amount);
        Amounts GetAmounts(Guid id);
    }
}
