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
using CombinedAPI.Entities;
using CombinedAPI;
using NBP_Neo4j_Redis.Activities;

namespace NBP_Neo4j_Redis.Controllers
{
    public class ViewController
    {
        Activity context;
        static ViewController _instance;

        ListView lista_profila;

        public static ViewController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ViewController();
                return _instance;
            }
        }

        public Activity Context
        {
            get { return context; }
            set { context = value; }
        }

        public ListView ListaProfila
        {
            get { return lista_profila; }
            set { lista_profila = value; }
        }

        public void RenderujProfile()
        {
            if (DataController.Instance.Profili.Count == 0)
                DataController.Instance.Profili = DataController.Instance.PreuzmiAktivneProfile();
            if (DataController.Instance.Profili != null)
            {
                lista_profila.Adapter = new UsersAdapter(context, DataController.Instance.Profili);
                //try { lista_profila.ItemClick -= ListaProfila_ItemClick; }
                //finally { lista_profila.ItemClick += ListaProfila_ItemClick; }
            }
            else
            {
                lista_profila.Adapter = new UsersAdapter(context, new List<string>());
            }
        }

        //private void ListaProfila_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //    string[] podaci = e.View.FindViewById<TextView>(Resource.Id.user).Text.Split(' ');
        //    DataController.Instance.KorisnickoOdabranogProfila = podaci[0];
        //    int index_odabranog_profila;

        //    DataController.Instance.OdabraniProfil = DataController.Instance.VratiOdabraniProfil();
        //    string profil = DataController.Instance.PronadjiProfilLokalno(DataController.Instance.OdabraniProfil.KorisnickoIme, out index_odabranog_profila);
        //    DataController.Instance.IndexOdabranogProfila = index_odabranog_profila;

        //    context.StartActivity(typeof(ProfileActivity));
        //}
    }
}