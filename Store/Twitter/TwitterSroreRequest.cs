using System;

namespace Store.Twitter
{
    public class TwitterSroreRequest : IStoreRequest
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Data { get; set; }
        public string FileExtension { get; set; }
    }
}
