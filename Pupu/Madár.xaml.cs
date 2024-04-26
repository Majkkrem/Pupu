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
        private DispatcherTimer timer_energy;
        private DispatcherTimer timer_mood;
        private DispatcherTimer timer_hunger;
        public Madár()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(13);
            timer.Tick += Timer_Tick;
            timer.Start();

            timer_energy = new DispatcherTimer();
            timer_energy.Interval = TimeSpan.FromSeconds(10);
            timer_energy.Tick += Timer_Tick;
            timer_energy.Start();

            timer_mood = new DispatcherTimer();
            timer_mood.Interval = TimeSpan.FromSeconds(8);
            timer_mood.Tick += Timer_Tick;
            timer_mood.Start();

            timer_hunger = new DispatcherTimer();
            timer_hunger.Interval = TimeSpan.FromSeconds(15);
            timer_hunger.Tick += Timer_Tick;
            timer_hunger.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isSleeping)
            {
                mood -= 10;
                hunger += 10;
                UpdateUI();
            }

            if (sender == timer_energy)
            {
                if (energy == 0)
                {
                    MessageBox.Show("Your bird is exhausted! Press the sleep button to recover energy!");
                }
                else if (isSleeping == true)
                {
                    energy += 20;
                }
                else
                {
                    energy -= 10;
                }

            }
            else if (sender == timer_mood)
            {
                if (mood == 0)
                {
                    MessageBox.Show("Your bird is moody! Let it sleep or do activities with it to recover mood meter!");
                }
                else if (isSleeping == true)
                {
                    mood += 15;
                }
                else
                {
                    mood -= 8;

                }
            }
            else if (sender == timer_hunger)
            {
                if (hunger == 100)
                {
                    MessageBox.Show("Your bird is starving! Feed it as soon as possible!");
                }
                else
                {
                    hunger += 5;

                }
            }

            UpdateUI();
        }

        private void btnFeed_Click(object sender, RoutedEventArgs e)
        {
            if (!isSleeping)
            {
                hunger = Math.Max(0, hunger - 20);
                health += 5;
                UpdateUI();
            }
        }

        private void btnSing_Click(object sender, RoutedEventArgs e)
        {
            if (!isSleeping)
            {
                energy -= 5;
                mood += 25;
                UpdateUI();
            }
        }

        private void btnFly_Click(object sender, RoutedEventArgs e)
        {
            if (!isSleeping)
            {
                energy -= 10;
                mood += 30;
                health -= 5;
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
                timer.Start();
            }
            UpdateUI();
        }

        private void UpdateUI()
        {
            energy = Math.Max(0, Math.Min(energy, 100));
            mood = Math.Max(0, Math.Min(mood, 100));
            hunger = Math.Max(0, Math.Min(hunger, 100));
            health = Math.Max(0, Math.Min(health, 100));

            txtHealth.Text = $"Health: {health}";
            txtMood.Text = $"Mood: {mood}";
            txtEnergy.Text = $"Energy: {energy}";
            txtHunger.Text = $"Hunger: {hunger}";
        }
    }
}
