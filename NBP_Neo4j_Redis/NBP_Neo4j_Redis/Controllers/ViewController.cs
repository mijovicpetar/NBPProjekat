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
        #region Atributes
        Activity context;
        static ViewController _instance;
        ListView lista_profila;

        #endregion

        ListView lista_profila_lajkovi;


        #region Properties
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
        #endregion


        #region Methodes

        public ListView ListaProfilaLajkovi
        {
            get { return lista_profila_lajkovi; }
            set { lista_profila_lajkovi = value; }
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

        #endregion


        public void RenederujProfileLajkovi()
        {
            if (DataController.Instance.ProfiliLajkovi.Count == 0)
                DataController.Instance.ProfiliLajkovi = DataController.Instance.PreuzmiProfileKojiSuLajkovaliSliku();
            if (DataController.Instance.ProfiliLajkovi != null)
                lista_profila_lajkovi.Adapter = new ImageAdapter(context, DataController.Instance.ProfiliLajkovi);
            else
                lista_profila_lajkovi.Adapter = new ImageAdapter(context, new List<string>());
        }

    }
}