using System;

namespace com.resturant.Droid.Model
{
    public class Checkout
    {
        public StoreCheckout()
        {
            objGSTPer = new GSTPercentage();
        }

        public long ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string Landmark { get; set; }
        public string SpecialNote { get; set; }
        public string Packing { get; set; }
        public string PayBy { get; set; }

        public long UserId { get; set; }
        public long ItemMasterId { get; set; }
        public string ItemId { get; set; }
        public string ItemDesc { get; set; }
        public decimal Quantity { get; set; }
        public decimal SelRate { get; set; }
        public byte[] ItemImage { get; set; }
        public string ItemImageType { get; set; }
        public DateTime? Date { get; set; }
        public long PartyMasterId { get; set; }
        public long OrderId { get; set; }

        public string Status { get; set; }
        public int rowstate { get; set; }
        public decimal GSTRate { get; set; }
        public decimal MRP { get; set; }
        public decimal BusinessValue { get; set; }
        public int Reward { get; set; }

        public int discount { get; set; }

        public string Tax { get; set; }

        public string ActivateUser { get; set; }

        public decimal ShippingCharges { get; set; }
        public decimal RedeemValue { get; set; }
        public decimal WalletRedeemRestrictionValue { get; set; }
        public int PlanID { get; set; }
        public long WarehouseId { get; set; }
        public decimal CappingAmount { get; set; }

        public string ItemCategoryType { get; set; }
        public GSTPercentage objGSTPer { get; set; }

        public string memberid { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string SEOName { get; set; }
        

    }
}