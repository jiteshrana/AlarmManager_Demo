//
//  RepeatingAlarm.cs
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
using Android.Widget;

namespace AlarmManager_Demo
{
	[BroadcastReceiver]
	[IntentFilter (new[]{Intent.ActionBootCompleted , Intent.ActionTimeChanged})]
	public class RepeatingAlarm : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			Toast.MakeText(context,"Repeating alarm received.", ToastLength.Short).Show();

			WakeReminderIntentService.acquireStaticLock(context);

			Intent i = new Intent(context, typeof(ReminderService));

			context.StartService(i);
		}
	}
}

