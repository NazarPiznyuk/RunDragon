using DragonRun;
using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace DragonRun

{
    
    class SettingPageViewModel : BaseViewModel
    {
       
        INavigation Navigation;
        ISimpleAudioPlayer Player;
        bool _isMusicOn;
        public bool IsMusicOn
        {
            get
            {
                return _isMusicOn;
                
            }
            set
            {
                _isMusicOn = value;
                ChangeState();
            }
        }
        private void ChangeState()
        {
            if (!_isMusicOn)
            {
                Player.Pause();
                return;      
            }
            if(!Player.IsPlaying && _isMusicOn)
            {
                Player.Play();
            }
        }
        public SettingPageViewModel()
        {
           
            
        }
        public ICommand AboutCommand { get; set; }
        public ICommand LeaderBoardCommand { get; set; }
        public SettingPageViewModel( ISimpleAudioPlayer player, INavigation Navigation)
        {
            this.Navigation = Navigation;
            this.Player = player;
           _isMusicOn = player.IsPlaying;

            AboutCommand = new Command(async () =>
            {
                await Navigation.PushAsync(new About());
            });
            LeaderBoardCommand = new Command(async () =>
            {
                await Navigation.PushAsync(new LeaderBoard());
            });
        }
    }
}
