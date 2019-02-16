using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CustomHeaderColourDataGrid {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            dataGrid.Loaded += dataGrid_Loaded;
        }

        void dataGrid_Loaded(object sender, RoutedEventArgs e) {
            dataGrid.Loaded -= dataGrid_Loaded;
            dataGrid.ItemsSource =           Enumerable.Range(0, 10).Select(_ => NewDataRow()).ToList();
        }
        private DataRow NewDataRow() => new DataRow
        {
            aas = Faker.CompanyFaker.BS(),
            aaass = Faker.LocationFaker.City(),
            aasas = Faker.NameFaker.FemaleFirstName(),
            addas = Faker.PhoneFaker.InternationalPhone()
        };


    }

}



public class DataRow
{
    public string aas { get; set; }
    public string aaass { get; set; }
    public string aasas { get; set; }
    public string addas { get; set; }
}


