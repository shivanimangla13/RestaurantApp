using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RestrauntApp.Adapters
{
    class RestrauntSearchListItemAdapter : BaseAdapter<string>
    {

        Context context;
        List<string> RestrauntList;

        public RestrauntSearchListItemAdapter(Context context,List<string> restrauntList)
        {
            this.context = context;
            this.RestrauntList = restrauntList;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return base.GetItem(position);
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                //view = LayoutInflater.From(context).Inflate(Resource.Layout.restraunt_searchlist_item_layout, parent, false);
                //RestrauntSearchListItemAdapterViewHolder holder = new RestrauntSearchListItemAdapterViewHolder();
                //holder.RestrauntImageView = view.FindViewById<ImageView>(Resource.Id.imageViewRestraunt);
                //holder.RestrauntNameTextView = view.FindViewById<TextView>(Resource.Id.textViewRestrauntName);
                //view.Tag = holder;
            }
            RestrauntSearchListItemAdapterViewHolder viewHolder = (RestrauntSearchListItemAdapterViewHolder)view.Tag;
            //viewHolder.RestrauntNameTextView.Text = "Refregerator Repair";
            return null;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return (RestrauntList ==null? 0 : RestrauntList.Count);
            }
        }
        public override string this[int position]
        {
            get
            {
                return RestrauntList[position];
            }
        }
    }

    class RestrauntSearchListItemAdapterViewHolder : Java.Lang.Object
    {
        public TextView RestrauntNameTextView { get; set; }
        public ImageView RestrauntImageView { get; set; }
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}