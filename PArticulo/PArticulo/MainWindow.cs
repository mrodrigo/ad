using System;
using Gtk;
using System.Data;

using SerpisAd;
using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	private IDbConnection dbConnection;

	private ListStore listArticulos;
	private ListStore listCategorias;

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

		listArticulos = new ListStore (typeof(ulong),typeof(string),typeof(ulong),typeof(string));
		treeArticulos.Model = listArticulos;
		fillListArticulos ();

	}

	private void llamarCategoria(){
		dbConnection = App.Instance.DbConnection;

		treeCategorias.AppendColumn ("ID", new CellRendererText (), "text", 0);
		treeCategorias.AppendColumn ("Nombre", new CellRendererText (), "text", 1);


		listCategorias= new ListStore (typeof(ulong),typeof(string));
		treeCategorias.Model = listCategorias;
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
			listArticulos.AppendValues (id, nombre, categoria, precio);
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

			listCategorias.AppendValues (id, nombre);
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

	protected void OnDeleteActionActivated (object sender, EventArgs e)
	{
		MessageDialog messageDialog = new MessageDialog (
			this,
			DialogFlags.Modal,
			MessageType.Question,
			ButtonsType.YesNo,
			"¿Seguro que quieres borrar?"
			);
		messageDialog.Title = Title;
		ResponseType response = (ResponseType) messageDialog.Run ();
		messageDialog.Destroy ();
		if (response != ResponseType.Yes)
			return;
		TreeIter treeIter;
		string deleteSql = "";
		if (notebook3.Page == 0) { //Si está elegida la pestaña Artículo (0)
			Console.WriteLine ("Elegida pestaña "+ notebook3.Page.ToString());
			treeArticulos.Selection.GetSelected (out treeIter);
			object id = listArticulos.GetValue (treeIter, 0);
			deleteSql = string.Format ("DELETE FROM articulo WHERE id={0}", id);
			Console.WriteLine (deleteSql);
		}
		if (notebook3.Page == 1) { //Si está elegida la pestaña Categoria (1)
			treeCategorias.Selection.GetSelected (out treeIter);
			object id = listCategorias.GetValue (treeIter, 0);
			deleteSql = string.Format ("DELETE FROM categoria WHERE id={0}", id);
		}
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = deleteSql;
		dbCommand.ExecuteNonQuery ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		dbConnection.Close ();
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnRefreshActionActivated (object sender, EventArgs e)
	{
		listArticulos.Clear ();
		listCategorias.Clear ();
		fillListCategorias ();
		fillListArticulos ();
	}
}
