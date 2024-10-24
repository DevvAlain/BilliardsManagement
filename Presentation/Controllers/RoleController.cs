using Application.Services.Interfaces;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;
[Route("api/roles")]
[ApiController]
public class RoleController : ControllerBase
{
   private readonly IRoleService _roleService;

   public RoleController(IRoleService roleService)
   {
      _roleService = roleService;
   }

   [HttpGet]
   public IActionResult GetRoles()
   {
      var roles = _roleService.GetRoles();
      return Ok(roles);
   }
}