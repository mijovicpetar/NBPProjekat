using Android.App;
using Android.Widget;
using Android.OS;

namespace NBP_Neo4j_Redis
{
    [Activity(Label = "NBP_Neo4j_Redis")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

