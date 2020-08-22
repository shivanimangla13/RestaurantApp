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
using CoolTechWorks.Views.Shimmer;
using com.resturant.Droid.Model;
using com.resturant.Droid.Resturant.Adapter;
using Android.Net;
using Volley;
using Volley.Toolbox;
using com.resturant.Droid.Resturant.Config;
using Java.Lang;
using Org.Json;
using Android.Support.V7.Widget;
using AndroidX.ConstraintLayout.Solver;
using com.resturant.Droid.Resturant.utils;
using System.Net.Http;
using Newtonsoft.Json;

namespace com.resturant.Droid.Resturant.Fragments
{
    public class Top_Deals_Fragment : Android.Support.V4.App.Fragment, Response.IListener, Response.IErrorListener
    {
        private ShimmerRecyclerView rv_top_selling;
        public TextView viewall;
        public ProgressDialog progressDialog;
        public CartAdapter topSellingAdapter;
        private List<CartModel> topSellList = new List<CartModel>();
        string catId, catName;
        public DatabaseHandler dbcart;
        public delegate void ErrorResponse(VolleyError p0);
        public Response.IErrorListener GetErrorListener;
        private Session_management sessionManagement;
        public Top_Deals_Fragment()
        {
            dbcart = new DatabaseHandler(this.Context);
            // Required empty public constructor
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState)
        {
            // Inflate the layout for this fragment
            View view = inflater.Inflate(Resource.Layout.fragment_top__deals_, container, false);


            rv_top_selling = (ShimmerRecyclerView)view.FindViewById(Resource.Id.recyclerTopSelling);
            progressDialog = new ProgressDialog(this.Context);
            progressDialog.SetMessage("Loading...");
            progressDialog.SetCancelable(false);

            viewall = (TextView)view.FindViewById(Resource.Id.viewall_topdeals);
            viewall.Click += delegate
            {
                //startActivity(new Intent(getActivity(), ViewAll_TopDeals.class));

            };

            if (isOnline())
            {
                topSellingUrl();
                getCartDetails();
            }

            return view;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        private bool isOnline()
        {
            ConnectivityManager cm = (ConnectivityManager)this.Activity.GetSystemService(Context.ConnectivityService);
            return cm.ActiveNetworkInfo != null && cm.ActiveNetworkInfo.IsConnected;
        }

        private void topSellingUrl()
        {
            StringRequest stringRequest = new StringRequest(Request.Method.Get, BaseURL.HomeTopSelling, this, this);
            AppController.getInstance().addToRequestQueue(stringRequest, "test");
        }

        public async void getCartDetails()
        {
            int UserId = 0;
            sessionManagement = new Session_management(this.Context);
            if (sessionManagement.isLoggedIn())
            {
                UserId = Convert.ToInt32(sessionManagement.getUserDetails().Get(BaseURL.KEY_ID).ToString());
            }
            var getDataUrl = new System.Uri(BaseURL.Get_CartList + UserId);
            var getCheckoutURL = new System.Uri(BaseURL.GetCheckoutList + UserId);

            using ( var client = new HttpClient())
            {
                var storeCartResponse = await client.GetStringAsync(getDataUrl);
                sessionManagement.SetStoreCart(storeCartResponse);

                MainActivity.totalBudgetCount.Text = JsonConvert.DeserializeObject<List<StoreCart>>(storeCartResponse).Count().ToString();

                var checkoutData = await client.GetStringAsync(getCheckoutURL);
                sessionManagement.SetCheckout(checkoutData);

            }
        }

        public void OnErrorResponse(VolleyError p0)
        {
        }

        public void OnResponse(Java.Lang.Object response)
        {
            progressDialog.Show();
            try
            {
                JSONArray jsonArray = new JSONArray(response.ToString());
                if (jsonArray.Length() > 0)
                {
                    topSellList.Clear();
                    for (int i = 0; i < jsonArray.Length(); i++)
                    {

                        JSONObject jsonObject1 = jsonArray.GetJSONObject(i);
                        string product_id = jsonObject1.GetString("itemmastid");
                        string varient_id = jsonObject1.GetString("itemmastid");
                        string product_name = jsonObject1.GetString("itemid");
                        string description = jsonObject1.GetString("itemdesc");
                        string pprice = jsonObject1.GetString("selrate");
                        string quantity = "50";
                        string product_image = jsonObject1.GetString("ItemImage");
                        string mmrp = jsonObject1.GetString("MRP");
                        string unit = jsonObject1.GetString("priunitvalue");
                        string count = "0";
                        string totalOff = string.Empty;
                        if ((decimal.Parse(mmrp)) > 0)
                        {
                            var savePrice = (decimal.Parse(mmrp) - decimal.Parse(pprice));
                            var per = (savePrice / (decimal.Parse(mmrp)) * 100);
                            totalOff = System.Math.Round(per, 0).ToString();
                        }

                        int warehouseid = jsonObject1.GetString("WareHouseToId") == "" ? 0 : Convert.ToInt32(jsonObject1.GetString("WareHouseToId"));
                        int rowstate = 0;

                        CartModel recentData = new CartModel(product_id, product_name, description, pprice, "Unit : " + " " + unit, product_image, "%" + totalOff + " " + "Off", mmrp, count, unit, warehouseid, rowstate);
                        recentData.setVarient_id(varient_id);
                        topSellList.Add(recentData);
                    }
                    topSellingAdapter = new CartAdapter(Activity, topSellList);
                    rv_top_selling.SetLayoutManager(new LinearLayoutManager(this.Activity));
                    rv_top_selling.SetAdapter(topSellingAdapter);
                    topSellingAdapter.NotifyDataSetChanged();

                }
                else
                {
                    string msg = "No Record found";
                    Toast.MakeText(Context, msg, ToastLength.Short).Show();
                }
                progressDialog.Dismiss();
            }
            catch (JSONException e)
            {
                e.PrintStackTrace();
            }
            progressDialog.Dismiss();

        }
    }
}

