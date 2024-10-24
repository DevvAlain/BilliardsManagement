
using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Data.UnitOfWorks.Interfaces;
using Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.UnitOfWork.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly BilliardsContext _context;
    private IDbContextTransaction _transaction = null!;

    public UnitOfWork(BilliardsContext context)
    {
        _context = context;
    }
    
    private IRoleRepository? _role;

    public IRoleRepository Role
    {
        get { return _role ??= new RoleRepository(_context); }
    }
    
    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public void Commit()
    {
        try
        {
            _transaction.Commit();
        }
        finally
        {
            _transaction.Dispose();
            _transaction = null!;
        }
    }

    public void Rollback()
    {
        try
        {
            _transaction.Rollback();
        }
        finally
        {
            _transaction.Dispose();
            _transaction = null!;
        }
    }

    public void Dispose()
    {
        _transaction.Dispose();
        _context.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
   
}