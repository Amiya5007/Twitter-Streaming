using Domain.Common;
using Domain.Dispather;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Twitter
{
    public class TwitterReport
    {
        public int TotalTweets { get; set; }
        public string AverageTweets { get; set; }
        public Dictionary<string, int> Emojis { get; set; }
        public Dictionary<string, int> TopEmojis { get; set; }
        public Dictionary<string, int> HasTags { get; set; }
        public Dictionary<string, int> TopHasTags { get; set; }
        public List<string> Url { get; set; }
        public double UrlPercentage { get; set; }
        public int PhotoUrl { get; set; }
        public double AveragePhotoUrl { get; set; }
        public Dictionary<string, int> Domains { get; set; }
        public Dictionary<string, int> TopDomains { get; set; }
        public int TotalTimeEllapsed { get; set; }
        public Dictionary<string,int> PersionTag { get; set; }
        public Dictionary<string, int> TopPersionTag { get; set; }
        public TwitterReport()
        {
            TotalTweets = 0;
            AverageTweets = "0";
            Emojis = new Dictionary<string, int>();
            TopEmojis = new Dictionary<string, int>();
            HasTags = new Dictionary<string, int>();
            TopHasTags = new Dictionary<string, int>();
            Url = new List<string>();
            UrlPercentage = 0;
            AveragePhotoUrl = 0;
            Domains = new Dictionary<string, int>();
            TopDomains = new Dictionary<string, int>();
            PersionTag = new Dictionary<string, int>();
            TopPersionTag = new Dictionary<string, int>();
            TotalTimeEllapsed = 0;
            PhotoUrl = 0;
        }
    }
}
