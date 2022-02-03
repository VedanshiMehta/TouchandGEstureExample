using Android.Views;

namespace TouchandGEstureExample
{
    class MyScaleListner : ScaleGestureDetector.SimpleOnScaleGestureListener
    {
        private readonly GestureRecognizerView gestrec;


        public MyScaleListner(GestureRecognizerView view)
        {
            gestrec = view;
        }

        public override bool OnScale(ScaleGestureDetector detector)
        {
            gestrec._scaleFactor *= detector.ScaleFactor;

            if (gestrec._scaleFactor > 5.0f)
            {
                gestrec._scaleFactor = 5.0f;

            }

            if (gestrec._scaleFactor < 0.1f)
            {

                gestrec._scaleFactor = 0.1f;

            }
            gestrec.Invalidate();
            return true;
        }
    }
}