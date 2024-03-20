using Baterias.NetMaui.Modelo;
namespace Baterias.NetMaui.Paginas;

public partial class ListarFecha : ContentPage
{
    Helper ?Sw;
    List<Producto> ?DatosProductos;
    DateTime fechai;
    DateTime fechaf;
	public ListarFecha()
	{
		InitializeComponent();
        FechaInicio.Date = new DateTime(2022, 04, 01); // Inicializar con la fecha minima como valor
        FechaFin.Date = new DateTime(2023, 12, 01);//Incializar con la fecha maxima como valor
    }

    private void FechaIncio_DateSelected(object sender, DateChangedEventArgs e)
    {
        fechai = e.NewDate; //para obtener la nueva fecha seleccionada
    }

    private void FechaFin_DateSelected(object sender, DateChangedEventArgs e)
    {
        fechaf = e.NewDate; //para obtener la nueva fecha seleccionada

    }

    private async void DatosProd_Clicked(object sender, EventArgs e)
    {
        if ((fechaf.Year > fechai.Year) || (fechaf.Month > fechai.Month)) {
            if (fechai.Date < FechaInicio.MinimumDate)
            {
                await DisplayAlert("Alerta", $"Seleccionar una fecha inicial mayor a {FechaInicio.MinimumDate}", "Ok");
            }
            Sw = new Helper();
            DatosProductos = await Sw.ProductosPorFecha(fechai, fechaf);

            if (DatosProductos != null)
            {
                collectionView.ItemsSource = DatosProductos;
            }
            else
            {
                await DisplayAlert("Error", "Ocurrió  un error al desplegar los datos", "Ok");
            }
        }
        
        else
        {
            await DisplayAlert("Alerta", "La fecha Final no puede ser menor que la inicial", "Ok");
        }



    }
}