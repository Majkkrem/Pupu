using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

        private bool health_box = false;
        private bool mood_box = false;
        private bool energy_box = false;
        private bool hunger_box = false;

        private DispatcherTimer timer_energy;
        private DispatcherTimer timer_mood;
        private DispatcherTimer timer_hunger;
        private DispatcherTimer timer_health;



        public Macska()
        {
            InitializeComponent();
            InitializeTimer();
           
        }

        private void InitializeTimer()
        {
            timer_energy = new DispatcherTimer();
            timer_energy.Interval = TimeSpan.FromSeconds(30);
            timer_energy.Tick += Timer_Tick;
            timer_energy.Start();

            timer_mood = new DispatcherTimer();
            timer_mood.Interval = TimeSpan.FromSeconds(60);
            timer_mood.Tick += Timer_Tick;
            timer_mood.Start();

            timer_hunger = new DispatcherTimer();
            timer_hunger.Interval = TimeSpan.FromSeconds(40);
            timer_hunger.Tick += Timer_Tick;
            timer_hunger.Start();

            timer_health = new DispatcherTimer();
            timer_health.Interval = TimeSpan.FromSeconds(50);
            timer_health.Tick += Timer_Tick;
            timer_health.Start();

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
                    energy_box = true;
                    if (energy_box == true)
                    {
                        MessageBox.Show("Your cat is exhausted! Let it sleep or do activities with it to recover mood meter!");
                        energy_box = false;

                    }
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
                    mood_box = true;
                    if (mood_box == true)
                    {
                        MessageBox.Show("Your cat is moody! Let it sleep or do activities with it to recover mood meter!");
                        mood_box = false;

                    }

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
                    hunger_box = true;
                    if (hunger_box == true)
                    {
                        MessageBox.Show("Your cat is starving! Let it sleep or do activities with it to recover mood meter!");
                        hunger_box = false;
                    }
                }
                else if (sleepStatus == true)
                {
                    hunger += 15;
                }
                
                else
                {
                    hunger += 5;
                }
            }


            else if (sender == timer_health)
            {
                if (health == 0)
                {
                    health_box = true;
                    if (health_box == true)
                    {
                        MessageBox.Show("Your cat is in pretty bad shape! Feed it or play with it to put some life in your pet!");
                        health_box = false;

                    }
                    

                }
                else if (sleepStatus == true)
                {
                    health += 15;
                }
                else
                {
                    health -= 8;

                }
            }

            ChangeValue();


        }






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
                timer_energy.Stop();
            }
            else
            {
                timer_energy.Start();
            }

            ChangeValue();



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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            timer_energy.Stop();
            timer_health.Stop();
            timer_hunger.Stop();
            timer_mood.Stop();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


        //Macska hangeffect
        private void catMeowClick(object sender, MouseEventArgs e)
        {
            Random random = new Random();
            int randomSound = random.Next(1, 3);

            SoundPlayer meow1 = new SoundPlayer(Properties.Resources.catMeow1);
            SoundPlayer meow2= new SoundPlayer(Properties.Resources.catMeow2);
            SoundPlayer meow3 = new SoundPlayer(Properties.Resources.catMeow3);  

            if (randomSound == 1)
            {
                meow1.Load();
                meow1.Play();
            }
            else if (randomSound == 2)
            {
                meow2.Load();
                meow2.Play();
            }
            else
            {
                meow3.Load();
                meow3.Play();
            }
        }
    }
}
