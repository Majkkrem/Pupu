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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pupu
{
    /// <summary>
    /// Interaction logic for Madár.xaml
    /// </summary>
    public partial class Madár : Window
    {
        private int health = 100;
        private int mood = 100;
        private int energy = 100;
        private int hunger = 0;
        private bool isSleeping = false;
        private DispatcherTimer timer;
        public Madár()
        {
            InitializeComponent();
            InitializeTimer();
        }
        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(2);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isSleeping)
            {
                mood -= 10;
                UpdateUI();
            }
        }

        private void btnFeed_Click(object sender, RoutedEventArgs e)
        {
            if (!isSleeping)
            {
                hunger = Math.Max(0, hunger - 20);
                health = Math.Min(100, health + 5);
                UpdateUI();
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (!isSleeping)
            {
                energy -= 10;
                mood += 20;
                UpdateUI();
            }
        }

        private void btnSing_Click(object sender, RoutedEventArgs e)
        {
            if (!isSleeping)
            {
                energy -= 10;
                mood += 20;
                UpdateUI();
            }
        }

        private void btnFly_Click(object sender, RoutedEventArgs e)
        {
            if (!isSleeping)
            {
                energy -= 10;
                mood += 20;
                UpdateUI();
            }
        }

        private void btnSleep_Click(object sender, RoutedEventArgs e)
        {
            isSleeping = !isSleeping;
            btnSleep.Content = isSleeping ? "Wake Up" : "Sleep";
            if (isSleeping)
            {
                timer.Stop();
            }
            else
            {
                energy = 100;
                timer.Start();
            }
            UpdateUI();
        }

        private void UpdateUI()
        {
            txtHealth.Text = $"Health: {health}";
            txtMood.Text = $"Mood: {mood}";
            txtEnergy.Text = $"Energy: {energy}";
            txtHunger.Text = $"Hunger: {hunger}";
        }
    }
}
