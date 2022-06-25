
using System.Net.Http.Json;
using System.Text.Json;

public static class Extensions
{
    public static IOrderedEnumerable<TSource> OrderByPropertyName<TSource> (this IEnumerable<TSource> source, string PropertyName,bool Descending) 
    {
        return source.OrderBy(x => (x.GetType().GetProperty(PropertyName) ?? throw new("Invalid PropertyName")).GetValue(x),Descending);
    }
    public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey> (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,bool Descending)
    {
        return Descending ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
    }
    public static async Task<TResult?> PostAsJsonAsync<TValue,TResult>(this HttpClient client, string? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
    {
        var response = await client.PostAsJsonAsync<TValue>(requestUri, value, options, cancellationToken);
        if (response == null) return default(TResult);
        return await response.Content.ReadFromJsonAsync<TResult>(options,cancellationToken);
    }

    public static async Task<TResult?> PutAsJsonAsync<TValue, TResult>(this HttpClient client, string? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
    {
        var response = await client.PutAsJsonAsync<TValue>(requestUri, value, options, cancellationToken);
        if (response == null) return default(TResult);
        return await response.Content.ReadFromJsonAsync<TResult>(options, cancellationToken);
    }

    public static bool NotNullAndGreaterThen(this string? x, int n) => x != null && x.Length > n;

}