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
        if (Nombre.Text != null && Presentacion.Text != null && ((FechaCaducidad.Date != DateTime.MinValue) && (FechaCaducidad.Date > DateTime.Now)) && Precio.Text != null && SeleccionarImagen.Text != null)

        {
            try
            {

                Producto nuevoProducto = new Producto
                {
                    Nombre = Nombre.Text,
                    Presentacion = Presentacion.Text,
                    FechaCaducidad = Fecha,
                    Precio = Convert.ToDouble(Precio.Text),
                    ImagenPath = Img
                };

                
                int si = await Sw.Agregar(nuevoProducto);

                if(si == 1)
                {
                    await DisplayAlert("Exito", "Se ha insertado correctamente", "ok");
                    Nombre.Text = "";
                    Presentacion.Text = "";
                    FechaCaducidad.Date = DateTime.Now;
                    Precio.Text = "";
                }
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

    private async void SeleccionarImagen_Clicked(object sender, EventArgs e)
    {
        var mediaResult = await MediaPicker.PickPhotoAsync();//Tomar la foto,hacerlo similar a un input type=file
        if (mediaResult != null)
        {
            Img = mediaResult.FileName;//se guarda la foto ahí
        }
    }
}