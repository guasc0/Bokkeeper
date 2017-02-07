using System;
using System.Collections.Generic;
using Android.Widget;
using SQLite;

namespace Bookkeeper
{

	public class BookKeeperManager
	{
		public List<Entry> Entries { get; private set;}
		public List<TaxRate> TaxRates { get; private set;}
		public List<Account> MoneyAccount { get; private set;}
		public List<Account> IncomeAccount { get; private set;}
		public List<Account> ExpenseAccount { get; private set;}


		string pathToDb = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);


		private static BookKeeperManager instance;

		private BookKeeperManager()
		{
			SQLiteConnection db = new SQLiteConnection(pathToDb);

			db.CreateTable<Entry>();
			db.CreateTable<Account>();
			db.CreateTable<TaxRate>();

			Entries = new List<Entry>();

			TaxRates = new List<TaxRate> { new TaxRate { Tax = 0.06 },
				{ new TaxRate { Tax = 0.12 } },
				{ new TaxRate { Tax = 0.25 } } };

			IncomeAccount = new List<Account> { new Account { Name = "Fotbollsskor", Type = "income", Number = 3020 },
				{ new Account { Name = "Båt", Type = "income", Number = 3001 } },
				{ new Account { Name = "Cykel", Type = "income", Number = 3002 } } };

			ExpenseAccount = new List<Account> { new Account { Name = "Virke", Type = "outcome", Number = 2000 },
				 { new Account { Name = "Färg", Type = "outcome", Number = 2001 } },
				 { new Account { Name = "Verktyg", Type = "outcome", Number = 2002 } } };

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
			SQLiteConnection db = new SQLiteConnection(pathToDb);
			Entries.Add(e);
			db.Insert(e);
			Console.WriteLine(e.ToString());


		}


	}
}
