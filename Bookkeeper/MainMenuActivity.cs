using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Bookkeeper
{
	[Activity(Label = "Bookkeeper", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.MainMenu);

			Button b1 = FindViewById<Button>(Resource.Id.event_btn);
			Button b2 = FindViewById<Button>(Resource.Id.view_event_btn);
			Button b3 = FindViewById<Button>(Resource.Id.create_report_btn);

			b1.Click += delegate{ Intent i = new Intent(this, typeof(NewEventActivity)); StartActivity(i); };
			b2.Click += delegate { Intent j = new Intent(this, typeof(EntryListActivity)); StartActivity(j); };
			b3.Click += delegate { Intent b = new Intent(this, typeof(CreateReportActivity)); StartActivity(b); };
		}
	}
}

