using MadExpenceTracker.Core.Exceptions;
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
            return _persistence.GetAll() ?? new List<Expences>();
        }

        public Expences GetExpences(Guid id)
        {
            return _persistence.Get(id) ?? throw new NotFoundException("Expences not found");
        }

        public Expences GetExpences(bool isActive)
        {
            return _persistence.GetByActive(isActive) ?? throw new NotFoundException("Expences not found");
        }

        public Expence GetExpence(Guid id)
        {
            return _persistence.GetExpence(id) ?? throw new NotFoundException("Expences not found");
        }

        public Expences Create(Expence expence)
        {
            expence.Id = expence.Id == Guid.Empty ? Guid.NewGuid() : expence.Id;
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
                throw new CannotUpdateException("Error updating expence");
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
