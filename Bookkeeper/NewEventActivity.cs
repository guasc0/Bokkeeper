
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
		Account ac;
		TaxRate tx;
		Account moneyAc;
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
		int typeAccount;
		int moneyAccount;
		double taxRate;

		EditText totalAmmountWithoutTax;
		DateTime dateTime;

		Spinner accountSpinner;
		Spinner typeSpinner;
		Spinner taxSpinner;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.NewEvent);

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

			//setStartAccount();
			setTaxSpinner();
			setTypeSpinner();
			setAdapter(income);

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
				setValues();
				Entry e = new Entry { Description = stDescription, Ammount = intAmmount, AccountId = moneyAccount ,
										TypeId = typeAccount, Date = dateTime, Income = income, TaxRateId = taxRate};

				BookKeeperManager.Instance.AddEntry(e);

			};

		}

		void DateSelect_OnClick(object sender, EventArgs eventArgs)
		{
			DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
																	 {
				dateDisplay.Text = time.ToString("yyyy-MM-dd");
																		 dateTime = time;
			
			});
			frag.Show(FragmentManager, DatePickerFragment.TAG);
		}

		public void setValues() 
		{
			//sätts inte utan att man trycker på inkomst/utgift först. NullPointer

			tx = BookKeeperManager.Instance.TaxRates[taxSpinner.SelectedItemPosition];
			moneyAc = BookKeeperManager.Instance.MoneyAccount[accountSpinner.SelectedItemPosition];

			moneyAccount = moneyAc.Number;

			//Värden
			taxRate = tx.Tax;
			stDescription = description.Text;
			intAmmount = Int32.Parse(ammount.Text);

			if (income)
			{
				ac = BookKeeperManager.Instance.IncomeAccount[accountSpinner.SelectedItemPosition];
			}
			else
			{ 
				ac = BookKeeperManager.Instance.ExpenseAccount[accountSpinner.SelectedItemPosition];
			}

			typeAccount = ac.Number;
		
		}

		public void setTypeSpinner() 
		{ 
			ArrayAdapter typeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
			                                            BookKeeperManager.Instance.MoneyAccount);
			typeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			typeSpinner.Adapter = typeAdapter;


		}

		public void setTaxSpinner()
		{
			ArrayAdapter taxAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
													   BookKeeperManager.Instance.TaxRates);
			taxAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			taxSpinner.Adapter = taxAdapter;


		}

		/*public void setStartAccount() 
		{ 
			ArrayAdapter incomeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
			                                              BookKeeperManager.Instance.IncomeAccount);
			incomeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			accountSpinner.Adapter = incomeAdapter;

			accountSpinner.Adapter = incomeAdapter;
			ac = BookKeeperManager.Instance.IncomeAccount[accountSpinner.SelectedItemPosition];
		}*/

		public void setAdapter(bool b) 
		{
			if (b)
			{
				ArrayAdapter incomeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
				                                              BookKeeperManager.Instance.IncomeAccount);
				incomeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

				//OBS!!!!
				//hämtar kontonr endast första gången inte när det ändras i spinnern.
				accountSpinner.Adapter = incomeAdapter;


			}
			else { 
				ArrayAdapter incomeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
				                                              BookKeeperManager.Instance.ExpenseAccount);
				incomeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

				accountSpinner.Adapter = incomeAdapter;


			}
		
		}

	}
}
