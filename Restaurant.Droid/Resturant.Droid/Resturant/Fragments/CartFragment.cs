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
using Android.Support.V7.Widget;
using com.resturant.Droid.Resturant.Adapter;
using com.resturant.Droid.Model;
using com.resturant.Droid.Resturant.utils;
using Android.Net;
using com.resturant.Droid.Resturant.Activities;
using com.resturant.Droid.Resturant.Config;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace com.resturant.Droid.Resturant.Fragments
{

    public class CartFragment : Android.Support.V4.App.Fragment
    {
        Button btn_ShopNOw;
        RecyclerView recyclerView;
        LinearLayout ll_Checkout;
        CartAdapter cartAdapter;
        public static RelativeLayout noData, viewCart;
        public static TextView totalItems;
        public static TextView tv_total;

        private List<CartModel> cartList = new List<CartModel>();
        private Session_management sessionManagement;
        public List<StoreCart> StoreCartData = new List<StoreCart>();
        ProgressDialog progressDialog;

        public CartFragment()
        {


        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public  override View OnCreateView(LayoutInflater inflater, ViewGroup container,
                            Bundle savedInstanceState)
        {
            // Inflate the layout for this fragment
            View view = inflater.Inflate(Resource.Layout.fragment_cart, container, false);
            recyclerView = (RecyclerView)view.FindViewById(Resource.Id.recyclerCart);
            btn_ShopNOw = (Button)view.FindViewById(Resource.Id.btn_ShopNOw);
            viewCart = (RelativeLayout)view.FindViewById(Resource.Id.viewCartItems);
            tv_total = (TextView)view.FindViewById(Resource.Id.txt_totalamount);
            totalItems = (TextView)view.FindViewById(Resource.Id.txt_totalQuan);
            noData = (RelativeLayout)view.FindViewById(Resource.Id.noData);
            ((MainActivity)this.Activity).Title = (GetString(Resource.String.cart));
            sessionManagement = new Session_management(Activity);
            sessionManagement.cleardatetime();

            ll_Checkout = (LinearLayout)view.FindViewById(Resource.Id.ll_Checkout);
            getCartData();

            btn_ShopNOw.Click += delegate
            {
                Intent intent = new Intent(Activity, typeof(MainActivity));
                StartActivity(intent);
            };

            ll_Checkout.Click += async delegate
            {
                if (isOnline())
                {
                    if (sessionManagement.isLoggedIn())
                    {
                        if (StoreCartData.Count == 0)
                        {
                            noData.Visibility = ViewStates.Visible;
                            viewCart.Visibility = ViewStates.Gone;
                        }
                        else
                        {
                            progressDialog = new Android.App.ProgressDialog(this.Context);
                            progressDialog.Indeterminate = true;
                            progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
                            progressDialog.SetMessage("Loading...");
                            progressDialog.SetCancelable(false);
                            progressDialog.Show();
                            int UserId = 0;
                            if (sessionManagement.isLoggedIn())
                            {
                                UserId = Convert.ToInt32(sessionManagement.getUserDetails().Get(BaseURL.KEY_ID).ToString());
                            }
                            var processCheckout = BaseURL.ProcessToCheckout + UserId;

                            using (var client = new HttpClient())
                            {
                                StringContent content = new StringContent("");
                                var response = await client.PostAsync(processCheckout, content);
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    Intent intent = new Intent(this.Activity, typeof(OrderSummary));
                                    StartActivity(intent);
                                }
                            }

                            
                        }
                    }
                    else
                    {
                        if (StoreCartData.Count == 0)
                        {
                            noData.Visibility = ViewStates.Visible;
                            viewCart.Visibility = ViewStates.Gone;
                        }
                        else
                        {
                            Intent intent = new Intent(this.Activity, typeof(OrderSummary));
                            StartActivity(intent);
                        }

                    }
                };
            };

            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));

            updateData();
            

            return view;

        }

        public async void getCartData()
        {
            progressDialog = new Android.App.ProgressDialog(this.Context);
            progressDialog.Indeterminate = true;
            progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            progressDialog.SetMessage("Loading...");
            progressDialog.SetCancelable(false);

            sessionManagement = new Session_management(this.Context);
            var UserId = Convert.ToInt32(sessionManagement.getUserDetails().Get(BaseURL.KEY_ID).ToString());
            var getDataUrl = new System.Uri(BaseURL.Get_CartList + UserId);

            using (var client = new HttpClient())
            {
                progressDialog.Show();
                var storeCartResponse = await client.GetStringAsync(getDataUrl);
                sessionManagement.SetStoreCart(storeCartResponse);
                StoreCartData = JsonConvert.DeserializeObject<List<StoreCart>>(storeCartResponse);

                if (sessionManagement.isLoggedIn())
                {

                    if (StoreCartData.Count == 0)
                    {
                        noData.Visibility = ViewStates.Visible;
                        viewCart.Visibility = ViewStates.Gone;
                    }
                }
                else
                {
                    if (StoreCartData.Count == 0)
                    {
                        noData.Visibility = ViewStates.Visible;
                        viewCart.Visibility = ViewStates.Gone;
                    }
                }
                updateData();
                progressDialog.Dismiss();
            }
            Cart_adapter adapter = new Cart_adapter(this.Activity, StoreCartData);
            recyclerView.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

        }

        public void updateData()
        {
            var totalAmount = (from data in StoreCartData select data).Sum(x => x.SelRate * x.Quantity);
            tv_total.Text = Resources.GetString(Resource.String.currency) + " " + totalAmount;
            totalItems.Text = "" + StoreCartData.Count() + "  Total Items:";
        }


        private bool isOnline()
        {
            ConnectivityManager cm = (ConnectivityManager)this.Activity.GetSystemService(Context.ConnectivityService);

            return cm.ActiveNetworkInfo != null && cm.ActiveNetworkInfo.IsConnected;
        }
    }
}