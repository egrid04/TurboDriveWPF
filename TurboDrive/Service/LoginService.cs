using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using TurboDrive.Classes;

namespace TurboDrive.Service
{
    public static class LoginService
    {
        public static string getSalt(HttpClient client, String username)
        {
            string uri = $"{client.BaseAddress}api/Login/GetSalt/{username}";
            try
            {
                var request = client.PostAsync(uri, null).Result;
                string salt = request.Content.ReadAsStringAsync().Result;
                return salt;
            }
           catch (Exception ex) 
            {
                return "getSalt Hiba:"+ex.Message;
            }
        }
        public static LoggedUser login(HttpClient client, String username, String hash)
        {
            string url = $"{client.BaseAddress}api/Login";
            LoginUser loginUser = new LoginUser(username, hash);
            string json = JsonSerializer.Serialize(loginUser);
            var request = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PostAsync (url, request).Result;
            try
            {
                var t = response.Content.ReadAsStringAsync().Result;
                MessageBox.Show(t);
                LoggedUser loggeduser = JsonSerializer.Deserialize<LoggedUser>(t);
                return loggeduser;
            }
            catch (Exception ex)
            {
                return null;
            }


        }
    }
}
