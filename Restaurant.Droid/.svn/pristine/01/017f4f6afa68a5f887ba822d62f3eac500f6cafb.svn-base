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
using Android.Support.V7.Widget;

namespace com.resturant.Droid.Resturant.Activities
{
    [Activity(Label = "Select Address")]
    public class SelectAddress : AppCompatActivity
    {
        Session_management session_management;
        LinearLayout back, addAddress, delieverHere;
        RecyclerView recycleraddressList;
        ProgressDialog progressDialog;
        String dName, dId, addId;
        String user_id;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SupportActionBar.Hide();
            SetContentView(Resource.Layout.activity_add_address);
        }
    }
}