using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DragonRun
{
    public partial class MainPage : ContentPage
    {
        Image img;
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainViewModel(ref this.player, ref this.layout, Navigation, name);
            
              

        }

        
    }
}
