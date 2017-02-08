
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
		DateTime dateTime;
		TextView dateDisplay;
		EditText description, ammount, totalAmmountWithoutTax;
		Button dateSelectButton, addEntry;
		RadioButton checkIncome;
		RadioButton checkOutcome;

		string stDescription = "";
		bool income = true;
		int intAmmount, typeAccount, moneyAccount; 
		double taxRate;

		Spinner accountSpinner, typeSpinner, taxSpinner;
		 

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
			setTypeSpinner(income);
			setMoneyAccountSpinner();

			checkIncome.Click += delegate
			{
				if (checkIncome.Checked)
				{
					setTypeSpinner(income = true);
				}
			};

			checkOutcome.Click += delegate
			{
				if (checkOutcome.Checked)
				{
					setTypeSpinner(income = false);
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
			tx = BookKeeperManager.Instance.getTaxRates()[taxSpinner.SelectedItemPosition];
			taxRate = tx.Tax;
			moneyAccount = ((Account)accountSpinner.SelectedItem).Number;
			stDescription = description.Text;
			intAmmount = Int32.Parse(ammount.Text);

			if (income)
			{
				ac = BookKeeperManager.Instance.getAccounts("income")[accountSpinner.SelectedItemPosition];
			}
			else
			{ 
				ac = BookKeeperManager.Instance.getAccounts("expense")[accountSpinner.SelectedItemPosition];
			}

			typeAccount = ((Account)accountSpinner.SelectedItem).Number;
		
		}

		public void setMoneyAccountSpinner() 
		{ 
			ArrayAdapter typeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
			                                            BookKeeperManager.Instance.getAccounts("MoneyAccount"));
			typeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			typeSpinner.Adapter = typeAdapter;


		}

		public void setTaxSpinner()
		{
			ArrayAdapter taxAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
			                                           BookKeeperManager.Instance.getTaxRates());
			taxAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			taxSpinner.Adapter = taxAdapter;
		
	}


	public void setTypeSpinner(bool b) 
		{
			if (b)
			{
				ArrayAdapter incomeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
				                                              BookKeeperManager.Instance.getAccounts("income"));
				incomeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

				accountSpinner.Adapter = incomeAdapter;


			}
			else { 
				ArrayAdapter incomeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
				                                              BookKeeperManager.Instance.getAccounts("expense"));
				incomeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

				accountSpinner.Adapter = incomeAdapter;


			}
		
		}

		public void calculateTotalSum() 
		{
			
		}

	}
}
