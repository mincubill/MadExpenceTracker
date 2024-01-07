using MadExpenceTracker.Core.Model;

namespace MadExpenceTracker.Core.Interfaces.Services
{
    public interface IAmountsService
    {
        Amount GetAmount(Guid expencesId, Guid incomesId);
        Amount GetAmount(Guid id);
        Amounts GetAmounts();
        Amounts Create(Amount amount);
    }
}
