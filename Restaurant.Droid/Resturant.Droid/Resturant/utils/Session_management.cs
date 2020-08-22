﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using com.resturant.Droid.Model;
using Java.Util;
using static com.resturant.Droid.Resturant.Config.BaseURL;

namespace com.resturant.Droid
{
    public class Session_management
    {

        ISharedPreferences prefs;
        ISharedPreferences prefs2;

        ISharedPreferencesEditor editor;
        ISharedPreferencesEditor editor2;

        Context context;

        public Session_management(Context context)
        {

            this.context = context;
            prefs = context.GetSharedPreferences(PREFS_NAME, FileCreationMode.Private);
            editor = prefs.Edit();

            prefs2 = context.GetSharedPreferences(PREFS_NAME2, FileCreationMode.Private);
            editor2 = prefs2.Edit();

        }

        public void createLoginSession(string partyMasterId,string Userid,string  email, string name, string mobile, string password)
        {

            editor.PutBoolean(IS_LOGIN, true);
            editor.PutString(KEY_PARTYMASTER_ID, partyMasterId);
            editor.PutString(KEY_ID, Userid);
            editor.PutString(KEY_EMAIL, email);
            editor.PutString(KEY_NAME, name);
            editor.PutString(KEY_MOBILE, mobile);
            editor.PutString(KEY_PASSWORD, password);
            editor.Commit();
        }

        public void createotpSession(string useremail, string username, string user_phone)
        {

            editor.PutBoolean(IS_LOGIN, true);
            editor.PutString(KEY_EMAIL, useremail);
            editor.PutString(KEY_NAME, username);
            editor.PutString(KEY_MOBILE, user_phone);

            editor.Commit();
        }

        public void createsignupSession(string id)
        {

            editor.PutBoolean(IS_LOGIN, true);
            editor.PutString(KEY_ID, id);

            editor.Commit();
        }

        public void checkLogin()
        {
            if (!this.isLoggedIn())
            {
                Intent loginsucces = new Intent(context, typeof(LoginActivity));
                // Closing all the Activities
                loginsucces.AddFlags(ActivityFlags.ClearTop);

                // Add new Flag to start new Activity
                loginsucces.SetFlags(ActivityFlags.NewTask);

                context.StartActivity(loginsucces);
            }

        }

        public void SetStoreCart(string store)
        {
            editor.PutString(KET_STORECART, store);
            editor.Apply();
        }

        public HashMap GetStoreCart()
        {
            HashMap store = new HashMap();
            store.Put(KET_STORECART, prefs.GetString(KET_STORECART, string.Empty));
            return store;
        }

        public void SetCheckout(string checkout)
        {
            editor.PutString(KEY_Checkout, checkout);
            editor.Apply();
        }

        public HashMap GetCheckout()
        {
            HashMap store = new HashMap();
            store.Put(KEY_Checkout, prefs.GetString(KEY_Checkout, string.Empty));
            return store;
        }

        public HashMap getUserDetails()
        {
            HashMap user = new HashMap();

            if (isLoggedIn())
            {
                user.Put(KEY_ID, prefs.GetString(KEY_ID, null));
            }
            else
            {
                user.Put(KEY_ID, prefs.GetString(KEY_ID, "0"));
            }
            user.Put(KEY_EMAIL, prefs.GetString(KEY_EMAIL, null));
            user.Put(KEY_NAME, prefs.GetString(KEY_NAME, null));
            user.Put(KEY_MOBILE, prefs.GetString(KEY_MOBILE, null));
            user.Put(KEY_IMAGE, prefs.GetString(KEY_IMAGE, null));
            user.Put(KEY_WALLET_Ammount, prefs.GetString(KEY_WALLET_Ammount, null));
            user.Put(KEY_REWARDS_POINTS, prefs.GetString(KEY_REWARDS_POINTS, null));
            user.Put(KEY_PAYMENT_METHOD, prefs.GetString(KEY_PAYMENT_METHOD, ""));
            user.Put(TOTAL_AMOUNT, prefs.GetString(TOTAL_AMOUNT, null));
            user.Put(KEY_PASSWORD, prefs.GetString(KEY_PASSWORD, null));

            // return user
            return user;
        }

        public void updateData(string name, string mobile, string pincode, string socity_id, string image, string wallet, string rewards, string house)
        {

            editor.PutString(KEY_NAME, name);
            editor.PutString(KEY_MOBILE, mobile);
            editor.PutString(KEY_IMAGE, image);
            editor.PutString(KEY_WALLET_Ammount, wallet);
            editor.PutString(KEY_REWARDS_POINTS, rewards);
            editor.Apply();
        }

        public void AddAddress(UserInfo user)
        {
            editor.PutString(ADDRESS, user.partyaddress);
            editor.PutString(COUNTRY, user.countryvalue);
            editor.PutString(STATE, user.statevalue);
            editor.PutString(CITY, user.city);
            editor.PutString(PINCODE, user.pincode.ToString());
            editor.PutString(FULLNAME, user.partyname);
            editor.PutString(MOBILE, user.mobno);
            editor.PutString(EMAIL, user.mailid);
            editor.PutString(COUNTRYID, user.country.ToString());
            editor.PutString(STATEID, user.state.ToString());
            editor.Apply();
        }

        public void AddSpecialNotes(string landmark,string specialnotes)
        {
            editor.PutString(LANDMARK, landmark);
            editor.PutString(SPECIALNOTES, specialnotes);
            editor.Apply();
        }
        public HashMap GetSpcialNotes()
        {
            HashMap remark = new HashMap();
            remark.Put(LANDMARK, prefs.GetString(LANDMARK, string.Empty));
            remark.Put(SPECIALNOTES, prefs.GetString(LANDMARK, string.Empty));
            return remark;
        }

        public HashMap GetAddressDetails()
        {
            HashMap addres = new HashMap();
            addres.Put(ADDRESS, prefs.GetString(ADDRESS, string.Empty));
            addres.Put(COUNTRY, prefs.GetString(COUNTRY, string.Empty));
            addres.Put(STATE, prefs.GetString(STATE, string.Empty));
            addres.Put(CITY, prefs.GetString(CITY, string.Empty));
            addres.Put(PINCODE, prefs.GetString(PINCODE, string.Empty));
            addres.Put(FULLNAME, prefs.GetString(FULLNAME, string.Empty));
            addres.Put(MOBILE, prefs.GetString(MOBILE, string.Empty));
            addres.Put(EMAIL, prefs.GetString(EMAIL, string.Empty));
            addres.Put(COUNTRYID, prefs.GetString(COUNTRYID, "0"));
            addres.Put(STATEID, prefs.GetString(STATEID, "0"));
            return addres;
        }

        public void logoutSession()
        {
            editor.Clear();
            editor.Commit();

            cleardatetime();

            Intent logout = new Intent(context, typeof(MainActivity));
            // Closing all the Activities
            logout.AddFlags(ActivityFlags.ClearTop);

            // Add new Flag to start new Activity
            logout.SetFlags(ActivityFlags.NewTask);

            context.StartActivity(logout);
        }

        public void logoutSessionwithchangepassword()
        {
            editor.Clear();
            editor.Commit();

            cleardatetime();

            Intent logout = new Intent(context, typeof(LoginActivity));
            // Closing all the Activities
            logout.AddFlags(ActivityFlags.ClearTop);

            // Add new Flag to start new Activity
            logout.SetFlags(ActivityFlags.NewTask);

            context.StartActivity(logout);
        }

        public void creatdatetime(String date, String time)
        {
            editor2.PutString(KEY_DATE, date);
            editor2.PutString(KEY_TIME, time);

            editor2.Commit();
        }

        public void cleardatetime()
        {
            editor2.Clear();
            editor2.Commit();
        }

        public void setBooleanData(bool val)
        {
            editor2.PutBoolean(ISOPEN, val);
            editor2.Commit();
        }

        public bool getBooleanData()
        {
            return prefs.GetBoolean(ISOPEN,false);
        }

        public Java.Util.HashMap getdatetime()
        {
            Java.Util.HashMap user = new Java.Util.HashMap();

            user.Put(KEY_DATE, prefs2.GetString(KEY_DATE, null));
            user.Put(KEY_TIME, prefs2.GetString(KEY_TIME, null));

            return user;
        }

        // Get Login State
        public bool isLoggedIn()
        {
            return prefs.GetBoolean(IS_LOGIN, false);
        }
    }
}