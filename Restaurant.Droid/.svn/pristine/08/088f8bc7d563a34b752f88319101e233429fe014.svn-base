using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using com.resturant.Droid.Model;
using com.resturant.Droid.Resturant.Activities;

namespace com.resturant.Droid.Resturant.Adapter
{
    public class SearchAdapter : RecyclerView.Adapter
    {
        public List<SearchModel> searchList;
        public Activity activity;

        public SearchAdapter(List<SearchModel> searchList,Activity activity)
        {
            this.searchList = searchList;
            this.activity = activity;
        }

        public override int ItemCount => searchList.Count();

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyViewHolder myViewHolder = holder as MyViewHolder;
            SearchModel map = new SearchModel();
            map = searchList[position];
            myViewHolder.title.Text = map.Name;
            myViewHolder.title.Click += delegate
            {
            var seaarchId = searchList[position].Id;
            var seaarchName = searchList[position].Name;
            Intent intent = new Intent(activity,typeof(ProductDetails));
                intent.PutExtra("sId",seaarchId);
                intent.PutExtra("sName",seaarchName);
                activity.StartActivity(intent);
    };
}

public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
{
    View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.layout_searchlist, parent, false);
    MyViewHolder view = new MyViewHolder(itemView);
    return view;
}

public class MyViewHolder : RecyclerView.ViewHolder
{
    public TextView title;
    public View mainView;

    public MyViewHolder(View view) : base(view)
    {
        title = (TextView)view.FindViewById(Resource.Id.pNAme);
        mainView = view;
    }
}
    }
}