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
        List<DbModel> dbModels = new List<DbModel>();
        public string dbPath = "";
        Label label = new Label();
		public LeaderBoard ()
		{
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
            InitializeComponent ();
            
       
        }
        private void GetDataFromDB()
        {
            using (var db = new ApllicationContext(dbPath))
            {
                dbModels = db.DbModels.ToList();
            }

            foreach (var a in dbModels)
            {
                label.Text = a.Name;
            }
            
        }
        public void CreateInterface()
        {
            Content = label;

        }
	}
}