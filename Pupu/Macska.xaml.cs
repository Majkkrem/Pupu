using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for Macska.xaml
    /// </summary>
    public partial class Macska : Window
    {

        private int health = 100;
        private int mood = 100;
        private int energy = 100;
        private int hunger = 0;
        private bool sleepStatus = false;
        private DispatcherTimer timer_energy;
        private DispatcherTimer timer_mood;
        private DispatcherTimer timer_hunger;



        public Macska()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer_energy = new DispatcherTimer();
            timer_energy.Interval = TimeSpan.FromSeconds(3);
            timer_energy.Tick += Timer_Tick;
            timer_energy.Start();

            timer_mood = new DispatcherTimer();
            timer_mood.Interval = TimeSpan.FromSeconds(8);
            timer_mood.Tick += Timer_Tick;
            timer_mood.Start();

            timer_hunger = new DispatcherTimer();
            timer_hunger.Interval = TimeSpan.FromSeconds(6);
            timer_hunger.Tick += Timer_Tick;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            {
                if (sender == timer_energy)
                {
                    if (energy == 0)
                    {
                        MessageBox.Show("Your bunny is exhausted! Press the sleep button to recover energy!");
                    }
                    else if (sleepStatus == true)
                    {
                        energy += 5;
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
                        MessageBox.Show("Your bunny is moody! Let it sleep or do activities with it to recover mood meter!");
                    }
                    else if (sleepStatus == true)
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
                    if (hunger == 0)
                    {
                        MessageBox.Show("Your bunny is starving! Feed it as soon as possible!");
                    }
                    else
                    {
                        hunger -= 5;

                    }
                }

                ChangeValue();
                sleep_wake_Click();
            }
        }

        private void sleep_wake_Click()
        {
            throw new NotImplementedException();
        }

        //TODO: Add sleep button, when clicked it changes to wake up button
        // when sleeping, the timer tick adds 10 to the sleep status every 3.5 seconds 

        private void ChangeValue()
        {

            energy = Math.Max(0, Math.Min(energy, 100));
            mood = Math.Max(0, Math.Min(mood, 100));
            hunger = Math.Max(0, Math.Min(hunger, 100));


            lbl_health.Content = $"Health: {health}";
            lbl_mood.Content = $"Mood: {mood}";
            lbl_energy.Content = $"Energy: {energy}";
            lbl_hunger.Content = $"Hunger: {hunger}";

        }

        private void sleep_wake_Click(object sender, RoutedEventArgs e)
        {
            Button sleepbutton = new Button();
            sleepStatus = true;
            if (sleepStatus == true)
            {
                sleepbutton.Content = "Wake Up";

            }
        }
    }
}
