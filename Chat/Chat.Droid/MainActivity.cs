﻿using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Chat.Droid;
namespace Chat.Droid
{
	[Activity (Label = "Chat.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
	

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            Init();
            
        }

        public static void Init()
        {
            NetWorking.Init();
            
        }
	}
}


