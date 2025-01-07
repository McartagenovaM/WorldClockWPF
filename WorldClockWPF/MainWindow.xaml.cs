using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Calendar = System.Globalization.Calendar;

namespace WorldClockWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Timer para control de la hora
        private DispatcherTimer timer;

        // Variable de idioma, puede ser "es" para español o "en" para inglés
        private string language = "es"; // Cambia entre "es" o "en"

        public MainWindow()
        {
            InitializeComponent();

            //Inicializo la ciudad por defecto
            // Valores dinámicos para ciudad y país
            string city = "Quito";
            string country = "Ecuador";

            // Establecer los valores en los elementos del Label
            CityName.Text = city;
            CountryName.Text = country;

            // Obtener la fecha actual
            DateTime currentDate = DateTime.Now;

            // Establecer la cultura según el idioma
            CultureInfo culture = language == "es" ? new CultureInfo("es-ES") : new CultureInfo("en-US");

            // Obtener el nombre del día y el mes con la cultura correspondiente
            string dayName = currentDate.ToString("dddd", culture);
            string monthName = currentDate.ToString("MMMM", culture);

            // Asegurar que la primera letra del día y mes esté en mayúscula
            TextInfo textInfo = culture.TextInfo;
            dayName = textInfo.ToTitleCase(dayName);  // Capitaliza el nombre del día
            monthName = textInfo.ToTitleCase(monthName);  // Capitaliza el nombre del mes

            // Obtener el número de la semana
            Calendar calendar = culture.Calendar;
            int weekNumber = calendar.GetWeekOfYear(currentDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            // Formatear la fecha en el estilo deseado
            string formattedDate = string.Format("{0}, {1} {2}, {3}, Semana {4}", dayName, monthName, currentDate.Day, currentDate.Year, weekNumber);

            // Establecer el contenido del Label con la fecha formateada
            DateLabel.Content = formattedDate;

            // Configuración del DispatcherTimer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Actualizar el contenido del Label con la hora actual
            CurrentTimeLabel.Content = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}