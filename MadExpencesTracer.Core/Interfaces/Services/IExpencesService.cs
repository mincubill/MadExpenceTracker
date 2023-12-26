using MadExpenceTracker.Core.Model;

namespace MadExpenceTracker.Core.Interfaces.Services
{
    public interface IExpencesService
    {
        IEnumerable<Expences> GetAll();
        Expences GetExpences(Guid id);
        Expences GetExpences(bool isActive);
        Expence GetExpence(Guid id);
        Expences Create(Expence expence);
        bool CreateNewMonth();
        Expence Update(Expence expence);
        bool Delete(Guid id);
        bool CloseMonth(string month);
    }
}
