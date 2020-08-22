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
using com.resturant.Droid.Resturant.Config;
using Android.Support.Design.Widget;

namespace com.resturant.Droid.Resturant.Activities
{
    [Activity(Label = "My_Order_activity")]
    public class My_Order_activity : AppCompatActivity
    {

        int padding = 0;
        ImageView back_button;
        ISharedPreferences sharedPreferences;
        ISharedPreferencesEditor editor;
        string moblie;
        ProgressDialog progressDialog;

        protected override void AttachBaseContext(Context newBase)
        {
            newBase = LocaleHelper.onAttach(newBase);
            base.AttachBaseContext(newBase);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_my__oreder_activity);
            // Create your application here

            Android.Support.V7.Widget.Toolbar toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.Title = "My Order";
            progressDialog = new ProgressDialog(this);

            toolbar.Click += delegate
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                OverridePendingTransition(0, 0);
                Finish();
            };
            sharedPreferences = GetSharedPreferences(BaseURL.MyPrefreance, FileCreationMode.Private);
            moblie = sharedPreferences.GetString(BaseURL.KEY_MOBILE, null);

            TabLayout tabLayout = (TabLayout)FindViewById(Resource.Id.tab_layout1);

            tabLayout.AddTab(tabLayout.NewTab().SetText("Pending"));
            tabLayout.AddTab(tabLayout.NewTab().SetText("Past Order"));
            tabLayout.TabGravity = TabLayout.GravityFill;
        }

        public void setTitle(string title)
        {
            SupportActionBar.Title = title;
        }

        public bool OnKeyDown(int keyCode, KeyEvent ev)
        {
            if (keyCode == (int)Keycode.Back && ev.RepeatCount == 0)
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                OverridePendingTransition(0, 0);
                Finish();
                return true;

            }
            return OnKeyDown(keyCode, ev);
        }

        public void wrapTabIndicatorToTitle(TabLayout tabLayout, int externalMargin, int internalMargin)
        {
            View tabStrip = tabLayout.GetChildAt(0);
            if (tabStrip is ViewGroup)
            {
                ViewGroup tabStripGroup = (ViewGroup)tabStrip;
                int childCount = ((ViewGroup)tabStrip).ChildCount;
                for (int i = 0; i < childCount; i++)
                {
                    View tabView = tabStripGroup.GetChildAt(i);
                    tabView.SetMinimumWidth(0);
                    tabView.SetPadding(0, tabView.PaddingTop, 0, tabView.PaddingBottom);
                    if (tabView.LayoutParameters is ViewGroup.MarginLayoutParams)
                    {
                        ViewGroup.MarginLayoutParams layoutParams = (ViewGroup.MarginLayoutParams)tabView.LayoutParameters;
                        if (i == 0)
                        {
                            setMargin(layoutParams, externalMargin, internalMargin);
                        }
                        else if (i == childCount - 1)
                        {
                            setMargin(layoutParams, internalMargin, externalMargin);
                        }
                        else
                        {
                            setMargin(layoutParams, internalMargin, internalMargin);
                        }
                    }
                }

                tabLayout.RequestLayout();
            }
        }
        private void setMargin(ViewGroup.MarginLayoutParams layoutParams, int start, int end)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
            {
                layoutParams.MarginStart = start;
                layoutParams.MarginEnd = end;
            }
            else
            {
                layoutParams.LeftMargin = start;
                layoutParams.RightMargin = end;
            }
        }
    }
}