using System;
using System.Data;

namespace SerpisAd
{
	public static class DbCommandExtensions
	{
		public static void AddParameter(this IDbCommand dbCommand, string name, object value) {
			IDbDataParameter dbDataParameter = dbCommand.CreateParameter ();
			dbDataParameter.ParameterName = name;
			dbDataParameter.Value = value;
			dbCommand.Parameters.Add (dbDataParameter);
		}
	}
}

