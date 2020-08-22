using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using com.resturant.Droid.Resturant.Fragments;
using DevMike.JadeCom;
using Java.Lang;

namespace com.resturant.Droid.Resturant.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class InfoActivity : AppCompatActivity, ViewPager.IOnPageChangeListener
    {
        public static ViewPager vpPager;
        MyPagerAdapter adapterViewPager;
        public static TextView btnNext;
        TextView btnSkip;
        int selectPage = 0;
        Session_management sessionManager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_info);
            btnNext = (TextView)FindViewById(Resource.Id.btn_next);
            btnSkip = (TextView)FindViewById(Resource.Id.btn_skip);
            vpPager = (ViewPager)FindViewById(Resource.Id.vpPager);
            sessionManager = new Session_management(this);
            Android.Support.V4.App.FragmentManager fragmentManager = SupportFragmentManager;
            adapterViewPager = new MyPagerAdapter(fragmentManager);
            vpPager.Adapter = adapterViewPager;
            PageStepIndicator extensiblePageIndicator = (PageStepIndicator)FindViewById(Resource.Id.page_stepper);
            extensiblePageIndicator.SetupWithViewPager(vpPager);
            vpPager.SetOnPageChangeListener(this);
            btnNext.Click += BtnNext_Click;
            btnSkip.Click += BtnSkip_Click;
            if (sessionManager.getBooleanData())
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                Finish();
            };
        }

        private void BtnSkip_Click(object sender, EventArgs e)
        {
            sessionManager.setBooleanData(true);
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (selectPage == 0)
            {
                vpPager.CurrentItem = 1;
            }
            else if (selectPage == 1)
            {
                vpPager.CurrentItem = 2;
            }
            else if (selectPage == 2)
            {
                sessionManager.setBooleanData(true);
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                Finish();
            }
        }

        public void OnPageScrollStateChanged(int state)
        {

        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
        }

        public void OnPageSelected(int position)
        {
            selectPage = position;

            if (position == 0 || position == 1)
            {
                btnSkip.Visibility = ViewStates.Visible;
                btnNext.Text = "Next";
            }
            else if (position == 2)
            {
                btnSkip.Visibility = ViewStates.Gone;
                btnNext.Text = "Finish";
            }
        }
    }
    public class MyPagerAdapter : FragmentPagerAdapter
    {
        private int NUM_ITEMS = 3;

        public MyPagerAdapter(Android.Support.V4.App.FragmentManager fragmentManager) : base(fragmentManager)
        {

        }
        public override int Count => NUM_ITEMS;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0:
                    return Info1Fragment.newInstance();
                case 1:
                    return Info2Fragment.newInstance();
                case 2:
                    return Info3Fragment.newInstance();
                default:
                    return null;
            }
        }
        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String("Page " + position);
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            AndroidX.Fragment.App.Fragment fragment = (AndroidX.Fragment.App.Fragment)base.InstantiateItem(container, position);
            return fragment;
        }
    }
}