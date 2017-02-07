using System;
using System.Collections.Generic;
using Android.Widget;
using SQLite;
using System.Linq;

namespace Bookkeeper
{

	public class BookKeeperManager
	{
		public List<Entry> Entries { get; private set;}
		//public List<TaxRate> TaxRates { get; private set;}
		public List<Account> MoneyAccount { get; private set;}
		public List<Account> IncomeAccount { get; private set;}
		public List<Account> ExpenseAccount { get; private set;}

		SQLiteConnection db;

		string pathToDb = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);


		private static BookKeeperManager instance;

		private BookKeeperManager()
		{
			db = new SQLiteConnection(pathToDb + @"\database.db");

			db.CreateTable<Entry>();
			db.CreateTable<Account>();
			db.CreateTable<TaxRate>();

			if (!db.Table<Account>().Any()) 
			{ 
				db.Insert(new Account { Name = "Fotbollsskor", Type = "income", Number = 3020 });
				db.Insert(new Account { Name = "Båt", Type = "income", Number = 3001 });
				db.Insert(new Account { Name = "Cykel", Type = "income", Number = 3002 });

				db.Insert(new Account { Name = "Virke", Type = "expense", Number = 2000 });
				db.Insert(new Account { Name = "Färg", Type = "expense", Number = 2001 });
				db.Insert(new Account { Name = "Verktyg", Type = "expense", Number = 2002 });

				db.Insert(new Account { Name = "Kassa", Type = "MoneyAccount", Number = 1910});

				db.Insert(new TaxRate { Tax = 0.06 });
				db.Insert(new TaxRate { Tax = 0.12 });
				db.Insert(new TaxRate { Tax = 0.25 });


			}
		}

		public List<Account> getAccounts(string type) 
		{ 
			return db.Table<Account>().Where(Account => Account.Type.Equals(type)).ToList();

		}

		public Account getOneAccount(int id) 
		{
			return db.Get<Account>(id);

		}

		public TaxRate getTaxRate(int id) 
		{
			return db.Get<TaxRate>(id);
		}

		public List<TaxRate> getTaxRates() 
		{
			return db.Table<TaxRate>().ToList();
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
			//ntries.Add(e);
			db.Insert(e);
			Console.WriteLine(e.ToString());


		}


	}
}
