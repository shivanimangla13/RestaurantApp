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
    public class AddToCartModel
    {
        public int UserId { get; set; }
        public int ItemMasterId { get; set; }
        public string ItemMastId { get; set; }
        public string ItemDesc { get; set; }
        public int Quantity { get; set; }
        public decimal SelRate { get; set; }
        public decimal GSTRate { get; set; }
        public string ItemImage { get; set; }
        public string ItemImageType { get; set; }
        public DateTime Date { get; set; }
        public int rowstate { get; set; }
        public int WarehouseID { get; set; }
    }

    public class DeleteCart
    {
        public int UserId { get; set; }
        public int ItemMasterId { get; set; }
    }
}