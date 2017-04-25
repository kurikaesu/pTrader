using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using System.Data;
using Mono.Data.Sqlite;

namespace pTrader
{
	public partial class TradeJournalEntry : AppKit.NSView
	{
		#region Constructors

		// Called when created from unmanaged code
		public TradeJournalEntry(IntPtr handle) : base(handle)
		{
			Initialize();
		}

		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public TradeJournalEntry(NSCoder coder) : base(coder)
		{
			Initialize();
		}

		// Shared initialization code
		void Initialize()
		{
		}

		#endregion

		public void SetupView()
		{

		}

		[Action("CancelButtonPressed:")]
		public void Cancel(NSObject sender)
		{
			this.Window.Close();
		}

		private DateTime NSDateToDateTime(NSDate date)
		{
			DateTime referece = TimeZone.CurrentTimeZone.ToLocalTime(
				new DateTime(2001, 1, 1, 0, 0, 0));

			return referece.AddSeconds(date.SecondsSinceReferenceDate);
		}

		private NSDate DateTimeToNSDate(DateTime date)
		{
			DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
				new DateTime(2001, 1, 1, 0, 0, 0));
			return NSDate.FromTimeIntervalSinceReferenceDate(
				(date - reference).TotalSeconds);
		}

		[Action("SaveButtonPressed:")]
		public void Save(NSObject sender)
		{
			string typeValue = EntryType.StringValue;
			Console.WriteLine(typeValue);

			IDbConnection conn = new SqliteConnection("URI=file:test.db");

			// Create a new entry
			TradeJournalEntryModel newEntry = new TradeJournalEntryModel(conn, 1);

			newEntry.EntryDate = NSDateToDateTime(EntryDate.DateValue);
			newEntry.EntryType = EntryType.StringValue;
			newEntry.EntrySetup = EntrySetup.StringValue;
			newEntry.EntrySize = float.Parse(EntrySize.StringValue);
			newEntry.EntryPrice = float.Parse(EntrySize.StringValue);
			newEntry.StopLoss = float.Parse(StopLoss.StringValue);
			newEntry.TakeProfit = float.Parse(TakeProfit.StringValue);

			newEntry.ExitDate = NSDateToDateTime(ExitDate.DateValue);
			newEntry.ExitPrice = float.Parse(ExitPrice.StringValue);
			newEntry.ProfitLoss = float.Parse(ProfitLoss.StringValue);
			newEntry.Fees = float.Parse(Fees.StringValue);

			newEntry.Save(conn);

			this.Window.Close();
		}
	}
}
