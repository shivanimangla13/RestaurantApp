using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Locations;
using Android.Net;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using com.resturant.Droid.Model;

using com.resturant.Droid.Resturant.Adapter;
using com.resturant.Droid.Resturant.Config;
using com.resturant.Droid.Resturant.Fragments;
using Newtonsoft.Json;
using static Android.Support.Design.Widget.TabLayout;

namespace com.resturant.Droid
{
    public class HomeeeFragment : Android.Support.V4.App.Fragment, RecyclerView.IOnItemTouchListener
    {
        ViewPager viewPager;
        TabLayout tabLayout;
        PageAdapter pageAdapter;

        TabItem tab1, tab2, tab3, tab4;
        float translationY = 100.00f;
        FloatingActionButton fabMain, fabOne, fabTwo, fabThree, fabfour;

        LinearLayout Search_layout;
        ScrollView scrollView;
        RecyclerView rv_items;

        OvershootInterpolator interpolator = new OvershootInterpolator();


        bool isMenuOpen = false;

        public string latitude, longitude, address, city, state, country, postalCode;
        LocationManager locationManager;
        ISharedPreferences sharedPreferences;
        private List<Category_model> category_modelList = new List<Category_model>();

        ImageView imageView;
        ImageView footerImage;
        int headerIndex;
        Android.Net.Uri[] headerImages;

        private Home_adapter adapter;

        public HomeeeFragment()
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container,
                            Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.fragment_home, container, false);
            imageView = view.FindViewById<ImageView>(Resource.Id.imageslider);
            footerImage = view.FindViewById<ImageView>(Resource.Id.imagefooterslider);
            headerIndex = 0;


            sharedPreferences = this.Activity.GetSharedPreferences(BaseURL.MyPrefreance, FileCreationMode.Private);

            rv_items = (RecyclerView)view.FindViewById(Resource.Id.rv_home);

            GridLayoutManager gridLayoutManager = new GridLayoutManager(this.Activity, 3);
            rv_items.SetLayoutManager(gridLayoutManager);
            rv_items.SetItemAnimator(new DefaultItemAnimator());
            rv_items.NestedScrollingEnabled = false;

            Search_layout = (LinearLayout)view.FindViewById(Resource.Id.search_layout);
            scrollView = (ScrollView)view.FindViewById(Resource.Id.scroll_view);
            scrollView.SmoothScrollingEnabled = true;
            if (isOnline())
            {
                HeaderLoadImages();
                FooterLoadImages();
                setHeaderSlideShow();
                makeGetCategoryRequest();
            }

            Search_layout.Click += delegate
             {
                 SearchFragment trending_fragment = new SearchFragment();
                 FragmentManager m = FragmentManager;
                 FragmentTransaction fragmentTransaction = m.BeginTransaction();
                 fragmentTransaction.Replace(Resource.Id.contentPanel, trending_fragment);
                 fragmentTransaction.Commit();
             };

            fabMain = (FloatingActionButton)view.FindViewById(Resource.Id.fabMain);
            fabOne = (FloatingActionButton)view.FindViewById(Resource.Id.fabOne);
            fabTwo = (FloatingActionButton)view.FindViewById(Resource.Id.fabTwo);
            fabThree = (FloatingActionButton)view.FindViewById(Resource.Id.fabThree);
            fabfour = (FloatingActionButton)view.FindViewById(Resource.Id.fabfour);



            fabOne.SetAlpha(0);
            fabTwo.SetAlpha(0);
            fabThree.SetAlpha(0);
            fabfour.SetAlpha(0);

            fabOne.TranslationY = translationY;
            fabTwo.TranslationY = translationY;
            fabThree.TranslationY = translationY;
            fabfour.TranslationY = translationY;

            fabMain.Click += FabMain_Click;
            fabOne.Click += FabOne_Click;
            fabTwo.Click += FabTwo_Click;
            fabThree.Click += FabThree_Click;
            fabfour.Click += Fabfour_Click;

            tab1 = (TabItem)view.FindViewById(Resource.Id.top_selling_item);
            tab2 = (TabItem)view.FindViewById(Resource.Id.recent_item);
            tab3 = (TabItem)view.FindViewById(Resource.Id.deals_item);
            tab4 = (TabItem)view.FindViewById(Resource.Id.whtsnewitem);

            tabLayout = (TabLayout)view.FindViewById(Resource.Id.tablayout);
            viewPager = (ViewPager)view.FindViewById(Resource.Id.pager_product);

            pageAdapter = new PageAdapter(ChildFragmentManager, 1);
            viewPager.Adapter = pageAdapter;
            tabLayout.TabSelected += TabLayout_TabSelected;

            viewPager.AddOnPageChangeListener(new TabLayoutOnPageChangeListener(tabLayout));
            return view;
        }

        private void TabLayout_TabSelected(object sender, TabSelectedEventArgs e)
        {
            viewPager.CurrentItem = e.Tab.Position;
        }

        private void Fabfour_Click(object sender, EventArgs e)
        {
            if (isPermissionGranted())
            {
                call_action();
            }
        }

        private void FabThree_Click(object sender, EventArgs e)
        {
            Intent sendIntent = new Intent();
            sendIntent.SetAction(Intent.ActionSend);
            sendIntent.PutExtra(Intent.ExtraText, "This app delivers all my grocery needs without any hassles. It's contactless & safe. Highly recommended! Download using this in link. https://#");
            sendIntent.SetType("text/plain");
            sendIntent.SetPackage("com.whatsapp");
            if (sendIntent.ResolveActivity(Activity.PackageManager) != null)
            {
                StartActivity(sendIntent);
            }
            else
            {
                Toast.MakeText(this.Activity, "Whatsapp isn't install please install whatsapp", ToastLength.Short).Show();
            }
        }

        private void FabTwo_Click(object sender, EventArgs e)
        {
            Android.Net.Uri uri = Android.Net.Uri.Parse("market://details?id=" + Activity.PackageName);
            Intent goToMarket = new Intent(Intent.ActionView, uri);
            goToMarket.AddFlags(ActivityFlags.NoHistory |
                   ActivityFlags.NewDocument |
                    ActivityFlags.MultipleTask);
            try
            {
                StartActivity(goToMarket);
            }
            catch (ActivityNotFoundException)
            {
                StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("http://play.google.com/store/apps/details?id=" + Activity.PackageName)));
            }
        }

        private void FabOne_Click(object sender, EventArgs e)
        {
            Intent sendIntent1 = new Intent();
            sendIntent1.SetAction(Intent.ActionSend);
            sendIntent1.PutExtra(Intent.ExtraText, "Hi friends i am using ." + " http://play.google.com/store/apps/details?id=" + this.Activity.PackageName + " APP");
            sendIntent1.SetType("text/plain");
            StartActivity(sendIntent1);
            if (isMenuOpen)
            {
                closeMenu();
            }
            else
            {
                openMenu();
            }
        }

        private void FabMain_Click(object sender, EventArgs e)
        {
            if (isMenuOpen)
            {
                fabOne.Visibility = ViewStates.Gone;
                fabTwo.Visibility = ViewStates.Gone;
                fabThree.Visibility = ViewStates.Gone;
                fabfour.Visibility = ViewStates.Gone;
                closeMenu();
            }
            else
            {
                fabOne.Visibility = ViewStates.Visible;
                fabTwo.Visibility = ViewStates.Visible;
                fabThree.Visibility = ViewStates.Visible;
                fabfour.Visibility = ViewStates.Visible;
                openMenu();
            }
        }

        private void openMenu()
        {
            isMenuOpen = !isMenuOpen;

            fabMain.Animate().SetInterpolator(interpolator).Rotation(45f).SetDuration(300).Start();

            fabOne.Animate().TranslationY(0f).Alpha(1f).SetInterpolator(interpolator).SetDuration(300).Start();
            fabTwo.Animate().TranslationY(0f).Alpha(1f).SetInterpolator(interpolator).SetDuration(300).Start();
            fabThree.Animate().TranslationY(0f).Alpha(1f).SetInterpolator(interpolator).SetDuration(300).Start();
            fabfour.Animate().TranslationY(0f).Alpha(1f).SetInterpolator(interpolator).SetDuration(300).Start();

        }

        private void closeMenu()
        {
            isMenuOpen = !isMenuOpen;

            fabMain.Animate().SetInterpolator(interpolator).Rotation(0f).SetDuration(300).Start();

            fabOne.Animate().TranslationY(translationY).Alpha(0f).SetInterpolator(interpolator).SetDuration(300).Start();
            fabTwo.Animate().TranslationY(translationY).Alpha(0f).SetInterpolator(interpolator).SetDuration(300).Start();
            fabThree.Animate().TranslationY(translationY).Alpha(0f).SetInterpolator(interpolator).SetDuration(300).Start();
            fabfour.Animate().TranslationY(translationY).Alpha(0f).SetInterpolator(interpolator).SetDuration(300).Start();
        }

        public bool isPermissionGranted()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                if (this.Context.CheckSelfPermission(Android.Manifest.Permission.CallPhone) == Permission.Granted)
                {
                    return true;
                }
                else
                {

                    ActivityCompat.RequestPermissions(Activity, new String[] { Android.Manifest.Permission.CallPhone }, 1);
                    return false;
                }
            }
            else
            { //permission is automatically granted on sdk<23 upon installation
                return true;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            switch (requestCode)
            {

                case 1:
                    {

                        if (grantResults.Length > 0
                                && grantResults[0] == (int)Permission.Granted)
                        {
                            Toast.MakeText(this.Activity, "Permission granted", ToastLength.Short).Show();
                            call_action();
                        }
                        else
                        {
                            Toast.MakeText(this.Activity, "Permission denied", ToastLength.Short).Show();
                        }
                        return;
                    }

            }
        }


        public void call_action()
        {
            Intent callIntent = new Intent(Intent.ActionCall);
            callIntent.SetData(Android.Net.Uri.Parse("tel:" + "1234567890"));
            StartActivity(callIntent);
        }

        private bool isOnline()
        {
            ConnectivityManager cm = (ConnectivityManager)this.Activity.GetSystemService(Context.ConnectivityService);

            return cm.ActiveNetworkInfo != null && cm.ActiveNetworkInfo.IsConnected;
        }

        private async void makeGetCategoryRequest()
        {

            try
            {
                using (var client = new HttpClient())
                {
                    var getDataUrl = new System.Uri(BaseURL.Categories);
                    var result = await client.GetStringAsync(getDataUrl);
                    var response = JsonConvert.DeserializeObject<ResultArray>(result);
                    string status = response.status;
                    if (status == "200")
                    {
                        var jsonArray = response.data;
                        if (jsonArray.Count() > 0)
                        {
                            for (int i = 0; i < jsonArray.Count(); i++)
                            {
                                Category_model category_Model = new Category_model();
                                if (jsonArray[i].level.ToLower() == "yes" && jsonArray[i].IsStorePublished)
                                {
                                    category_Model.cat_id = jsonArray[i].GroupMastID.ToString();
                                    category_Model.title = jsonArray[i].ItemCategory.ToString();
                                    category_Model.image = jsonArray[i].m_ParentItemImageURL.ToString();
                                    category_modelList.Add(category_Model);
                                }
                            }
                        }
                        adapter = new Home_adapter(category_modelList, Context);
                        rv_items.SetAdapter(adapter);
                        adapter.NotifyDataSetChanged();
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }


        //private void Rv_items_Click(object sender, EventArgs e)
        //{
        //    string getid = category_modelList.IndexOf(sender.position).getCat_id();

        //    Intent intent = new Intent(Activity, typeof(CategoryPage));
        //    intent.PutExtra("cat_id", getid);
        //    intent.PutExtra("title", category_modelList.IndexOf(position).getTitle());
        //    intent.PutExtra("image", category_modelList.IndexOf(position).getImage());
        //    StartActivity(intent);
        //}

        void setHeaderSlideShow()
        {
            Timer timer;
            timer = new Timer();
            timer.Interval = 4000; // Interval to change image (4000 = 4 second per image)
            timer.Elapsed += (sender, e) =>
            {
                showNextHeaderImage();
            };
            timer.Start();
            timer.AutoReset = true;
        }
        void showNextHeaderImage()
        {
            try
            {
                headerIndex++;
                if (headerIndex == headerImages.Length) { headerIndex = 0; }
            }
            catch(Exception ex)
            {
                headerIndex = 0;
            }
            if(headerImages != null)
            {
                Activity.RunOnUiThread(async () => imageView.SetImageBitmap(await GetImageBitmapFromURL(headerImages[headerIndex].ToString())));
            }
          
        }

        public async void HeaderLoadImages()
        {
            using (var client = new HttpClient())
            {
                var getDataUrl = new System.Uri(BaseURL.BANNER);
                var bannerList = await client.GetStringAsync(getDataUrl);
                var data = JsonConvert.DeserializeObject<List<ImageSlider>>(bannerList);
                if (data.Count > 0)
                {
                    var imageList = data.Select(x => Android.Net.Uri.Parse(x.SliderPath)).ToArray();
                    headerImages = imageList;
                    imageView.SetImageBitmap(await GetImageBitmapFromURL(headerImages[0].ToString()));
                }
                else
                {
                    headerImages = null;
                }
            }
        }
        public async void FooterLoadImages()
        {
            using (var client = new HttpClient())
            {
                var getDataUrl = new System.Uri(BaseURL.secondary_banner);
                var bannerList = await client.GetStringAsync(getDataUrl);
                var url = JsonConvert.DeserializeObject<string>(bannerList);
                footerImage.SetImageBitmap(await GetImageBitmapFromURL(Android.Net.Uri.Parse(url).ToString()));
            }
        }

        private async Task<Bitmap> GetImageBitmapFromURL(string url)
        {
            Bitmap imageBitmap = null;
            var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
            try
            {
                using (var httpResponse = await httpClient.GetAsync(url))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        using (var webClient = new WebClient())
                        {
                            var imageBytes = webClient.DownloadData(url);
                            if (imageBytes != null && imageBytes.Length > 0)
                            {
                                imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return imageBitmap;
            }

            return imageBitmap;
        }

        public bool OnInterceptTouchEvent(RecyclerView recyclerView, MotionEvent @event)
        {
            throw new NotImplementedException();
        }

        public void OnRequestDisallowInterceptTouchEvent(bool disallow)
        {
            throw new NotImplementedException();
        }

        public void OnTouchEvent(RecyclerView recyclerView, MotionEvent @event)
        {

        }
    }

    public class ImageSlider
    {
        public int StoreSliderID { get; set; }
        public string SliderPath { get; set; }
        public string SliderImage { get; set; }
        public string SliderName { get; set; }
        public string ImageData { get; set; }
        public string imgsrc { get; set; }
    }
}

