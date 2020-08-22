using System;

namespace com.resturant.Droid.Model
{
     public class CheckoutData
    {

        public StoreCheckout CheckoutObj { get; set; }
        public Decimal TotalAmount { get; set; }
        public Decimal RedeemValue { get; set; }
        public Decimal CouponAmount { get; set; }
        public string CouponPin { get; set; }
        public Decimal Discount { get; set; }
        public Decimal REAmount { get; set; }

    }
}