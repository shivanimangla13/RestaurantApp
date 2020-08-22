using System;
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
using System.Drawing;
using com.resturant.Droid.Resturant.Config;
using Android.Support.V7.Widget;
using Xamarin.Essentials;
using Java.Util;
using Org.Json;
using com.resturant.Droid.Resturant.utils;
using static Volley.Request;
using Volley;
using Android.Util;
using Newtonsoft.Json;
using Android.Text;
using Java.Lang;
using Java.IO;
using Android.Provider;
using Android.Database;
using Android.Graphics;
using System.IO;
using System.Text.RegularExpressions;

namespace com.resturant.Droid.Resturant.Fragments
{
    [Activity(Label = "Contact_Us_fragment")]
    public class Contact_Us_fragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container,
                            Bundle savedInstanceState)
        {
            // Inflate the layout for this fragment
            View view = inflater.Inflate(Resource.Layout.fragment_contact_us, container, false);

           

            ((MainActivity)this.Activity).Title = "About Us";


            return View;
        }
    }
}