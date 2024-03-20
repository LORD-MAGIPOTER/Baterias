using Baterias.NetMaui.Modelo;
namespace Baterias.NetMaui.Paginas;

public partial class ListarNombre : ContentPage
{
    Helper ?Sw;
    List<string> ?NombresProd;
    string ?pr;
	public ListarNombre()
	{
		InitializeComponent();
        Sw = new Helper();
        Producto.Items.Clear();
        CargarNombres();
    }
    
    private async void CargarNombres()
    {
        NombresProd = await Sw.NombresProd();
        //se itera la lista de nombres
        for (int i = 0; i < NombresProd.Count; i++)
        {
            // se agrega el nombre uno por uno al picker
            Producto.Items.Add(NombresProd[i]);
        }

    }
    private void Producto_SelectedIndexChanged(object sender, EventArgs e)
    {
        pr = Producto.SelectedItem.ToString();
    }

    private async void DatosProd_Clicked(object sender, EventArgs e)
    {
        Sw = new Helper();
        if(Producto.SelectedItem != null)
        {
            List<Producto> DatosProducto = await Sw.ProductosPorNombre(pr);
            if(DatosProducto != null)
            {
                collectionView.ItemsSource = DatosProducto;
            }
            else
            {
                await DisplayAlert("Error", "Ocurrió un error al cargar los datos","Ok");
            }
        }
        else
        {
            await DisplayAlert("Alerta", "Seleccione un producto ", "Ok");
        }
    }
}