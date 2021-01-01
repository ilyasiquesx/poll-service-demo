using Newtonsoft.Json;

namespace Service.ViewModels.Votes
{
    public class VotePoint
    {
        public VotePoint(int count, string option)
        {
            Count = count;
            Option = option;
            LegendText = option;
        }

        [JsonProperty("y")] 
        public int Count;

        [JsonProperty("label")] 
        public string Option;

        [JsonProperty("legendText")] 
        public string LegendText;
    }
}