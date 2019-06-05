using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DragonRun
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Setting : ContentPage
	{
		public Setting ()
		{
			InitializeComponent ();
        }

        public Setting(ISimpleAudioPlayer player) : this()
        {
            this.BindingContext = new SettingPageViewModel(player,Navigation);
        }
        
        
    }
    
}