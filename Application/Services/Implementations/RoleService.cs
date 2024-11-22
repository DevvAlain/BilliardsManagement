using Application.Services.Interfaces;
using Common.Extensions;
using Data.Repositories.Interfaces;
using Data.UnitOfWorks.Interfaces;
using Domain.Entities;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models.Creates;
using Domain.Models.Updates;
using Domain.Models.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _roleRepository = unitOfWork.Role;
            _mapper = mapper;
        }

        public IActionResult GetRoles(RoleFilterModel filter, PaginationRequestModel pagination)
        {
            var query = _roleRepository.GetAll();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(role => role.Name.Contains(filter.Name));
            }

            var totalRows = query.Count();

            var roles = query
                .OrderByDescending(role => role.CreateAt)
                .Paginate(pagination)

                // Map thu cong
                // .Select(x => new RoleViewModel()
                // {
                //     Id = x.Id,
                //     Ten = x.Name,
                //     NgayTao = x.CreateAt,
                // })
                //

                // Auto mapper
                .ProjectTo<RoleViewModel>(_mapper.ConfigurationProvider)
                .ToList();

            return new OkObjectResult(roles.ToPaged(pagination, totalRows));
        }

        public IActionResult GetRoleById(Guid id)
        {
            var role = _roleRepository.Where(x => x.Id.Equals(id))
                .ProjectTo<RoleViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
            if (role == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(role);
        }

        public async Task <IActionResult> CreateRole(RoleCreateModel model)
        {
            // var role = new Role()
            // {
            //     Id = Guid.NewGuid(),
            //     Name = model.Name,
            //     CreateAt = DateTime.UtcNow,
            // };
            
            var role = _mapper.Map<Role>(model);
            
            _roleRepository.Add(role);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0 ? GetRoleById(role.Id) : new BadRequestResult();
        }

        public async Task<IActionResult> UpdateRole(Guid id, RoleUpdateModel model)
        {
            var role = await _roleRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            if (role == null)
            {
                return new NotFoundResult();
            }
            
            var update = _mapper.Map(model, role);
            
            _roleRepository.Update(update); 
            
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0 ? GetRoleById(role.Id) : new BadRequestResult();
        }

        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var role = await _roleRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            if (role == null)
            {
                return new NotFoundResult();
            }

            _roleRepository.Delete(role);

            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0 ? new OkResult() : new BadRequestResult();
        }

        
    }
}