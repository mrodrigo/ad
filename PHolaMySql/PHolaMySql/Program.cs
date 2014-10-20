using System;
using MySql.Data.MySqlClient;

namespace PHolaMySql
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			MySqlConnection mySqlConnection = new MySqlConnection (
				"DataSource=localhost;Database=dbprueba;User ID=root;Password=sistemas"
			);

			mySqlConnection.Open ();

			Console.WriteLine ("Hola MySql");

//			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
//			mySqlCommand.CommandText = 
//				string.Format ("insert into categoria (nombre) values ('{0}')", 
//			               DateTime.Now);
//
//			mySqlCommand.ExecuteNonQuery ();

			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();


			mySqlCommand.CommandText = "select * from categoria";

			MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

			Console.WriteLine ("FieldCount={0}", mySqlDataReader.FieldCount);
			for (int index = 0; index < mySqlDataReader.FieldCount; index++)
				Console.WriteLine ("column {0}={1}", index, mySqlDataReader.GetName (index));

			while (mySqlDataReader.Read()) {
				object id = mySqlDataReader ["id"];
				object nombre = mySqlDataReader ["nombre"];
				Console.WriteLine ("id={0, -10} nombre={1, -20} *", id, nombre);
			}

			mySqlDataReader.Close ();

			mySqlConnection.Close ();

		}
	}
}
