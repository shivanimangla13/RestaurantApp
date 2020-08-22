using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using RestrauntApp.Adapters;

namespace RestrauntApp.Fragments
{

    public class RestrauntSearchFragment : Android.Support.V4.App.Fragment
    {
        Android.Support.V7.Widget.SearchView SearchView;
        ListView ListViewRestrauntSearch;
        RestrauntSearchListItemAdapter adapter;
        List<string> RestrauntList;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RestrauntList = new List<string> { 
            "test","test","test","test"
            };
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            //var view = inflater.Inflate(Resource.Layout.restraunt_search_layout, null);
            //ListViewRestrauntSearch = view.FindViewById<ListView>(Resource.Id.listViewRestraunt);
            //adapter = new RestrauntSearchListItemAdapter(this.Context, RestrauntList);
            //ListViewRestrauntSearch.Adapter = adapter;

            //  adapter = new RestrauntSearchListItemAdapter(DirectoryModelList, activity, activity);

            return null;
        }
    }
}