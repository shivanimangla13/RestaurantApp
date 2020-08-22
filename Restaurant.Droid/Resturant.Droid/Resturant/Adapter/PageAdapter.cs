using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using com.resturant.Droid.Resturant.Fragments;

namespace com.resturant.Droid.Resturant.Adapter
{
    public class PageAdapter : FragmentPagerAdapter
    {
        public int numsoftabs;

        public PageAdapter(Android.Support.V4.App.FragmentManager fm, int numsoftabs):base(fm)
        {
            this.numsoftabs = numsoftabs;
        }

        public override int Count => numsoftabs;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            switch (position)
            {

                case 0:
                    return new Top_Deals_Fragment();
                default:
                    return null;
            }
        }
    }
}