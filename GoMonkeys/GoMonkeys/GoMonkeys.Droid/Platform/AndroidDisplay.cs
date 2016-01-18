
using Android.App;
using Xamarin.Forms;
using Android.Util;

[assembly: Dependency(typeof(GoMonkeys.AndroidDisplay))]
namespace GoMonkeys
{
    public class AndroidDisplay : IDisplay
    {
        readonly float height;
        readonly float width;
        public AndroidDisplay()
        {
            var displaymetrics = new DisplayMetrics();
            var defaultDisplay = ((Activity)Forms.Context).WindowManager.DefaultDisplay;
            defaultDisplay.GetMetrics(displaymetrics);

            height = displaymetrics.HeightPixels / displaymetrics.Density;
            width = displaymetrics.WidthPixels / displaymetrics.Density;
        }

        Xamarin.Forms.Size IDisplay.Size
        {
            get
            {
                return new Xamarin.Forms.Size(width, height);
            }
        }
    }
}