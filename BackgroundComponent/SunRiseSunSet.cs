using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

    namespace Dynamic_Mojave
{
    public sealed class SunRiseSunSet
    {

        public async static Task<RootObject> GetDayTime(double lat, double lon)
        {
            var http = new HttpClient();
            var url = String.Format("https://api.sunrise-sunset.org/json?lat={0}&lng={1}&date=today");
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject)serializer.ReadObject(ms);

            return data;
        }

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            throw new NotImplementedException();
            IBackgroundTrigger trigger = new TimeTrigger(720, false);
        }

    }
    


    [DataContract]

    public class Results
    {
        [DataMember]
        public string sunrise { get; set; }

        [DataMember]
        public string sunset { get; set; }

        [DataMember]
        public string solar_noon { get; set; }

        [DataMember]
        public string day_length { get; set; }

        [DataMember]
        public string civil_twilight_begin { get; set; }

        [DataMember]
        public string civil_twilight_end { get; set; }

        [DataMember]
        public string nautical_twilight_begin { get; set; }

        [DataMember]
        public string nautical_twilight_end { get; set; }

        [DataMember]
        public string astronomical_twilight_begin { get; set; }

        [DataMember]
        public string astronomical_twilight_end { get; set; }
        
    }


    [DataContract]


    public class RootObject
    {
        public Results results { get; set; }
        public string status { get; set; }
    }

}
