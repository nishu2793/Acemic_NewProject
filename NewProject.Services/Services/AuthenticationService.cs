using Microsoft.Extensions.Options;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Services.Entities.LoginDto;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NewProject.Utility;
using NewProject.Utility.Exceptions;

namespace NewProject.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly IJwtService _jwtService;
        private readonly AppSettings _appSettings;
        public AuthenticationService(
             IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
             IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
             IJwtService jwtService,
             IOptions<AppSettings> appSettings)
        {
            this._readOnlyUnitOfWork = readOnlyUnitOfWork;
            this._jwtService = jwtService;
            this._appSettings = appSettings.Value;
            this._readWriteUnitOfWork = readWriteUnitOfWork;
        }

        public async Task<UserLoginDto> AuthenticateAsync(UserAuthRequestDto request, string ipAddress)
        {
            var hashPassword = GenericMethods.GetHash(request.Password);
            var user = await _readOnlyUnitOfWork.UserRegisterRepository.GetFirstOrDefaultAsync(x => x.EmailAddress == request.EmailId && x.Password == hashPassword);
            if (user == null)
                throw new BadResultException("Email and Password Not valid");

            UserLoginDto loginDto = new UserLoginDto();
            loginDto.EmailId = user.EmailAddress;
            loginDto.UserId = user.Did;
            loginDto.FirstName = user.FirstName;
            loginDto.LastName = user.LastName;
            loginDto.MobileNo = user.MobileNo;
            loginDto.JwtToken = _jwtService.GenerateSecurityToken(new SessionDetailsDto
            {
                FirstName = loginDto.FirstName,
                LastName = loginDto.LastName,
                UserId = loginDto.UserId
            }, _appSettings, out var expiresOn);

            var refreshToken = _jwtService.GenerateRefreshToken(ipAddress);
            refreshToken.UserId = loginDto.UserId;
            loginDto.RefreshToken = refreshToken.Token;
            var isRefTokenExist = await _readOnlyUnitOfWork.RefreshTokenRepository.AnyAsync(x => x.UserId == user.Did);
            if (isRefTokenExist)
            {
                // remove old refresh tokens from user
                RemoveOldRefreshTokens(user.Did);
                //await _readWriteUnitOfWork.RefreshTokenRepository.AttachUpdateEntity(refreshToken);
            }
            else
            {
                await _readWriteUnitOfWork.RefreshTokenRepository.AddAsync(refreshToken);
            }
            await _readWriteUnitOfWork.CommitAsync();
            //var account = await _readOnlyUnitOfWork.AccountsRepository.GetFirstOrDefaultAsync(x => x.Name == username);
            //var account = await _masterDBContext.AccountsRepository.GetFirstOrDefaultAsync(x => x.Name == request.EmailId);
            return loginDto;
        }

        private void RemoveOldRefreshTokens(Guid UserId)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            var data = _readWriteUnitOfWork.RefreshTokenRepository.GetAll(x => x.UserId == UserId).ToList();
            data.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

    }
}
