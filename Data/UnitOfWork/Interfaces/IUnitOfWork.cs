using Data.Repositories.Interfaces;

namespace Data.UnitOfWorks.Interfaces;

public interface IUnitOfWork
{
    public IRoleRepository Role { get; }

    void BeginTransaction();
    
    void Commit();
    
    void Rollback();
    
    void Dispose();
    
    Task<int> SaveChangesAsync();
}