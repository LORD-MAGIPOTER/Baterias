using Baterias.NetMaui.Modelo;
namespace Baterias.NetMaui.Paginas;


public partial class Listar : ContentPage
{
    Helper ?Sw;
    public Listar()
	{
		InitializeComponent();
	}

    private async void MostrarLista_Clicked(object sender, EventArgs e)
    {
		//Se crea el objeto de la clase helper
		Sw = new Helper(); 
		
		//Lista de datos que va a tener los datos de obtenerProductos
		List<Producto> DatosProductos = await Sw.ObtenerProductos();

		if(DatosProductos != null)
		{
			//Items Del collection view serán los datos que devuelva el metodo ObtenerProductos
			collectionView.ItemsSource = DatosProductos;
		}
		else
		{
			await DisplayAlert("Error", "Ocurrió un error al desplegar los datos", "Ok");
		}

    }
}