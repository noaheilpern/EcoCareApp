using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;
using EcoCareApp;
using EcoCareApp.Models;
using System.Linq;
using System.Collections.ObjectModel;

namespace EcoCareApp.Services
{
    class EcoCareAPIProxy
    {
        //change this to my url
        private const string CLOUD_URL = "TBD"; //API url when going on the cloud
        private const string CLOUD_PHOTOS_URL = "TBD";
        private const string DEV_ANDROID_EMULATOR_URL = "http://10.0.2.2:60653/EcoCareAPI"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_URL = "http://10.58.55.25:60653/EcoCareAPI"; //API url when using physucal device on android
        private const string DEV_WINDOWS_URL = "http://localhost:60653/EcoCareAPI"; //API url when using windoes on development
        private const string DEV_ANDROID_EMULATOR_PHOTOS_URL = "http://10.0.2.2:5000/Images/"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_PHOTOS_URL = "http://10.58.55.25:5000/Images/"; //API url when using physucal device on android
        private const string DEV_WINDOWS_PHOTOS_URL = "https://localhost:5001/Images/"; //API url when using windoes on development

        private HttpClient client;
        private string baseUri;
        private string basePhotosUri;
        private static EcoCareAPIProxy proxy = null;

        public static EcoCareAPIProxy CreateProxy()
        {
            string baseUri;
            string basePhotosUri;
            if (App.IsDevEnv)
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    if (DeviceInfo.DeviceType == DeviceType.Virtual)
                    {
                        baseUri = DEV_ANDROID_EMULATOR_URL;
                        basePhotosUri = DEV_ANDROID_EMULATOR_PHOTOS_URL;
                    }
                    else
                    {
                        baseUri = DEV_ANDROID_PHYSICAL_URL;
                        basePhotosUri = DEV_ANDROID_PHYSICAL_PHOTOS_URL;
                    }
                }
                else
                {
                    baseUri = DEV_WINDOWS_URL;
                    basePhotosUri = DEV_WINDOWS_PHOTOS_URL;
                }
            }
            else
            {
                baseUri = CLOUD_URL;
                basePhotosUri = CLOUD_PHOTOS_URL;
            }

            if (proxy == null)
                proxy = new EcoCareAPIProxy(baseUri, basePhotosUri);
            return proxy;
        }

        private EcoCareAPIProxy(string baseUri, string basePhotosUri)
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            //Create client with the handler!
            this.client = new HttpClient(handler, true);
            this.baseUri = baseUri;
            this.basePhotosUri = basePhotosUri;
        }
        public async Task<string> TestAsync()
        {
            try
            {
                string s = $"{this.baseUri}/Test";
                //System.Net.ServicePointManager.SecurityProtocol =
                //    System.Net.SecurityProtocolType.Tls12 |
                //    System.Net.SecurityProtocolType.Tls11 |
                //    System.Net.SecurityProtocolType.Tls;
                HttpResponseMessage response = await this.client.GetAsync(s);
                if (response.IsSuccessStatusCode)
                {
                    //JsonSerializerOptions options = new JsonSerializerOptions
                    //{
                    //    ReferenceHandler = ReferenceHandler.Preserve,
                    //    PropertyNameCaseInsensitive = true
                    //};
                    string content = await response.Content.ReadAsStringAsync();
                    //string eString = JsonSerializer.Deserialize<string>(content, options);
                    return content;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetProducts");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    List<Product> Products = JsonSerializer.Deserialize<List<Product>>(content, options);

                    return Products;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return null;
            }
        }
        public async Task<List<Country>> GetCountriesAsync()
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetCountries");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    List<Country> l = JsonSerializer.Deserialize<List<Country>>(content, options);
                    l.OrderBy(c => c.CountryName);                     
               
                    return l;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return null;
            }
        }

        public async Task<RegularUser> GetRegularUserDataAsync(string userName)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetRegularUserData?userName={userName}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    RegularUser ru = JsonSerializer.Deserialize<RegularUser>(content, options);


                    return ru;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return null;
            }
        }

        public async Task<Seller> GetSellerDataAsync(string userName)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetSellerData?userName={userName}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    Seller s = JsonSerializer.Deserialize<Seller>(content, options);


                    return s;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return null;
            }
        }


        public async Task<User> GetUserDataAsync(string userName)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetUserData?userName={userName}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    User u = JsonSerializer.Deserialize<User>(content, options);
                    

                    return u;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return null;
            }
        }
        public async Task<bool> IsUserNameExistAsync(string userName)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/IsUserNameExist?username={userName}");

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };
                string content = await response.Content.ReadAsStringAsync();
                bool b = JsonSerializer.Deserialize<bool>(content, options);

                if (response.IsSuccessStatusCode)
                {
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return false;
            }
        }

        public async Task<bool> IsRegularUserAsync(string userName)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/IsRegularUser?userName={userName}");
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };
                string content = await response.Content.ReadAsStringAsync();
                bool b = JsonSerializer.Deserialize<bool>(content, options);

                if (response.IsSuccessStatusCode)
                {
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return false;
            }
        }


 
        public async Task<bool> IsEmailExistAsync(string email)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/IsEmailExist?email={email}");

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };
                string content = await response.Content.ReadAsStringAsync();
                bool b = JsonSerializer.Deserialize<bool>(content, options);

                if (response.IsSuccessStatusCode)
                {
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return false;
            }
        }


        public string GetBasePhotoUri() { return this.basePhotosUri; }

        //Login!
        public async Task<User> LoginAsync(string email, string pass)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/Login?email={email}&pass={pass}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    User u = JsonSerializer.Deserialize<User>(content, options);
                    return u;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool> DeleteItemAsync(Product p)
        {
            try
            {

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };

                string json = JsonSerializer.Serialize(p, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/DeleteItem", content);
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool succeed = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    if (succeed)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        
        }

        public async Task<bool> UpdateProductAsync(Product p)
        {

            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };

                string json = JsonSerializer.Serialize(p, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/UpdateProduct", content);
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool succeed = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    if (succeed)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public async Task<bool> UpdateUserAsync(RegularUser ru)
        {
            

            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };

                string json = JsonSerializer.Serialize(ru, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/UpdateUser", content);
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool succeed = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    if (succeed)
                    {
                        App a = (App)App.Current;
                        a.CurrentRegularUser = ru;
                        a.CurrentUser = ru.UserNameNavigation;
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateSellerAsync(Seller s)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };

                string json = JsonSerializer.Serialize<Seller>(s, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/UpdateSeller", content);
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool succeed = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    if (succeed)
                    {
                        App a = (App)App.Current;
                        a.CurrentSeller = s;
                        a.CurrentUser = s.UserNameNavigation;
                        return true;
                    }
                    else
                        return false;
                 
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> IsDataExist(string category)
        {
            int id = await GetCategory(category);
            App app = (App)App.Current;
            string userName = app.CurrentUser.UserName;
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/IsDataExist?categoryId={id}&&userName={userName}");

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };
                string content = await response.Content.ReadAsStringAsync();
                bool exist = JsonSerializer.Deserialize<bool>(content, options);

                if (response.IsSuccessStatusCode)
                {
                    return exist;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return false;
            }
        }
        public async Task<int> GetCategory(string category)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetCategoryId?category={category}");

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };
                string content = await response.Content.ReadAsStringAsync();
                int id = JsonSerializer.Deserialize<int>(content, options);

                if (response.IsSuccessStatusCode)
                {
                    return id;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return 0;
            }
        }
        public async Task<bool> AddData(double value, string category)
        {
            try
            {
                int id = await GetCategory(category);
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };
                App a = (App)App.Current;
                User u = a.CurrentUser;
                UserData data = new UserData
                {
                    CategoryId = id,
                    CategoryValue = value,
                    DateT = DateTime.Today,
                    UserName = u.UserName,
                };

                string json = JsonSerializer.Serialize(data, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/AddData",content);
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }
        //This method register a new user into the server database. A previous login is NOT required! The nick name and email must be uniqe!
        //it returns true is succeeded or false otherwise
        //questions are ignored upon registering a user and shoul dbe added seperatly.
        //if succeeded - the user is automatically logged in on the server
        public async Task<RegularUser> RegisterRegularUser(RegularUser u)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };
                
                string json = JsonSerializer.Serialize<RegularUser>(u, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/RegisterUser", content);
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    RegularUser b = JsonSerializer.Deserialize<RegularUser>(jsonContent, options);
                    return b;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<Seller> RegisterBusinessOwner(Seller u)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };
                string json = JsonSerializer.Serialize<Seller>(u, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/RegisterBusinessOwner", content);
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    Seller b = JsonSerializer.Deserialize<Seller>(jsonContent, options);
                    return b;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}
