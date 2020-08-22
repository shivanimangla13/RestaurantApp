using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Glide;
using Android.Glide.Request;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using com.resturant.Droid.Model;
using com.resturant.Droid.Resturant.Config;
using com.resturant.Droid.Resturant.Fragments;
using com.resturant.Droid.Resturant.utils;

namespace com.resturant.Droid.Resturant.Adapter
{
    public class Cart_adapter : RecyclerView.Adapter
    {
        List<StoreCart> list;
        Activity activity;
        private Session_management sessionManagement;
        public Cart_adapter(Activity activity, List<StoreCart> list)
        {
            this.list = list;
            this.activity = activity;
            sessionManagement = new Session_management(activity);
        }


        public override int ItemCount => list.Count();

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyViewHolder myViewHolder = holder as MyViewHolder;
            StoreCart map = new StoreCart();
            map = list[position];
            byte[] imageByteArray = map.ItemImage != null ? Base64.Decode(map.ItemImage, Base64Flags.Default) : null ;

            myViewHolder.prodNAme.Text = map.ItemMastId;

            string items = map.Quantity.ToString();
            string sprice = map.SelRate.ToString("0.##");

            myViewHolder.price = float.Parse(sprice);
            myViewHolder.pPrice.Text = "" + (decimal.Parse(sprice) * decimal.Parse(items)).ToString("0.##");
            var totalAmount = (from data in list select data).Sum(x => x.SelRate * x.Quantity);
            CartFragment.tv_total.Text = activity.Resources.GetString(Resource.String.currency) + " " + totalAmount.ToString("0.##");
            myViewHolder.txtQuan.Text = items.ToString();
            myViewHolder.minteger = float.Parse(items);
            myViewHolder.pQuan.Text = map.Quantity.ToString();
            myViewHolder.pMrp.Visibility = ViewStates.Gone;
            myViewHolder.pMrp.Text = "";
            myViewHolder.pMrp.PaintFlags = (myViewHolder.pMrp.PaintFlags | Android.Graphics.PaintFlags.StrikeThruText);

            myViewHolder.btn_Add.Click += delegate
            {
                myViewHolder.btn_Add.Visibility = ViewStates.Gone;
                myViewHolder.ll_addQuan.Visibility = ViewStates.Visible;

                float items = map.Quantity;
                float price = float.Parse(sprice);
                double reward = double.Parse("0.00");
                myViewHolder.pPrice.Text = "" + price * items;
                updateintent();
            };

            myViewHolder.txt_close.Click += async delegate
             {
                 
                 if(list.Count > 0)
                 {
                     var itemMasterId = list[position].ID;

                     var deleteURI = BaseURL.DeleteCartItem + itemMasterId;

                     using (var client = new HttpClient())
                     {
                         StringContent content = new StringContent("");
                         var response = await client.PostAsync(deleteURI, content);
                         if (response.StatusCode == HttpStatusCode.OK)
                         {
                             Toast.MakeText(activity, "Delete successfully", ToastLength.Short);
                         }
                     }
                     list.RemoveAt(position);
                 }
                 var totalAmount = (from data in list select data).Sum(x => x.SelRate * x.Quantity);
                 CartFragment.tv_total.Text = activity.Resources.GetString(Resource.String.currency) + " " + totalAmount;
                 

                 if (list.Count == 0)
                 {
                     CartFragment.noData.Visibility = ViewStates.Visible;
                     CartFragment.viewCart.Visibility = ViewStates.Gone;
                 }
                 CartFragment.totalItems.Text = "" + list.Count() + "  Total Items:";
                 NotifyDataSetChanged();
                 updateintent();
             };

            myViewHolder.plus.Click += delegate
            {
                increaseInteger(myViewHolder);
                updateMultiply(myViewHolder, map);

            };
           
            myViewHolder.minus.Click += delegate
            {
                decreaseInteger(myViewHolder);
                updateMultiply( myViewHolder, map);
            };


            Glide.With(activity)
                    .Load(imageByteArray)
                    .Apply(RequestOptions.CircleCropTransform())
                    .Into(myViewHolder.image);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.layout_cart, parent, false);
            MyViewHolder view = new MyViewHolder(itemView);
            return view;
        }

        public void decreaseInteger(MyViewHolder myViewHolder)
        {
            if (myViewHolder.minteger == 1)
            {
                myViewHolder.minteger = 1;
            }
            else
            {
                myViewHolder.minteger = myViewHolder.minteger - 1;
                display(myViewHolder.minteger, myViewHolder);
            }
        }

        private void updateMultiply(MyViewHolder myViewHolder, StoreCart tempMap)
        {
            float items = tempMap.Quantity;
            myViewHolder.txtQuan.Text = "" + int.Parse(items.ToString());
            myViewHolder.pPrice.Text = "" + myViewHolder.price * items;

            var totalAmount = (from data in list select data).Sum(x => x.SelRate * x.Quantity);
            CartFragment.tv_total.Text = (activity.Resources.GetString(Resource.String.currency) + " " + totalAmount);
            updateintent();
        }

        public void increaseInteger(MyViewHolder myViewHolder)
        {
            myViewHolder.minteger = myViewHolder.minteger + 1;
            display(myViewHolder.minteger,myViewHolder);
        }


        private async void display(float number, MyViewHolder myViewHolder)
        {
            myViewHolder.txtQuan.Text = "" + number;

            AddToCartModel addToCartModel = new AddToCartModel();
            addToCartModel.UserId = Convert.ToInt32(sessionManagement.getUserDetails().Get(BaseURL.KEY_ID).ToString());
            addToCartModel.ItemMasterId = Convert.ToInt32(list[myViewHolder.Position].ItemMasterId);
            var deleteURI = BaseURL.CartQuantityUpdate + "?userid=" + addToCartModel.UserId + "&itemMasterId="+ addToCartModel.ItemMasterId.ToString() + "&qty=" + number;
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent("");
                var response = await client.PostAsync(deleteURI, content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var getDataUrl = new System.Uri(BaseURL.Get_CartList + addToCartModel.UserId);
                    var storeCartResponse = await client.GetStringAsync(getDataUrl);
                    sessionManagement.SetStoreCart(storeCartResponse);
                }
            }
            updateintent();
            NotifyDataSetChanged();
        }

        private void updateintent()
        {
            Intent updates = new Intent("Grocery_cart");
            updates.PutExtra("type", "update");
            activity.SendBroadcast(updates);
        }


        public class MyViewHolder : RecyclerView.ViewHolder
        {
            public TextView prodNAme, pDescrptn, pQuan, pPrice, pdiscountOff, pMrp, minus, plus, txtQuan, txt_close;
            public ImageView image;
            public LinearLayout btn_Add, ll_addQuan;
            public RelativeLayout rlQuan;
            public View mainView;
            public float minteger;
            public float price;

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
                txt_close = (TextView)view.FindViewById(Resource.Id.txt_close);
                mainView = view;
            }
        }
    }
}