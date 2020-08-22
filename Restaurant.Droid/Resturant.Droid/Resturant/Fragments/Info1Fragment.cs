using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace com.resturant.Droid.Resturant.Fragments
{
    public class Info1Fragment : Fragment
    {
        public Info1Fragment()
        {
        }
        public static Info1Fragment newInstance()
        {
            Info1Fragment fragment = new Info1Fragment();
            Bundle args = new Bundle();
            fragment.Arguments = args;
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_info1, container, false);
        }
    }
}