using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Annotation;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;

namespace com.resturant.Droid
{
   public class LocaleHelper
    {
        private static string SELECTED_LANGUAGE = "Locale.Helper.Selected.Language";

        public static Context onAttach(Context context)
        {
            string lang = getPersistedData(context, Locale.Default.Language);
            return setLocale(context, lang);
        }

        public static Context onAttach(Context context, string defaultLanguage)
        {
            string lang = getPersistedData(context, defaultLanguage);
            return setLocale(context, lang);
        }

        public static string getLanguage(Context context)
        {
            return getPersistedData(context, Locale.Default.Language);
        }

        public static Context setLocale(Context context, string language)
        {
            persist(context, language);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
            {
                return updateResources(context, language);
            }

            return updateResourcesLegacy(context, language);
        }

        private static string getPersistedData(Context context, string defaultLanguage)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);
            return preferences.GetString(SELECTED_LANGUAGE, defaultLanguage);
        }

        private static void persist(Context context, string language)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);
            ISharedPreferencesEditor editor = preferences.Edit();

            editor.PutString(SELECTED_LANGUAGE, language);
            editor.Apply();
        }

        //[ TargetApi (Value = (int)Build.VERSION_CODES.N)]
        private static Context updateResources(Context context, string language)
        {
            Locale locale = new Locale(language);
            Locale.SetDefault(Locale.Category.Format ,locale);

            Configuration configuration = context.Resources.Configuration;
            configuration.SetLocale(locale);

            return context.CreateConfigurationContext(configuration);
        }

        [Obsolete]
        private static Context updateResourcesLegacy(Context context, string language)
        {
            Locale locale = new Locale(language);
            Locale.SetDefault(Locale.Category.Format,locale);

            Resources resources = context.Resources;

            Configuration configuration = resources.Configuration;
            configuration.Locale = locale;

            resources.UpdateConfiguration(configuration, resources.DisplayMetrics);

            return context;
        }
    }
}