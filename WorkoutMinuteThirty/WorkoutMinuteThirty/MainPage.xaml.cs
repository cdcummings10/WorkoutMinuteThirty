using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WorkoutMinuteThirty
{
    public partial class MainPage : ContentPage
    {
        Stopwatch stopwatch;
        public int CurrentSetNumber { get; set; }

        public MainPage()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();
            lblStopwatch.Text = "00:00:00.00";
            CurrentSetNumber = 1;
            lblWorkoutSetNumber.Text = CurrentSetNumber.ToString();

            //This sends an alert popup to the phone
            //NotificationCenter.Current.NotificationReceived += Current_NotificationReceived;

        }
        /// <summary>
        /// Sends an alert popup to the phone.
        /// </summary>
        /// <param name="e"></param>
        private void Current_NotificationReceived(NotificationEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            DisplayAlert("Test Title", "Test Message", "Cancel"));
        }
        /// <summary>
        /// On button start, increments set number property, starts stopwatch and updates the respective label fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Clicked(object sender, EventArgs e)
        {
            if (stopwatch.IsRunning == false)
            {
                CurrentSetNumber++;
            }
            lblWorkoutSetNumber.Text = CurrentSetNumber.ToString();
            stopwatch.Start();
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                TimeSpan ts = stopwatch.Elapsed;

                lblStopwatch.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                if (ts.Hours == 0 && ts.Minutes == 1 && ts.Seconds == 30)
                {
                    SendNotification();
                }
                if (ts.Hours == 0 && ts.Minutes == 3 && ts.Seconds == 0)
                {
                    SendNotification();
                }
                return true;
            }
            );

        }
        /// <summary>
        /// Sends a push notification.
        /// </summary>
        private void SendNotification()
        {
            var notification = new NotificationRequest
            {
                BadgeNumber = 1,
                Description = "Test Description",
                Title = "Test Title",
                ReturningData = "Dummy Data",
                Android = new AndroidOptions
                {
                    VibrationPattern = new long[] { 50, 50, 50, 50 }
                }
            };

            NotificationCenter.Current.Show(notification);
        }
        /// <summary>
        /// Stops the stopwatch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Clicked(object sender, EventArgs e)
        {
            stopwatch.Stop();
        }
        /// <summary>
        /// Resets the stopwatch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Clicked(object sender, EventArgs e)
        {
            stopwatch.Reset();
        }
        /// <summary>
        /// Resets set number.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetSetNumber_Clicked(object sender, EventArgs e)
        {
            if (stopwatch.IsRunning == false)
            {
                CurrentSetNumber = 1;
                lblWorkoutSetNumber.Text = CurrentSetNumber.ToString();
            }
        }
        private void btnStats_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new StatsPage();
        }
    }
}
