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

namespace NBP_Neo4j_Redis
{
    public class MessageContent
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }
        public bool IsSend { get; set; }
        public MessageContent() { }

        public MessageContent(string name, string Message, bool send)
        {
            this.Name = name;
            this.Message = Message;
            this.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            IsSend = send;
        }
    }
}