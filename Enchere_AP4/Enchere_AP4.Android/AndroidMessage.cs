using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Enchere_AP4.Droid;
using System;


[assembly: Xamarin.Forms.Dependency(typeof(AndroidMessage))]
namespace Enchere_AP4.Droid
{
    public class AndroidMessage : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}