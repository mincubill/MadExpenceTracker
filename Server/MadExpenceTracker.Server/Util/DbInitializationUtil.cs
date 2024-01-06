using MadExpenceTracker.Core.Interfaces.Utils;

namespace MadExpenceTracker.Server.Util
{
    public class DbInitializationUtil
    {
        IDbInitialization _dbInitialization;

        public DbInitializationUtil(IDbInitialization dbInitialization)
        {
            _dbInitialization = dbInitialization;
        }

        public void Initialize()
        {
            _dbInitialization.InitializeDb();
        }
    }
}
