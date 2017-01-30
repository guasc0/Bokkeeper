
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
	[Activity(Label = "Ny händelse")]
	public class NewEventActivity : Activity
	{
		TextView _dateDisplay;
		Button _dateSelectButton;
		EditText description;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.NewEvent);
			// Create your application here
			_dateDisplay = FindViewById<TextView>(Resource.Id.date_button);
			_dateSelectButton = FindViewById<Button>(Resource.Id.date_button);
			_dateSelectButton.Click += DateSelect_OnClick;

			RadioGroup checkIncome = FindViewById<RadioGroup>(Resource.Id.checkbox_income);
			RadioGroup checkOutcome = FindViewById<RadioGroup>(Resource.Id.checkbox_outcome);
			Button addEntry = FindViewById<Button>(Resource.Id.add_event_button);
			description = FindViewById<EditText>(Resource.Id.description_text_view);

			checkIncome.Click += delegate {
				d
			};

			checkOutcome.Click += delegate {
				
			};

			addEntry.Click += delegate {
				

			};

		}

		void DateSelect_OnClick(object sender, EventArgs eventArgs)
		{
			DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
																	 {
																		 _dateDisplay.Text = time.ToLongDateString();
																	 });
			frag.Show(FragmentManager, DatePickerFragment.TAG);
		}


	}
}
