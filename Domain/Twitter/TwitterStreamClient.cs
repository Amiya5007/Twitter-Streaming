using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Domain.Common;
using Domain.Twitter;
using Domain.Events.EventArguments;



namespace Domain.Twitter
{
    public class TwitterStreamClient
    {

        private HttpWebRequest _webRequest;

        public Common.Common.StreamState StreamState;

        public event TweetReceivedHandler TweetReceivedEvent;

        public delegate void TweetReceivedHandler(TwitterStreamClient s, TweetEventArgs e);

        public event TwitterExceptionHandler ExceptionReceived;

        public delegate void TwitterExceptionHandler(TwitterStreamClient s, TwitterExceptionEventArgs e);
        //private Stopwatch stopwatch = new Stopwatch();
        public async Task<Response> Start(string ApiKey, string ApiUrl)
        {

           // var x = JsonConvert.SerializeObject(new TwitterReport()).ToString();
            int wait = 250;
            try
            {
                _webRequest = (HttpWebRequest)WebRequest.Create(ApiUrl);
                _webRequest.Headers.Add("Authorization", "Bearer " + ApiKey);
                _webRequest.Method = "GET";
                _webRequest.ContentType = "application/x-www-form-urlencoded";
                Encoding encode = Encoding.GetEncoding("utf-8");
                StreamState = Common.Common.StreamState.Running;
                
                using (var response = await _webRequest.GetResponseAsync())
                {
                    using (var reader = new StreamReader(response.GetResponseStream(), encode))
                    {
                        //stopwatch.Start();
                        while (!reader.EndOfStream)
                        {
                            try
                            {
                                if (reader.ReadLine() != null && !string.IsNullOrEmpty(reader.ReadLine().ToString()) && !string.IsNullOrWhiteSpace(reader.ReadLine().ToString()))
                                {
                                    var jsonObj = JsonConvert.DeserializeObject<Tweet>(reader.ReadLine(),
                                        new JsonSerializerSettings()
                                        {
                                            NullValueHandling = NullValueHandling.Ignore,
                                            StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                                            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                                           

                                        });
                                         
                                    //jsonObj.TweetData.Timer = stopwatch.ElapsedTicks;
                                    Raise(TweetReceivedEvent, new TweetEventArgs(jsonObj == null ? new TweetData() : jsonObj.TweetData));
                                }
                            }
                            catch (JsonSerializationException jsonSerializerEx)
                            {
                                return new Response()
                                {
                                    Status = Common.Common.ProcessState.Fail,
                                    StatusMessage = $"Error : (TwitterStreamClient - {jsonSerializerEx.Message}"
                                };
                            }
                            catch (JsonReaderException jsonReaderEx)
                            {
                                return new Response()
                                {
                                    Status = Common.Common.ProcessState.Fail,
                                    StatusMessage = $"Error : (TwitterStreamClient - {jsonReaderEx.Message}"
                                };
                            }
                        }
                    }
                };

            }
            catch (WebException webEx)
            {
                StreamState = Common.Common.StreamState.Stopped;
              //  var wRespStatusCode = ((HttpWebResponse)webEx.Response).StatusCode;
                Raise(ExceptionReceived,
                      new TwitterExceptionEventArgs(new TwitterException(HttpStatusCode.BadRequest, webEx.Message)));
                if (webEx.Status == WebExceptionStatus.ProtocolError)
                {
                    if (wait < 10000)
                        wait = 10000;
                    else
                    if (wait < 240000)
                        wait *= 2;
                }
                else
                {
                    if (wait < 16000)
                        wait += 250;
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Status = Common.Common.ProcessState.Fail,
                    StatusMessage = $"Error : (TwitterStreamClient - {ex.Message}"
                };
            }
            finally
            {
                if (_webRequest != null)
                    _webRequest.Abort();

                Thread.Sleep(wait);
            }
            return new Response();
        }

        public void Raise(TwitterExceptionHandler handler, TwitterExceptionEventArgs e)
        {
            handler?.Invoke(this, e);
        }

        public void Raise(TweetReceivedHandler handler, TweetEventArgs e)
        {
            handler?.Invoke(this, e);
        }


    }
}
