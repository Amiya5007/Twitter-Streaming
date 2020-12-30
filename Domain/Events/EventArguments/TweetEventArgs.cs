using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Twitter;

namespace Domain.Events.EventArguments
{
    [Serializable]
    public class TweetEventArgs : EventArgs
    {
        public TweetEventArgs(TweetData tweet)
        {
            Tweet = tweet;
        }

        public TweetData Tweet { get; private set; }
    }
}
