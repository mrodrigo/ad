using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnButtonClicked (object sender, EventArgs e)
	{
		Console.WriteLine ("has hecho click en Aceptar");
		labelSaludo.Text = "Hola " + entry.Text;
	}

	protected void OnNewActionActivated (object sender, EventArgs e)
	{
		Console.WriteLine ("has activado la acción newAction");
	}
}
