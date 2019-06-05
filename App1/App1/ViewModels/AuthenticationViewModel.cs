using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Linq;
using Xamarin.Forms;

namespace DragonRun
{
    public class AuthenticationViewModel : BaseViewModel
    {
        public ContentPage CurrentPage { get; set; }

        
        public IEnumerable<ContentPage> ContentPages { get; set;}

        public AuthenticationViewModel(Page Caller)
        {
            List<ContentPage> pages = new List<ContentPage>()
            {
                new Setting(),
                new About(),
            };
            ContentPages = new ObservableCollection<ContentPage>(pages);

            CurrentPage = ContentPages.First();

            Debug.WriteLine(CurrentPage);


        }

       
    }
}
