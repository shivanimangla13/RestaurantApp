
using Android.Arch.Lifecycle;

namespace com.resturant.Droid
{
    public class HomeViewModel : Android.Arch.Lifecycle.ViewModel
    {
        private MutableLiveData mText;

        public HomeViewModel()
        {
            mText = new MutableLiveData();
            mText.SetValue("This is home fragment");
        }

        public LiveData getText()
        {
            return mText;
        }
    }
}