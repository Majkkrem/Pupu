using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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



            Windows = new List<Window>
           {
            new Kutya(),
            new Macska(),
            new Nyúl(),
            new Madár()

           };
       
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            if (PetComboBox.SelectedItem != null)
            {
                string selectedWindowName = ((ComboBoxItem)PetComboBox.SelectedItem).Content.ToString();
                Window selectedWindow = Windows.Find(w => w.GetType().Name == selectedWindowName);

                if (selectedWindow != null)
                {
                    selectedWindow.Show();
                }
            }
            else
            {
                MessageBox.Show("Válassz egy állatot a játék megkezdéséhez");
            }
        }

        public static string selected;
        private void Pet_Selection(object sender, SelectionChangedEventArgs e)
        {
            selected = (e.AddedItems[0] as ComboBoxItem).Content as string;


        }
    }
}
