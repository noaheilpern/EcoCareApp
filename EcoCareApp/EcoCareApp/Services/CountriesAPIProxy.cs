using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;
using EcoCareApp;
using EcoCareApp.Models;

namespace EcoCareApp.Services
{
    class CountriesAPIProxy
    {
        private const string CLOUD_URL = "TBD"; //API url when going on the cloud
        private HttpClient client;
        private string baseUri;
        private string basePhotosUri;
        private static CountriesAPIProxy proxy = null;

        public static CountriesAPIProxy CreateProxy()
        {
            string baseUri;
            baseUri = "https://api.first.org/data/v1";
            if (proxy == null)
                proxy = new CountriesAPIProxy(baseUri);
            return proxy;
        }


        private CountriesAPIProxy(string baseUri)
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            //Create client with the handler!
            this.client = new HttpClient(handler, true);
            this.baseUri = baseUri;
        }
        //public async List<Country> GetCountries()
        //{
        //    baseUri += "/country";
        //}
    }
}
