using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Store.Twitter
{
    class TwitterStoreRequestHandler : IStore<TwitterSroreRequest>
    {
        public async Task Save(TwitterSroreRequest data)
        {
            using (var fs = new FileStream(
                                            $"{AppContext.BaseDirectory}{data.FilePath}{data.FileName}{Guid.NewGuid()}{data.FileExtension}",
                                            FileMode.OpenOrCreate,
                                            FileAccess.ReadWrite,
                                            FileShare.Read,
                                            4096,
                                            true
                                            ) )
            {
                byte[] bytes = Encoding.ASCII.GetBytes(data.Data);
                await fs.WriteAsync(bytes, 0, bytes.Length);
            }
        }
    }
}
