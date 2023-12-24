using MadExpenceTracker.Core.Model;

namespace MadExpenceTracker.Core.Persistence
{
    public interface IExpencePersistence
    {
        IEnumerable<Expences> GetAll();
        Expences Get(Guid id);
        Expences AddExpence(Expence expence);
        bool CreateNewExpencesDocument(string runningMonth);
        bool Update(Expence expence);
        bool UpdateExpencesIsActive(bool isActive, string runningMonth);
        bool Delete(Guid id);
    }
}
