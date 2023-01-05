using Microsoft.Extensions.Options;
using NewProject.Data.Contexts;
using NewProject.Data.Infrastructure;
using NewProject.Services.Entities.LoginDto;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using NewProject.Utility;
using NewProject.Utility.Exceptions;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

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
             IOptions<AppSettings> appSettings
             


          )
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
        public async Task<string> MQTT(string message)
        {
            string brocker = "a8gw615bfoveo-ats.iot.ap-northeast-1.amazonaws.com"; // <AWS IOT Endpoint>
            int Port = 8883;
            var clientid = Guid.NewGuid().ToString();
            var certpass = "123456789";
            string topic = "TestMQTT";

            //var currentDirectoryPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory));
            var currentDirectoryPath = Directory.GetCurrentDirectory();
            var certificatepath = Path.Combine(currentDirectoryPath, "Cert");

            var caCert = X509Certificate.CreateFromCertFile(Path.Combine(certificatepath, "AmazonRootCA1.pem"));

            var devicepath = Path.Combine(certificatepath, "certificate.cert.pfx");
            var devicecert = new X509Certificate(devicepath, certpass);
            //var clientCert = new X509Certificate(Path.Combine(certificatepath, "cert-GV2EAJBV7GUY4IKUZOEXSWTCHCJT2UVQ.pem"));

            var client = new MqttClient(brocker, Port, true, caCert, devicecert, MqttSslProtocols.TLSv1_2);

           client.MqttMsgPublishReceived += IotClient_MqttMsgPublishReceived;
           client.MqttMsgSubscribed += IotClient_MqttMsgSubscribed;

            //Connection
            client.Connect(clientid);

            client.Publish("TestMQTT", Encoding.UTF8.GetBytes($"{message}"));
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            return message;
        }
        static void IotClient_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Console.WriteLine($"Successfully subscribed to the AWS IoT topic.");
        }
        static void IotClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //Console.WriteLine("Message received: " + Encoding.UTF8.GetString(e.Message));
            string s = "Message received: " + Encoding.UTF8.GetString(e.Message);
        }
    }
}

