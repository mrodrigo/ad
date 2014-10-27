using System;
using Gtk;
using System.Data;

using SerpisAd;
using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	private IDbConnection dbConnection;
	private ListStore listStore;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		
		deleteAction.Sensitive = false;
		editAction.Sensitive = false;

		llamarArticulo ();
		llamarCategoria ();

		treeArticulos.Selection.Changed += selectionChanged;
		treeCategorias.Selection.Changed += selectionChanged;
	}
	private void llamarArticulo(){
		dbConnection = App.Instance.DbConnection;

		treeArticulos.AppendColumn ("ID", new CellRendererText (), "text", 0);
		treeArticulos.AppendColumn ("Nombre", new CellRendererText (), "text", 1);
		treeArticulos.AppendColumn ("Categoria", new CellRendererText (), "text", 2);
		treeArticulos.AppendColumn ("Precio", new CellRendererText (), "text", 3);

		listStore = new ListStore (typeof(ulong),typeof(string),typeof(ulong),typeof(string));
		treeArticulos.Model = listStore;
		fillListArticulos ();

	}

	private void llamarCategoria(){
		dbConnection = App.Instance.DbConnection;

		treeCategorias.AppendColumn ("ID", new CellRendererText (), "text", 0);
		treeCategorias.AppendColumn ("Nombre", new CellRendererText (), "text", 1);


		listStore = new ListStore (typeof(ulong),typeof(string));
		treeCategorias.Model = listStore;
		fillListCategorias ();
	}

	private void fillListArticulos() {
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = "select * from articulo";
		IDataReader dataReader = dbCommand.ExecuteReader ();
		while (dataReader.Read()) {
			object id = dataReader ["id"];
			object nombre = dataReader ["nombre"];
			object categoria = dataReader ["categoria"];
			object precio = dataReader ["precio"].ToString (); ;
			listStore.AppendValues (id, nombre, categoria, precio);
		}
		dataReader.Close ();
	}

	private void fillListCategorias() {
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = "select * from categoria";
		IDataReader dataReader = dbCommand.ExecuteReader ();
		while (dataReader.Read()) {
			object id = dataReader ["id"];
			object nombre = dataReader ["nombre"];

			listStore.AppendValues (id, nombre);
		}
		dataReader.Close ();
	}

	private void selectionChanged (object sender, EventArgs e) {
		Console.WriteLine ("selectionChanged");
		bool hasSelected = treeArticulos.Selection.CountSelectedRows () > 0;
		if (treeArticulos.Selection.CountSelectedRows () > 0) {
			deleteAction.Sensitive = hasSelected;
			editAction.Sensitive = hasSelected;
		} else {
			bool hasSelected2 = treeCategorias.Selection.CountSelectedRows () > 0;
			deleteAction.Sensitive = hasSelected2;
			editAction.Sensitive = hasSelected2;
		}
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		dbConnection.Close ();
		Application.Quit ();
		a.RetVal = true;
	}
}
