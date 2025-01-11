namespace Commons.Services
{
    public interface IBaseService<Entity, Response>
    {
        Task<Response> GetAll();
        Task<Response> GetById(Guid Id);
        Task<Response> Create(Entity entity, CancellationToken cancellationToken);
        Task<Response> Update(Entity entity, CancellationToken cancellationToken);
        Task<Response> Delete(Guid Id, CancellationToken cancellationToken);
    }
}
