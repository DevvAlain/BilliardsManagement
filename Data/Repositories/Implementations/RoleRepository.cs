using Data.Repositories.Interfaces;
using Domain.Context;
using Domain.Entities;

namespace Data.Repositories.Implementations;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(BilliardsContext context) : base(context)
    {
    }
}