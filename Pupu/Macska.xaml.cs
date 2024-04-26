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
        private bool scratchStatus = false;

        private DispatcherTimer timer_energy;
        private DispatcherTimer timer_mood;
        private DispatcherTimer timer_hunger;
        private DispatcherTimer timer_sleep;



        public Macska()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer_energy = new DispatcherTimer();
            timer_energy.Interval = TimeSpan.FromSeconds(5);
            timer_energy.Tick += Timer_Tick;
            timer_energy.Start();

            timer_mood = new DispatcherTimer();
            timer_mood.Interval = TimeSpan.FromSeconds(18);
            timer_mood.Tick += Timer_Tick;
            timer_mood.Start();

            timer_hunger = new DispatcherTimer();
            timer_hunger.Interval = TimeSpan.FromSeconds(16);
            timer_hunger.Tick += Timer_Tick;
            timer_hunger.Start();

            timer_sleep = new DispatcherTimer();
            timer_sleep.Interval = TimeSpan.FromSeconds(25);
            timer_sleep.Tick += Timer_Tick;
            timer_sleep.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!sleepStatus)
            {
                mood -= 10;
                hunger += 15;
                ChangeValue();
            }

            if (sender == timer_energy)
            {
                if (energy == 0)
                {
                    MessageBox.Show("Your cat is exhausted! Press the sleep button to recover energy!");
                }
                else if (sleepStatus == true)
                {
                    energy += 5;
                }
                else
                {
                    energy -= 2;
                }


            }
            else if (sender == timer_mood)
            {
                if (mood == 0)
                {
                    MessageBox.Show("Your cat is moody! Let it sleep or do activities with it to recover mood meter!");
                    
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
                if (hunger == 100)
                {
                    MessageBox.Show("Your cat is starving! Feed it as soon as possible!");
                }
                else
                {
                    hunger += 5;

                }
            }

            ChangeValue();


        }



        //TODO: Add sleep button, when clicked it changes to wake up button
        // when sleeping, the timer tick adds 10 to the sleep status every 3.5 seconds 

        private void ChangeValue()
        {

            energy = Math.Max(0, Math.Min(energy, 100));
            mood = Math.Max(0, Math.Min(mood, 100));
            hunger = Math.Max(0, Math.Min(hunger, 100));
            health = Math.Max(0, Math.Min(health, 100));

            lbl_health.Content = $"Health: {health}";
            lbl_mood.Content = $"Mood: {mood}";
            lbl_energy.Content = $"Energy: {energy}";
            lbl_hunger.Content = $"Hunger: {hunger}";

        }

        private void sleep_wake_Click(object sender, RoutedEventArgs e)
        {
            
                sleepStatus = !sleepStatus;

                if (sleepStatus)
                {
                    timer_sleep.Stop();
                }
                else
                {
                    timer_sleep.Start();
                }

                ChangeValue() ;

                
            
        }


        private void scratch_click(object sender, RoutedEventArgs e)
        {
            if (!sleepStatus)
            {
                energy -= 5;
                mood += 25;
                ChangeValue();
            }

            if (!scratchStatus)
                {
                    mood += 10;
                    energy -= 15;
                    health -= 10;
                    hunger += 10;
                    ChangeValue();
                }
            
        }

        private void play_click(object sender, RoutedEventArgs e)
        {
             

                if (!sleepStatus)
                {
                    mood += 14;
                    energy -= 20;
                    hunger += 16;
                    ChangeValue();
                }
            
        }

        private void eat_click(object sender, RoutedEventArgs e)
        {

            if (!sleepStatus)
            {
            energy -= 5;
            mood += 5;
            hunger -= 20;
            ChangeValue();
            }



        }
    }
}
