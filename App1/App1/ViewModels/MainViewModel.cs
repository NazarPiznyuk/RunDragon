using DragonRun;
using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;



namespace DragonRun
{
    public class MainViewModel : BaseViewModel
    {
        Thread t;
        Image Player;
        Image Enemy;
        ImageSource image;
        ImageSource imageEnemy;
        AbsoluteLayout Layout;
        public Label label { get; set; }
        bool isJumpAnimation = false;
        bool isGameStarted = false;
        INavigation Navigation;
        ISimpleAudioPlayer musicPlayer;

        public int Score
        {
            get;
            set;
        }


        public ImageSource Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public ImageSource ImageEnemy
        {
            get { return imageEnemy; }
            set
            {
                imageEnemy = value;
                OnPropertyChanged(nameof(ImageEnemy));
            }
        }



        public ICommand AnimationCommand { get; set; }
        public ICommand TapCommand { get; set; }
        public ICommand SettingCommand { get; set; }
        

        public MainViewModel(ref Image Player, ref AbsoluteLayout layout, INavigation Navigation)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream("DragonRun." + "bang.mp3");
            musicPlayer =  CrossSimpleAudioPlayer.Current;
            musicPlayer.Load(audioStream);
            musicPlayer.Play();

            this.Player = Player;
            this.Layout = layout;
            Image = ImageSource.FromFile("pic5.png");
            ImageEnemy = ImageSource.FromFile("enemy2.png");

            layout = this.Layout;
            this.Navigation = Navigation;

            AnimationCommand = new Command(() =>
            {
                NewGame();
            });

            TapCommand = new Command(() =>
            {
                if (!isJumpAnimation)
                    JumpAnimation();
            });
            SettingCommand = new Command(async () =>
            {
                await Navigation.PushAsync(new Setting(this.musicPlayer));
            });
        }

        private void Enemy_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
            var tmp = (sender as Image);
            Debug.WriteLine($"Message from {tmp.StyleId}");
            //Debug.WriteLine($"Player right: {Player.Bounds.Right} Enemy left: {Enemy.TranslationX}");
            Score++;
            if (tmp.TranslationX + this.Layout.Width - this.Player.Width - tmp.Width <= 0
                && tmp.TranslationX + this.Layout.Width > 0)
            {

                if (Player.TranslationY * -1 <= tmp.Height)
                {
                    Debug.WriteLine("Lose");
                    isGameStarted = false;
                    //isEnemyAnimationEnded = true;

                    CancelAllAnimations();

                }
                Debug.WriteLine("inside if");
            }
            Debug.WriteLine("Outside if");
        }

        private void CancelAllAnimations()
        {
            foreach (var a in this.Layout.Children)
            {
                ViewExtensions.CancelAnimations(a);
            }
        }

        private Image CreateEnemy()
        {
            Image a = new Image()
            {
                StyleId = $"Enemy",
                Source = imageEnemy
            };
            a.PropertyChanged += Enemy_PropertyChanged;

            AbsoluteLayout.SetLayoutFlags(a, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(a, new Rectangle(1f, 0.5f, 45, 35));

            a.TranslationX = Layout.Bounds.Right;

            return a;
        }

        private void SpawnEnemy()
        {
            Enemy = CreateEnemy();


            this.Layout.Children.Add(Enemy);

            Thread t1 = new Thread(MoveEnemy);
            t1.Start(Enemy);
            
           
        }

        private void NewGame()
        {
            LoadToDB();
            for (int i = 0; i < this.Layout.Children.Count(x => x.StyleId.Contains("Enemy")); i++)
            {
                var a = this.Layout.Children.First(x => x.StyleId.Contains("Enemy"));
                this.Layout.Children.Remove(a);
                
                
            }

            this.Player.TranslationY = (double)AbsoluteLayout.HeightProperty.DefaultValue / 2;

            isGameStarted = true;
            SpawnEnemy();


        }

        private void LoadToDB()
        {
            var dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
            using (var db = new ApllicationContext(dbPath))
            {
                DbModel dbModel = new DbModel() { Name = "bla", Score = Score }; 
                db.DbModels.Add(dbModel);
                db.SaveChanges();
            }
        }

        private void ChangePicture()
        {
            int j = 2;
            while (true)
            {

                string src = $"pic{j.ToString()}.png";
                Image = ImageSource.FromFile(src);
                j = j == 4 ? 1 : ++j;
                Thread.Sleep(50);
               
                
            }
        }

        private async void MoveEnemy(object sender)
        {
            bool isEnemyAnimationEnded = true;
            var enemy = (sender as Image);
            while (isGameStarted)
            {
                if (isEnemyAnimationEnded)
                {
                    isEnemyAnimationEnded = false;
                    await enemy.TranslateTo(this.Layout.Width * -1, 0, 4000);
                    isEnemyAnimationEnded = true;
                    (sender as Image).TranslationX = Layout.Bounds.Right;
                    //if (!tmp)
                    //{
                    //    
                    //    
                    //}
                    Debug.WriteLine("Move enemy otside animation");

                }
            }
        }

        [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
        private async void JumpAnimation()
        {
            //if(t != null)
            //t.Abort();
            //t = null;
            if (isGameStarted)
            {
                isJumpAnimation = true;
                await this.Player.TranslateTo(0, -200, 500);
                await this.Player.TranslateTo(0, 0, 1500);
                //t = new Thread(ChangePicture);
                //t.Start();
                isJumpAnimation = false;
            }
            else
            {
                isGameStarted = true;
                NewGame();

            }

        }
    }
}
