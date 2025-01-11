using Commons.Entities;

namespace Commons.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        T GetById(Guid id);
        T GetAll();
    }
}
