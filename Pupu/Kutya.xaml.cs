using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pupu
{
    /// <summary>
    /// Interaction logic for Kutya.xaml
    /// </summary>
    public partial class Kutya : Window
    {
        public Kutya()
        {
            InitializeComponent();
            Timer();
            Update();
        }


        private int health = 100;
        private int mood = 100;
        private int energy = 100;
        private int hunger = 0;


        private CancellationTokenSource cts = new CancellationTokenSource();
        private void Timer()
        {
            if (cts.Token.IsCancellationRequested)
                return;

            Task.Run(async () => {
                while (true)
                {
                    if (cts.Token.IsCancellationRequested)
                        break;

                    await Task.Delay(TimeSpan.FromMinutes(1));
                    health = Math.Max(0, health - 10);
                    if (health == 0)
                    {
                        MessageBox.Show("Your dog is sick! Let it sleep to recover health!");
                    }
                    Application.Current.Dispatcher.Invoke(() => Update());
                }
            }, cts.Token);

            Task.Run(async () => {
                while (true)
                {
                    if (cts.Token.IsCancellationRequested)
                        break;

                    await Task.Delay(TimeSpan.FromSeconds(30));
                    mood = Math.Max(0, mood - 30);

                    if (mood == 0)
                    {
                        MessageBox.Show("Your dog is moody! Let it sleep or do activities with it to recover mood meter!");
                    }
                    Application.Current.Dispatcher.Invoke(() => Update());
                }
            }, cts.Token);

            Task.Run(async () => {
                while (true)
                {
                    if (cts.Token.IsCancellationRequested)
                        break;

                    await Task.Delay(TimeSpan.FromMinutes(1));
                    energy = Math.Max(0, energy - 5);
                    if (energy == 0)
                    {
                        MessageBox.Show("Your dog is exhausted! Press the sleep button to recover energy!");
                    }
                    Application.Current.Dispatcher.Invoke(() => Update());
                }
            }, cts.Token);

            Task.Run(async () => {
                while (true)
                {
                    if (cts.Token.IsCancellationRequested)
                        break;

                    await Task.Delay(TimeSpan.FromSeconds(30));
                    hunger = Math.Min(100, hunger + 5);
                    if (hunger == 0)
                    {
                        MessageBox.Show("Your dog is starving! Feed it as soon as possible!");
                    }
                    Application.Current.Dispatcher.Invoke(() => Update());
                }
            }, cts.Token);
        }

        private void food_button_Click(object sender, RoutedEventArgs e)
        {
            hunger = Math.Max(0, hunger - 20);
            health = Math.Min(100, health + 5);
            Update();
        }

        private void sleep_button_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
            cts = new CancellationTokenSource();
            energy = Math.Min(100, energy + 20);
            mood = Math.Min(100, mood + 20);
            health = Math.Min(100, health + 20);
            Timer();
            Update();

        }

        private void walk_button_Click(object sender, RoutedEventArgs e)
        {
            mood = Math.Min(100, mood + 20);
            energy = Math.Max(0, energy - 20);
            hunger = Math.Min(100, hunger + 10);
            Update();
        }

        private void catch_button_Click(object sender, RoutedEventArgs e)
        {
            mood = Math.Min(100, mood + 20);
            energy = Math.Max(0, energy - 20);
            hunger = Math.Min(100, hunger + 5);
            Update();
        }

        private void Update()
        {
            lbl_energy.Content = $"Energy: {energy}";
            lbl_mood.Content =$"Mood: {mood}";
            lbl_hunger.Content = $"Hunger: {hunger}";
            lbl_health.Content = $"Health: { health}";
        }
    }
}
