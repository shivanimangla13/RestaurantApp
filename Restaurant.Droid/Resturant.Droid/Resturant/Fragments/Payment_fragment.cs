﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V7.App;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace com.resturant.Droid.Resturant.Fragments
{
    [Activity(Label = "Payment_fragment")]
    public class Payment_fragment : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
           // SetContentView(Resource.Layout.fragment_p);
            // Create your application here
        }
    }
}