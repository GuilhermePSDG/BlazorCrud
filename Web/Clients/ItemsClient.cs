
using System.Net.Http.Json;

internal class ItemsClient
{
    private string baseUrl="/items";
    public ItemsClient(HttpClient client) => Client = client;
    public HttpClient Client { get; }
    public async Task<Item?> PostAsync(PostPutItem item)
    {
        return (await this.Client.PostAsJsonAsync<PostPutItem, Item>(this.baseUrl, item));
    }
    public async Task<Item?> PutAsync(Guid ItemId, PostPutItem item)
    {
        return (await this.Client.PutAsJsonAsync<PostPutItem, Item>($"{this.baseUrl}/{ItemId}", item));
    }
    public async Task<bool> DeleteAsync(Guid ItemId)
    {
        return (await this.Client.DeleteAsync($"{this.baseUrl}/{ItemId}")).IsSuccessStatusCode;
    }
    public async Task<List<Item>> GetAllAsync()
    {
        return (await this.Client.GetFromJsonAsync<List<Item>>($"{this.baseUrl}") ?? new());
    }

}
