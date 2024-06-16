using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuickbaseTask
{
    public class CreateContactInput
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }

        [JsonPropertyName("twitter_id")]
        public string TwitterId { get; set; }

        [JsonPropertyName("unique_external_id")]
        public string UniqueExternalId { get; set; }

    }
}
