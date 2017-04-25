using System;
using System.Data;
using Newtonsoft.Json;

namespace pTrader
{
	public class TradeJournalEntryModel
	{
		private long profileId;
		private long entryId;

		// Data points

		// Entry
		private DateTime entryDate;
		private string entryType;
		private string entrySetup;
		private float entrySize;
		private float entryPrice;
		private float entryStopLoss;
		private float entryTakeProfit;

		// Exit
		private DateTime exitDate;
		private float exitPrice;
		private float profitLoss;
		private float fees;

		// Data parameters used for queries
		IDbDataParameter profileParam;
		IDbDataParameter entryParam;
		IDbDataParameter dataParam;

		// Various queries used
		private const string checkDbExistsSql = "SELECT * FROM TradeJournalEntry";
		private const string createTableSql = "CREATE TABLE TradeJournalEntry (profile_id INTEGER " +
			"NOT NULL, entry_id INTEGER NOT NULL, data TEXT)";

		private const string getDataSql = "SELECT data FROM TradeJournalEntry WHERE profile_id=" +
			"@profileId AND entry_id=@entryId";

		private const string updateSql = "UPDATE TradeJournalEntry SET data=@data WHERE " +
			"profile_id=@profileId AND entry_id=@entryId";

		private const string insertSql = "INSERT INTO TradeJournalEntry (profile_id, entry_id, " +
			"data) VALUES (@profileId, @entryId, @data)";

		private const string lastEntryIdSql = "SELECT MAX(entry_id) FROM TradeJournalEntry WHERE " +
			"profile_id=@profileId";

		public void CheckCreateTable(IDbConnection conn)
		{
			conn.Open();
			IDbCommand cmd = conn.CreateCommand();
			cmd.CommandText = checkDbExistsSql;

			try
			{
				IDataReader reader = cmd.ExecuteReader();
				reader.Close();
			}
			catch (Exception)
			{
				cmd.CommandText = createTableSql;
				cmd.ExecuteNonQuery();
			}

			conn.Close();
		}

		public static TradeJournalEntryModel FromDatabaseRecord(IDbConnection conn, long profileId, long entryId)
		{
			string data = "";
			if (profileId != 0 && entryId != 0)
			{
				IDbCommand cmd = conn.CreateCommand();

				IDbDataParameter profileParam = cmd.CreateParameter();
				IDbDataParameter entryParam = cmd.CreateParameter();

				cmd.CommandText = getDataSql;

				profileParam.ParameterName = "@profileId";
				entryParam.ParameterName = "@entryId";

				profileParam.DbType = DbType.Int64;
				profileParam.DbType = DbType.Int64;

				profileParam.Value = profileId;
				entryParam.Value = entryId;

				conn.Open();

				cmd.Parameters.Add(profileParam);
				cmd.Parameters.Add(entryParam);
				cmd.Prepare();

				IDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					data = ModelUtils.UnpackData(reader.GetString(0));
				}

				reader.Close();

				conn.Close();
			}
			return JsonConvert.DeserializeObject<TradeJournalEntryModel>(data);
		}

		public TradeJournalEntryModel(IDbConnection conn, long profileId = 0)//, long entryId = 0)
		{
			CheckCreateTable(conn);
			
			this.profileId = profileId;
			this.entryId = 0;

			IDbCommand cmd = conn.CreateCommand();

			// Create the profile parameter
			profileParam = cmd.CreateParameter();
			profileParam.ParameterName = "@profileId";
			profileParam.DbType = DbType.Int64;

			// Create the entry parameter
			entryParam = cmd.CreateParameter();
			entryParam.ParameterName = "@entryId";
			entryParam.DbType = DbType.Int64;

			// Create the data parameter
			dataParam = cmd.CreateParameter();
			dataParam.ParameterName = "@data";
			entryParam.DbType = DbType.String;

			/*
			if (profileId != 0 && entryId != 0)
			{
				cmd.CommandText = getDataSql;

				profileParam.Value = profileId;
				entryParam.Value = entryId;

				conn.Open();

				cmd.Parameters.Add(profileParam);
				cmd.Parameters.Add(entryParam);
				cmd.Prepare();

				IDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					// = ModelUtils.UnpackData(reader.GetString(0));
				}

				reader.Close();

				conn.Close();
			}
			*/
		}

		public bool Save(IDbConnection conn)
		{
			if (profileId != 0)
			{
				dataParam.Value = ModelUtils.PackData(JsonConvert.SerializeObject(this));
				profileParam.Value = profileId;
				
				conn.Open();
				IDbCommand cmd = conn.CreateCommand();

				if (entryId != 0)
				{
					entryParam.Value = entryId;

					cmd.CommandText = updateSql;
					cmd.Parameters.Add(profileParam);
					cmd.Parameters.Add(entryParam);
					cmd.Parameters.Add(dataParam);
					cmd.Prepare();

					cmd.ExecuteNonQuery();
				}
				else
				{
					cmd.CommandText = lastEntryIdSql;
					cmd.Parameters.Add(profileParam);
					cmd.Prepare();

					IDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						try
						{
							entryId = reader.GetInt64(0) + 1;
						}
						catch (InvalidCastException)
						{
							entryId = 1;
							break;
						}
					}

					reader.Close();

					cmd.CommandText = insertSql;
					entryParam.Value = entryId;
					cmd.Parameters.Add(entryParam);
					cmd.Parameters.Add(dataParam);
					cmd.Prepare();

					cmd.ExecuteNonQuery();
				}

				conn.Close();
				return true;
			}

			return false;
		}

		public long EntryId
		{
			get
			{
				return entryId;
			}
			set
			{
				entryId = value;
			}
		}

		// Entry fields
		public DateTime EntryDate
		{
			get
			{
				return entryDate;
			}
			set
			{
				entryDate = value;
			}
		}

		public string EntryType
		{
			get
			{
				return entryType;
			}
			set
			{
				entryType = value;
			}
		}

		public string EntrySetup
		{
			get
			{
				return entrySetup;
			}
			set
			{
				entrySetup = value;
			}
		}

		public float EntrySize
		{
			get
			{
				return entrySize;
			}
			set
			{
				entrySize = value;
			}
		}

		public float EntryPrice
		{
			get
			{
				return entryPrice;
			}
			set
			{
				entryPrice = value;
			}
		}

		public float StopLoss
		{
			get
			{
				return entryStopLoss;
			}
			set
			{
				entryStopLoss = value;
			}
		}

		public float TakeProfit
		{
			get
			{
				return entryStopLoss;
			}
			set
			{
				entryTakeProfit = value;
			}
		}

		// Exit fields
		public DateTime ExitDate
		{
			get
			{
				return exitDate;
			}
			set
			{
				exitDate = value;
			}
		}

		public float ExitPrice
		{
			get
			{
				return exitPrice;
			}
			set
			{
				exitPrice = value;
			}
		}

		public float ProfitLoss
		{
			get
			{
				return profitLoss;
			}
			set
			{
				profitLoss = value;
			}
		}

		public float Fees
		{
			get
			{
				return fees;
			}
			set
			{
				fees = value;
			}
		}
	}
}
