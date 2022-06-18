using Kursach.dto.Review;
using Kursach.http;
using Kursach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Services
{
    public class MyProfileService
    {
        MyHttpClient myHttpClient;

        public MyProfileService()
        {
            myHttpClient = new MyHttpClient("users/");
        }

        public async Task<HttpData<User>> UpdateName(UpdateNameDto updateNameDto)
        {
            return await myHttpClient.Put<User,UpdateNameDto>("name",updateNameDto);
        }

        public async Task<HttpData<User>> UpdatePassword(UpdatePasswordDto updatePasswordDto)
        {
            return await myHttpClient.Put<User,UpdatePasswordDto>("password", updatePasswordDto);
        }

        public void Dispose()
        {
            myHttpClient.Dispose();
        }
    }
}
