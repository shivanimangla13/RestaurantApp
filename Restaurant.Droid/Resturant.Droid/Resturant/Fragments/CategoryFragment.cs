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

namespace com.resturant.Droid.Resturant.Fragments
{
    [Activity(Label = "CategoryFragment")]

    public class CategoryFragment : Android.Support.V4.App.Fragment
    {
        public CategoryFragment()
        {
            

        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container,
                            Bundle savedInstanceState)
        {
            // Inflate the layout for this fragment
            View view = inflater.Inflate(Resource.Layout.fragment_categories, container, false);
            ((MainActivity)this.Activity).Title = "Category";


            return view;
        }
    }
}