# RedisCacheTagHelper
A simple Redis cache TagHelper example.

To create a Redis Cache in Azure, see the following article https://azure.microsoft.com/en-us/documentation/articles/cache-dotnet-how-to-use-azure-redis-cache/

After creating Redis, you can put the connectionstring in appsetting or secretmanager.

Following code shows how to use the Redis TagHelper. Current usage is shown in Views/Home/Index.cshtml

```html
<redis-cache>
    <p>The current time (in ticks): @DateTime.Now.Ticks.</p>
    <p><em>The above time stamp should never change.</em></p>
</redis-cache>

<br />

<redis-cache absolute-expiration="DateTime.Now + TimeSpan.FromSeconds(5)">
    <p>The current time (in ticks): @DateTime.Now.Ticks.</p>
    <p><em>The above time stamp should change every 5 seconds.</em></p>
</redis-cache>

<br />

<redis-cache relative-absolute-expiration="TimeSpan.FromSeconds(7)">
    <p>The current time (in ticks): @DateTime.Now.Ticks.</p>
    <p><em>The above time stamp should change every 7 seconds.</em></p>
</redis-cache>

<br />

<redis-cache sliding-expiration="TimeSpan.FromSeconds(10)">
    <p>The current time (in ticks): @DateTime.Now.Ticks.</p>
    <p><em>The above time stamp should change every 10 seconds IF not requested again within 10 seconds (sliding window).</em></p>
</redis-cache>
```
