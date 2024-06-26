﻿using System;
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
    /// Interaction logic for Nyúl.xaml
    /// </summary>
    public partial class Nyúl : Window
    {
        public Nyúl()
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
                        MessageBox.Show("Your bunny is sick! Let it sleep to recover health!");
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
                        MessageBox.Show("Your bunny is moody! Let it sleep or do activities with it to recover mood meter!");
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
                        MessageBox.Show("Your bunny is exhausted! Press the sleep button to recover energy!");
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
                        MessageBox.Show("Your bunny is starving! Feed it as soon as possible!");
                    }
                    Application.Current.Dispatcher.Invoke(() => Update());
                }
            }, cts.Token);
        }


        private void food_button_Click_1(object sender, RoutedEventArgs e)
        {
            hunger = Math.Max(0, hunger - 20);
            health = Math.Min(100, health + 5);
            Update();
        }

        private bool isSleeping = false;

        private void sleep_button_Click(object sender, RoutedEventArgs e)
        {
            isSleeping = !isSleeping;

            if (isSleeping)
            {

                food_button.IsEnabled = false;
                jump_button.IsEnabled = false;
                scrach_button.IsEnabled = false;

                cts.Cancel();
                cts = new CancellationTokenSource();

                Task.Run(async () =>
                {
                    while (isSleeping)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        energy = Math.Min(100, energy + 5);
                        mood = Math.Min(100, mood + 5);
                        health = Math.Min(100, health + 5);
                        Application.Current.Dispatcher.Invoke(() => Update());
                    }
                });
            }
            else
            {
                food_button.IsEnabled = true;
                jump_button.IsEnabled = true;
                scrach_button.IsEnabled = true;

                Timer();
            }
        }

        private void jump_button_Click_1(object sender, RoutedEventArgs e)
        {
            mood = Math.Min(100, mood + 20);
            energy = Math.Max(0, energy - 20);
            hunger = Math.Min(100, hunger + 10);
            Update();
        }

        private void scrach_button_Click(object sender, RoutedEventArgs e)
        {
            mood = Math.Min(100, mood + 20);
            energy = Math.Max(0, energy - 20);
            hunger = Math.Min(100, hunger + 5);
            Update();
        }

        private void Update()
        {
            lbl_energy.Content = $"Energy: {energy}";
            lbl_mood.Content = $"Mood: {mood}";
            lbl_hunger.Content = $"Hunger: {hunger}";
            lbl_health.Content = $"Health: {health}";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            cts.Cancel();
        }
    }
}
