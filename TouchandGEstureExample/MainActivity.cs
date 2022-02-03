using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace TouchandGEstureExample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ImageView myTouch;
        private TextView myView;
        private Button button1;
        private Button button2;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            myTouch = FindViewById<ImageView>(Resource.Id.imageView1);
            myView = FindViewById<TextView>(Resource.Id.textView1);
            button1 = FindViewById<Button>(Resource.Id.button1);
            button2 = FindViewById<Button>(Resource.Id.button2);

            button1.Click += Button_Click;
            button2.Click += Button2_Click;
            myTouch.Touch += MyTouch_Touch1;

        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            Intent j = new Intent(Application.Context, typeof(GestureRecogActivitycs));
            StartActivity(j);
        }

        private void MyTouch_Touch1(object sender, Android.Views.View.TouchEventArgs e)
        {
            string message;

            switch (e.Event.Action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                case MotionEventActions.Move:
                    message = "Touch Begins";
                    break;

                case MotionEventActions.Up:
                    message = "Touch Ends";
                    break;

                default:
                    message = string.Empty;
                    break;

            }

            myView.Text = message;
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            Intent i = new Intent(Application.Context, typeof(CustomGestureRecognizer));
            StartActivity(i);
        }


    }
}