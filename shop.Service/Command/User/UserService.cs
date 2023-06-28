using Newtonsoft.Json.Linq;
using shop.Core.Domain.Role;
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
        private readonly IRepository<Role> _RoleRepository;
        private readonly IRepository<UserRole> _UserRoleRepository;
        private readonly IRepository<Wallet> _WalletRepository;
        private readonly IRepository<UserToken> _TokenRepository;

        public UserService(IRepository<User> Repository,
            IRepository<Role> RoleRepository,
            IRepository<UserRole> UserRoleRepository,
            IFileService fileService,
            IRepository<Wallet> WalletRepository,
            IRepository<UserToken> tokenRepository)
        {
            _repository = Repository;
            _fileService = fileService;
            _RoleRepository = RoleRepository;
            _UserRoleRepository = UserRoleRepository;
            _WalletRepository = WalletRepository;
            _TokenRepository = tokenRepository;
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
            user.Password = Sha256Hasher.Hash(EditUserDto.Password);
            user.Email = EditUserDto.Email;
            user.PhoneNumber = EditUserDto.PhoneNumber;
            user.Family = EditUserDto.Family;
            user.Name = EditUserDto.Name;
            user.UpdateON = DateTime.Now;
            user.IsActive = EditUserDto.IsActive;


            await _repository.UpdateAsync(user);
            if (EditUserDto.Avatar != null || oldAvatarName != "avatar.png")
                _fileService.DeleteFile(Directories.UserAvatars, oldAvatarName);

            return OperationResult.Success();
        }
        public async Task<OperationResult> AddUserRole(AddUserRoleDto AddUserRoleDto)
        {
            var User = _repository.FindByIdAsync(AddUserRoleDto.UserId);
            if (User == null)
                return OperationResult.NotFound("!کاربر مورد نظر یافت نشد");

            var Role = _RoleRepository.FindByIdAsync(AddUserRoleDto.RoleId);
            if (Role == null)
                return OperationResult.NotFound("!نقش مورد نظر یافت نشد");

            var UserRole = new UserRole()
            {
                RoleId = AddUserRoleDto.RoleId,
                UserId = AddUserRoleDto.UserId
            };
            await _UserRoleRepository.AddAsync(UserRole);
            return OperationResult.Success();
        }
        public async Task<OperationResult> RemoveUserRole(RemoveUserRoleDto RemoveUserRoleDto)
        {
            var User = await _repository.FindByIdAsync(RemoveUserRoleDto.UserId);
            if (User == null)
                return OperationResult.NotFound("!کاربر مورد نظر یافت نشد");

            var Role = await _RoleRepository.FindByIdAsync(RemoveUserRoleDto.RoleId);
            if (Role == null)
                return OperationResult.NotFound("!نقش مورد نظر یافت نشد");

            var UserRole = new UserRole()
            {
                RoleId = RemoveUserRoleDto.RoleId,
                UserId = RemoveUserRoleDto.UserId
            };
            await _UserRoleRepository.DeleteAsync(UserRole);
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

            await _WalletRepository.AddAsync(wallet);
            return OperationResult.Success();
        }

        public async Task<OperationResult> AddToken(AddTokenDto AddTokenDto)
        {
            var user = await _repository.FindByIdAsync(AddTokenDto.UserId);
            if (user == null)
                return OperationResult.NotFound();

            var activeTokenCount = _TokenRepository.Table.Where(c => c.UserId == user.Id && c.Deleted == false).Count();
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

            await _TokenRepository.AddAsync(Token);
            return OperationResult.Success();
        }


        public async Task<OperationResult> RemoveUserToken(RemoveUserTokenDto RemoveUserTokenDto)
        {

            var UserToken = await _TokenRepository.FindByIdAsync(RemoveUserTokenDto.TokenId);
            if (UserToken == null)
                return OperationResult.Error("invalid TokenId");

            await _TokenRepository.DeleteAsync(UserToken);
            return OperationResult.Success();
        }


    }
}
