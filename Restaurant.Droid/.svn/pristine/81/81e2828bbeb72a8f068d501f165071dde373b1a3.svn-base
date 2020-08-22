using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Java.Util;
using System;
using System.Collections.Generic;
using Volley;

namespace com.resturant.Droid.Resturant.utils
{
#if DEBUG
    [Application(Debuggable =true)]
#else
    [Application(Debuggable =false)]
#endif

    public class AppController : Application
    {
        public static string TAG = typeof(AppController).Name;
        private RequestQueue mRequestQueue;

        private static AppController mInstance;

        public AppController(IntPtr intPtr,JniHandleOwnership transfer) : base(intPtr, transfer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();
            mInstance = this;
            List<Locale> locales = new List<Locale>();
            locales.Add(Locale.English);
            locales.Add(new Locale("ar", "ARABIC"));
            LocaleHelper.setLocale(this, "en");
        }
       

        public static AppController getInstance()
        {
            return mInstance;
        }

       
        public RequestQueue getRequestQueue()
        {
            if (mRequestQueue == null)
            {
                mRequestQueue = Volley.Toolbox.Volley.NewRequestQueue(this);
            }

            return mRequestQueue;
        }
        public void addToRequestQueue(Request req, string tag)
        {
            req.SetTag(TextUtils.IsEmpty(tag) ? TAG : tag);
            getRequestQueue().Add(req);
        }

        public void addToRequestQueue(Request req)
        {
            req.SetTag(TAG);
            getRequestQueue().Add(req);
        }

        public void setConnectivityListener(ConnectivityReceiver.ConnectivityReceiverListener listener)
        {
            ConnectivityReceiver.connectivityReceiverListener = listener;
        }
    }
}