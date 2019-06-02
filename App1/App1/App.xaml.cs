using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static DragonRun.Setting;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DragonRun
{
    public partial class App : Application
    {
        public const string DBFILENAME = "DbModelsapp.db";
        public string dbPath;
        public App()
        {
            InitializeComponent();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME);
            using (var db = new DragonRun.ApllicationContext(dbPath))
            {
                db.Database.EnsureCreated();
            }
                MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
