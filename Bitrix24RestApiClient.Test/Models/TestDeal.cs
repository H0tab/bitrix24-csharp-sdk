using Newtonsoft.Json;

namespace Bitrix24RestApiClient.Test.Models
{
    public class TestDeal
    {

        [JsonProperty("id")]
        public int? Id { get; set; }
    }
}
