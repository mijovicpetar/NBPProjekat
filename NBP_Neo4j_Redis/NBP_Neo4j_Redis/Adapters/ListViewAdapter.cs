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
using NBP_Neo4j_Redis.Activities;
namespace NBP_Neo4j_Redis.Adapters
{
    class ListViewAdapter : BaseAdapter
    {
        #region Atributes
        private List<MessageContent> _listAdapter;
        private ChatActivity _chatActivity;
        #endregion

        #region Constructors
        public ListViewAdapter(ChatActivity chatActivity, List<MessageContent> list)
        {
            _listAdapter = list;
            _chatActivity = chatActivity;
        }
        #endregion

        #region Overrides
        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)_chatActivity.BaseContext.GetSystemService(Context.LayoutInflaterService);
            View itemView = inflater.Inflate(Resource.Layout.List_item, null);

            TextView message_user, message_time, message_content;
            message_user = itemView.FindViewById<TextView>(Resource.Id.message_user);
            message_content = itemView.FindViewById<TextView>(Resource.Id.message_text);
            message_time = itemView.FindViewById<TextView>(Resource.Id.message_time);

            message_user.Text = _listAdapter[position].Name;
            message_content.Text = _listAdapter[position].Message;
            message_time.Text = _listAdapter[position].Time;

            return itemView;
        }        
        public override int Count
        {
            get
            {
                return _listAdapter.Count;
            }
        }
        #endregion
    }

    class ListViewAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}