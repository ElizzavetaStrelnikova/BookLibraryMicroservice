using BookService;
using Grpc.Core;
using StackExchange.Redis;
using System.Text.Json;
using static BookService.BookService;

namespace BookService.Services
{
    public class BookService : BookServiceBase
    {
        // gRPC SERVICE
        private readonly IDatabase _redis;

        public override async Task<BookResponse> GetBook(GetBookRequest request, ServerCallContext context)
        {
            var cacheKey = $"book:{request.Id}";
            var cachedBook = await _redis.StringGetAsync(cacheKey);

            if (cachedBook.HasValue)
                return JsonSerializer.Deserialize<BookResponse>(cachedBook);

            // ... get from DB
            await _redis.StringSetAsync(cacheKey, JsonSerializer.Serialize(book), TimeSpan.FromMinutes(10));
        }
    }
}
