using System;
using System.Collections.Generic;
using Android.Widget;
using SQLite;
using System.Linq;

namespace Bookkeeper
{

	public class BookKeeperManager
	{
		
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
				db.Insert(new Account { Name = "Försäljning Bil", Type = "income", Number = 3020 });
				db.Insert(new Account { Name = "Försäljning Reservdelar", Type = "income", Number = 3001 });

				db.Insert(new Account { Name = "Köp Bil", Type = "expense", Number = 2000 });
				db.Insert(new Account { Name = "Köp Reservdelar", Type = "expense", Number = 2001 });

				db.Insert(new Account { Name = "Kontant betalning", Type = "MoneyAccount", Number = 1910});
				db.Insert(new Account { Name = "Kort betalning", Type = "MoneyAccount", Number = 1912 });

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

		public List<TaxRate> getTaxRates()
		{
			return db.Table<TaxRate>().ToList();
		}

		public TaxRate getTaxRate(int id) 
		{
			return db.Get<TaxRate>(id);
		}

		public List<Entry> getEntries() 
		{
			return db.Table<Entry>().ToList();
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
			
			db.Insert(e);
		}

		public string getTaxReport() 
		{
			var taxReport = getEntries().Select(e => 
			string.Format(e.Date.ToString("*************************************\nyyyy-MM-dd")
			                                                                                  + "   "
																							  + e.Description + "   " +
			                                                                                  (e.isIncome ? 
			                                                                                   (e.Ammount * e.TaxRateId)
			                                                                                   :(e.Ammount * e.TaxRateId)*-1) + ":-"));

			return string.Join("\n", taxReport);
		}

	}
}

