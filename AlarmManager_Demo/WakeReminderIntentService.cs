//
//  WakeReminderIntentService.cs
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
using Android.Util;
using System.Runtime.CompilerServices;

namespace AlarmManager_Demo
{
	public abstract class WakeReminderIntentService : IntentService
	{
		private static readonly string TAG =  typeof(WakeReminderIntentService).Name;

		internal abstract void DoReminderWork(Intent intent);

		public const string LOCK_NAME_STATIC = "AlarmManager.Demo.Static";
		private static PowerManager.WakeLock lockStatic = null;

		public static void acquireStaticLock(Context context)
		{
			try
			{
				GetLock(context).Acquire();
			}
			catch (Exception ex)
			{
				Log.Error(TAG, ex.Message);
			}
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		private static PowerManager.WakeLock GetLock(Context context)
		{
			if (lockStatic == null)
			{
				PowerManager mgr = (PowerManager)context.GetSystemService(Context.PowerService);
				lockStatic = mgr.NewWakeLock(WakeLockFlags.Partial, LOCK_NAME_STATIC);
				lockStatic.SetReferenceCounted(true);
			}
			return (lockStatic);
		}
		public WakeReminderIntentService() : base("WakeReminderIntentService") {}

		public WakeReminderIntentService(String name) : base(name) {}

		protected override void OnHandleIntent(Intent intent)
		{
			try
			{
				Android.OS.PowerManager.WakeLock screenOn = ((PowerManager) GetSystemService(PowerService)).NewWakeLock(WakeLockFlags.ScreenBright | WakeLockFlags.AcquireCausesWakeup, TAG);
				screenOn.Acquire();

				DoReminderWork(intent);

				screenOn.Release();
			}
			finally
			{
				GetLock(this).Release();
			}
		}
	}
}

