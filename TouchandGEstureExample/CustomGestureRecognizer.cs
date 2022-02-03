using Android.App;
using Android.Content;
using Android.Gestures;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TouchandGEstureExample
{
  
        [Activity(Label = "CustomGestureRecognizer", Theme = "@style/AppTheme")]
        public class CustomGestureRecognizer : Activity, GestureOverlayView.IOnGesturePerformedListener
        {
            private GestureLibrary mygestureLib;
            public ImageView myimageView;



            protected override void OnCreate(Bundle savedInstanceState)
            {
                GestureOverlayView gestureoverlay = new GestureOverlayView(this);
                gestureoverlay.AddOnGesturePerformedListener(this);

                base.OnCreate(savedInstanceState);
                SetContentView(gestureoverlay);

                View v = LayoutInflater.Inflate(Resource.Layout.customGesture, null);
                myimageView = FindViewById<ImageView>(Resource.Id.imageView1);
                gestureoverlay.AddView(v);


                mygestureLib = GestureLibraries.FromRawResource(this, Resource.Raw.gestures);
                if (!mygestureLib.Load())
                {
                    Log.Wtf(GetType().FullName, "There was a problem loading the gesture library.");
                    Finish();
                }
            }



            public void OnGesturePerformed(GestureOverlayView overlay, Gesture gesture)
            {
                ImageView myimageView = FindViewById<ImageView>(Resource.Id.imageView1);

                IEnumerable<Prediction> predictions = from p in mygestureLib.Recognize(gesture)
                                                      orderby p.Score descending
                                                      where p.Score > 1.0
                                                      select p;
                Prediction prediction = predictions.FirstOrDefault();

                if (prediction == null)
                {
                    Log.Debug(GetType().FullName, "Nothing seemed to match the user's gesture, so don't do anything.");
                    return;
                }

                Log.Debug(GetType().FullName, "Using the prediction named {0} with a score of {1}.", prediction.Name, prediction.Score);

                if (prediction.Name.StartsWith("checkme"))
                {
                    myimageView.SetImageResource(Resource.Drawable.ic_checkedme);
                }
                else if (prediction.Name.StartsWith("zerome"))
                {

                    myimageView.SetImageResource(Resource.Drawable.ic_checkme);
                }
            }
        }
    
}