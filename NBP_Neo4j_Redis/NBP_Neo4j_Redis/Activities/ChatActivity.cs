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
using NBP_Neo4j_Redis.Adapters;
using Refractored.Fab;

namespace NBP_Neo4j_Redis.Activities
{
    [Activity(Theme = "@android:style/Theme.NoTitleBar", Icon = "@drawable/user", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ChatActivity : Activity
    {
        #region Atributes
        private List<MessageContent> _listMessage = new List<MessageContent>();
        #endregion

        #region Controls
        private ListView _listChat;
        private EditText _editChat;
        private FloatingActionButton _button;
        #endregion

        #region Overrides
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Chat);

            _button = FindViewById<FloatingActionButton>(Resource.Id.fab);
            _editChat = FindViewById<EditText>(Resource.Id.input);
            _listChat = FindViewById<ListView>(Resource.Id.list_of_messages);

            _button.Click += delegate
            {
                PostMessage();
            };


            OsposobiAdapter();

        }
        #endregion

        #region Methodes
        private void OsposobiAdapter()
        {
            bool send = false;
            for (int i = 0; i < 20; i++)
            {
                MessageContent message = new MessageContent();
                int broj = i + 1;
                message.Message = "Probna poruka " + broj;
                message.Name = "Ime " + broj;
                message.Time = DateTime.Now.ToShortTimeString();
                message.IsSend = send;
                send = !send;
                _listMessage.Add(message);
            }

            ListViewAdapter adapter = new ListViewAdapter(this, _listMessage);
            _listChat.Adapter = adapter;
        }
        private void PostMessage()
        {
            //kod za slanje poruke
        }
        #endregion
    }
}