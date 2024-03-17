using Baterias.NetMaui.Modelo;
namespace Baterias.NetMaui.Paginas;

public partial class Agregar : ContentPage
{
	Helper Sw;
	DateTime Fecha;
	string Img;
	public Agregar()
	{
		InitializeComponent();
	}
    private void FechaCaducidad_DateSelected(object sender, DateChangedEventArgs e)
    {
		Fecha = Convert.ToDateTime(FechaCaducidad.Date);
    }


    private async void Enviar_Clicked(object sender, EventArgs e)
	{
		Sw = new Helper();
        if (Nombre.Text != null && Presentacion.Text != null && FechaCaducidad.Date != DateTime.MinValue && Precio.Text != null && SeleccionarImagen.Text != null)

        {
            try
            {

                Producto nuevoProducto = new Producto
                {
                    Nombre = Nombre.Text,
                    Presentacion = Presentacion.Text,
                    FechaCaducidad = Fecha,
                    Precio = Convert.ToDouble(Precio.Text),
                    ImagenPath = SeleccionarImagen.Text
                };

                await Sw.Agregar(nuevoProducto);
                //Redirigir a la Pagina Listar
                await Navigation.PushAsync(new Listar());
            }
            catch
            {
                await DisplayAlert("Error", "Ocurrió un error al Intentar hacer el registro", "Ok");
            }
        }
        else
        {
            await DisplayAlert("Alerta", "Llenar todos los datos solicitados", "Ok");
        }
    }

	//Para tomar la imagen y guardarla
    private async void SeleccionarImagen_Clicked(object sender, EventArgs e)
    {
        var mediaResult = await MediaPicker.PickPhotoAsync();
        if (mediaResult != null)
        {
            Img = mediaResult.FullPath;
        }
    }


}