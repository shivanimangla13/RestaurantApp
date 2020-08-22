﻿using System;

namespace com.resturant.Droid.Resturant.Config
{
    public class BaseURL
    {
        public static string APP_NAME = "GoGrocer";
        public static string MyPrefreance = "my_preprence";

        public static string PREFS_NAME = "GroceryLoginPrefs";
        public static string PREFS_NAME2 = "GroceryLoginPrefs2";
        public static string IS_LOGIN = "isLogin";
        public static string KEY_NAME = "user_fullname";
        public static string KEY_EMAIL = "user_email";
        public static string TOTAL_AMOUNT = "TOTAL_AMOUNT";
        public static string WALLET_TOTAL_AMOUNT = "WALLET_TOTAL_AMOUNT";
        public static string COUPON_TOTAL_AMOUNT = "COUPON_TOTAL_AMOUNT";
        public static string KEY_PARTYMASTER_ID = "partymasterid";
        public static string KEY_ID = "user_id";
        public static string KEY_MOBILE = "user_phone";
        public static string KEY_IMAGE = "user_image";
        public static string KEY_WALLET_Ammount = "wallet_ammount";
        public static string KEY_REWARDS_POINTS = "rewards_points";
        public static string KEY_PAYMENT_METHOD = "payment_method";
        public static string KEY_REWARDS = "rewards";
        public static string KEY_DATE = "date";
        public static string KEY_TIME = "time";
        public static string ISOPEN = "isopen";


        //Store
        public static string KET_STORECART = "storecart";
        public static string KEY_Checkout = "checkout";



        //adreeessss
        public static string ADDRESS = "address";
        public static string CITY = "city";
        public static string STATE = "state";
        public static string COUNTRY = "country";
        public static string STATEID = "stateid";
        public static string COUNTRYID = "countryid";
        public static string PINCODE = "pincode";
        public static string FULLNAME = "fullname";
        public static string MOBILE = "mobile";
        public static string EMAIL = "email";
        public static string LAT = "lat";
        public static string LONG = "long";


        public static string LANDMARK = "landmark";
        public static string SPECIALNOTES = "specialnotes";

        //Store Select

        public static string KEY_STORE_COUNT = "STORE_COUNT";
        public static string KEY_NOTIFICATION_COUNT = "NOTIFICATION_COUNT";

        //Firebase
        public static string SHARED_PREF = "ah_firebase";
        public static string TOPIC_GLOBAL = "global";
        public static int NOTIFICATION_ID = 100;
        public static int NOTIFICATION_ID_BIG_IMAGE = 101;


        public static string KEY_PASSWORD = "password";

        //City and Store Id
        public static string CITY_ID = "CITY_ID";
        public static string STORE_ID = "STORE_ID";

        public static string BASE_URL = "https://www.theapnamart.com/api/";
        public static string IMG_URL = "https://www.theapnamart.com/";
        public static string BANN_IMG_URL = "https://www.theapnamart.com/";
        public static string SignUp = BASE_URL + "api/v1/Register/SignUp";
        public static string SignUpOtp = BASE_URL + "verify_phone";

        public static string Login = BASE_URL + "api/v1/Register/Login";
        public static string forget_password = BASE_URL + "forget_password";
        public static string verify_otp = BASE_URL + "verify_otp";
        public static string ChangePass = BASE_URL + "change_password";

        public static string HomeTopSelling = BASE_URL + "api/v1/ItemMaster/GetFiletredItemMasterList?userid=0";
        public static string HomeRecent = BASE_URL + "recentselling";
        public static string HomeDeal = BASE_URL + "dealproduct";
        public static string redeem_rewards = BASE_URL + "redeem_rewards";
        public static string BANNER = BASE_URL + "api/v1/Company/GetStoreSliders";
        public static string secondary_banner = BASE_URL + "api/v1/Company/GetFooterBannerUrl";

        public static string Categories = BASE_URL + "api/v1/ItemCategory/Get";
        public static string ProductVarient = BASE_URL + "varient";
        public static string Search = BASE_URL + "api/v1/ItemMaster/GetItemListBySearch/";

        public static string AddToCart = BASE_URL + "api/v1/Cart/AddToCart";
        public static string DeleteCart = BASE_URL + "api/v1/Cart/DeleteCartByItemMastIdAndUserID";
        public static string DeleteCartItem = BASE_URL + "/api/v1/Cart/DeleteCart/";
        public static string ProcessToCheckout = BASE_URL + "api/v1/Cart/ProceedToCheckout?userid=";
        public static string GetCheckoutList = BASE_URL + "api/v1/Cart/GetCheckoutList/";
        public static string SaveCheckoutList = BASE_URL + "api/v1/Cart/SaveCheckout";
        public static string CartQuantityUpdate = BASE_URL +  "api/v1/Cart/CartQuantityUpdate";

        //Country 
        public static string GET_COUNTRY = BASE_URL + "api/v1/Country/Get";

        //State
        public static string GET_STATE = BASE_URL + "api/v1/Country/GetStates/";

        public static string CityListUrl = BASE_URL + "city";
        public static string SocietyListUrl = BASE_URL + "society";
        public static string Add_address = BASE_URL + "add_address";
        public static string ShowAddress = BASE_URL + "show_address";
        public static string SelectAddressURL = BASE_URL + "select_address";
        public static string EditAddress = BASE_URL + "edit_address";
        public static string DELETE_ORDER_URL = BASE_URL + "cancelling_reasons";
        public static string delete_order = BASE_URL + "delete_order";
        public static string delivery_info = BASE_URL + "delivery_info";
        public static string Get_CartList = BASE_URL + "api/v1/Cart/GetCartListByUserID/";

        public static string CalenderUrl = BASE_URL + "timeslot";

        public static string WALLET_REFRESH = BASE_URL + "walletamount?user_id=";
        public static string RecharegeWallet = BASE_URL + "recharge_wallet";
        public static string myprofile = BASE_URL + "api/v1/UserInfo/GetPartyMasterByUserid/";
        public static string OrderDoneUrl = BASE_URL + "completed_orders";
        public static string PendingOrderUrl = BASE_URL + "ongoing_orders";

        public static string AboutUrl = BASE_URL + "appaboutus";
        public static string topsix = BASE_URL + "topsix";
        public static string TermsUrl = BASE_URL + "appterms";

        public static string delete_all_notification = BASE_URL + "delete_all_notification";
        public static string SupportUrl = BASE_URL + "appterms";

        public static string EDIT_PROFILE_URL = BASE_URL + "/api/v1/UserInfo/UpdateUserProfil";
        public static string cat_product = BASE_URL + "cat_product";
        public static string OrderContinue = BASE_URL + "make_an_order";
        public static string MinMaxOrder = BASE_URL + "minmax";
        public static string rewardlines = BASE_URL + "rewardlines";

        public static string Wallet_CHECKOUT = BASE_URL + "";
        public static string ADD_ORDER_URL = BASE_URL + "checkout";
        public static string COUPON_CODE = BASE_URL + "couponlist";
        public static string apply_coupon = BASE_URL + "apply_coupon";
        public static string whatsnew = BASE_URL + "whatsnew";

        public static string NoticeURl = BASE_URL + "notificationlist";
        public static string updatenotifyby = BASE_URL + "updatenotifyby";

        /*  public static string REGISTER_URL = BASE_URL + "register";
          public static string Otp = BASE_URL + "verify_otp";
          public static string LOGIN_URL = BASE_URL + "login";

          public static string IMG_SLIDER_URL = BASE_URL + "uploads/sliders/";
          public static string IMG_CATEGORY_URL = BASE_URL + "uploads/category/";
          public static string IMG_PRODUCT_URL = BASE_URL + "uploads/products/";

          public static string IMG_PROFILE_URL = BASE_URL + "uploads/profile/";
          public static string GET_SLIDER_URL = BASE_URL + "index.php/api/get_sliders";
          public static string GET_FEAATURED_SLIDER_URL = BASE_URL + "index.php/api/get_feature_banner";
          public static string GET_BANNER_URL = BASE_URL + "index.php/api/get_banner";

          public static string WALLET_REFRESH = BASE_URL + "index.php/api/wallet?user_id=";
          public static string REWARDS_REFRESH = BASE_URL + "index.php/api/rewards?user_id=";

          public static string GET_CATEGORY_URL = BASE_URL + "index.php/api/get_categories";
          public static string GET_SLIDER_CATEGORY_URL = BASE_URL + "index.php/api/get_sub_cat";
          public static string GET_CATEGORY_ICON_URL = BASE_URL + "index.php/api/icon";
          public static string COUPON_CODE = BASE_URL + "index.php/api/get_coupons";

          //Home PAGE

          public static string GET_MENU_PRODUCTS = BASE_URL + "index.php/api/icon";
          public static string GET_MENU_ICON_PRODUCT_URL = BASE_URL + "index.php/api/get_header_products";
          public static string GET_DEAL_OF_DAY_PRODUCTS = BASE_URL + "index.php/api/deal_product";
          public static string GET_ALL_DEAL_OF_DAY_PRODUCTS = BASE_URL + "index.php/api/get_all_deal_product";
          public static string GET_TOP_SELLING_PRODUCTS = BASE_URL + "index.php/api/top_selling_product";
          public static string GET_ALL_TOP_SELLING_PRODUCTS = BASE_URL + "index.php/api/get_all_top_selling_product";

          public static string GET_PRODUCT_URL = BASE_URL + "index.php/api/get_products";

          public static string GET_ABOUT_URL = BASE_URL + "index.php/api/aboutus";

          public static string GET_SUPPORT_URL = BASE_URL + "index.php/api/support";

          public static string GET_TERMS_URL = BASE_URL + "index.php/api/terms";

          public static string GET_TIME_SLOT_URL = BASE_URL + "index.php/api/get_time_slot";

          public static string GET_SOCITY_URL = BASE_URL + "index.php/api/get_society";

          public static string EDIT_PROFILE_URL = BASE_URL + "index.php/api/update_userdata";

          public static string ADD_ORDER_URL = BASE_URL + "index.php/api/send_order";
          public static string Wallet_CHECKOUT = BASE_URL + "index.php/api/wallet_at_checkout";
          public static string GET_ORDER_URL = BASE_URL + "index.php/api/my_orders";

          public static string GET_DELIVERD_ORDER_URL = BASE_URL + "index.php/api/delivered_complete";

          public static string ORDER_DETAIL_URL = BASE_URL + "index.php/api/order_details";

          public static string DELETE_ORDER_URL = BASE_URL + "index.php/api/cancel_order";

          public static string GET_LIMITE_SETTING_URL = BASE_URL + "index.php/api/get_limit_settings";

          public static string ADD_ADDRESS_URL = BASE_URL + "index.php/api/add_address";

          public static string GET_ADDRESS_URL = BASE_URL + "index.php/api/get_address";

          public static string FORGOT_URL = BASE_URL + "index.php/api/forgot_password";

          public static string JSON_RIGISTER_FCM = BASE_URL + "index.php/api/register_fcm";

          public static string CHANGE_PASSWORD_URL = BASE_URL + "index.php/api/change_password";

          public static string DELETE_ADDRESS_URL = BASE_URL + "index.php/api/delete_address";

          public static string EDIT_ADDRESS_URL = BASE_URL + "index.php/api/edit_address";

          // global topic to receive app wide push notifications

          // broadcast receiver intent filters
          public static final string REGISTRATION_COMPLETE = "registrationComplete";

          public static final string PUSH_NOTIFICATION = "pushNotification";
      */

    }
}