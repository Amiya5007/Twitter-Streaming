using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Twitter
{
    public class Tweet
    {
        [JsonProperty("data")]
       public TweetData TweetData { get; set; }
    }
    public class TweetData
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonIgnore]
        public long Timer { get; set; }

    }
}
