using System;
using SQLite;
namespace Bookkeeper
{
	public class TaxRate
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set;}
		public double Tax { get; set;}

		public TaxRate()
		{
			
		}

		public override string ToString()
		{
			return string.Format("% " + Tax);
		}
	}
}
