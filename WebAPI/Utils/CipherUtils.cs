using Microsoft.AspNetCore.DataProtection;

namespace WebAPI.Utils
{
    public class CipherUtils : ICipherUtils
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private IConfiguration _configuration;
        public CipherUtils(IDataProtectionProvider dataProtectionProvider, IConfiguration configuration)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _configuration = configuration;
        }

        public string Encrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(_configuration.GetSection("AppSettings:EncryptToken").Value);
            return protector.Protect(input);
        }

        public string Decrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(_configuration.GetSection("AppSettings:EncryptToken").Value);
            return protector.Unprotect(input);
        }
    }
}
