using Android.Content;
using Android.Widget;

namespace BroadCast.CustomControls
{
    public class SquareImageView : ImageView
    {
        Context mContext;
        public SquareImageView(Context context) : base(context)
        {
            init(context, null);
        }

        public SquareImageView(Context context, Android.Util.IAttributeSet attrs) : base(context, attrs)
        {
            init(context, attrs);
        }

        public SquareImageView(Context context, Android.Util.IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            init(context, attrs);
        }

        public SquareImageView(Context context, Android.Util.IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            init(context, attrs);
        }

        private void init(Context ctx, Android.Util.IAttributeSet attrs)
        {
            mContext = ctx;
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, widthMeasureSpec); // This is the key that will make the height equivalent to its width
        }
    }
}