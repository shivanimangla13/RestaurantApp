using Android.Arch.Lifecycle;
using Android.OS;
using Android.Views;

namespace com.resturant.Droid
{
    public class HomeFragment : Android.Support.V4.App.Fragment
    {
        private HomeViewModel homeViewModel;
        public View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            homeViewModel = ViewModelProviders.Of(this).Get(Java.Lang.Class.FromType(typeof(HomeViewModel))) as HomeViewModel;
            View root = inflater.Inflate(Resource.Layout.fragment_home, container, false);

            return root;
        }
    }
}