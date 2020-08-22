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
using Google.Android.Material.TextField;
using Android.Support.V7.Widget;
using com.resturant.Droid.Resturant.Config;
using Volley;
using Volley.Toolbox;
using com.resturant.Droid.Resturant.utils;
using System.Net.Http;
using Org.Json;
using com.resturant.Droid.Model;
using com.resturant.Droid.Resturant.Adapter;
using Android.Net;

namespace com.resturant.Droid.Resturant.Activities
{
    [Activity()]
    public class Add_address : AppCompatActivity
    {
        Session_management session_management;
        public LinearLayout back;
        public Button Save, EditBtn;
        public EditText pinCode, address, remark, city, specialRemark, name, email, mobNo, alterMob;
        public List<SearchModel> countryModel = new List<SearchModel>();
        public List<SearchModel> stateModel = new List<SearchModel>();
        public SearchAdapter countryAdapter;
        public Spinner countrySpiner, statespinner;
        string user_id;
        string cityId, cityName, socetyId, SocetyName, landmaarkkkk, updtae, addressId, receiver_name, receiver_phone, house_no, landmark, state_st, pincode;
        ProgressDialog progressDialog;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SupportActionBar.Hide();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_add_address);
            Setup();
        }

        public async void Setup()
        {
            address = (EditText)FindViewById(Resource.Id.input_HouseNO);
            countrySpiner = FindViewById<Spinner>(Resource.Id.countryspinner);
            statespinner = FindViewById<Spinner>(Resource.Id.statespinner);
            city = (EditText)FindViewById(Resource.Id.input_city);
            pinCode = (EditText)FindViewById(Resource.Id.input_pinCode);
            remark = (EditText)FindViewById(Resource.Id.input_remark);
            specialRemark = (EditText)FindViewById(Resource.Id.input_specialRmk);
            name = (EditText)FindViewById(Resource.Id.input_NAme);
            email = (EditText)FindViewById(Resource.Id.input_Email);
            mobNo = (EditText)FindViewById(Resource.Id.input_mobNO);
            alterMob = (EditText)FindViewById(Resource.Id.input_AltermobileNO);
            session_management = new Session_management(this);
            address.Text = session_management.GetAddressDetails().Get(BaseURL.ADDRESS).ToString();
            city.Text = session_management.GetAddressDetails().Get(BaseURL.CITY).ToString();
            pinCode.Text = session_management.GetAddressDetails().Get(BaseURL.PINCODE).ToString();
            name.Text = session_management.GetAddressDetails().Get(BaseURL.FULLNAME).ToString();
            email.Text = session_management.GetAddressDetails().Get(BaseURL.EMAIL).ToString();
            mobNo.Text = session_management.GetAddressDetails().Get(BaseURL.MOBILE).ToString();
            progressDialog = new ProgressDialog(this);
            progressDialog.SetMessage("Loading...");
            progressDialog.SetCancelable(false);

            RunOnUiThread(() =>
            {
                GetCountryList();
            });

            back = (LinearLayout)FindViewById(Resource.Id.back);
            Save = (Button)FindViewById(Resource.Id.SaveBtn);
            EditBtn = (Button)FindViewById(Resource.Id.EditBtn);


            back.Click += delegate
            {
                Finish();
            };

            Save.Click += Save_Click;

        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (address.Text.ToString().Trim() == string.Empty)
            {
                Toast.MakeText(ApplicationContext, "Enter Address", ToastLength.Long).Show();
            }
             else if (statespinner.SelectedItemPosition == 0)
            {
                Toast.MakeText(ApplicationContext, "Select state", ToastLength.Long).Show();
            }
            else if (pinCode.Text.ToString().Trim() == string.Empty)
            {
                Toast.MakeText(ApplicationContext, "Enter Pincode", ToastLength.Long).Show();
            }

            else if (city.Text.ToString().Trim() == string.Empty)
            {
                Toast.MakeText(ApplicationContext, "Enter City", ToastLength.Long).Show();
            }
           
            else if (name.Text.ToString().Trim() == string.Empty)
            {
                Toast.MakeText(ApplicationContext, "Enter your Name", ToastLength.Long).Show();
            }
            else if (mobNo.Text.ToString().Trim() == string.Empty)
            {
                Toast.MakeText(ApplicationContext, "Enter Mobile No.", ToastLength.Long).Show();
            }
            else if (!isOnline())
            {
                Toast.MakeText(ApplicationContext, "Check your Internet Connection!", ToastLength.Long).Show();
            }
            else
            {
                UserInfo user = new UserInfo();
                user.partyaddress = address.Text;
                user.country = countryModel[countrySpiner.SelectedItemPosition].Id;
                user.state = stateModel[statespinner.SelectedItemPosition].Id;
                user.city = city.Text;
                user.pincode = pinCode.Text;
                user.partyname = name.Text;
                user.mobno = mobNo.Text;
                user.mailid = email.Text;
                user.countryvalue = countryModel[countrySpiner.SelectedItemPosition].Name;
                user.statevalue = stateModel[statespinner.SelectedItemPosition].Name;
                session_management.AddAddress(user);

                session_management.AddSpecialNotes(remark.Text, specialRemark.Text);

                Intent i = new Intent(this, typeof(OrderSummary));
                StartActivity(i);
            }
        }

        private async void GetCountryList()
        {
            progressDialog.Show();
            countryModel = new List<SearchModel>();
            var getDataUrl = new System.Uri(BaseURL.GET_COUNTRY);
            using (var client = new HttpClient())
            {
                var countryData = await client.GetStringAsync(getDataUrl);
                JSONArray jsonArray = new JSONArray(countryData.ToString());
                if (jsonArray.Length() > 0)
                {
                    for (int i = 0; i < jsonArray.Length(); i++)
                    {
                        SearchModel country = new SearchModel();
                        JSONObject jsonObject1 = jsonArray.GetJSONObject(i);
                        country.Id = Convert.ToInt32(jsonObject1.GetString("countrymastid"));
                        country.Name = jsonObject1.GetString("countryname");
                        countryModel.Add(country);
                    }
                    countryAdapter = new SearchAdapter(countryModel,this);
                    string[] countryName = countryModel.Select(x => x.Name).ToArray();
                    int countryID = Array.IndexOf(countryName, session_management.GetAddressDetails().Get(BaseURL.COUNTRY).ToString());

                    countrySpiner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
                    var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, countryName);
                    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    countrySpiner.Adapter = adapter;
                    countrySpiner.SetSelection(countryID);

                }
            }
            progressDialog.Dismiss();

        }

        private async void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner countrySpinner = (Spinner)sender;
            countrySpinner.TooltipText = countryModel[e.Position].Name;
            stateModel = new List<SearchModel>();
            progressDialog.Show();
            var getDataUrl = new System.Uri(BaseURL.GET_STATE);
            using (var client = new HttpClient())
            {
                var countryData = await client.GetStringAsync(getDataUrl + countryModel[e.Position].Id.ToString());

                JSONArray jsonArray = new JSONArray(countryData.ToString());
                if (jsonArray.Length() > 0)
                {
                    for (int i = 0; i < jsonArray.Length(); i++)
                    {
                        SearchModel state = new SearchModel();
                        JSONObject jsonObject1 = jsonArray.GetJSONObject(i);
                        state.Id = Convert.ToInt32(jsonObject1.GetString("statemastid"));
                        state.Name = jsonObject1.GetString("statename");
                        stateModel.Add(state);
                    }
                    statespinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(stateItem_selected);
                    var stateName = stateModel.Select(x => x.Name).ToArray();
                    var stateId = Array.IndexOf(stateName, session_management.GetAddressDetails().Get(BaseURL.STATE).ToString());
                    var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, stateName);
                    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    statespinner.Adapter = adapter;
                    statespinner.SetSelection(stateId);

                }
            }
            progressDialog.Dismiss();
        }

        private void stateItem_selected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            spinner.TooltipText = stateModel[e.Position].Name;
        }
        private bool isOnline()
        {
            ConnectivityManager cm = (ConnectivityManager)GetSystemService(Context.ConnectivityService);

            return cm.ActiveNetworkInfo != null && cm.ActiveNetworkInfo.IsConnected;
        }
    }
    
}