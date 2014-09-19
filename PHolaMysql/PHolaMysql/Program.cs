using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PHolaMysql
{
	class MainClass
	{
		public static void Main (string[] args)
		{


			MySqlConnection mySqlConnection = new MySqlConnection (
				"Server=localhost;Database=dbprueba;User ID=root;Password=sistemas");

			mySqlConnection.Open ();

			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		//	mySqlCommand.CommandText = 
		//		string.Format ("insert into categoria (nombre) values ('{0}')", DateTime.Now);
		// mySqlCommand.ExecuteNonQuery ();
			mySqlCommand.CommandText = "select * from categoria";

			MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

			Console.WriteLine ("FieldCount={0}", mySqlDataReader.FieldCount);
			for (int index = 0; index < mySqlDataReader.FieldCount; index++)
				Console.WriteLine("Colum {0}={1}", index, mySqlDataReader.GetName (index));

			while (mySqlDataReader.Read()) {
				object id = mySqlDataReader ["id"];
				object nombre = mySqlDataReader ["nombre"];
				Console.WriteLine ("id={0} nombre={1}", id, nombre);
						}

			mySqlDataReader.Close ();

			mySqlConnection.Close ();
		}
	}
}
