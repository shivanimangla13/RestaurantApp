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
using com.resturant.Droid.Model;
using com.resturant.Droid.Resturant.Adapter;
using Android.Net;
using Volley;
using Volley.Toolbox;
using com.resturant.Droid.Resturant.Config;
using com.resturant.Droid.Resturant.utils;
using Org.Json;

namespace com.resturant.Droid.Resturant.Fragments
{
    [Activity(Label = "SearchFragment")]
    public class SearchFragment : Android.Support.V4.App.Fragment, Response.IListener, Response.IErrorListener
    {
        RecyclerView recyclerSearch;
        EditText txtSearch;
        ProgressDialog progressDialog;
        SearchAdapter searchAdapter;
        List<SearchModel> searchlist = new List<SearchModel>();
        string seaarchId, seaarchName;
        public SearchFragment()
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
            View view = inflater.Inflate(Resource.Layout.fragment_search, container, false);
            ((MainActivity)this.Activity).Title = "Search";
            recyclerSearch = (RecyclerView)view.FindViewById(Resource.Id.recyclerSearch);
            txtSearch = (EditText)view.FindViewById(Resource.Id.txtSearch);
            progressDialog = new ProgressDialog(this.Context);
            progressDialog.SetMessage("Loading...");
            progressDialog.SetCancelable(false);
            txtSearch.AfterTextChanged += TxtSearch_AfterTextChanged;

            return view;
        }

        private void TxtSearch_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            if (isOnline())
            {
                if (e.Editable.ToString().Length > 0)
                {
                    searchlist.Clear();
                    searchUrl(e.Editable.ToString());
                }
                else if (e.Editable.ToString().Length < 2)
                {
                    searchlist.Clear();
                    if (searchAdapter != null)
                    {
                        searchAdapter.NotifyDataSetChanged();
                    }
                }
            }
            else
            {
                Toast.MakeText(this.Context, "Please check internet", ToastLength.Short).Show();
            }

        }

        public void searchUrl(string name)
        {
            StringRequest stringRequest = new StringRequest(Request.Method.Get, BaseURL.Search + name, this, this);

            AppController.getInstance().addToRequestQueue(stringRequest, "search");
            stringRequest.SetRetryPolicy(new DefaultRetryPolicy(1000,
                    DefaultRetryPolicy.DefaultMaxRetries,
                    DefaultRetryPolicy.DefaultBackoffMult));
            AppController.getInstance().getRequestQueue().Cache.Clear();
            AppController.getInstance().getRequestQueue().Add(stringRequest);
        }

        private bool isOnline()
        {
            ConnectivityManager cm = (ConnectivityManager)this.Activity.GetSystemService(Context.ConnectivityService);

            return cm.ActiveNetworkInfo != null && cm.ActiveNetworkInfo.IsConnected;
        }

        public void OnResponse(Java.Lang.Object response)
        {
            progressDialog.Show();
            try
            {
                JSONArray jsonArray = new JSONArray(response.ToString());
                if (jsonArray.Length() > 0)
                {
                    searchlist.Clear();
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

                        SearchModel recentData = new SearchModel();
                        recentData.Id = Convert.ToInt32(product_id);
                        recentData.Name = product_name;
                        searchlist.Add(recentData);
                    }
                    searchAdapter = new SearchAdapter(searchlist,this.Activity);
                    recyclerSearch.SetLayoutManager(new LinearLayoutManager(this.Activity));
                    recyclerSearch.SetAdapter(searchAdapter);
                    searchAdapter.NotifyDataSetChanged();

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

        public void OnErrorResponse(VolleyError p0)
        {
        }
    }
}
