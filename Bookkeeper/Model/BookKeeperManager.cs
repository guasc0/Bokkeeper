using System;
using System.Collections.Generic;
using Android.Widget;

namespace Bookkeeper
{
	public class BookKeeperManager
	{
		public List<Entry> Entries { get; private set;}
		public List<TaxRate> TaxRates { get; private set;}
		public List<Account> MoneyAccount { get; private set;}
		public List<Account> IncomeAccount { get; private set;}
		public List<Account> ExpenseAccount { get; private set;}

		Spinner accountSpinner;



		private static BookKeeperManager instance;

		private BookKeeperManager()
		{
			Entries = new List<Entry>();

			TaxRates = new List<TaxRate> { new TaxRate { Tax = 0.06 },
				{ new TaxRate { Tax = 0.12 } },
				{ new TaxRate { Tax = 0.25 } } };

			IncomeAccount = new List<Account> { new Account { Name = "Fotbollsskor", Type = "income", Number = 3000 },
				{ new Account { Name = "Båt", Type = "income", Number = 3000 } },
				{ new Account { Name = "Cykel", Type = "income", Number = 3000 } } };

			ExpenseAccount = new List<Account> { new Account { Name = "Virke", Type = "outcome", Number = 2000 },
				 { new Account { Name = "Färg", Type = "outcome", Number = 2000 } },
				 { new Account { Name = "Verktyg", Type = "outcome", Number = 2000 } } };

			MoneyAccount = new List<Account> { new Account { Name = "Kassa", Type = "MoneyAccount", Number = 1910 },
				 { new Account { Name = "Skit", Type = "MoneyAccount", Number = 2023 } } };
		
		}

		public static BookKeeperManager Instance 
		{ 
			get 
			{
				if (instance == null) 
				{
					instance = new BookKeeperManager();
				}
				return instance;
			}
		
		}

		public void AddEntry(Entry e) 
		{
			Entries.Add(e);
		}

		/*public void setAdapter(bool b)
		{
			if (b)
			{
				ArrayAdapter incomeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, 
				                                              BookKeeperManager.Instance.IncomeAccount);
				incomeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
				accountSpinner.Adapter = incomeAdapter;
			}
			else
			{
				ArrayAdapter incomeAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
				                                              BookKeeperManager.Instance.ExpenseAccount);
				incomeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
				accountSpinner.Adapter = incomeAdapter;
			}

		}*/
	}
}
