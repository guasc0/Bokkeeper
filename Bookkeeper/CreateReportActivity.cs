
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

namespace Bookkeeper
{
	[Activity(Label = "Activity Reports")]
	public class CreateReportActivity : Activity
	{
		TextView taxReport;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.TaxReport);

			taxReport = FindViewById<TextView>(Resource.Id.report);
			taxReport.Text = BookKeeperManager.Instance.getTaxReport();
		}
	}
}
