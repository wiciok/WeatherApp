using System;
using System.IO;
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
//using System.Windows.Shapes;
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

            api = new APIOpenWeatherMapController();
            FillConstLabels();
            LoadImage("UNKNOWN");
        }

        private void FillConstLabels()
        {
            CityLabelValue.Content = api.StringsFactory.GetCityLabelContent();
            CountryLabelValue.Content = api.StringsFactory.GetCountryLabelContent();

            TemperatureLabel.Content = api.StringsFactory.GetTemperatureLabelContent();
            CloudsLabel.Content = api.StringsFactory.GetCloudsLabelContent();
            HumidityLabel.Content = api.StringsFactory.GetHumidityLabelContent();
            PressureLabel.Content = api.StringsFactory.GetPressureLabelContent();
            WindNameLabel.Content = api.StringsFactory.GetWindLabelContent();
            WindSpeedLabel.Content = api.StringsFactory.GetWindspeedLabelContent();
            LastUpdateLabel.Content = api.StringsFactory.GetLastUpdateLabelContent();

            CityInputTextBox.Text = api.StringsFactory.GetCityInputTextBoxText();
            CountryInputTextBox.Text = api.StringsFactory.GetCountryInputTextBoxText();

            CheckWeatherButton.Content = api.StringsFactory.GetCheckweatherButtonContent();
        }

        private void ReloadMeasurementsLabels()
        {
            api.Parse();
            api.InsertToDB();

            CountryLabelValue.Content = SingletonApiParser.Instance.Parser.countryTag;
            CityLabelValue.Content = SingletonApiParser.Instance.Parser.cityName;

            TemperatureValueLabel.Content = TemperatureSelector.GetTemperatureValue();
            TemperatureUnitLabel.Content = TemperatureSelector.GetTemperatureUnit();
            CloudsNameLabel.Content = SingletonApiParser.Instance.Parser.cloudsName;
            HumidityValueLabel.Content = SingletonApiParser.Instance.Parser.humidity;
            PressureValueLabel.Content = SingletonApiParser.Instance.Parser.pressure;
            WindNameValueLabel.Content = SingletonApiParser.Instance.Parser.windName;
            WindSpeedValueLabel.Content = SingletonApiParser.Instance.Parser.windSpeed;
            WindDirectionCodeValueLabel.Content = SingletonApiParser.Instance.Parser.windDirectionCode;
            LastUpdateValueLabel.Content = SingletonApiParser.Instance.Parser.lastUpdate;

            LoadImage(CloudsNameLabel.Content.ToString());
        }

        private void ReloadMeasurementsUnits()
        {
            HumidityUnitLabel.Content = api.UnitStringsFactory.GetHumidityUnitLabelContent();
            PressureUnitLabel.Content = api.UnitStringsFactory.GetPressureUnitLabelContent();
            WindSpeedUnitLabel.Content = api.UnitStringsFactory.GetWindSpeedUnitLabelContent();
        }

        private void LoadImage(string imageName)
        {
            try
            {
                var path = Path.Combine(Environment.CurrentDirectory, "../../Images", imageName + ".png");
                var uri = new Uri(path);
                var bitmap = new BitmapImage(uri);
                CloudsImage.Source = bitmap;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CityInputTextBox.Text == "" || CountryInputTextBox.Text == "" 
                    || CountryInputTextBox.Text == api.StringsFactory.GetCountryInputTextBoxText() 
                    || CityInputTextBox.Text == api.StringsFactory.GetCityInputTextBoxText())
                    throw new Exception("City and/or country not properly typed!");

                InternetConnectionChecker.Check();

                api.Init(CityInputTextBox.Text, CountryInputTextBox.Text);
                ReloadMeasurementsLabels();
                ReloadMeasurementsUnits();
            }
            catch (Exception ex)
            {
                string messBoxText = "Exception occured! " + ex.Message + " \n";

                Exception tmp = ex;
                while (tmp.InnerException != null)
                {
                    messBoxText += tmp.Message;
                }
                MessageBoxResult errBox = MessageBox.Show(messBoxText,"Exception occured");
            }
        }

        private void CityInputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CityInputTextBox.Text == api.StringsFactory.GetCityInputTextBoxText())
                CityInputTextBox.Text = "";
        }

        private void CountryInputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CountryInputTextBox.Text == api.StringsFactory.GetCountryInputTextBoxText())
                CountryInputTextBox.Text = "";
        }

        private void CityInputTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CityInputTextBox.Text == "")
                CityInputTextBox.Text = api.StringsFactory.GetCityInputTextBoxText();
        }

        private void CountryInputTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CountryInputTextBox.Text == "")
                CountryInputTextBox.Text = api.StringsFactory.GetCountryInputTextBoxText();
        }

        private void UnitRadioKelvin_Checked(object sender, RoutedEventArgs e)
        {
            Settings.tempUnit = TemperatureUnits.Kelvin;
        }

        private void UnitRadioCelsius_Checked(object sender, RoutedEventArgs e)
        {
            Settings.tempUnit = TemperatureUnits.Celsius;
        }
    }
}
