using System;
using Newtonsoft.Json;

namespace zob.api.Chain
{
    public class Block
    {
        [JsonProperty(Order = 1)]
        public int Index;
        
        [JsonProperty(Order = 2)]
        public DateTime Timestamp;
        
        [JsonProperty(Order = 3)]
        public int Proof;
        
        [JsonProperty(Order = 4)]
        public string PreviousHash;
        
        [JsonProperty(Order = 5)]
        public string Data;
    }
}