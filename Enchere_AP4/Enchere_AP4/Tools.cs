using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using System.Linq;

namespace Enchere_AP4
{
    public static class Tools
    {
        //modifier seulement l'adresse avec une des deux dans la region ci-dessous
        private static string BaseApiAddress => "http://80.13.113.244:2081/";


        #region NE PAS TOUCHER SEULEMENT CHANGER L'ADRESSE NON COMMENTEE CI-DESSUS
        //réseau interne
        //private static string BaseApiAddress => "http://172.17.0.61:8000/";

        //externe
        //private static string BaseApiPublicAddress => "http://80.13.113.244:2081/";

        #endregion

        private static HttpClient _clientHttp = new HttpClient();



        public static async Task<ObservableCollection<T>> GetAllAsync<T>(string paramUrl,List<T>param)
        {
            try
            {
                
                var json = await _clientHttp.GetStringAsync(BaseApiAddress+paramUrl);
                return JsonConvert.DeserializeObject<ObservableCollection<T>>(json, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm:ss" });

            }
            catch (Exception ex)
            {
                ShowException(ex);
                return null;
            }
        }



        public static async Task<ObservableCollection<T>> GetAllAsync<T>(string paramUrl)
        {
            try
            {
                
                var json = await _clientHttp.GetStringAsync(BaseApiAddress + paramUrl);
                return JsonConvert.DeserializeObject<ObservableCollection<T>>(json);

            }
            catch (Exception ex)
            {
                ShowException(ex);
                return null;
            }
        }



        public static void ShowException(Exception e)
        {
            DependencyService.Get<IMessage>().LongAlert(e.Message);
        }



        public static async Task<T> GetOneAsync<T>(string paramUrl,int paramId)
        {
            try
            {
                string jsonString = @"{'Id':'" + paramId + "'}";
                var getResult = JObject.Parse(jsonString);

                var jsonContent = new StringContent(getResult.ToString(), Encoding.UTF8,"application/json");

                var response = await _clientHttp.PostAsync(BaseApiAddress + paramUrl, jsonContent);
                if(response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(json);
                }
                else
                {
                    throw new Exception("Impossible de récupérer les données : " + response.StatusCode.ToString() + ":" + response.ReasonPhrase.ToString());
                }
                
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return default(T);
            }
        }

        public static async Task<T> GetOneAsync<T>(string paramUrl, string paramName, int paramId)
        {
            try
            {
                string jsonString = @"{'"+paramName+"':'" + paramId + "'}";
                var getResult = JObject.Parse(jsonString);

                var jsonContent = new StringContent(getResult.ToString(), Encoding.UTF8, "application/json");

                var response = await _clientHttp.PostAsync(BaseApiAddress + paramUrl, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(json);
                }
                else
                {
                    throw new Exception("Impossible de récupérer les données : " + response.StatusCode.ToString() + ":" + response.ReasonPhrase.ToString());
                }

            }
            catch (Exception ex)
            {
                ShowException(ex);
                return default(T);
            }
        }

        /// <summary>
        ///  retourne un booléeen
        /// </summary>
        /// <param name="paramUrl"></param>
        /// <param name="DicoParam"></param>
        /// <returns></returns>
        public static async Task<bool> PostAsyncMultyParam(string paramUrl, Dictionary<string, string> DicoParam)
        {
            try
            {
                string jsonString = @"{";
                foreach(KeyValuePair<string, string> kvp in DicoParam)
                {
                    jsonString += "'" + kvp.Key + "':'" + kvp.Value + "',";
                }
                jsonString += "}"; 
                var getResult = JObject.Parse(jsonString);

                var jsonContent = new StringContent(getResult.ToString(), Encoding.UTF8, "application/json");
                var response = await _clientHttp.PostAsync(BaseApiAddress + paramUrl, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Impossible d'insérer les données : " + response.StatusCode.ToString() + ":" + response.ReasonPhrase.ToString());
                }

            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        /// <summary>
        /// retourne l'objet spécifié
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramUrl"></param>
        /// <param name="DicoParam"></param>
        /// <returns></returns>
        public static async Task<T> PostAsyncMultyParam<T>(string paramUrl, Dictionary<string, string> DicoParam)
        {
            try
            {
                string jsonString = @"{";
                foreach (KeyValuePair<string, string> kvp in DicoParam)
                {
                    jsonString += "'" + kvp.Key + "':'" + kvp.Value + "',";
                }
                jsonString += "}";
                var getResult = JObject.Parse(jsonString);

                var jsonContent = new StringContent(getResult.ToString(), Encoding.UTF8, "application/json");
                var response = await _clientHttp.PostAsync(BaseApiAddress + paramUrl, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(json);
                }
                else
                {
                    throw new Exception("Impossible d'insérer les données : " + response.StatusCode.ToString() + ":" + response.ReasonPhrase.ToString());
                }

            }
            catch (Exception ex)
            {
                ShowException(ex);
                return default(T);
            }
        }

        public static int ExistById<T>(ObservableCollection<T> param, int id)
        {
            if(HasProperty<T>())
            {
                return ConvertToList(param).FindIndex(x => (int)x.GetType().GetProperty("Id").GetValue(x) == id);
            }

            return -1;
        }

        private static bool HasProperty<T>() 
        {
            if (typeof(T).GetProperty("Id") != null)
                return true;
            else
                return false;
        }

        public static List<T> ConvertToList<T>(ObservableCollection<T> param)
        {
            List<T> list = new List<T>();
            list.AddRange(param);
            return list;
            
        }


        public static async Task<Position> GetPosition()
        {

            try
            {
                Location location = new Location();
                location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    return new Position(location.Latitude, location.Longitude);
                }

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                ShowLongToast("Handle not supported on device exception");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                ShowLongToast("Handle not enabled on device exception");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                ShowLongToast("Handle permission exception");
            }
            catch (Exception ex)
            {
                // Unable to get location
                ShowLongToast("Unable to get location");
            }
            
            return default(Position);
            

        }

        public static void ShowLongToast(string param)
        {
            DependencyService.Get<IMessage>().LongAlert(param);

        }
        
        public static void ShowShortToast(string param)
        {
            DependencyService.Get<IMessage>().ShortAlert(param);
        }

        public static T GetElementById<T,Y>(Y param,int idToSearch) where Y : IList<T>
        {
            return param[GetIndex<T, Y>(idToSearch, param.Count(), 0, param)-1];
        }

        private static int GetIndex<T,Y>(int id, int max, int min, Y param) where Y : IList<T>
        {
            int res;
            int middle = (max + min) / 2;
            
            if (id != (int)param[middle - 1].GetType().GetProperty("Id").GetValue(param[middle - 1]))
            {
                if (id > (int)param[middle - 1].GetType().GetProperty("Id").GetValue(param[middle - 1]))
                {
                    min = middle;
                    middle += (max - middle) / 2;

                }
                else
                {
                    max = middle;
                    middle -= (min + middle) / 2;

                }
            }


            res = middle;

            if (id != (int)param[middle - 1].GetType().GetProperty("Id").GetValue(param[middle - 1]))
                res = GetIndex<T,Y>(id, max, min, param);


            return res;

        }


        
    }
}
