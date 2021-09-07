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

        public MainPage()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();
            lblStopwatch.Text = "00:00:00.00";

            //NotificationCenter.Current.NotificationReceived += Current_NotificationReceived;

        }

        private void Current_NotificationReceived(NotificationEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            DisplayAlert("Test Title", "Test Message", "Cancel"));
        }

        private void btnStart_Clicked(object sender, EventArgs e)
        {
            stopwatch.Start();
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                TimeSpan ts = stopwatch.Elapsed;

                lblStopwatch.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                return true;
            }
            );

        }

        private void btnStop_Clicked(object sender, EventArgs e)
        {
            stopwatch.Stop();
        }

        private void btnReset_Clicked(object sender, EventArgs e)
        {
            stopwatch.Reset();
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

    };

}
