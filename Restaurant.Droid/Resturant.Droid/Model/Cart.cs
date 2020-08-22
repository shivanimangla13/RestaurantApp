using System;

namespace com.resturant.Droid.Model
{
    public class Cart
    {
        public long ID { get; set; }
        public long UserId { get; set; }
        public long ItemMasterId { get; set; }
        public string ItemMastId { get; set; }
        public string ItemDesc { get; set; }
        public decimal Quantity { get; set; }
        public decimal SelRate { get; set; }
        public decimal GSTRate { get; set; }
        public byte[] ItemImage { get; set; }
        public string ItemImageType { get; set; }
        public DateTime Date { get; set; }
        

        public long rowstate { get; set; }
        public long WarehouseID { get; set; }
    }
}