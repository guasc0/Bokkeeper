
using System;
using System.Collections.Generic;
using System.Globalization;
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
	[Activity(Label = "Ny händelse")]
	public class NewEventActivity : Activity
	{
		TextView dateDisplay;
		Button dateSelectButton;
		bool income = true;
		RadioButton checkIncome;
		RadioButton checkOutcome;
		Button addEntry;

		EditText description;
		string stDescription = "";
		EditText ammount;
		int intAmmount;

		EditText totalAmmountWithoutTax;
		DateTime dateTime;

		Spinner accountSpinner;
		Spinner typeSpinner;
		Spinner taxSpinner;

		//string pathToDb = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.NewEvent);
			// Create your application here
			//SQLiteConnection db = new SQLiteConnection(pathToDb + @"\database.db");

			//db.CreateTable<Entry>();
			//db.CreateTable<Account>();
			//db.CreateTable<TaxRate>();

			/*Account c4 = new Account { Name = "afasf", Number = 12, Type = "dfe" };
			db.Insert(c4);
			Account c3 = db.Get<Account>(1);
			Console.WriteLine(c3);*/
			accountSpinner = FindViewById<Spinner>(Resource.Id.type_spinner);
			typeSpinner = FindViewById<Spinner>(Resource.Id.to_from_spinner);
			taxSpinner = FindViewById<Spinner>(Resource.Id.taxes_spinner);

			dateDisplay = FindViewById<TextView>(Resource.Id.date_button);
			dateSelectButton = FindViewById<Button>(Resource.Id.date_button);
			dateSelectButton.Click += DateSelect_OnClick;

			checkIncome = FindViewById<RadioButton>(Resource.Id.checkbox_income);
			checkOutcome = FindViewById<RadioButton>(Resource.Id.checkbox_outcome);
			addEntry = FindViewById<Button>(Resource.Id.add_event_button);
			description = FindViewById<EditText>(Resource.Id.description_text_view);
			ammount = FindViewById<EditText>(Resource.Id.amount_edit_text);

			setStartAccount();
			setTaxSpinner();
			setTypeSpinner();
			//setValues();


			checkIncome.Click += delegate
			{
				if (checkIncome.Checked)
				{
					Console.WriteLine(income = true);
					setAdapter(income);
				}
			};

			checkOutcome.Click += delegate
			{
				if (checkOutcome.Checked)
				{
					Console.WriteLine(income = false);
					setAdapter(income);
				}
			};

			addEntry.Click += delegate
			{
				//ExpenseAccount = new List<Account> { new Account { Name = "Virke", Type = "outcome", Number = 2000 } };
				setValues()
				Entry e = new Entry { Description = stDescription, Ammount = intAmmount};
				Console.WriteLine(e.Description + e.Ammount);

			};
		}

		void DateSelect_OnClick(object sender, EventArgs eventArgs)
		{
			DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
																	 {
				dateDisplay.Text = time.ToString("d", CultureInfo.CreateSpecificCulture("en-NZ"));
																		 dateTime = time;
			
			});
			frag.Show(FragmentManager, DatePickerFragment.TAG);
		}

		public void setValues() 
		{
			stDescription = description.Text;
			intAmmount = Int32.Parse(ammount.Text);

		}

		public void setTypeSpinner() 
		{ 
			ArrayAdapter typeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, BookKeeperManager.Instance.MoneyAccount);
			typeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			typeSpinner.Adapter = typeAdapter;
		}

		public void setTaxSpinner() 
		{ 
			ArrayAdapter taxAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, BookKeeperManager.Instance.TaxRates);
			taxAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			taxSpinner.Adapter = taxAdapter;
		}

		public void setStartAccount() 
		{ 
			ArrayAdapter incomeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, BookKeeperManager.Instance.IncomeAccount);
			incomeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			accountSpinner.Adapter = incomeAdapter;
		}

		public void setAdapter(bool b) 
		{
			if (b)
			{
				ArrayAdapter incomeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, BookKeeperManager.Instance.IncomeAccount);
				incomeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
				accountSpinner.Adapter = incomeAdapter;
			}
			else { 
				ArrayAdapter incomeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, BookKeeperManager.Instance.ExpenseAccount);
				incomeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
				accountSpinner.Adapter = incomeAdapter;
			}
		
		}



	}
}
