using System;
using System.Data;
using Gtk;
using MySql.Data;
using MySql.Data.MySqlClient;


public partial class MainWindow: Gtk.Window
{	
	private ListStore listStore;

	private MySqlConnection mySqlConnection;
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		treeView.AppendColumn("id", new CellRendererText(), "text", 0);
		treeView.AppendColumn("nombre", new CellRendererText(), "text", 1);

		listStore = new ListStore(typeof(string), typeof(string));

		treeView.Model = listStore;
		mySqlConnection = new MySqlConnection (
			"Server=localhost;Database=dbprueba;User ID=root;Password=sistemas");
		mySqlConnection.Open ();

		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = "select * from categoria";

		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

		while (mySqlDataReader.Read()) {
			object id = mySqlDataReader ["id"].ToString();
			object nombre = mySqlDataReader ["nombre"];
			listStore.AppendValues (id, nombre);
		}
		mySqlDataReader.Close ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	protected void OnAddActionActivated (object sender, EventArgs e)
	{
		//listStore.AppendValues (id, nombre);
	}



}
