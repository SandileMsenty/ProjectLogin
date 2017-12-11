using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Firebase.Auth;
using static Android.Views.View;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;
using Java.Lang;

namespace ProjectLog
{
    [Activity(Label = "Dashboard", Theme = "@style/AppTheme")]
    public class Dashboard : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        TextView txtWelcome;
        EditText input_new_password;
        Button btnChangePass, btnLogout;
        RelativeLayout acitivity_dashboard;

        FirebaseAuth auth;

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.dashboard_btn_change_pass)
                changePassword(input_new_password.Text);
            else if (v.Id == Resource.Id.dashboard_btn_logout)
                logoutUser();
        }

        private void logoutUser()
        {
            //using method sign out
            auth.SignOut();

            if(auth.CurrentUser == null) //check if session is clear then navigation login pane
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
                Finish();
            }
        }

        private void changePassword(string newPassword)
        {
            FirebaseUser user = auth.CurrentUser;
            //using the update method

            user.UpdatePassword(newPassword) 
                .AddOnCompleteListener(this);

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Dashboard);

            //init firebase
            auth = FirebaseAuth.GetInstance(MainActivity.app);

            //view
            txtWelcome = FindViewById<TextView>(Resource.Id.dashboard_welcome);
            input_new_password = FindViewById<EditText>(Resource.Id.dashboard_newpassword);
            btnChangePass = FindViewById<Button>(Resource.Id.dashboard_btn_change_pass);
            btnLogout = FindViewById<Button>(Resource.Id.dashboard_btn_logout);
            acitivity_dashboard = FindViewById<RelativeLayout>(Resource.Id.activity_dashboard);

            btnChangePass.SetOnClickListener(this);
            btnLogout.SetOnClickListener(this);

            //check session
            if(auth.CurrentUser !=null)
            {
                txtWelcome.Text = "Welcome ," + auth.CurrentUser.Email;
            }
        }

        public void OnComplete(Task task)
        {
            if(task.IsSuccessful == true)
            {
                Snackbar.Make(acitivity_dashboard, "Password changed", Snackbar.LengthLong)
                    .Show();
            }
        }
    }
}