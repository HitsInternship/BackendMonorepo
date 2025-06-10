using OtpNet;


namespace AuthModel.Service.Service
{
    public class OTPService
    {
        private readonly int _step; // время жизни кода в секундах
        private readonly int _digits; // количество цифр в коде

        public OTPService(int step = 300, int digits = 6)
        {
            _step = step;
            _digits = digits;
        }

        public string GenerateOtp(string secretKey)
        {
            var otp = new Totp(Base32Encoding.ToBytes(secretKey), _step, OtpHashMode.Sha1, _digits);
            return otp.ComputeTotp(DateTime.UtcNow);
        }

        public bool VerifyOtp(string secretKey, string code)
        {
            var otp = new Totp(Base32Encoding.ToBytes(secretKey), _step, OtpHashMode.Sha1, _digits);
            return otp.VerifyTotp(DateTime.UtcNow, code, out _, new VerificationWindow(1, 1));
        }

        public string GenerateSecretKey()
        {
            var key = KeyGeneration.GenerateRandomKey(20);
            return Base32Encoding.ToString(key);
        }
    }
}
