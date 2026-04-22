namespace Domain.Interfaces;

public interface IRepository<T> where T : class

{
   Task<T?> GetById(Guid id);
    Task Add(T t);
    Task Update(T t);
    Task Delete(Guid id);

}