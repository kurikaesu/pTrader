using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

using System.Data;
using Mono.Data.Sqlite;
using Newtonsoft.Json;

namespace pTrader
{
	public partial class TradeJournal : AppKit.NSView
	{
		#region Constructors

		// Called when created from unmanaged code
		public TradeJournal(IntPtr handle) : base(handle)
		{
			Initialize();
		}

		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public TradeJournal(NSCoder coder) : base(coder)
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
			IDbConnection conn = new SqliteConnection("URI=file:test.db");
			conn.Open();

			const string getRecordsSql = "SELECT entry_id, data FROM TradeJournalEntry WHERE " +
				"profile_id=1 ORDER BY entry_id ASC";

			IDbCommand cmd = conn.CreateCommand();
			cmd.CommandText = getRecordsSql;
			cmd.Prepare();

			IDataReader reader = cmd.ExecuteReader();
			string temp;
			//long entryId;
			List<TradeJournalEntryModel> retrievedRecords = new List<TradeJournalEntryModel>();
			while (reader.Read())
			{
				//entryId = reader.GetInt64(0);
				temp = reader.GetString(1);
				try
				{
					TradeJournalEntryModel obj = JsonConvert.DeserializeObject<TradeJournalEntryModel>(temp);
					retrievedRecords.Add(obj);
				}
				catch (JsonReaderException)
				{
					//
				}
				catch (NullReferenceException)
				{
					//
				}
			}
		}
	}
}
