﻿using shop.Core.Domain.Role;
using shop.Core.Domain.User;
using shop.Data.Repository;
using shop.Service.DTOs.UserCommand;
using shop.Service.Extension.FileUtil.Interfaces;
using shop.Service.Extension.SecurityUtil;
using shop.Service.Extension.Util;

namespace shop.Service.Command
{
    public class UserService : IUserService
    {
        private readonly IFileService _fileService;
        private readonly IRepository<User> _repository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<Wallet> _walletRepository;
        private readonly IRepository<UserToken> _tokenRepository;
        private readonly IRepository<UserAddress> _userAddressRepository;

        public UserService(IRepository<User> Repository,
            IRepository<Role> RoleRepository,
            IRepository<UserRole> UserRoleRepository,
            IFileService fileService,
            IRepository<Wallet> WalletRepository,
            IRepository<UserToken> tokenRepository,
            IRepository<UserAddress> userAddressRepository)
        {
            _repository = Repository;
            _fileService = fileService;
            _roleRepository = RoleRepository;
            _userRoleRepository = UserRoleRepository;
            _walletRepository = WalletRepository;
            _tokenRepository = tokenRepository;
            _userAddressRepository = userAddressRepository;
        }

        public async Task<OperationResult> AddUser(CreateUserDto CreateUserDto)
        {
            var password = Sha256Hasher.Hash(CreateUserDto.Password);
            var IsExistUser = await _repository.GetEntity(u => u.PhoneNumber == CreateUserDto.PhoneNumber ||
                 u.Email == CreateUserDto.Email);
            if (IsExistUser != null)
                return OperationResult.Error(" !کاربری با مشخصات وارد شده وجود دارد");

            var user = new User()
            {
                Name = CreateUserDto.Name,
                Family = CreateUserDto.Family,
                PhoneNumber = CreateUserDto.PhoneNumber,
                Email = CreateUserDto.Email,
                Password = password,
                IsActive = true
            };

            await _repository.AddAsync(user);
            return OperationResult.Success();
        }

        public async Task<OperationResult> EditUser(EditUserDto EditUserDto)
        {
            var user = await _repository.FindByIdAsync(EditUserDto.UserId);
            if (user == null)
                return OperationResult.NotFound();

            var oldAvatarName = user.AvatarName;
            var NewAvatarName = user.AvatarName;
            if (EditUserDto.Avatar != null)
                NewAvatarName = await _fileService
                .SaveFileAndGenerateName(EditUserDto.Avatar, Directories.UserAvatars);

            user.AvatarName = NewAvatarName;
            user.Email = EditUserDto.Email;
            user.PhoneNumber = EditUserDto.PhoneNumber;
            user.Family = EditUserDto.Family;
            user.Name = EditUserDto.Name;
            user.UpdateON = DateTime.Now;


            _repository.Update(user);
            if (EditUserDto.Avatar != null || oldAvatarName != "avatar.png")
                _fileService.DeleteFile(Directories.UserAvatars, oldAvatarName);

            return OperationResult.Success();
        }
        public async Task<OperationResult> AddUserRole(AddUserRoleDto AddUserRoleDto)
        {
            var User = await _repository.FindByIdAsync(AddUserRoleDto.UserId);
            if (User == null)
                return OperationResult.NotFound("!کاربر مورد نظر یافت نشد");

            var Role = await _roleRepository.FindByIdAsync(AddUserRoleDto.RoleId);
            if (Role == null)
                return OperationResult.NotFound("!نقش مورد نظر یافت نشد");

            var UserRole = new UserRole()
            {
                RoleId = AddUserRoleDto.RoleId,
                UserId = AddUserRoleDto.UserId
            };
            await _userRoleRepository.AddAsync(UserRole);
            return OperationResult.Success();
        }
        public async Task<OperationResult> RemoveUserRole(RemoveUserRoleDto RemoveUserRoleDto)
        {
            var User = await _repository.FindByIdAsync(RemoveUserRoleDto.UserId);
            if (User == null)
                return OperationResult.NotFound("!کاربر مورد نظر یافت نشد");

            var Role = await _roleRepository.FindByIdAsync(RemoveUserRoleDto.RoleId);
            if (Role == null)
                return OperationResult.NotFound("!نقش مورد نظر یافت نشد");

            var UserRole = await _userRoleRepository.GetEntity(u => u.RoleId == RemoveUserRoleDto.RoleId && u.UserId == RemoveUserRoleDto.UserId);
            if (UserRole == null)
                return OperationResult.NotFound();
            
            _userRoleRepository.Delete(UserRole);
            return OperationResult.Success();
        }
        public async Task<OperationResult> ChangeWallet(ChargeWalletDto ChargeWalletDto)
        {
            var user = await _repository.FindByIdAsync(ChargeWalletDto.UserId);
            if (user == null)
                return OperationResult.NotFound("!کاربر مورد نظر یافت نشد ");

            var wallet = new Wallet()
            {
                Price = ChargeWalletDto.Price,
                IsFinally = ChargeWalletDto.IsFinally,
                Desciption = ChargeWalletDto.Description,
                Status = ChargeWalletDto.Type,
                FinallyDate = DateTime.Now,
                UserId = ChargeWalletDto.UserId
            };

            await _walletRepository.AddAsync(wallet);
            return OperationResult.Success();
        }

        public async Task<OperationResult> AddToken(AddTokenDto AddTokenDto)
        {
            var user = await _repository.FindByIdAsync(AddTokenDto.UserId);
            if (user == null)
                return OperationResult.NotFound();

            var activeTokenCount = _tokenRepository.Table.Where(c => c.UserId == user.Id && c.Deleted == false).Count();
            //var activeTokenCount2 = _tokenRepository.Get(c => c.UserId == user.Id && c.Deleted == false).Count();

            if (activeTokenCount == 3)
                return OperationResult.Error("امکان استفاده از 4 دستگاه همزمان وجود ندارد");

            var Token = new UserToken()
            {
                Device = AddTokenDto.Device,
                HashJwtToken = AddTokenDto.HashJwtToken,
                HashRefreshToken = AddTokenDto.HashRefreshToken,
                RefreshTokenExpireDate = AddTokenDto.RefreshTokenExpireDate,
                TokenExpireDate = AddTokenDto.TokenExpireDate,
                UserId = user.Id
            };

            await _tokenRepository.AddAsync(Token);
            return OperationResult.Success();
        }


        public async Task<OperationResult> RemoveUserToken(RemoveUserTokenDto RemoveUserTokenDto)
        {

            var UserToken = await _tokenRepository.FindByIdAsync(RemoveUserTokenDto.TokenId);
            if (UserToken == null)
                return OperationResult.Error("invalid TokenId");

            UserToken.Deleted = true;
            _tokenRepository.Update(UserToken);
            return OperationResult.Success();
        }

        public async Task<OperationResult> ChangePassword(ChangePasswordDto ChangePasswordDto)
        {
            var user = await _repository.FindByIdAsync(ChangePasswordDto.UserId);
            if (user == null)
                return OperationResult.NotFound("کاربر یافت نشد");

            var currentPasswordHash = Sha256Hasher.Hash(ChangePasswordDto.CurrentPassword);
            if (user.Password != currentPasswordHash)
            {
                return OperationResult.Error("کلمه عبور فعلی نامعتبر است");
            }

            var newPasswordHash = Sha256Hasher.Hash(ChangePasswordDto.Password);
            user.Password = newPasswordHash;
            _repository.Update(user);

            return OperationResult.Success();
        }


        public async Task<OperationResult> AddUserAddress(AddUserAddressDto AddUserAddressDto)
        {
            var user = await _repository.FindByIdAsync(AddUserAddressDto.UserId);
            if (user == null)
                return OperationResult.NotFound("کاربر یافت نشد");

            var address = new UserAddress()
            {
                UserId = user.Id,
                Name = AddUserAddressDto.Name,
                NationalCode = AddUserAddressDto.NationalCode,
                PhoneNumber = AddUserAddressDto.PhoneNumber,
                PostalAddress = AddUserAddressDto.PostalCode,
                PostalCode = AddUserAddressDto.PostalCode,
                Shire = AddUserAddressDto.Shire,
                Family = AddUserAddressDto.Family,
                City = AddUserAddressDto.City,
                ActiveAddress = false
            };

            await _userAddressRepository.AddAsync(address);
            return OperationResult.Success();
        }
        //
        public async Task<OperationResult> RemoveUserAddress(RemoveUserAddressDto RemoveUserAddressDto)
        {
            var user = await _repository.FindByIdAsync(RemoveUserAddressDto.UserId);
            if (user == null)
                return OperationResult.NotFound("کاربر یافت نشد");

            var Address = await _userAddressRepository.FindByIdAsync(RemoveUserAddressDto.AddressId);

            if (Address == null)
                return OperationResult.NotFound();

            Address.Deleted = true;
            _userAddressRepository.Update(Address);
            return OperationResult.Success();
        }

        public async Task<OperationResult> EditUserAddress(EditUserAddressDto EditUserAddressDto)
        {
            var user = await _repository.FindByIdAsync(EditUserAddressDto.UserId);
            if (user == null)
                return OperationResult.NotFound("کاربر یافت نشد");

            var Address = await _userAddressRepository.FindByIdAsync(EditUserAddressDto.Id);
            if (Address == null)
                return OperationResult.NotFound("Address Not found");

            Address.UpdateON = DateTime.Now;
            Address.Shire = EditUserAddressDto.Shire;
            Address.PhoneNumber = EditUserAddressDto.PhoneNumber;
            Address.Family = EditUserAddressDto.Family;
            Address.NationalCode = EditUserAddressDto.NationalCode;
            Address.PostalCode = EditUserAddressDto.PostalCode;
            Address.Name = EditUserAddressDto.Name;
            Address.PostalAddress = EditUserAddressDto.PostalAddress;
            Address.City = EditUserAddressDto.City;

            _userAddressRepository.Update(Address);
            return OperationResult.Success();
        }

        public async Task<OperationResult> SetActiveUserAddress(SetActiveUserAddressDto SetActiveUserAddressDto)
        {
            var user = await _repository.FindByIdAsync(SetActiveUserAddressDto.UserId);
            if (user == null)
                return OperationResult.NotFound("کاربر یافت نشد");

            var currentAddress = await _userAddressRepository.FindByIdAsync(SetActiveUserAddressDto.AddressId);
            if (currentAddress == null)
                return OperationResult.NotFound("Address Not found");

            var Addresses = _userAddressRepository.Get(ad => ad.UserId == user.Id).ToList();

            foreach (var address in Addresses)
            {
                address.ActiveAddress = false;
            }
            currentAddress.ActiveAddress = true;

            _userAddressRepository.Update(currentAddress);
            return OperationResult.Success();
        }
    }
}
