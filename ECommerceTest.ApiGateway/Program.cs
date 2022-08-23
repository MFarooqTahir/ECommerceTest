using ECommerceTest.ApiGateway.Data;
using ECommerceTest.ApiGateway.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//We can have an http client message handler to insert jwt token if we want to go with that approach for authentication.
builder.Services.AddHttpClient(ApiClientNames.CartApi,
    client =>
    {
        client.BaseAddress = new(builder.Configuration.GetValue<string>("CartApiLink"));
    });
builder.Services.AddHttpClient(ApiClientNames.ProductsApi,
    client =>
    {
        client.BaseAddress = new(builder.Configuration.GetValue<string>("ProductsApiLink"));
    });
builder.Services.AddHttpClient(ApiClientNames.BillingApi,
    client =>
    {
        client.BaseAddress = new(builder.Configuration.GetValue<string>("BillingApiLink"));
    });

builder.Services.AddScoped<ICartService, CartService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();