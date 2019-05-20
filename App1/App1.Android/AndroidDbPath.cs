using System;
using System.IO;
using Xamarin.Forms;
using App1.Droid;
using DragonRun;


[assembly: Dependency(typeof(AndroidDbPath))]
namespace App1.Droid
{
    class AndroidDbPath : IPath
    {
        public string GetDatabasePath(string filename)
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);
        }
    }
}