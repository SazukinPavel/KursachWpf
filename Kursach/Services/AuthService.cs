using Kursach.dto;
using Kursach.http;
using Kursach.Models;
using Microsoft.Win32;
using System.Threading.Tasks;

namespace Kursach.Services
{
    public class AuthService
    {
        MyHttpClient myHttpClient;
        public AuthService()
        {
            myHttpClient = new MyHttpClient("auth/");
        }

        public async Task<HttpData<User>> Register(RegisterDto registerDto)
        {
            return await myHttpClient.Post<User, RegisterDto>("register", registerDto);
        }

        public async Task<HttpData<AuthResponseDto>> Login(LoginDto loginDto)
        {
            return await myHttpClient.Post<AuthResponseDto, LoginDto>("login", loginDto);
        }

        private async Task<HttpData<User>> CheckToken()
        {
            return await myHttpClient.Get<User>("check");
        }

        public void SetAuthorize(AuthResponseDto authResponseDto)
        {
            SaveToken(authResponseDto.token);
        }

        public async Task<User> Authorize()
        {
            string token=Registry.CurrentUser.OpenSubKey("courses").GetValue("token") as string;
            if (token != null)
            {
                var user = CheckToken();
                return (await user).Data;
            }
            await checkConnection();
            return null;
        }
        private void SaveToken(string token)
        {
            Registry.CurrentUser.CreateSubKey("courses").SetValue("token", token);
        }

        private async Task<HttpData<bool>> checkConnection()
        {
            return await myHttpClient.Get<bool>("connection");
        }

        public static void ResetToken()
        {
            var subKey = Registry.CurrentUser.OpenSubKey("courses",true);
            if (subKey != null && subKey.GetValue("token")!=null)
            {
               subKey.DeleteValue("token");
            }
        }

        public void Dispose()
        {
            myHttpClient.Dispose();
        }
    }   
}
