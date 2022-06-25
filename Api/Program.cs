
using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();


List<Item> Items = new List<Item>()
{
    new Item(Guid.NewGuid(),"Item1","Valor1"),
    new Item(Guid.NewGuid(),"Item2","Valor2"),
    new Item(Guid.NewGuid(),"Item3","Valor3"),
};

app.MapGet("/items", (HttpContext ctx) => Results.Ok(Items));
app.MapPost("/items", (PostItem postedItem, HttpContext ctx) =>
{
    var item = new Item(Guid.NewGuid(), postedItem.Name, postedItem.Value);
    Items.Add(item);
    return Results.Ok(item);
});
app.MapPut("/items/{itemId}", async (Guid itemId, PostItem toPut, HttpContext ctx) =>
{
    var i = Items.FindIndex(x => x.Id == itemId);
    if (i == -1)
        return Results.BadRequest();
    return Results.Ok(Items[i] = new Item(Items[i].Id, toPut.Name, toPut.Value));
});
app.MapDelete("/items/{itemId}",  (Guid itemId, HttpContext ctx) =>
{
    var item = Items.FirstOrDefault(x => x.Id == itemId);
    if(item != null && Items.Remove(item))
        return Results.Ok();
    return Results.BadRequest();
});
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.Run();

public record Item(Guid Id, string Name, string Value);
public record PostItem(string Name, string Value);
