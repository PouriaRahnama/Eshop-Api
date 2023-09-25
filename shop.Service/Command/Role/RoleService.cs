﻿using shop.Core.Domain.Role;
using shop.Data.Repository;
using shop.Service.DTOs.RoleCommand;
using shop.Service.Extension.Util;

namespace shop.Service.Command
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<RolePermission> _RolePermissionRepository;
        private readonly IRepository<Role> _RoleRepository;
        public RoleService(IRepository<Role> RoleRepository,
            IRepository<RolePermission> RolePermissionRepository)
        {
            _RoleRepository = RoleRepository;
            _RolePermissionRepository = RolePermissionRepository;
        }

        public async Task<OperationResult> AddRole(CreateRoleDto CreateRoleDto)
        {
            var role = new Role()
            {
                Title = CreateRoleDto.Title
            };

            await _RoleRepository.AddAsync(role);
            return OperationResult.Success();
        }
        public async Task<OperationResult> AddRolePermission(AddRolePermissionDto AddRolePermissionDto)
        {
            var role = await _RoleRepository.FindByIdAsync(AddRolePermissionDto.RoleId);
            if (role == null)
                return OperationResult.NotFound();

            var rolePermission = new RolePermission()
            {
                RoleId = role.Id,
                PermissionStatus = AddRolePermissionDto.PermissionStatus
            };
            await _RolePermissionRepository.AddAsync(rolePermission);
            return OperationResult.Success();
        }
        public async Task<OperationResult> RemoveRolePermission(RemoveRolePermissionDto RemoveRolePermissionDto)
        {
            var RolePermission = await _RolePermissionRepository.FindByIdAsync(RemoveRolePermissionDto.RolePermissionId);
            if (RolePermission == null || RolePermission.RoleId != RemoveRolePermissionDto.RoleId)
                return OperationResult.NotFound();

            RolePermission.Deleted = true;
            _RolePermissionRepository.Update(RolePermission);
            return OperationResult.Success();
        }
        public async Task<OperationResult> UpdateRole(EditRoleDto EditRoleDto)
        {
            var Role = await _RoleRepository.FindByIdAsync(EditRoleDto.RoleId);
            if (Role == null)
                return OperationResult.NotFound();

            Role.Title = EditRoleDto.Title;
            Role.UpdateON = DateTime.Now;


            _RoleRepository.Update(Role);
            return OperationResult.Success();
        }
    }
}
