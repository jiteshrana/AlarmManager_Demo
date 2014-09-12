//
//  WakeLocker.cs
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
using Android.Content;
using Android.OS;

namespace AlarmManager_Demo
{
	public abstract class WakeLocker
	{
		private static readonly string TAG =  typeof(WakeLocker).Name;
		private static Android.OS.PowerManager.WakeLock wakeLock;

		public static void Acquire(Context ctx) {
			if (wakeLock != null) wakeLock.Release();

			PowerManager pm = (PowerManager) ctx.GetSystemService(Context.PowerService);
			wakeLock = pm.NewWakeLock(WakeLockFlags.Full |
				WakeLockFlags.AcquireCausesWakeup |
				WakeLockFlags.OnAfterRelease, TAG);
			wakeLock.Acquire();
		}

		public static void Release() {
			if (wakeLock != null) wakeLock.Release(); wakeLock = null;
		}
	}
}

