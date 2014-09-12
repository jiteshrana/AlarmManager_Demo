//
//  ReminderService.cs
//
//  Author:
//       welcome <>
//
//  Copyright (c) 2014 welcome
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Media;
using Android.Util;

namespace AlarmManager_Demo
{
	[Service]
	public class ReminderService  : WakeReminderIntentService
	{
		private static readonly string TAG =  typeof(ReminderService).Name;

		public ReminderService() : base("ReminderService") {}

		internal override void DoReminderWork(Intent intent)
		{
			Bundle valuesForActivity = new Bundle();
			//valuesForActivity.PutInt("p_data", 1);


			// Create the PendingIntent with the back stack
			// When the user clicks the notification, SecondActivity will start up.
			Intent resultIntent = new Intent(this, typeof(MainActivity));
			resultIntent.PutExtras(valuesForActivity); // Pass some values to SecondActivity.

			Android.Support.V4.App.TaskStackBuilder stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(this);
			stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
			stackBuilder.AddNextIntent(resultIntent);

			PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

			// Build the notification
			NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
				.SetAutoCancel(true) // dismiss the notification from the notification area when the user clicks on it
				.SetContentIntent(resultPendingIntent) // start up this activity when the user clicks the intent.
				.SetContentTitle("Alarm Manager Demo") // Set the title
				//.SetNumber(rowId) // Display the count in the Content Info
				.SetSmallIcon(Resource.Drawable.Icon) // This is the icon to display
				.SetContentText(String.Format("Alarm Manager Demo", 1)); // the message to display.

				builder.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification));
				builder.SetVibrate(new long[] { 500, 800 });

			// Finally publish the notification
			NotificationManager notificationManager = (NotificationManager)GetSystemService(NotificationService);


			//Toast.MakeText(ApplicationContext,"Alarm Manager Demo ", ToastLength.Short).Show();
			notificationManager.Notify(0, builder.Build());
		}
	}
}

