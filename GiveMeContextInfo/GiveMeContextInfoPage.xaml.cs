using Xamarin.Forms;

namespace GiveMeContextInfo
{
    public partial class GiveMeContextInfoPage : ContentPage
    {
        public GiveMeContextInfoPage ()
        {
            InitializeComponent ();
            this.BindingContext = new GiveMeContextInfoViewModel ();
        }
    }
}
