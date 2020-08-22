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
using Java.IO;
using Java.Util;
using Newtonsoft.Json;
using Org.Json;
using Volley;
using Volley.Toolbox;
using Xamarin.Essentials;

namespace com.resturant.Droid.Resturant.utils
{
    public class CustomVolleyJsonRequest : Request
    {
        private Response.IListener listener;
        private HashMap parameter;


        

        public CustomVolleyJsonRequest(int method, string url, HashMap paramss, Response.IListener reponseListener, Response.IErrorListener errorListener)
            : base(method, url, errorListener)
        {
            this.listener = reponseListener;
            this.parameter = paramss;
        }

        protected HashMap getParams()
        {
            return parameter;
        }
        public CustomVolleyJsonRequest(string url, HashMap paramss, Response.IListener reponseListener, Response.IErrorListener errorListener)
            : base(Method.Get, url, errorListener)
        {
            this.listener = reponseListener;
            this.parameter = paramss;
        }

        protected override void DeliverResponse(Java.Lang.Object response)
        {
            listener.OnResponse(response);
        }

        protected override Response ParseNetworkResponse(NetworkResponse response)
        {
            try
            {
                var responseData = response.Data.ToString()[0];
                var header = new Dictionary<string, string>();

                foreach (KeyValuePair<string, string> kvp in response.Headers)
                {
                    header.Add(kvp.Key, kvp.Value);
                }
                Dictionary<string, string> headerData = header;

                string jsonString = new string(responseData, HttpHeaderParser.ParseCharset(headerData).Count());
                return Response.Success(new JSONObject(jsonString),
                        HttpHeaderParser.ParseCacheHeaders(response));
            }
            catch (UnsupportedEncodingException e)
            {
                return Response.Error(new ParseError(e));
            }
            catch (JSONException je)
            {
                return Response.Error(new ParseError(je));
            }
        }
    }
}