using Domain.Entities;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IRoleService
    {
        IActionResult GetRoles(RoleFilterModel filter, PaginationRequestModel pagination);
        IActionResult GetRoleById(Guid id);
        Task<IActionResult> CreateRole(RoleCreateModel model);

        Task<IActionResult> UpdateRole(Guid id, RoleUpdateModel model);

        Task<IActionResult> DeleteRole(Guid id);
    }
}