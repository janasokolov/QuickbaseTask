using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuickbaseTask
{
    public class Contact
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
