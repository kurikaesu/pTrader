using System;
using System.Data;

namespace pTrader
{
	public class TradeJournalEntryModel
	{
		private long profileId;
		private long entryId;
		private string data;

		// Data parameters used for queries
		IDbDataParameter profileParam;
		IDbDataParameter entryParam;
		IDbDataParameter dataParam;

		// Various queries used
		private const string getDataSql = "SELECT data FROM TradeJournalEntry WHERE profile_id=" +
			"@profileId AND entry_id=@entryId";

		private const string updateSql = "UPDATE TradeJournalEntry SET data=@data WHERE " +
			"profile_id=@profileId AND entry_id=@entryId";

		private const string insertSql = "INSERT INTO TradeJournalEntry (profile_id, entry_id, " +
			"data) VALUES (@profileId, @entryId, @data)";

		private const string lastEntryIdSql = "SELECT MAX(entry_id) FROM TradeJournalEntry WHERE " +
			"profile_id=@profileId";

		public TradeJournalEntryModel(IDbConnection conn, long profileId = 0, long entryId = 0)
		{
			this.profileId = profileId;
			this.entryId = entryId;

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
					this.data = ModelUtils.UnpackData(reader.GetString(0));
				}

				reader.Close();

				conn.Close();
			}
		}

		public string EntryData
		{
			get
			{
				return data;
			}
			set
			{
				// Might do something about this in the future?
				data = value;
			}
		}

		public bool Save(IDbConnection conn)
		{
			if (profileId != 0)
			{
				dataParam.Value = ModelUtils.PackData(data);
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
						entryId = reader.GetInt64(0) + 1;
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
	}
}
