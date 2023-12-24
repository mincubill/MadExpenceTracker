using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;

namespace MadExpenceTracker.Core.Services
{
    public class ExpencesService : IExpencesService
    {
        private readonly IExpencePersistence _persistence;

        public ExpencesService(IExpencePersistence persistence)
        {
            _persistence = persistence;
        }

        public IEnumerable<Expences> GetAll()
        {
            return _persistence.GetAll();
        }

        public Expences GetExpences(Guid id)
        {
            return _persistence.Get(id);
        }

        public Expence GetExpence(Guid id)
        {
            return _persistence.Get(id).Expence.First(e => e.Id == id);
        }

        public Expences Create(Expence expence)
        {
            return _persistence.AddExpence(expence);
        }

        public bool CreateNewMonth()
        {
            return _persistence.CreateNewExpencesDocument($"{DateTime.Now.Year}/{DateTime.Now.Month}");
        }

        public Expence Update(Expence expence)
        {
            if (_persistence.Update(expence))
            {
                return expence;
            }
            else
            {
                throw new Exception("Error updating expence");
            }
        }

        public bool Delete(Guid id)
        {
            return _persistence.Delete(id);
        }

        public bool CloseMonth(string month)
        {
            return _persistence.UpdateExpencesIsActive(false, month);
        }
    }
}
