using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Jukusui.RandomGen.Droid;
[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    //protected override void OnCreate(Bundle? savedInstanceState)
    //{
    //    SetFont("RandomGenIcon.ttf");
    //    base.OnCreate(savedInstanceState);
    //}

    //private void SetFont(string fontFileName)
    //{
    //    if (CacheDir != null && Assets != null)
    //    {
    //        string fontPath = Path.Combine(CacheDir.AbsolutePath, fontFileName);
    //        using (Stream asset = Assets.Open(fontFileName))
    //        {
    //            using (FileStream dest = File.Open(fontPath, FileMode.Create))
    //            {
    //                asset.CopyTo(dest);
    //            }
    //        }
    //    }
    //}
}
