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

namespace com.resturant.Droid.Model
{
   public class ResultArray
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Category> data { get; set; }
    }
}