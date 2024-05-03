using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pupu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Window> Windows { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }


      
       //private void playsoundOne(object sender, RoutedEventArgs e)
       // {
       //     SoundPlayer playSound = new SoundPlayer(Properties.Resources.Kainbeats___Lonely_views_in_the_park__Ft__Refeeld_);
       //     playSound.Play();
       // }
       

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            if (PetComboBox.SelectedItem != null)
            {
                string selectedWindowName = ((ComboBoxItem)PetComboBox.SelectedItem).Content.ToString();
                Window selectedWindow = null;

                switch (selectedWindowName)
                {
                    case "Dog":
                        selectedWindow = new Kutya();
                        break;
                    case "Cat":
                        selectedWindow = new Macska();
                        break;
                    case "Bunny":
                        selectedWindow = new Nyúl();
                        break;
                    case "Bird":
                        selectedWindow = new Madár();
                        break;
                    default:
                        MessageBox.Show("Nem található ablak a választott háziállathoz!");
                        break;
                }

                if (selectedWindow != null)
                {
                    selectedWindow.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Kérlek válassz egy háziállatot a játékhoz!");
            }
        }


        public static string selected;
        private void Pet_Selection(object sender, SelectionChangedEventArgs e)
        {

            if (e.AddedItems.Count > 0)
            {
                ComboBoxItem selectedItem = e.AddedItems[0] as ComboBoxItem;
                if (selectedItem != null)
                {
                    selected = selectedItem.Content.ToString();
                }
            }

        }
    }
}
