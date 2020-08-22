﻿using System;
using System.Collections.Generic;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.Navigation;
using AndroidX.Navigation.UI;
using com.resturant.Droid.Resturant.Activities;
using com.resturant.Droid.Resturant.Config;
using com.resturant.Droid.Resturant.Fragments;
using com.resturant.Droid.Resturant.utils;

namespace com.resturant.Droid
{
    [Activity(Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        public static BottomNavigationView navigation;
        private Session_management sessionManagement;
        LinearLayout My_Order, My_Reward, btn_checkout, My_Cart;
        private IMenu nav_menu;
        public NavigationView navigationView;
        LinearLayout viewpa;
        TextView username;
        private ImageView iv_profile;
        ActionBarDrawerToggle toggle;
        DrawerLayout drawer;
        public static TextView totalBudgetCount;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            sessionManagement = new Session_management(this);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            //SupportActionBar.SetTitle(Resource.String.app_name);

            drawer = (DrawerLayout)FindViewById(Resource.Id.drawer_layout);

            toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.SetDrawerListener(toggle);
            toggle.SyncState();

            navigationView = (NavigationView)FindViewById(Resource.Id.nav_view);

            IMenu m = navigationView.Menu;
            for (int i = 0; i < m.Size(); i++)
            {
                IMenuItem mi = m.GetItem(i);

                //for aapplying a font to subMenu ...
                ISubMenu subMenu = mi.SubMenu;
                if (subMenu != null && subMenu.Size() > 0)
                {
                    for (int j = 0; j < subMenu.Size(); j++)
                    {
                        IMenuItem subMenuItem = subMenu.GetItem(j);
                    }
                }
            }

            View headerView = navigationView.GetHeaderView(0);
            navigationView.Background.SetColorFilter(Android.Graphics.Color.Rgb(128, 0, 0), PorterDuff.Mode.Multiply);
            navigationView.SetNavigationItemSelectedListener(this);
            nav_menu = navigationView.Menu;
            View header = ((NavigationView)FindViewById(Resource.Id.nav_view)).GetHeaderView(0);
            viewpa = (LinearLayout)header.FindViewById(Resource.Id.viewpa);
            if (sessionManagement.isLoggedIn())
            {
                viewpa.Visibility = ViewStates.Visible;
            }

            My_Order = (LinearLayout)header.FindViewById(Resource.Id.my_orders);
            My_Reward = (LinearLayout)header.FindViewById(Resource.Id.my_reward);
            btn_checkout = (LinearLayout)header.FindViewById(Resource.Id.checkout);
            My_Cart = (LinearLayout)header.FindViewById(Resource.Id.my_cart);
            iv_profile = (ImageView)header.FindViewById(Resource.Id.iv_header_img);
            username = (TextView)header.FindViewById(Resource.Id.tv_header_name);
            totalBudgetCount = (TextView)FindViewById(Resource.Id.totalBudgetCount);

            My_Order.Click += delegate
              {

                  if (sessionManagement.isLoggedIn())
                  {
                      Intent i = new Intent(this, typeof(My_Order_activity));
                      StartActivity(i);
                  }
                  else
                  {
                      Intent i = new Intent(this, typeof(LoginActivity));
                      StartActivity(i);
                      OverridePendingTransition(0, 0);
                  };


              };
            My_Reward.Click += delegate
            {

                if (sessionManagement.isLoggedIn())
                {
                    Intent i = new Intent(this, typeof(Reward_fragment));
                    StartActivity(i);
                }
                else
                {
                    Intent i = new Intent(this, typeof(LoginActivity));
                    StartActivity(i);
                    OverridePendingTransition(0, 0);
                };
            };
            btn_checkout.Click += delegate
            {
                if (sessionManagement.isLoggedIn())
                {
                    Intent i = new Intent(this, typeof(OrderSummary));
                    StartActivity(i);
                }
                else
                {
                    Intent i = new Intent(this, typeof(LoginActivity));
                    StartActivity(i);
                    OverridePendingTransition(0, 0);
                };


            };
            My_Cart.Click += delegate
            {

                if (sessionManagement.isLoggedIn())
                {
                    Intent i = new Intent(this, typeof(CartFragment));
                    StartActivity(i);
                }
                else
                {
                    Intent i = new Intent(this, typeof(LoginActivity));
                    StartActivity(i);
                    OverridePendingTransition(0, 0);
                };

            };

            iv_profile.Click += delegate
            {
                if (sessionManagement.isLoggedIn())
                {
                    Edit_profile_fragment fm = new Edit_profile_fragment();
                    Android.Support.V4.App.FragmentManager fragmentManager = SupportFragmentManager;
                    fragmentManager.BeginTransaction().Replace(Resource.Id.contentPanel, fm)
                            .AddToBackStack(null).Commit();
                }
                else
                {
                    Intent i = new Intent(this, typeof(LoginActivity));
                    StartActivity(i);
                    OverridePendingTransition(0, 0);
                };
            };


            sideMenu();

            if (savedInstanceState == null)
            {
                HomeeeFragment fm = new HomeeeFragment();
                Android.Support.V4.App.FragmentManager fragmentManager = SupportFragmentManager;
                fragmentManager.BeginTransaction()
                        .Replace(Resource.Id.contentPanel, fm, "Home_fragment")
                        .SetTransition(Android.Support.V4.App.FragmentTransaction.TransitFragmentOpen)
                        .Commit();
            }
            SupportFragmentManager.BackStackChanged += SupportFragmentManager_BackStackChanged;
            initComponent();
            loadFragment(new HomeeeFragment());
        }

        public override bool OnSupportNavigateUp()
        {
            //NavController navController = Navigation.FindNavController(this, Resource.Id.nav_host_fragment);
            //return NavigationUI.NavigateUp(navController, mAppBarConfiguration) || base.OnSupportNavigateUp();
            return false;
        }

        private void SupportFragmentManager_BackStackChanged(object sender, EventArgs e)
        {
            OnBackStackChanged();
        }

        public void sideMenu()
        {

            if (sessionManagement.isLoggedIn())
            {
                nav_menu.FindItem(Resource.Id.nav_logout).SetVisible(true);
                nav_menu.FindItem(Resource.Id.nav_my_profile).SetVisible(true);
                nav_menu.FindItem(Resource.Id.sign).SetVisible(false);
                nav_menu.FindItem(Resource.Id.nav_powerd).SetVisible(true);
                nav_menu.FindItem(Resource.Id.nav_my_reward).SetVisible(true);
                username.Text = sessionManagement.getUserDetails().Get(BaseURL.KEY_NAME).ToString();
            }
            else
            {
                nav_menu.FindItem(Resource.Id.nav_my_reward).SetVisible(false);
                nav_menu.FindItem(Resource.Id.login).SetVisible(false);
                nav_menu.FindItem(Resource.Id.nav_my_profile).SetVisible(false);
                nav_menu.FindItem(Resource.Id.nav_logout).SetVisible(false);
                nav_menu.FindItem(Resource.Id.sign).SetVisible(true);
            }
        }


        [Obsolete]
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            Android.Support.V4.App.Fragment fm = null;
            Bundle args = new Bundle();

            if (id == Resource.Id.sign)
            {
                Intent i = new Intent(this, typeof(LoginActivity));
                StartActivity(i);
            }

            else if (id == Resource.Id.nav_logout)
            {
                sessionManagement.logoutSession();
                Finish();
            }
            if (id == Resource.Id.nav_aboutus)
            {
                fm = new About_us_fragment();
            }
            if (id == Resource.Id.nav_policy)
            {
                fm = new Terms_and_Condition_fragment();
            }
            if (id == Resource.Id.nav_share)
            {
                Intent sendIntent = new Intent();
                sendIntent.SetAction(Intent.ActionSend);
                sendIntent.PutExtra(Intent.ExtraText, "Hi friends i am using ." + " http://play.google.com/store/apps/details?id=" + "" + " APP"); //getPackageName()
                sendIntent.SetType("text/plain");
                StartActivity(sendIntent);

            }

            else if (id == Resource.Id.navigation_home)
            {
                loadFragment(new HomeeeFragment());

                HomeeeFragment appNewsHome1Fragment = new HomeeeFragment();
                Android.Support.V4.App.FragmentManager manager = SupportFragmentManager;
                Android.Support.V4.App.FragmentTransaction transaction = manager.BeginTransaction();
                transaction.Replace(Resource.Id.contentPanel, appNewsHome1Fragment);
                transaction.Commit();
                return true;
            }

            /* else if (id == Resource.Id.nav_shop_now) {
                 fm = new Shop_Now_fragment();
             } */
            else if (id == Resource.Id.nav_my_profile)
            {
                fm = new Edit_profile_fragment();

            }
            //        else if (id == Resource.Id.nav_aboutus)
            //        {
            //            //            toolbar.setTitle("About");
            //            StartActivity(new Intent(this, About_us));
            //    } else if (id == Resource.Id.nav_policy) {
            //        fm = new Terms_and_Condition_fragment();
            //args.putString("url", TermsUrl);
            //        args.putString("title", getResources().getString(R.string.nav_terms));
            //        fm.setArguments(args);
            //    }
            //        else if (id == Resource.Id.nav_review) {
            //            //reviewOnApp();
            //        }
            //        else if (id == Resource.Id.nav_contact) {
            //            fm = new Contact_Us_fragment();
            //args.putString("url", SupportUrl);
            //            args.putString("title", getResources().getString(R.string.nav_terms));
            //            fm.setArguments(args);

            //        }
            //        else if (id == Resource.Id.nav_review) {
            //            reviewOnApp();
            //        }
            //else if (id == Resource.Id.nav_share) {
            //    shareApp();

            //        else if (id == Resource.Id.nav_powerd) {
            //            // stripUnderlines(textView);
            //            String url = "http://sameciti.com";
            //            Intent i = new Intent(Intent.ACTION_VIEW);
            //            i.setData(Uri.parse(url));
            //            startActivity(i);
            //            finish();
            //        }

            if (fm != null)
            {
                Android.Support.V4.App.FragmentManager fragmentManager = SupportFragmentManager;
                fragmentManager.BeginTransaction().Replace(Resource.Id.contentPanel, fm)
                                                .AddToBackStack(null).Commit();
            }

            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        public void OnBackStackChanged()
        {
            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            var currentFocus = Window.CurrentFocus;
            if (currentFocus != null)
            {
                inputMethodManager.HideSoftInputFromWindow(currentFocus.WindowToken, 0);
            }
            Android.Support.V4.App.Fragment fr = SupportFragmentManager.FindFragmentById(Resource.Id.contentPanel);

            string fm_name = fr.Class.SimpleName;
            if (fm_name.Contains("Home_fragment"))
            {
                drawer.SetDrawerLockMode(DrawerLayout.LockModeUnlocked);
                toggle.DrawerIndicatorEnabled = true;
                SupportActionBar.SetDisplayHomeAsUpEnabled(false);
                toggle.SyncState();

            }
            else
            {

                drawer.SetDrawerLockMode(DrawerLayout.LockModeUnlocked);

                toggle.DrawerIndicatorEnabled = true;
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                toggle.SyncState();


            }
            //initComponent();
            //loadFragment(new HomeeeFragment());
        }

        private void loadFragment(Android.Support.V4.App.Fragment fragment)
        {
            this.SupportFragmentManager.BeginTransaction()
                    .Replace(Resource.Id.contentPanel, fragment)
                    .CommitAllowingStateLoss();
        }

        private void initComponent()
        {
            navigation = (BottomNavigationView)FindViewById(Resource.Id.nav_view12);
            navigation.NavigationItemSelected += delegate (object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
            {

                int id = e.Item.ItemId;
                switch (id)
                {
                    case Resource.Id.navigation_home:
                        loadFragment(new HomeeeFragment());
                        HomeeeFragment appNewsHome1Fragment = new HomeeeFragment();
                        Android.Support.V4.App.FragmentManager manager = SupportFragmentManager;
                        Android.Support.V4.App.FragmentTransaction transaction = manager.BeginTransaction();
                        transaction.Replace(Resource.Id.contentPanel, appNewsHome1Fragment);
                        transaction.Commit();
                        break;
                    case Resource.Id.navigation_dashboard:
                        loadFragment(new CategoryFragment());
                        //loadFragment(new CategoryFragment());
                        //CategoryFragment catFragment = new CategoryFragment();
                        //Android.Support.V4.App.FragmentManager manager = SupportFragmentManager;
                        //Android.Support.V4.App.FragmentTransaction transaction = manager.BeginTransaction();
                        //transaction.Replace(Resource.Id.contentPanel, catFragment);
                        //transaction.Commit();
                        break;
                    case Resource.Id.navigation_notifications1:
                        loadFragment(new SearchFragment());
                        break;
                    case Resource.Id.navigation_notifications12:
                        loadFragment(new NotificationFragment());

                        break;
                    case Resource.Id.navigation_notifications123:
                        loadFragment(new CartFragment());

                        break;
                }
            };
        }
    }
}

