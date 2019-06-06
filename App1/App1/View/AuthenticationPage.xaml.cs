using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DragonRun
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthenticationPage : CarouselPage
    {
        public AuthenticationPage()
        {
            InitializeComponent();
        }
    }
}