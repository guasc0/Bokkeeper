
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
using SQLite;

namespace Bookkeeper
{

	[Activity(Label = "Entry List")]
	public class EntryListActivity : Activity
	{

		ListView entryList;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.EntryList);

			entryList = FindViewById<ListView>(Resource.Id.entry_list);
			entryList.Adapter = new EntryAdapter(this);

		}
	}
}
