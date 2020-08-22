using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using com.resturant.Droid.Model;

namespace com.resturant.Droid.Resturant.Adapter
{
    public class Timing_Adapter : RecyclerView.Adapter
    {
        Context context;

        bool showingfirst = true;
        int myPos = 0;

        string timeslot;
        public List<timing_model> OfferList;
        public Timing_Adapter(Context context, List<timing_model> offerList)
        {
            OfferList = offerList;
            this.context = context;
        }

        public override int ItemCount => OfferList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyViewHolder myViewHolder = holder as MyViewHolder;
            timing_model lists = OfferList[position];

            myViewHolder.time.Text = lists.getTiming();


            if (myPos == position)
            {
                timeslot = lists.getTiming();

                myViewHolder.time.SetTextColor(Color.ParseColor("#FE8100"));
                myViewHolder.linear.SetBackgroundResource(Resource.Drawable.blue_dateday_rect);
            }
            else
            {

                myViewHolder.time.SetTextColor(Color.ParseColor("#8f909e"));
                myViewHolder.linear.SetBackgroundResource(Resource.Drawable.gray_dateday_rect);
            }

            myViewHolder.time.Click += delegate
            {
                myPos = position;
                NotifyDataSetChanged();
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.item_lay_time, parent, false);

            return new MyViewHolder(itemView);
        }

        public class MyViewHolder : RecyclerView.ViewHolder
        {
            public TextView time;
            public LinearLayout linear;

            public MyViewHolder(View view) : base(view)
            {
                time = (TextView)view.FindViewById(Resource.Id.time);
                linear = (LinearLayout)view.FindViewById(Resource.Id.linear);
            }
        }

    }

}