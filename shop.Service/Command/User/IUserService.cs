﻿using shop.Service.DTOs.UserCommand;
using shop.Service.Extension.Util;

namespace shop.Service.Command
{
    public interface IUserService
    {
        Task<OperationResult> AddUserRole(AddUserRoleDto AddUserRoleDto);
        Task<OperationResult> ChangeWallet(ChargeWalletDto ChargeWalletDto);
        Task<OperationResult> AddUser(CreateUserDto CreateUserDto);
        Task<OperationResult> EditUser(EditUserDto EditUserDto);
        Task<OperationResult> RemoveUserRole(RemoveUserRoleDto RemoveUserRoleDto);
    }
}