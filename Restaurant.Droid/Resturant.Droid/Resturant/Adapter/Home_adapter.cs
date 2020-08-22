﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Glide;
using Android.Glide.Request;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using com.resturant.Droid.Model;
using com.resturant.Droid.Resturant.Activities;
using com.resturant.Droid.Resturant.Config;

namespace com.resturant.Droid.Resturant.Adapter
{
    public class Home_adapter : RecyclerView.Adapter
    {
        private List<Category_model> modelList;
        private Context context;
        string language;
        ISharedPreferences preferences;

        public Home_adapter(List<Category_model> modelList, Context _context)
        {
            this.modelList = modelList;
            this.context = _context;
            NotifyDataSetChanged();
        }

        public override int ItemCount => modelList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Category_model category_ModelList = modelList[position];
            MyViewHolder myViewHolder = holder as MyViewHolder;
            if (category_ModelList.image != "" || !string.IsNullOrEmpty(category_ModelList.image))
            {
                Glide.With(context)
               .Load(category_ModelList.image)
               .Apply(RequestOptions.CircleCropTransform())
               .Into(myViewHolder.image);
            }


            preferences = context.GetSharedPreferences("lan", FileCreationMode.Private);
            language = preferences.GetString("language", "");
            myViewHolder.title.Text = category_ModelList.title;
            myViewHolder.mainView.Click += delegate
            {
                string getid = modelList[position].cat_id;

                Intent intent = new Intent(context, typeof(CategoryPage));
                intent.PutExtra("cat_id", getid);
                intent.PutExtra("title", modelList[position].title);
                intent.PutExtra("image", modelList[position].image);
                context.StartActivity(intent);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.row_home_rv, parent, false);
            TextView title = (TextView)itemView.FindViewById(Resource.Id.tv_home_title);
            ImageView image = (ImageView)itemView.FindViewById(Resource.Id.iv_home_img);

            MyViewHolder view = new MyViewHolder(itemView)
            {
                title = title,
                image = image
            };
            return view;
        }

        public class MyViewHolder : RecyclerView.ViewHolder
        {
            public TextView title { get; set; }
            public ImageView image { get; set; }
            public View mainView { get; set; }

            public MyViewHolder(View view) : base(view)
            {
                mainView = view;
            }
        }
    }


}