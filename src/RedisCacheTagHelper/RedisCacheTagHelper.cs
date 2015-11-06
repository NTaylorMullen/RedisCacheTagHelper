using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.Framework.Caching.Distributed;

namespace WebApplication5
{
    public class RedisCacheTagHelper : TagHelper
    {
        private readonly IDistributedCache _redisCache;

        public RedisCacheTagHelper(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public DateTimeOffset? AbsoluteExpiration { get; set; }

        public TimeSpan? RelativeAbsoluteExpiration { get; set; }

        public TimeSpan? SlidingExpiration { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var bytes = await _redisCache.GetAsync(context.UniqueId);
            string content;

            if (bytes == null)
            {
                var childContent = await context.GetChildContentAsync();
                content = childContent.GetContent();
                bytes = Encoding.UTF8.GetBytes(content);

                await _redisCache.SetAsync(context.UniqueId, bytes, new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = AbsoluteExpiration,
                    AbsoluteExpirationRelativeToNow = RelativeAbsoluteExpiration,
                    SlidingExpiration = SlidingExpiration
                });
            }
            else
            {
                content = Encoding.UTF8.GetString(bytes);
            }

            output.SuppressOutput();

            // Unsupress by setting the content again.
            output.Content.SetContentEncoded(content);
        }
    }
}
