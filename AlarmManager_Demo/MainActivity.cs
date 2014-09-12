using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Util;

namespace AlarmManager_Demo
{
	[Activity (Label = "AlarmManager_Demo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		Button btn_startrepeating, btn_stoprepeating;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it

			btn_startrepeating = FindViewById<Button>(Resource.Id.btn_start_repeating);
			btn_stoprepeating = FindViewById<Button>(Resource.Id.btn_stop_repeating);
			
			btn_startrepeating.Click += delegate {
				Intent intent = new Intent(this, typeof(RepeatingAlarm));
				PendingIntent sender = PendingIntent.GetBroadcast(this, 0, intent, 0);

				Calendar calendar = Calendar.GetInstance(Java.Util.TimeZone.Default);
				calendar.Set(CalendarField.HourOfDay, 17);
				calendar.Set(CalendarField.Minute, 30);

				AlarmManager am = (AlarmManager)GetSystemService(Context.AlarmService);
				am.SetRepeating(AlarmType.RtcWakeup, calendar.TimeInMillis, AlarmManager.IntervalDay * 10 , sender);


				sender = PendingIntent.GetBroadcast(this, 1, intent, 0);

				calendar.Set(CalendarField.HourOfDay, 17);
				calendar.Set(CalendarField.Minute, 35);

				am.SetRepeating(AlarmType.RtcWakeup, calendar.TimeInMillis, AlarmManager.IntervalDay * 10 , sender);
			};

			btn_stoprepeating.Click += delegate {
				Intent intent = new Intent(this, typeof(RepeatingAlarm));
				PendingIntent sender = PendingIntent.GetBroadcast(this, 0, intent, 0);

				AlarmManager am = (AlarmManager)GetSystemService(Context.AlarmService);
				am.Cancel(sender);

				sender = PendingIntent.GetBroadcast(this, 1, intent, 0);

				am.Cancel(sender);
			};
		}
	}
}


