using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherApp.API;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    ///</summary>

    //todo: porozdzielać to jakoś sensownie zgodnie z mvc a nie tak jak teraz wszystko na kupie

    public partial class MainWindow : Window
    {
        private APIController api;
        public MainWindow()
        {
            InitializeComponent();

            api = new APIWeatherController();
        }

        private void RefreshWindowContent()
        {
            api.Parse();
            api.Insert();

            CountryLabelValue.Content = SingletonApiParser.Instance.Parser.countryTag;
            CityLabelValue.Content = SingletonApiParser.Instance.Parser.cityName;

            TemperatureValueLabel.Content = SingletonApiParser.Instance.Parser.temperatureValue;
            TemperatureUnitLabel.Content = SingletonApiParser.Instance.Parser.unitName;
            CloudsNameLabel.Content = SingletonApiParser.Instance.Parser.cloudsName;
            HumidityValueLabel.Content = SingletonApiParser.Instance.Parser.humidity;
            PressureValueLabel.Content = SingletonApiParser.Instance.Parser.pressure;
            WindNameLabel.Content = SingletonApiParser.Instance.Parser.windName;
            WindSpeedValueLabel.Content = SingletonApiParser.Instance.Parser.windSpeed;
            LastUpdateValueLabel.Content = SingletonApiParser.Instance.Parser.lastUpdate;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            api.Init(CityInputTextBox.Text, CountryInputTextBox.Text);
            RefreshWindowContent();
        }
    }
}
