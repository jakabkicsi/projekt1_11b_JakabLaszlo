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
using System.IO;

namespace FeladatNyilvantarto_projekt1_JakabLaszlo11B
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<CheckBox> feladatok = new List<CheckBox>();
        List<CheckBox> toroltek = new List<CheckBox>();

        public MainWindow()
        {
            InitializeComponent();
            feladatokListaja.ItemsSource = feladatok;
            toroltFeladatokListaja.ItemsSource = toroltek;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            List<string> checkboxok = new List<string>();
            foreach (CheckBox x in feladatokListaja.Items)
            {
                int allapot = 0;
                if (x.IsChecked == true)
                    allapot = 1;
                string CBallapot = x.Content.ToString() + ";" + allapot;
                checkboxok.Add(CBallapot);
            }
            File.WriteAllLines("adatokmentese.txt", checkboxok.ToArray());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var be = File.ReadAllLines("adatokmentese.txt");
            foreach (var x in be)
            {
                CheckBox uj = new CheckBox();
                uj.Content = x.Split(';')[0];
                uj.Checked += new RoutedEventHandler(CheckBox_Checked);
                uj.Unchecked += new RoutedEventHandler(CheckBox_Unchecked);
                uj.IsChecked = x.Split(';')[1] == "1" ? true : false;
                feladatok.Add(uj);
            }
             feladatokListaja.Items.Refresh();
        }

        private void FeladatHozzaad_Click(object sender, RoutedEventArgs e)
        {
            if (feladatSzovege.Text != "")
            {
                CheckBox uj = new CheckBox();
                uj.Content = feladatSzovege.Text;
                uj.Checked += new RoutedEventHandler(CheckBox_Checked);
                uj.Unchecked += new RoutedEventHandler(CheckBox_Unchecked);
                feladatok.Add(uj);
                feladatSzovege.Text = "";
                feladatokListaja.Items.Refresh();
            }
        }

        private void feladatokListaja_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CheckBox kijeloltCB = (CheckBox)feladatokListaja.SelectedItem;
            feladatSzovege.Text = kijeloltCB.Content.ToString();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox aktualis = (CheckBox)sender;
            aktualis.FontStyle = FontStyles.Italic;
            aktualis.Foreground = Brushes.Gray;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox aktualis = (CheckBox)sender;
            aktualis.FontStyle = FontStyles.Normal;
            aktualis.Foreground = Brushes.Black;
        }

        private void kijeloltModositasa_Click(object sender, RoutedEventArgs e)
        {
            CheckBox kijeloltCB = (CheckBox)feladatokListaja.SelectedItem;
            kijeloltCB.Content = feladatSzovege.Text;
        }

        private void FeladatTorol_Click(object sender, RoutedEventArgs e)
        {
            CheckBox kijeloltCB = (CheckBox)feladatokListaja.SelectedItem;
            toroltek.Add(kijeloltCB);
            feladatok.Remove(kijeloltCB);
            feladatokListaja.Items.Refresh();
            toroltFeladatokListaja.Items.Refresh();
        }

        private void feladatVissza_Click(object sender, RoutedEventArgs e)
        {
            CheckBox kijeloltCB = (CheckBox)toroltFeladatokListaja.SelectedItem;
            feladatok.Add(kijeloltCB);
            toroltek.Remove(kijeloltCB);
            feladatokListaja.Items.Refresh();
            toroltFeladatokListaja.Items.Refresh();
        }

        private void kijeloltVeglegTorlese_Click(object sender, RoutedEventArgs e)
        {
            CheckBox kijeloltCB = (CheckBox)toroltFeladatokListaja.SelectedItem;
            toroltek.Remove(kijeloltCB);
            toroltFeladatokListaja.Items.Refresh();
        }

        
        
    }    
}
