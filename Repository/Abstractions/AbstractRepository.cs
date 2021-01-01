using System.Threading.Tasks;
using Repository.Context;

namespace Repository.Abstractions
{
    public abstract class AbstractRepository
    {
        protected readonly ApplicationContext ApplicationContext;

        protected AbstractRepository(ApplicationContext applicationContext)
        {
            ApplicationContext = applicationContext;
        }

        protected async Task SaveChangesAsync()
        {
            await ApplicationContext.SaveChangesAsync();
        }
    }
}