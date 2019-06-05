using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DragonRun
{
    public class LoginViewModel : BaseViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public string ErrorText { get; set; }
        public bool ErrorVisibility { get; set; }

        public ICommand LoginCommand { get; set; }
        public LoginViewModel()
        {
            ErrorVisibility = false;

            LoginCommand = new Command(() => {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                ErrorText = "Enter all input fields!";
                ErrorVisibility = true;
                return;
            }
                SignIn();
            });
        }

        private async void SignIn()
        {
            HttpClient client = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.Timeout = TimeSpan.FromMinutes(2);

            HttpResponseMessage res = new HttpResponseMessage();

            try
            {
                res = await client.GetAsync("http://10.0.2.2:5001/api/users/" + Login + "/" + Password);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            if (!res.IsSuccessStatusCode)
            {
                ErrorText = await res.Content.ReadAsStringAsync();
                ErrorVisibility = true;
                return;
            }

            Application.Current.MainPage = new NavigationPage(new MainPage());
            

        }
    }
}
