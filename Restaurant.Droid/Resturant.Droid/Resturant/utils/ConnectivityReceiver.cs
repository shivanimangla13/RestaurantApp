using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace com.resturant.Droid.Resturant.utils
{
    public class ConnectivityReceiver : BroadcastReceiver
    {
        public static ConnectivityReceiverListener connectivityReceiverListener;
        public override void OnReceive(Context context, Intent intent)
        {
            ConnectivityManager cm = (ConnectivityManager)context
                .GetSystemService(Context.ConnectivityService);
            NetworkInfo activeNetwork = cm.ActiveNetworkInfo;
            bool isConnected = activeNetwork != null
                    && activeNetwork.IsConnectedOrConnecting;

            if (connectivityReceiverListener != null)
            {
                connectivityReceiverListener.onNetworkConnectionChanged(isConnected);
            }
        }

        public interface ConnectivityReceiverListener
        {
            void onNetworkConnectionChanged(bool isConnected);
        }
    }
}