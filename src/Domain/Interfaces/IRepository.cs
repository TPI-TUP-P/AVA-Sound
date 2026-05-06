namespace Domain.Interfaces;

public interface IRepository<T> where T : class

{
    Task<T> GetById(Guid id);
    Task<T> Create(T t, CancellationToken cancellationToken);

    Task<T> Update(T t);
    Task Delete(Guid id);

}