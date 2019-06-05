using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DragonRun
{
    public class RegisterViewModel : BaseViewModel
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string ErrorText { get; set; }
        public bool ErrorVisibility { get; set; }


        public ICommand RegisterCommand { get; set; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(() =>
            {
                ErrorVisibility = false;
                if(string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Email) 
                || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
                {
                    ErrorText = "Enter all input fields!";
                    ErrorVisibility = true;
                    return;
                }
                if (Password != ConfirmPassword)
                {
                    ErrorText = "Password confirmation does not match the password";
                    ErrorVisibility = true;
                    return;
                }
                if (!Helpers.Helper.CheckValidEmail(Email))
                {
                    ErrorText = "Enter the correct email";
                    ErrorVisibility = true;
                    return;
                }
                Register();
            });
        }

        private async Task Register()
        {
            HttpClient client = new HttpClient();
            var User = new User()
            {
                Id = Guid.NewGuid(),
                Login = Login,
                Email = Email,
                Password = Password,
                OwnScore = null
            };
            var content = JsonConvert.SerializeObject(User);
            var strcontent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("http://10.0.2.2:5001/api/users/", strcontent);
            if (!response.IsSuccessStatusCode)
            {
                ErrorText = await response.Content.ReadAsStringAsync();
                ErrorVisibility = true;
                return;
            }
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}
