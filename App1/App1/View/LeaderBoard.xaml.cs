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
    public partial class LeaderBoard : ContentPage
    {
        public string dbPath = "";
        public LeaderBoard()
        {
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            string dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
            using (ApllicationContext db = new ApllicationContext(dbPath))
            {
                leaderBoard.ItemsSource = db.DbModels.ToList();
            }
            base.OnAppearing();
        }
    }
}