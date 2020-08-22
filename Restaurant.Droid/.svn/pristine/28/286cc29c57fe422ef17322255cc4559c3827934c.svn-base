using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;
using com.resturant.Droid.Model;
using com.resturant.Droid.Resturant.Adapter;
using com.resturant.Droid.Resturant.Config;
using com.resturant.Droid.Resturant.utils;
using Devs.Mulham.Horizontalcalendar;
using Java.Text;
using Java.Util;
using Newtonsoft.Json;
using Org.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace com.resturant.Droid.Resturant.Activities
{
    [Activity]
    public class OrderSummary : AppCompatActivity
    {
        Session_management session_management;
        string total_atm;
        Button btn_AddAddress;
        LinearLayout back;
        TextView btn_Contine, txt_deliver, txtTotalItems, pPrice, pMrp, totalItms, price, DeliveryCharge
            , Amounttotal, txt_totalPrice;
        string dname;
        JSONArray array;
        public static string cart_id;

        RecyclerView recycler_itemsList;
        public HorizontalCalendar horizontalCalendar;
        private List<timing_model> dateDayModelClasses1;
        Timing_Adapter bAdapter1;
        string timeslot;
        string addressid, totalprice;
        string user_id;
        ProgressDialog progressDialog;

        String minVAlue, maxValue;
        private List<CartModel> cartList = new List<CartModel>();
        private Session_management sessionManagement;
        List<ProcessCheckout> CheckoutList = new List<ProcessCheckout>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_order_summary);
            SupportActionBar.Hide();
            //this.Window.SetFlags(Android.Views.WindowManagerFlags.Fullscreen, Android.Views.WindowManagerFlags.Fullscreen);
            // Create your application here
            sessionManagement = new Session_management(ApplicationContext);
            user_id = sessionManagement.getUserDetails().Get(BaseURL.KEY_ID).ToString();
            array = new JSONArray();

            init();
        }

        private async void init()
        {
            session_management = new Session_management(this.ApplicationContext);
            progressDialog = new ProgressDialog(this);
            progressDialog.SetMessage("Loading...");
            progressDialog.SetCancelable(false);

            sessionManagement = new Session_management(this);
            sessionManagement.cleardatetime();

            dname = Intent.GetStringExtra("dName");
            addressid = Intent.GetStringExtra("dId");
            btn_AddAddress = (Button)FindViewById(Resource.Id.btn_AddAddress);
            txtTotalItems = (TextView)FindViewById(Resource.Id.txtTotalItems);
            btn_Contine = (TextView)FindViewById(Resource.Id.btn_Contine);
            recycler_itemsList = (RecyclerView)FindViewById(Resource.Id.recycler_itemsList);
            //recyclerTimeSlot = (RecyclerView)FindViewById(Resource.Id.recyclerTimeSlot);
            pPrice = (TextView)FindViewById(Resource.Id.pPrice);
            pMrp = (TextView)FindViewById(Resource.Id.pMrp);
            totalItms = (TextView)FindViewById(Resource.Id.totalItms);
            price = (TextView)FindViewById(Resource.Id.price);
            DeliveryCharge = (TextView)FindViewById(Resource.Id.DeliveryCharge);
            Amounttotal = (TextView)FindViewById(Resource.Id.Amounttotal);
            txt_totalPrice = (TextView)FindViewById(Resource.Id.txt_totalPrice);
            back = (LinearLayout)FindViewById(Resource.Id.back);



            back.Click += delegate
              {
                  Finish();
              };

            btn_AddAddress.Click += delegate
            {
                Intent In = new Intent(ApplicationContext, typeof(Add_address));
                StartActivity(In);
            };


            SimpleDateFormat format = new SimpleDateFormat("dd/MM/yyyy");
            var startDate = DateTime.Now.AddMonths(-1);
            Date sdate = format.Parse(startDate.Date.ToShortDateString());

            var endDate = DateTime.Now.AddMonths(1);
            Date edate = format.Parse(endDate.Date.ToShortDateString());

            string todaydate = sdate.ToString();
            dateDayModelClasses1 = new List<timing_model>();
            //horizontalCalendar = FindViewById<HorizontalCalendar>(Resource.Id.calendarView1);
            //horizontalCalendar = new HorizontalCalendar.Builder(this, Resource.Id.calendarView1)
            //    .StartDate(sdate)
            //    .EndDate(edate)
            //    .DatesNumberOnScreen(5)
            //    .MonthFormat("MMM")
            //    .DayNameFormat("dd")
            //    .DayNumberFormat("EEE")
            //    .TextSize(11f, 20f, 12f)
            //    .TextColor(Color.Gray, Color.Black)
            //    .Build();
            //horizontalCalendarListner = new OnDateSelected(DateSelected);
            sessionManagement = new Session_management(this);
            var UserId = Convert.ToInt32(sessionManagement.getUserDetails().Get(BaseURL.KEY_ID).ToString());
            var getCheckoutURL = new System.Uri(BaseURL.GetCheckoutList + UserId);
            CheckoutList = new List<ProcessCheckout>();

            using (var client = new HttpClient())
            {
                var checkoutData = await client.GetStringAsync(getCheckoutURL);
                CheckoutList = JsonConvert.DeserializeObject<List<ProcessCheckout>>(checkoutData);
                var deliverycharges = CheckoutList.Select(x => x.ShippingCharges).Sum();
                DeliveryCharge.Text = deliverycharges.ToString();
                totalprice = DeliveryCharge.Text.ToString() + price.Text.ToString();
                Amounttotal.Text = totalprice;
            }

            updateData();
            btn_Contine.Click += Btn_Contine_Click;
        }

        private async void Btn_Contine_Click(object sender, EventArgs e)
        {
            if (CheckoutList.Count > 0)
            {
                if (string.IsNullOrEmpty(session_management.GetAddressDetails().Get(BaseURL.ADDRESS).ToString()) ||
                string.IsNullOrEmpty(session_management.GetAddressDetails().Get(BaseURL.CITY).ToString()) ||
                string.IsNullOrEmpty(session_management.GetAddressDetails().Get(BaseURL.PINCODE).ToString()) ||
                string.IsNullOrEmpty(session_management.GetAddressDetails().Get(BaseURL.FULLNAME).ToString()) ||
               string.IsNullOrEmpty(session_management.GetAddressDetails().Get(BaseURL.EMAIL).ToString()) ||
                string.IsNullOrEmpty(session_management.GetAddressDetails().Get(BaseURL.MOBILE).ToString()))
                {
                    Toast.MakeText(this, "Please add the address", ToastLength.Short).Show();
                }
                else
                {
                    var totalAmount = (from data in CheckoutList select data).Sum(x => x.SelRate * x.Quantity);
                    var deliverycharges = CheckoutList.Select(x => x.ShippingCharges).Sum();
                    ProcessCheckout CheckoutObj = new ProcessCheckout();
                    CheckoutObj = CheckoutList.FirstOrDefault();
                    CheckoutObj.UserId = CheckoutList.Select(x => x.UserId).FirstOrDefault();
                    CheckoutObj.Name = session_management.GetAddressDetails().Get(BaseURL.FULLNAME).ToString();
                    CheckoutObj.Phone = session_management.GetAddressDetails().Get(BaseURL.MOBILE).ToString();
                    CheckoutObj.Email = session_management.GetAddressDetails().Get(BaseURL.EMAIL).ToString();
                    CheckoutObj.Address = session_management.GetAddressDetails().Get(BaseURL.ADDRESS).ToString();
                    CheckoutObj.City = session_management.GetAddressDetails().Get(BaseURL.CITY).ToString();
                    CheckoutObj.Pincode = session_management.GetAddressDetails().Get(BaseURL.PINCODE).ToString();
                    CheckoutObj.Country = Convert.ToInt32(session_management.GetAddressDetails().Get(BaseURL.COUNTRYID).ToString());
                    CheckoutObj.CountryName = session_management.GetAddressDetails().Get(BaseURL.COUNTRY).ToString();
                    CheckoutObj.State = Convert.ToInt32(session_management.GetAddressDetails().Get(BaseURL.STATEID).ToString());
                    CheckoutObj.StateName = session_management.GetAddressDetails().Get(BaseURL.STATE).ToString();
                    CheckoutObj.Mobile = session_management.GetAddressDetails().Get(BaseURL.MOBILE).ToString();
                    CheckoutObj.PayBy = "COD";
                    CheckoutObj.Landmark = session_management.GetSpcialNotes().Get(BaseURL.LANDMARK).ToString();
                    CheckoutObj.SpecialNote = session_management.GetSpcialNotes().Get(BaseURL.SPECIALNOTES).ToString();
                    CheckoutObj.WarehouseId = 1000254;
                    CheckoutObj.ShippingCharges = deliverycharges;
                    CheckoutObj.ItemImage =null;
                    SaveCheckout processCheckout = new SaveCheckout();
                    processCheckout.CheckoutObj = CheckoutObj;
                   
                    processCheckout.TotalAmount = Convert.ToDecimal(totalAmount + deliverycharges);
                    processCheckout.CouponAmount = 0;
                    processCheckout.CouponPin = "";
                    processCheckout.Discount = 0;
                    processCheckout.REAmount = 0;
                    processCheckout.RedeemValue = 0;

                    var uri = BaseURL.SaveCheckoutList;
                    var saveOrder = JsonConvert.SerializeObject(processCheckout);

                    using (var client = new HttpClient())
                    {
                        var jsonContent = new StringContent(saveOrder, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(uri, jsonContent);
                        if (response.IsSuccessStatusCode)
                        {
                            response.EnsureSuccessStatusCode();
                            var resultString = await response.Content.ReadAsStringAsync();
                            var intent = new Intent(this, typeof(MainActivity));
                            this.StartActivity(intent);
                        }
                        else
                        {
                            Toast.MakeText(this, "Something went wrong!", ToastLength.Short).Show();
                        }

                    }

                }
                
            }
            else
            {
                Toast.MakeText(this, "Please add at list 1(one) item", ToastLength.Short).Show();
            }
        }

        private void updateData()
        {
            var totalAmount = (from data in CheckoutList select data).Sum(x => x.SelRate * x.Quantity);
            var deliverycharges = CheckoutList.Select(x => x.ShippingCharges).Sum();
            pPrice.Text = totalAmount.ToString("0.##");
            price.Text = "" + totalAmount.ToString("0.##");
            total_atm = (totalAmount + deliverycharges).ToString("0.##");
            txt_totalPrice.Text = total_atm;
            txtTotalItems.Text = ("" + CheckoutList.Count());
            totalItms.Text = ("" + CheckoutList.Count() + " " + " Items");
        }

    }
}

