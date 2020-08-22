﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V7.App;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Drawing;
using com.resturant.Droid.Resturant.Config;
using Android.Support.V7.Widget;
using Xamarin.Essentials;
using Java.Util;
using Org.Json;
using com.resturant.Droid.Resturant.utils;
using static Volley.Request;
using Volley;
using Android.Util;
using Newtonsoft.Json;
using Android.Text;
using Java.Lang;
using Java.IO;
using Android.Provider;
using Android.Database;
using Android.Graphics;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Http;
using Android.Support.Design.Widget;

namespace com.resturant.Droid.Resturant.Fragments
{
    [Activity(Label = "Edit_profile_fragment")]
    public class Edit_profile_fragment : Android.Support.V4.App.Fragment, View.IOnClickListener, Response.IListener, Response.IErrorListener
    {


        private EditText et_phone, et_name, et_email;
        private RelativeLayout btn_update;
        private ImageView iv_profile;
        ISharedPreferences myPrefrence;

        private string filePath = string.Empty;
        private static int GALLERY_REQUEST_CODE1 = 201;
        private Android.Graphics.Bitmap bitmap;
        string getphone, getid, user_id;
        string getname;
        string getemail, getpassword;
        private Session_management sessionManagement;
        string emailPattern = "[a-zA-Z0-9._-]+@[a-z]+\\.+[a-z]+";
        CardView circle1, circle2, circle3, circle4, circle5, circle6;
        bool Email_Status = false, Sms_Status = false, In_App = false;
        ISharedPreferences sharedPreferences;
        ISharedPreferencesEditor editor;
        TextView update_profile;

        public Edit_profile_fragment()
        {
            // Required empty public constructor
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState)
        {
            // Inflate the layout for this fragment
            View view = inflater.Inflate(Resource.Layout.fragment_edit_profile, container, false);

            sharedPreferences = this.Context.GetSharedPreferences("User_profile", FileCreationMode.Private);
            editor = sharedPreferences.Edit();

            ((MainActivity)this.Activity).Title = "Edit Profile";

            Email_Status = sharedPreferences.GetBoolean("Email", true);
            Sms_Status = sharedPreferences.GetBoolean("Sms", true);
            In_App = sharedPreferences.GetBoolean("App", true);
            sessionManagement = new Session_management(this.Activity);
            user_id = sessionManagement.getUserDetails().Get(BaseURL.KEY_ID).ToString();

            et_phone = (EditText)view.FindViewById(Resource.Id.et_pro_phone);
            et_name = (EditText)view.FindViewById(Resource.Id.et_pro_name);
            et_email = (EditText)view.FindViewById(Resource.Id.et_pro_email);
            btn_update = (RelativeLayout)view.FindViewById(Resource.Id.btn_pro_edit);

            getemail = sessionManagement.getUserDetails().Get(BaseURL.KEY_EMAIL).ToString();
            getpassword = sessionManagement.getUserDetails().Get(BaseURL.KEY_PASSWORD).ToString();
            getname = sessionManagement.getUserDetails().Get(BaseURL.KEY_NAME).ToString();
            getphone = sessionManagement.getUserDetails().Get(BaseURL.KEY_MOBILE).ToString();
            getid = sessionManagement.getUserDetails().Get(BaseURL.KEY_ID).ToString();

            et_name.Text = getname;
            et_phone.Text = getphone;
            et_email.Text = getemail;
            update_profile = (TextView)view.FindViewById(Resource.Id.update_profile);
            circle1 = (CardView)view.FindViewById(Resource.Id.circle1);
            circle2 = (CardView)view.FindViewById(Resource.Id.circle2);
            circle3 = (CardView)view.FindViewById(Resource.Id.circle3);
            circle4 = (CardView)view.FindViewById(Resource.Id.circle4);
            circle5 = (CardView)view.FindViewById(Resource.Id.circle5);
            circle6 = (CardView)view.FindViewById(Resource.Id.circle6);


            if (Email_Status)
            {
                circle1.SetCardBackgroundColor(Resource.Color.colorPrimaryDark);
                circle2.SetCardBackgroundColor(Resource.Color.grey);
                circle1.Enabled = false;
                circle2.Enabled = true;

            }
            else
            {
                circle2.SetCardBackgroundColor(Resource.Color.colorPrimaryDark);
                circle1.SetCardBackgroundColor(Resource.Color.grey);
                circle2.Enabled = false;
                circle1.Enabled = true;

            }
            if (Sms_Status)
            {
                circle3.SetCardBackgroundColor(Resource.Color.colorPrimaryDark);
                circle4.SetCardBackgroundColor(Resource.Color.grey);
                circle3.Enabled = false;
                circle4.Enabled = true;

            }
            else
            {
                circle4.SetCardBackgroundColor(Resource.Color.colorPrimaryDark);
                circle3.SetCardBackgroundColor(Resource.Color.grey);
                circle4.Enabled = false;
                circle3.Enabled = true;
            }

            if (In_App)
            {
                circle5.SetCardBackgroundColor(Resource.Color.colorPrimaryDark);
                circle6.SetCardBackgroundColor(Resource.Color.grey);
                circle5.Enabled = false;
                circle6.Enabled = true;

            }
            else
            {
                circle6.SetCardBackgroundColor(Resource.Color.colorPrimaryDark);
                circle5.SetCardBackgroundColor(Resource.Color.grey);
                circle6.Enabled = false;
                circle5.Enabled = true;
            }
            update_profile.Click += delegate
            {
                string email_, sms_;
                editor.PutBoolean("Email", Email_Status);
                editor.PutBoolean("Sms", Sms_Status);
                editor.PutBoolean("App", In_App);
                editor.Commit();
                editor.Apply();
                //recreate();
                if (Email_Status)
                {
                    email_ = "1";

                }
                else
                {
                    email_ = "0";
                }
                if (Sms_Status)
                {
                    sms_ = "1";
                }
                else
                {
                    sms_ = "0";
                }
                if (In_App)
                {
                    sms_ = "1";
                }
                else
                {
                    sms_ = "0";
                }
                Notif(user_id, email_, "1", sms_);
            };

            circle1.Click += delegate
            {
                circle1.SetCardBackgroundColor(Resource.Color.colorPrimaryDark);
                circle2.SetCardBackgroundColor(Resource.Color.grey);
                circle1.Enabled = (false);
                Email_Status = true;
                circle2.Enabled = (true);

            };
            circle2.Click += delegate
            {
                Email_Status = false;
                circle2.SetCardBackgroundColor(Resource.Color.colorPrimaryDark);
                circle1.SetCardBackgroundColor(Resource.Color.grey);
                circle2.Enabled = (false);
                circle1.Enabled = (true);

            };

            circle3.Click += delegate
            {
                Sms_Status = true;
                circle3.SetCardBackgroundColor(Resource.Color.colorPrimaryDark);
                circle4.SetCardBackgroundColor(Resource.Color.grey);
                circle3.Enabled = (false);
                circle4.Enabled = (true);

            };
            circle4.Click += delegate
        {
            Sms_Status = false;
            circle4.SetCardBackgroundColor(Resource.Color.colorPrimaryDark);
            circle3.SetCardBackgroundColor(Resource.Color.grey);
            circle4.Enabled = (false);
            circle3.Enabled = (true);
        };
            circle5.Click += delegate
            {
                In_App = true;
                circle5.SetCardBackgroundColor(Resource.Color.colorPrimaryDark);
                circle6.SetCardBackgroundColor(Resource.Color.grey);
                circle5.Enabled = (false);
                circle6.Enabled = (true);

            };

            circle6.Click += delegate
            {
                In_App = false;
                circle6.SetCardBackgroundColor(Resource.Color.colorPrimaryDark);
                circle5.SetCardBackgroundColor(Resource.Color.grey);
                circle6.Enabled = (false);
                circle5.Enabled = (true);
            };

            if (!TextUtils.IsEmpty(getemail))
            {
                et_email.Text = getemail;
            }


            btn_update.SetOnClickListener(this);

            return view;
        }

        public void OnClick(View view)
        {
            int id = view.Id;
            if (id == Resource.Id.btn_pro_edit)
            {

                if (string.IsNullOrEmpty(et_email.Text.ToString()))
                {
                    Toast.MakeText(this.Activity, "enter email address", ToastLength.Short).Show();
                }
                else
                {
                    
                    if (Regex.IsMatch(et_email.Text.ToString().Trim(), emailPattern))
                    {
                        getphone = et_phone.Text.ToString();
                        getname = et_name.Text.ToString();
                        getemail = et_email.Text.ToString();

                        storeImage(bitmap);

                        updateprofile();

                    }

                    else
                    {
                        Toast.MakeText(this.Activity, "Invalid email address", ToastLength.Short).Show();
                    }
                }
            }
        }

        private void Notif(string user_id, string email1, string app, string sms)
        {
            HashMap user = new HashMap();
            string tag_json_obj = "json store req";
            user.Put("user_id", user_id);
            user.Put("sms", sms);
            user.Put("app", app);
            user.Put("email", email1);
            CustomVolleyJsonRequest jsonObjectRequest = new CustomVolleyJsonRequest(Method.Post, BaseURL.updatenotifyby, user, this, this);


            AppController.getInstance().addToRequestQueue(jsonObjectRequest, tag_json_obj);
        }

        public void OnResponse(Java.Lang.Object response)
        {
            Log.Debug("Tag", response.ToString());

            try
            {
                var result = JsonConvert.DeserializeObject<ResponseMessage>(response.ToString());
                string message = result.message;

                string status = result.status;

                if (status.Contains("1"))
                {

                    try
                    {
                        JSONObject obj = result.data;
                        string partymaster_id = obj.GetString("partymasterid");
                        string user_id = obj.GetString("user_id");
                        string user_fullname = obj.GetString("user_name");
                        string user_email = obj.GetString("user_email");
                        string user_phone = obj.GetString("user_phone");
                        string password = obj.GetString("user_password");

                        Session_management sessionManagement = new Session_management(this.Context);
                        ISharedPreferencesEditor editor = this.Context.GetSharedPreferences(BaseURL.MyPrefreance, FileCreationMode.Private).Edit();
                        editor.PutString(BaseURL.KEY_MOBILE, user_phone);
                        editor.PutString(BaseURL.KEY_PASSWORD, password);
                        editor.Apply();
                        sessionManagement.createLoginSession(partymaster_id,user_id, user_email,user_fullname, user_phone, password);
                        Intent intent = new Intent(Context, typeof(MainActivity));
                        StartActivity(intent);

                        Edit_profile_fragment fm = new Edit_profile_fragment();

                        Android.Support.V4.App.FragmentManager fragmentManager = FragmentManager;
                        fragmentManager.BeginTransaction().Replace(Resource.Id.contentPanel, fm).AddToBackStack(null).Commit();

                        Toast.MakeText(Context, message, ToastLength.Short).Show();


                    }
                    catch (Java.Lang.Exception e)
                    {

                    }


                    Toast.MakeText(this.Context, "" + message, ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this.Context, "" + message, ToastLength.Short).Show();

                }
            }
            catch (JSONException e)
            {
                e.PrintStackTrace();
            }
        }

        private async void updateprofile()
        {

            HashMap paramss = new HashMap();
            paramss.Put("partyname", getname);
            paramss.Put("partymastid", getid);
            paramss.Put("mailid", getemail);

            var uri = BaseURL.EDIT_PROFILE_URL;
            var registerData = JsonConvert.SerializeObject(paramss);

            using (var client = new HttpClient())
            {
                var jsonContent = new StringContent(registerData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    var resultString = await response.Content.ReadAsStringAsync();
                    Snackbar.Make(this.View, "Sucessfull", Snackbar.LengthShort).Show();
                }

            }

            //CustomVolleyJsonRequest jsonObjectRequest = new CustomVolleyJsonRequest(Method.Post, BaseURL.EDIT_PROFILE_URL, paramss, this, this);

            //AppController.getInstance().addToRequestQueue(jsonObjectRequest, tag_json_obj);
        }



        public void OnErrorResponse(VolleyError p0)
        {
            throw new NotImplementedException();
        }

        private void storeImage(Android.Graphics.Bitmap thumbnail)
        {
            //        if (iv_profile.getDrawable() == null) {
            //            //  Toast.makeText(getActivity(), "Select Image", Toast.LENGTH_SHORT).show();
            //
            //        } else {
            //            myPrefrence = PreferenceManager.getDefaultSharedPreferences(getActivity());
            //            SharedPreferences.Editor edit = myPrefrence.edit();
            //            edit.remove("image_data");
            //            edit.commit();
            //            ByteArrayOutputStream bytes = new ByteArrayOutputStream();
            //            thumbnail.compress(Bitmap.CompressFormat.JPEG, 100, bytes);
            //            File destination = new File(Environment.getExternalStorageDirectory(),
            //                    System.currentTimeMillis() + ".jpg");
            //            FileOutputStream fo;
            //            try {
            //                destination.createNewFile();
            //                fo = new FileOutputStream(destination);
            //                fo.write(bytes.toByteArray());
            //                fo.close();
            //            } catch (FileNotFoundException e) {
            //                e.printStackTrace();
            //            } catch (IOException e) {
            //                e.printStackTrace();
            //            }
            //            iv_profile.setImageBitmap(thumbnail);
            //            byte[] b = bytes.toByteArray();
            //            String encodedImage = Base64.encodeToString(b, Base64.DEFAULT);
            //            edit.putString("image_data", encodedImage);
            //            edit.commit();
            //
            //        }

        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            // if the result is capturing Image
            if ((requestCode == GALLERY_REQUEST_CODE1))
            {

                try
                {
                    Android.Net.Uri selectedImage = data.Data;
                    string[] filePathColumn = { MediaStore.Images.Media.InterfaceConsts.Data };

                    // Get the cursor
                    ICursor cursor = this.Activity.ContentResolver.Query(selectedImage, filePathColumn, null, null, null);
                    // Move to first row
                    cursor.MoveToFirst();

                    int columnIndex = cursor.GetColumnIndex(filePathColumn[0]);
                    string imgDecodableString = cursor.GetString(columnIndex);
                    cursor.Close();

                    //filePath = imgDecodableString;

                    Android.Graphics.Bitmap b = BitmapFactory.DecodeFile(imgDecodableString);
                    Android.Graphics.Bitmap outputb = Android.Graphics.Bitmap.CreateScaledBitmap(b, 1200, 1024, false);

                    //getting image from gallery
                    bitmap = MediaStore.Images.Media.GetBitmap(this.Activity.ContentResolver, selectedImage);


                    Java.IO.File file = new Java.IO.File(imgDecodableString);
                    filePath = file.AbsolutePath;
                    FileStream fOut;
                    try
                    {
                        fOut = new FileStream(filePath, FileMode.Create);
                        outputb.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 80, fOut);
                        fOut.Flush();
                        fOut.Close();
                        //b.recycle();
                        //out.recycle();
                    }
                    catch (Java.Lang.Exception e)
                    {
                        e.PrintStackTrace();
                    }

                    if (requestCode == GALLERY_REQUEST_CODE1)
                    {

                        // Set the Image in ImageView after decoding the String
                        iv_profile.SetImageBitmap(bitmap);
                    }
                }
                catch (NullPointerException e)
                {
                    e.PrintStackTrace();
                }
                catch (Java.Lang.Exception e)
                {
                    e.PrintStackTrace();
                }
            }
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            // TODO Add your menu entries here
            base.OnCreateOptionsMenu(menu, inflater);

            /*  MenuItem cart = menu.findItem(R.id.action_cart);
              cart.setVisible(false);
      */
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {

            }
            return false;
        }

    }

    public class ResponseMessage
    {
        public string message { get; set; }
        public string status { get; set; }
        public JSONObject data { get; set; }
    }

}