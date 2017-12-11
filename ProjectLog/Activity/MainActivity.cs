using Android.App;
using Android.Widget;
using Android.OS;
using Firebase;
using Firebase.Auth;
using System;
using Android.Support.V7.App;
using static Android.Views.View;
using Android.Views;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;


namespace ProjectLog
{
    [Activity(Label = "ProjectLog", MainLauncher = true, Theme ="@style/AppTheme")]
    public class MainActivity : AppCompatActivity, IOnClickListener,IOnCompleteListener
    {
        Button btnLogin;
        EditText input_email, input_password;
        TextView btnSignUp, btnForgotPassword;

        RelativeLayout activity_main;

        public static FirebaseApp app;  //we define 'static' becuase we will to access this variable from another class
        FirebaseAuth auth;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);


            //init firebase
            iniFirebaseAuth();

            //view
            btnLogin = FindViewById<Button>(Resource.Id.login_btn_login);
            input_email = FindViewById<EditText>(Resource.Id.login_email);
            input_password = FindViewById<EditText>(Resource.Id.login_password);
            btnSignUp = FindViewById<TextView>(Resource.Id.login_btn_sign_up);
            btnForgotPassword = FindViewById<TextView>(Resource.Id.login_btn_forgot_password);
            activity_main = FindViewById<RelativeLayout>(Resource.Id.activity_main);

            btnSignUp.SetOnClickListener(this);
            btnLogin.SetOnClickListener(this);
            btnForgotPassword.SetOnClickListener(this);

        }

        private void iniFirebaseAuth()
        {
            var options = new FirebaseOptions.Builder()
                 .SetApplicationId("1:137956596509:android:14ddaff5651cee0d")  //found on firebase , click on menu icon on the project you created >> Settings
                 .SetApiKey("AIzaSyC4FsarLSH3M-6OplN9miinND8hqRP1www")  // found on com.google.firebase.json, download the file on firebase and open it, Scroll down to  API key
                 .Build();

            if(app == null)
            {
                app = FirebaseApp.InitializeApp(this, options);
                auth = FirebaseAuth.GetInstance(app);
            }
        }

        //Setting all buttons if clicked must show something e.g login_btn_forgot_password will show ForgotPassword UI
        public void OnClick(View v)
        {
          if(v.Id == Resource.Id.login_btn_forgot_password)
            {
                StartActivity(new Android.Content.Intent(this, typeof(ForgotPassword)));
                Finish();
            }
          else if(v.Id == Resource.Id.login_btn_sign_up)
            {
                StartActivity(new Android.Content.Intent(this, typeof(SignUp)));
                Finish();
            }
          else if(v.Id == Resource.Id.login_btn_login)
            {
                LoginUser(input_email.Text, input_password.Text);
            }
        }


        private void LoginUser(string email, string password)
        {
            //using signing in  with email and password method
            auth.SignInWithEmailAndPassword(email, password) //error is here when I run it: System.NullReferenceException: <Timeout exceeded getting exception details>

            .AddOnCompleteListener(this);
        }

        public void OnComplete(Task task)
        {
            //implementing the on complete listener
            if(task.IsSuccessful)
            {
                //if login is succesful, we will nevigate to dashboard
                StartActivity(new Android.Content.Intent(this, typeof(Dashboard)));
                Finish();
            }
            else
            {
                Snackbar snack = Snackbar.Make(activity_main, "Login failed", Snackbar.LengthLong);
                snack.Show();
            }
        }

    }
}

