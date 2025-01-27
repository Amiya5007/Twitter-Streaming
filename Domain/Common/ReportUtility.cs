﻿using Domain.Dispather;
using Domain.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Domain.Common
{
    public class ReportUtility
    {
        //private readonly string regex = "(?:0\x20E3|1\x20E3|2\x20E3|3\x20E3|4\x20E3|5\x20E3|6\x20E3|7\x20E3|8\x20E3|9\x20E3|#\x20E3|\\*\x20E3|\xD83C(?:\xDDE6\xD83C(?:\xDDE8|\xDDE9|\xDDEA|\xDDEB|\xDDEC|\xDDEE|\xDDF1|\xDDF2|\xDDF4|\xDDF6|\xDDF7|\xDDF8|\xDDF9|\xDDFA|\xDDFC|\xDDFD|\xDDFF)|\xDDE7\xD83C(?:\xDDE6|\xDDE7|\xDDE9|\xDDEA|\xDDEB|\xDDEC|\xDDED|\xDDEE|\xDDEF|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF6|\xDDF7|\xDDF8|\xDDF9|\xDDFB|\xDDFC|\xDDFE|\xDDFF)|\xDDE8\xD83C(?:\xDDE6|\xDDE8|\xDDE9|\xDDEB|\xDDEC|\xDDED|\xDDEE|\xDDF0|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF5|\xDDF7|\xDDFA|\xDDFB|\xDDFC|\xDDFD|\xDDFE|\xDDFF)|\xDDE9\xD83C(?:\xDDEA|\xDDEC|\xDDEF|\xDDF0|\xDDF2|\xDDF4|\xDDFF)|\xDDEA\xD83C(?:\xDDE6|\xDDE8|\xDDEA|\xDDEC|\xDDED|\xDDF7|\xDDF8|\xDDF9|\xDDFA)|\xDDEB\xD83C(?:\xDDEE|\xDDEF|\xDDF0|\xDDF2|\xDDF4|\xDDF7)|\xDDEC\xD83C(?:\xDDE6|\xDDE7|\xDDE9|\xDDEA|\xDDEB|\xDDEC|\xDDED|\xDDEE|\xDDF1|\xDDF2|\xDDF3|\xDDF5|\xDDF6|\xDDF7|\xDDF8|\xDDF9|\xDDFA|\xDDFC|\xDDFE)|\xDDED\xD83C(?:\xDDF0|\xDDF2|\xDDF3|\xDDF7|\xDDF9|\xDDFA)|\xDDEE\xD83C(?:\xDDE8|\xDDE9|\xDDEA|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF6|\xDDF7|\xDDF8|\xDDF9)|\xDDEF\xD83C(?:\xDDEA|\xDDF2|\xDDF4|\xDDF5)|\xDDF0\xD83C(?:\xDDEA|\xDDEC|\xDDED|\xDDEE|\xDDF2|\xDDF3|\xDDF5|\xDDF7|\xDDFC|\xDDFE|\xDDFF)|\xDDF1\xD83C(?:\xDDE6|\xDDE7|\xDDE8|\xDDEE|\xDDF0|\xDDF7|\xDDF8|\xDDF9|\xDDFA|\xDDFB|\xDDFE)|\xDDF2\xD83C(?:\xDDE6|\xDDE8|\xDDE9|\xDDEA|\xDDEB|\xDDEC|\xDDED|\xDDF0|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF5|\xDDF6|\xDDF7|\xDDF8|\xDDF9|\xDDFA|\xDDFB|\xDDFC|\xDDFD|\xDDFE|\xDDFF)|\xDDF3\xD83C(?:\xDDE6|\xDDE8|\xDDEA|\xDDEB|\xDDEC|\xDDEE|\xDDF1|\xDDF4|\xDDF5|\xDDF7|\xDDFA|\xDDFF)|\xDDF4\xD83C\xDDF2|\xDDF5\xD83C(?:\xDDE6|\xDDEA|\xDDEB|\xDDEC|\xDDED|\xDDF0|\xDDF1|\xDDF2|\xDDF3|\xDDF7|\xDDF8|\xDDF9|\xDDFC|\xDDFE)|\xDDF6\xD83C\xDDE6|\xDDF7\xD83C(?:\xDDEA|\xDDF4|\xDDF8|\xDDFA|\xDDFC)|\xDDF8\xD83C(?:\xDDE6|\xDDE7|\xDDE8|\xDDE9|\xDDEA|\xDDEC|\xDDED|\xDDEE|\xDDEF|\xDDF0|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF7|\xDDF8|\xDDF9|\xDDFB|\xDDFD|\xDDFE|\xDDFF)|\xDDF9\xD83C(?:\xDDE6|\xDDE8|\xDDE9|\xDDEB|\xDDEC|\xDDED|\xDDEF|\xDDF0|\xDDF1|\xDDF2|\xDDF3|\xDDF4|\xDDF7|\xDDF9|\xDDFB|\xDDFC|\xDDFF)|\xDDFA\xD83C(?:\xDDE6|\xDDEC|\xDDF2|\xDDF8|\xDDFE|\xDDFF)|\xDDFB\xD83C(?:\xDDE6|\xDDE8|\xDDEA|\xDDEC|\xDDEE|\xDDF3|\xDDFA)|\xDDFC\xD83C(?:\xDDEB|\xDDF8)|\xDDFD\xD83C\xDDF0|\xDDFE\xD83C(?:\xDDEA|\xDDF9)|\xDDFF\xD83C(?:\xDDE6|\xDDF2|\xDDFC)))|[\xA9\xAE\x203C\x2049\x2122\x2139\x2194-\x2199\x21A9\x21AA\x231A\x231B\x2328\x23CF\x23E9-\x23F3\x23F8-\x23FA\x24C2\x25AA\x25AB\x25B6\x25C0\x25FB-\x25FE\x2600-\x2604\x260E\x2611\x2614\x2615\x2618\x261D\x2620\x2622\x2623\x2626\x262A\x262E\x262F\x2638-\x263A\x2648-\x2653\x2660\x2663\x2665\x2666\x2668\x267B\x267F\x2692-\x2694\x2696\x2697\x2699\x269B\x269C\x26A0\x26A1\x26AA\x26AB\x26B0\x26B1\x26BD\x26BE\x26C4\x26C5\x26C8\x26CE\x26CF\x26D1\x26D3\x26D4\x26E9\x26EA\x26F0-\x26F5\x26F7-\x26FA\x26FD\x2702\x2705\x2708-\x270D\x270F\x2712\x2714\x2716\x271D\x2721\x2728\x2733\x2734\x2744\x2747\x274C\x274E\x2753-\x2755\x2757\x2763\x2764\x2795-\x2797\x27A1\x27B0\x27BF\x2934\x2935\x2B05-\x2B07\x2B1B\x2B1C\x2B50\x2B55\x3030\x303D\x3297\x3299]|\xD83C[\xDC04\xDCCF\xDD70\xDD71\xDD7E\xDD7F\xDD8E\xDD91-\xDD9A\xDE01\xDE02\xDE1A\xDE2F\xDE32-\xDE3A\xDE50\xDE51\xDF00-\xDF21\xDF24-\xDF93\xDF96\xDF97\xDF99-\xDF9B\xDF9E-\xDFF0\xDFF3-\xDFF5\xDFF7-\xDFFF]|\xD83D[\xDC00-\xDCFD\xDCFF-\xDD3D\xDD49-\xDD4E\xDD50-\xDD67\xDD6F\xDD70\xDD73-\xDD79\xDD87\xDD8A-\xDD8D\xDD90\xDD95\xDD96\xDDA5\xDDA8\xDDB1\xDDB2\xDDBC\xDDC2-\xDDC4\xDDD1-\xDDD3\xDDDC-\xDDDE\xDDE1\xDDE3\xDDEF\xDDF3\xDDFA-\xDE4F\xDE80-\xDEC5\xDECB-\xDED0\xDEE0-\xDEE5\xDEE9\xDEEB\xDEEC\xDEF0\xDEF3]|\xD83E[\xDD10-\xDD18\xDD80-\xDD84\xDDC0]";
       
        private void GetTotalTweets(TwitterReportRequest twitterReportRequest)
        {
            twitterReportRequest.Report.TotalTweets += twitterReportRequest.SourceData.Count * twitterReportRequest.BatchVolume;
        }
        private void GetAverageTweets(double Time, TwitterReportRequest twitterReportRequest)
        {
            twitterReportRequest.Report.AverageTweets = $"{string.Format("{0:0.00}",(twitterReportRequest.Report.TotalTweets/ TimeSpan.FromMilliseconds(Time).TotalSeconds))}/" +
                $"{string.Format("{0:0.00}", (twitterReportRequest.Report.TotalTweets / TimeSpan.FromMilliseconds(Time).TotalMinutes))}/" +
                $"{string.Format("{0:0.00}", (twitterReportRequest.Report.TotalTweets / TimeSpan.FromMilliseconds(Time).TotalHours))} - (ss/mm/hh)";
        }
        private void GetTopHasTags(TwitterReportRequest twitterReportRequest)
        {
            twitterReportRequest.Report.TopHasTags = twitterReportRequest.Report.HasTags.OrderByDescending(entry => entry.Value)
                     .Take(10)
                     .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        private void GetTopPersonTags(TwitterReportRequest twitterReportRequest)
        {
            twitterReportRequest.Report.TopPersionTag = twitterReportRequest.Report.PersionTag.OrderByDescending(entry => entry.Value)
                     .Take(10)
                     .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        private void GetTopDomains(TwitterReportRequest twitterReportRequest)
        {
            twitterReportRequest.Report.TopDomains = twitterReportRequest.Report.Domains.OrderByDescending(entry => entry.Value)
                     .Take(10)
                     .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        private void GetTopEmojis(TwitterReportRequest twitterReportRequest)
        {
            twitterReportRequest.Report.TopEmojis = twitterReportRequest.Report.Emojis.OrderByDescending(entry => entry.Value)
                     .Take(10)
                     .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        private void GetUrlPercentage(TwitterReportRequest twitterReportRequest)
        {
            twitterReportRequest.Report.UrlPercentage = Math.Round(((double)twitterReportRequest.Report.Url.Count/ (double)twitterReportRequest.Report.TotalTweets)* 100, 2, MidpointRounding.AwayFromZero);
        }
        private void GetPhotoPercentage(TwitterReportRequest twitterReportRequest)
        {
            twitterReportRequest.Report.AveragePhotoUrl = Math.Round(((double)twitterReportRequest.Report.PhotoUrl / (double)twitterReportRequest.Report.TotalTweets) * 100, 2, MidpointRounding.AwayFromZero);
        }
        public void GetAllReports(TwitterReportRequest twitterReportRequest)
        {
            try
            {
                var Photos = new List<string>() { "(pic.twitter.com", "Instagram" };
                 GetTotalTweets(twitterReportRequest);
                foreach (var DataCollection in twitterReportRequest.SourceData.Values)
                {
                    int i = 0;
                    foreach (string data in DataCollection)
                    {
                        if (i == 0)
                        {
                            twitterReportRequest.Report.TotalTimeEllapsed += Convert.ToInt32(data);
                            i++;
                        }
                        else
                        {
                            string[] words = data.Split(' ');
                            var hasTags = Array.FindAll(words, s => s.StartsWith("#"));
                            var personTags = Array.FindAll(words, s => s.StartsWith("@"));
                            twitterReportRequest.Report.Url.AddRange(Array.FindAll(words, s => s.StartsWith("https://")));
                            twitterReportRequest.Report.PhotoUrl += (Array.FindAll(words, s => Photos.Contains(s)).Count());

                            if (personTags != null && personTags.Length > 0)
                            {
                                foreach (string personTag in personTags)
                                {
                                    if (twitterReportRequest.Report.PersionTag.ContainsKey(personTag))
                                    {
                                        twitterReportRequest.Report.PersionTag[personTag]++;
                                    }
                                    else
                                    {
                                        twitterReportRequest.Report.PersionTag.Add(personTag, 1);
                                    }
                                }
                            }
                            if (hasTags != null && hasTags.Length > 0)
                            {
                                foreach (string hasTag in hasTags)
                                {
                                    if (twitterReportRequest.Report.HasTags.ContainsKey(hasTag))
                                    {
                                        twitterReportRequest.Report.HasTags[hasTag]++;
                                    }
                                    else
                                    {
                                        twitterReportRequest.Report.HasTags.Add(hasTag, 1);
                                    }
                                }
                            }
                            if (twitterReportRequest.Report.Url.Count > 0)
                            {
                                foreach (string url in twitterReportRequest.Report.Url)
                                {
                                    if (twitterReportRequest.Report.Domains.ContainsKey(new Uri(url).Host))
                                    {
                                        twitterReportRequest.Report.Domains[new Uri(url).Host]++;
                                    }
                                    else
                                    {
                                        twitterReportRequest.Report.Domains.Add(new Uri(url).Host, 1);
                                    }
                                }
                            }
                            foreach (string word in words)
                            {
                                var chars = word.ToCharArray();
                                for (int k = 0; k < chars.Length; k++)
                                {
                                    if (Regex.Match(chars[k].ToString(), @"\p{Cs}").Success)
                                    {
                                        if (twitterReportRequest.Report.Emojis.ContainsKey(word.Substring(k, 2)))
                                        {
                                            twitterReportRequest.Report.Emojis[word.Substring(k, 2)]++;
                                        }
                                        else
                                        {
                                            twitterReportRequest.Report.Emojis.Add(word.Substring(k, 2), 1);
                                        }


                                        k++;
                                    }
                                }
                            }


                        }

                    }
                }
                 GetAverageTweets(twitterReportRequest.Report.TotalTimeEllapsed, twitterReportRequest);
                 GetTopHasTags(twitterReportRequest);
                 GetUrlPercentage(twitterReportRequest);
                 GetTopDomains(twitterReportRequest);
                 GetPhotoPercentage(twitterReportRequest);
                 GetTopEmojis(twitterReportRequest);
                 GetTopPersonTags(twitterReportRequest);
            }
            catch(Exception ex)
            {
                twitterReportRequest.ProcessStatus = new Response() { Status = Common.ProcessState.Fail, StatusMessage = ex.InnerException.ToString() };
            }
        }
    }
}

