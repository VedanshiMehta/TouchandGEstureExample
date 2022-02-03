using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
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
    public class GestureRecognizerView : View
    {
       
       private static readonly int InvalidPointerId = -1;

        private readonly Drawable _icon;
        private readonly ScaleGestureDetector _scaleDetector;

        private int _activePointerId = InvalidPointerId;
        private float _lastTouchX;
        private float _lastTouchY;
        private float _posX;
        private float _posY;
        public float _scaleFactor = 1.0f;

        [Obsolete]
        public GestureRecognizerView(Context context) : base(context)
        {
            _icon = Resources.GetDrawable(Resource.Drawable.ic_bedtime);
            _icon.SetBounds(0, 0, _icon.IntrinsicWidth, _icon.IntrinsicHeight);
            _scaleDetector = new ScaleGestureDetector(context, new MyScaleListner(this));
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            canvas.Save();
            canvas.Translate(_posX, _posY);
            canvas.Scale(_scaleFactor, _scaleFactor);
            _icon.Draw(canvas);
            canvas.Restore();
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            _scaleDetector.OnTouchEvent(e);

            MotionEventActions actions = e.Action & MotionEventActions.Mask;
            int pointerIndex;

            switch (actions)
            {
                case MotionEventActions.Down:
                    _lastTouchX = e.GetX();
                    _lastTouchX = e.GetY();
                    _activePointerId = e.GetPointerId(0);

                    break;

                case MotionEventActions.Move:
                    pointerIndex = e.FindPointerIndex(_activePointerId);
                    float x = e.GetX(pointerIndex);
                    float y = e.GetY(pointerIndex);
                    if (!_scaleDetector.IsInProgress)
                    {
                        float deltaX = x - _lastTouchX;
                        float deltaY = y - _lastTouchY;
                        _posX += deltaX;
                        _posY += deltaY;
                        Invalidate();

                    }

                    _lastTouchX = x;
                    _lastTouchY = y;
                    break;

                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                    _activePointerId = InvalidPointerId;
                    break;

                case MotionEventActions.PointerUp:

                    // check to make sure that the pointer that went up is for the gesture we're tracking.
                    pointerIndex = (int)(e.Action & MotionEventActions.PointerIndexMask) >> (int)MotionEventActions.PointerIndexShift;

                    int pointerId = e.GetPointerId(pointerIndex);

                    if (pointerId == _activePointerId)
                    {
                        // This was our active pointer going up. Choose a new
                        // action pointer and adjust accordingly
                        int newpointerIndex = pointerIndex == 0 ? 1 : 0;
                        _lastTouchX = e.GetX(pointerIndex);
                        _lastTouchX = e.GetY(pointerIndex);
                        _activePointerId = e.GetPointerId(newpointerIndex);
                    }
                    break;
            }

            return true;

        }
    }

        
    
}