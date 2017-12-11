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

namespace ProjectLog
{
    [Activity(Label = "SignUp", Theme = "@style/AppTheme")]
    public class SignUp : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        Button btnSignUp;
        TextView btnLogin, btnForgotPass;
        EditText input_email, input_password;
        RelativeLayout activtiy_sign_up;

        FirebaseAuth auth;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SignUp);

            //init firebase
            //we will need variable 'mainActivity.app' for ini firebase
            auth = FirebaseAuth.GetInstance(MainActivity.app);

            //view
            btnSignUp = FindViewById<Button>(Resource.Id.signup_btn_register);
            btnLogin = FindViewById<TextView>(Resource.Id.signup_btn_login);
            btnForgotPass = FindViewById<TextView>(Resource.Id.signup_btn_forgot_password);
            input_email = FindViewById<EditText>(Resource.Id.signup_email);
            input_password = FindViewById<EditText>(Resource.Id.signup_password);
            activtiy_sign_up = FindViewById<RelativeLayout>(Resource.Id.activity_Sign_Up);

            btnLogin.SetOnClickListener(this);
            btnForgotPass.SetOnClickListener(this);
            btnSignUp.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.signup_btn_login)
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
                Finish();
            }
            else if (v.Id == Resource.Id.signup_btn_forgot_password)
            {
                StartActivity(new Intent(this, typeof(ForgotPassword)));
                Finish();
            }
            else if (v.Id == Resource.Id.signup_btn_register)
            {
                SignUpUser(input_email.Text, input_password.Text);
            }
        }

        private void SignUpUser(string email, string password)
        {
            //use createUserWithEmailAndPassword method
            auth.CreateUserWithEmailAndPassword(email, password)
                .AddOnCompleteListener(this, this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful == true)
            {
                Snackbar snack = Snackbar.Make(activtiy_sign_up, "Succesfully registered", Snackbar.LengthLong);
                snack.Show();
            }
            else
            {
                Snackbar snack = Snackbar.Make(activtiy_sign_up, "Registration failed", duration: Snackbar.LengthLong);
               snack.Show();
            }
        }

    }
}