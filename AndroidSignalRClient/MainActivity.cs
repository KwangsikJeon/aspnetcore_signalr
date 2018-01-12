using Android.App;
using Android.Widget;
using Android.OS;

namespace AndroidSignalRClient
{
    [Activity(Label = "AndroidSignalRClient", MainLauncher = true)]
    public class MainActivity : Activity
    {

        SignalRClient _client = null;
        Button _connectBtn = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _connectBtn = FindViewById<Button>(Resource.Id.connect);
            _connectBtn.Click += Button_Click;
        }

        private async void Button_Click(object sender, System.EventArgs e)
        {
            if (_client != null)
                return;

            _client = new SignalRClient();
            await _client.ConnectAsync();
            _connectBtn.Text = "Connected";
        }
    }
}

