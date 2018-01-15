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

namespace NBP_Neo4j_Redis.NecessaryClasses
{
    public class JavaObject<T> : Java.Lang.Object
    {
        public JavaObject(T obj)
        {
            this.Value = obj;
        }
        public T Value { get; set; }

    }
}