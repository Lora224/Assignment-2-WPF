using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assignment_2_WPF.Ultilities
{

    public class WeatherResponse
    {
        [JsonPropertyName("current")]
        public Current Current { get; set; }
    }

    public class Current
    {
        [JsonPropertyName("temp_c")]
        public double Temperature { get; set; }

        [JsonPropertyName("condition")]
        public Condition Condition { get; set; }
    }

    public class Condition
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }



}
 
