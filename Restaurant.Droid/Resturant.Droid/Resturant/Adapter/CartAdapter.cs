using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.Glide;
using Android.Glide.Request;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using com.resturant.Droid.Model;
using com.resturant.Droid.Resturant.Activities;
using com.resturant.Droid.Resturant.Config;
using com.resturant.Droid.Resturant.utils;
using Java.Lang;
using Newtonsoft.Json;
using Volley;

namespace com.resturant.Droid.Resturant.Adapter
{
    public class CartAdapter : RecyclerView.Adapter
    {
        ISharedPreferences preferences;
        public List<CartModel> cartList;
        public int limit = 4;
        public Context context;
        public int minteger = 0;
        public string catId, catName;
        private Session_management sessionManagement;

        public List<varient_product> varientProducts = new List<varient_product>();

        RecyclerView recyler_popup;
        LinearLayout cancl;
        string varient_id, product_id;

        public override int ItemCount
        {
            get
            {
                return countVal();
            }
        }

        public int countVal()
        {
            int countValue = 0;
            if (cartList.Count > limit)
            {
                countValue = limit;
            }
            else
            {
                countValue = cartList.Count;
            }
            return countValue;
        }

        public CartAdapter(Context context, List<CartModel> cartList)
        {
            this.cartList = cartList;
            this.context = context;
            sessionManagement = new Session_management(context);

        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyViewHolder myViewHolder = holder as MyViewHolder;

            CartModel cc = cartList[position];
            myViewHolder.prodNAme.Text = (cc.getpNAme());
            myViewHolder.pDescrptn.Text = (cc.getpDes());
            myViewHolder.pQuan.Text = (cc.getpQuan());
            myViewHolder.pPrice.Text = (cc.getpPrice());
            myViewHolder.pdiscountOff.Text = (cc.getDiscountOff());
            myViewHolder.pMrp.Text = (cc.getpMrp());
            myViewHolder.pMrp.PaintFlags = (myViewHolder.pMrp.PaintFlags | Android.Graphics.PaintFlags.StrikeThruText);
            byte[] imageByteArray = Base64.Decode(cc.getpImage(), Base64Flags.Default);
            Glide.With(context)
                    .Load(imageByteArray)
                    .Apply(RequestOptions.CircleCropTransform())
                    .Into(myViewHolder.image);

            double items = double.Parse("0.00");
            double price = double.Parse(cartList[position].getpPrice());

            myViewHolder.image.Click += delegate
            {
                catId = cartList[position].getpId();
                catName = cartList[position].getpNAme();
                Intent intent = new Intent(context, typeof(ProductDetails));
                intent.PutExtra("sId", catId);
                intent.PutExtra("sName", catName);
                context.StartActivity(intent);
            };

            myViewHolder.btn_Add.Click += delegate
             {
                 if (sessionManagement.isLoggedIn())
                 {
                     updateMultiplyAsync(position, myViewHolder);
                 }
                 else
                 {
                     Toast.MakeText(this.context, "Please login", ToastLength.Short).Show();
                 }
             };
            myViewHolder.plus.Click += delegate
            {
                increaseInteger(myViewHolder);

            };
            myViewHolder.minus.Click += delegate
            {
                decreaseInteger(myViewHolder);
            };
        }

        private async void updateMultiplyAsync(int position, MyViewHolder myViewHolder)
        {
            List<StoreCart> listOfCart = new List<StoreCart>();
            List<ProcessCheckout> listOfCheckout = new List<ProcessCheckout>();
            StoreCart storeCart = new StoreCart();
            ProcessCheckout checkout = new ProcessCheckout();
            string soreValue = sessionManagement.GetStoreCart().Get(BaseURL.KET_STORECART).ToString();
            string checkoutValue = sessionManagement.GetCheckout().Get(BaseURL.KEY_Checkout).ToString();

            if (!string.IsNullOrEmpty(soreValue))
            {
                listOfCart = JsonConvert.DeserializeObject<List<StoreCart>>(soreValue);
                if (listOfCart.Count > 0)
                {
                    storeCart = (from data in listOfCart where data.ItemMasterId == Convert.ToInt32(cartList[position].getVarient_id()) select data).FirstOrDefault();
                }

            }
            if (!string.IsNullOrEmpty(checkoutValue))
            {
                listOfCheckout = JsonConvert.DeserializeObject<List<ProcessCheckout>>(checkoutValue);
                if (listOfCheckout.Count > 0)
                {
                    checkout = (from data in listOfCheckout where data.ItemMasterId == Convert.ToInt64(cartList[position].getVarient_id()) select data).FirstOrDefault();
                }
            }


            if (storeCart != null && storeCart.ItemMasterId > 0 || checkout != null && checkout.ItemMasterId > 0)
            {
                Toast.MakeText(this.context, "Item Aready Added", ToastLength.Short).Show();
            }
            else
            {
                myViewHolder.btn_Add.Visibility = ViewStates.Gone;
                myViewHolder.ll_addQuan.Visibility = ViewStates.Visible;
                myViewHolder.txtQuan.Text = "1";
                minteger = 1;

                AddToCartModel addToCartModel = new AddToCartModel();
                addToCartModel.UserId = Convert.ToInt32(sessionManagement.getUserDetails().Get(BaseURL.KEY_ID).ToString());
                addToCartModel.ItemDesc = cartList[position].getpDes();
                addToCartModel.ItemMasterId = Convert.ToInt32(cartList[position].getVarient_id());
                addToCartModel.SelRate = Convert.ToDecimal(cartList[position].getpPrice());
                addToCartModel.GSTRate = 0;
                addToCartModel.ItemImage = cartList[position].getpImage();
                addToCartModel.ItemImageType = "image/jpeg";
                addToCartModel.Date = DateTime.Now;
                addToCartModel.WarehouseID = Convert.ToInt32(cartList[position].getWarehouseId());

                var itemCount = Convert.ToInt32(MainActivity.totalBudgetCount.Text);
                MainActivity.totalBudgetCount.Text = (itemCount + 1).ToString();

                if (myViewHolder.txtQuan.Text.ToString() != "0")
                {
                    addToCartModel.Quantity = int.Parse(myViewHolder.txtQuan.Text.ToString());
                    var uri = BaseURL.AddToCart;
                    var registerData = JsonConvert.SerializeObject(addToCartModel);
                    using (var client = new HttpClient())
                    {
                        var jsonContent = new StringContent(registerData, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(uri, jsonContent);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var getDataUrl = new System.Uri(BaseURL.Get_CartList + addToCartModel.UserId);
                            var storeCartResponse = await client.GetStringAsync(getDataUrl);
                            sessionManagement.SetStoreCart(storeCartResponse);
                        }
                    }
                }

                try
                {
                    int items = (int)double.Parse(myViewHolder.txtQuan.Text);
                    double price = double.Parse(cartList[position].getpPrice().Trim());
                    double mrp = double.Parse(cartList[position].getpMrp().Trim());
                    myViewHolder.pDescrptn.Text = "" + cartList[position].getpDes();
                    myViewHolder.pPrice.Text = "" + price * items;
                    myViewHolder.txtQuan.Text = "" + items;
                    myViewHolder.pMrp.Text = "" + mrp * items;
                }
                catch (IndexOutOfBoundsException e)
                {
                    e.ToString();
                }
            }
        }

        public void increaseInteger(MyViewHolder myViewHolder)
        {
            minteger = minteger + 1;
            display(minteger, myViewHolder);
        }

        public async void decreaseInteger(MyViewHolder myViewHolder)
        {
            if (minteger == 1)
            {
                minteger = 1;
                display(minteger, myViewHolder);
                myViewHolder.ll_addQuan.Visibility = ViewStates.Gone;
                myViewHolder.btn_Add.Visibility = ViewStates.Visible;

                AddToCartModel addToCartModel = new AddToCartModel();
                addToCartModel.UserId = Convert.ToInt32(sessionManagement.getUserDetails().Get(BaseURL.KEY_ID).ToString());
                addToCartModel.ItemMasterId = Convert.ToInt32(cartList[myViewHolder.Position].getVarient_id());

                var deleteURI = BaseURL.DeleteCart + "?itemmastid=" + addToCartModel.ItemMasterId.ToString() + "&userid=" + addToCartModel.UserId;
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(deleteURI);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var getDataUrl = new System.Uri(BaseURL.Get_CartList + addToCartModel.UserId);
                        var storeCartResponse = await client.GetStringAsync(getDataUrl);
                        sessionManagement.SetStoreCart(storeCartResponse);
                    }
                }
            }
            else
            {
                if (minteger > 0)
                {
                    minteger = minteger - 1;
                    display(minteger, myViewHolder);
                }

            }
        }

        private async void display(int number, MyViewHolder myViewHolder)
        {
            myViewHolder.txtQuan.Text = "" + number;

            AddToCartModel addToCartModel = new AddToCartModel();
            addToCartModel.UserId = Convert.ToInt32(sessionManagement.getUserDetails().Get(BaseURL.KEY_ID).ToString());
            addToCartModel.ItemMasterId = Convert.ToInt32(cartList[myViewHolder.Position].getVarient_id());
            var deleteURI = BaseURL.CartQuantityUpdate + "?userid=" + addToCartModel.UserId + "&itemmastid=" + addToCartModel.ItemMasterId.ToString() + "&qty=" + number;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(deleteURI);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var getDataUrl = new System.Uri(BaseURL.Get_CartList + addToCartModel.UserId);
                    var storeCartResponse = await client.GetStringAsync(getDataUrl);
                    sessionManagement.SetStoreCart(storeCartResponse);
                }
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.layout_product_add, parent, false);
            MyViewHolder view = new MyViewHolder(itemView);
            return view;
        }

        public class MyViewHolder : RecyclerView.ViewHolder
        {
            public TextView prodNAme, pDescrptn, pQuan, pPrice, pdiscountOff, pMrp, minus, plus, txtQuan;
            public ImageView image;
            public LinearLayout btn_Add, ll_addQuan;
            public RelativeLayout rlQuan;
            public View mainView;

            public MyViewHolder(View view) : base(view)
            {
                prodNAme = (TextView)view.FindViewById(Resource.Id.txt_pName);
                pDescrptn = (TextView)view.FindViewById(Resource.Id.txt_pInfo);
                pQuan = (TextView)view.FindViewById(Resource.Id.txt_unit);
                pPrice = (TextView)view.FindViewById(Resource.Id.txt_Pprice);
                image = (ImageView)view.FindViewById(Resource.Id.prodImage);
                pdiscountOff = (TextView)view.FindViewById(Resource.Id.txt_discountOff);
                pMrp = (TextView)view.FindViewById(Resource.Id.txt_Mrp);
                rlQuan = (RelativeLayout)view.FindViewById(Resource.Id.rlQuan);
                btn_Add = (LinearLayout)view.FindViewById(Resource.Id.btn_Add);
                ll_addQuan = (LinearLayout)view.FindViewById(Resource.Id.ll_addQuan);
                txtQuan = (TextView)view.FindViewById(Resource.Id.txtQuan);
                minus = (TextView)view.FindViewById(Resource.Id.minus);
                plus = (TextView)view.FindViewById(Resource.Id.plus);
                mainView = view;
            }
        }
    }
}