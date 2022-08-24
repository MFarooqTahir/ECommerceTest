# ECommerce Test Project
>Disclaimer
>This project is intended to be the implementation of a test by floward. 

This solution and it's projects are structured in the following manner. The project itself uses a microservice architecture and is containerized with docker. Test questions are answered below.

1.  Design the  Microservice Architecture  for the example (diagram that represent  it). <br/><img src="https://github.com/MFarooqTahir/ECommerceTest/blob/master/Architecture%20Diagram.png?raw=true"></img>
2.  Define the design pattern you are going to use.  
		The project uses
		 Factory Creational Pattern which means that the objects are created with a factory method of several derived class as requested, implemented in HttpClient in the ApiGateway. And Bridge Structural Pattern which means that the object's implementation is seperate from the implementation itself. And the proxy Pattern, as it uses an api gateway to mediate the requests to the microservices themselves
3.  Create a .net core project that have the designed architecture  (only to show  
architecture). <br/>
 <img src="https://github.com/MFarooqTahir/ECommerceTest/blob/master/Solution%20Structure.png?raw=true"></img>
4.  Create  at  least  one of the  databases  you  are  going to use  (locally).  
Locally the cart database has been made, the implementation is available in the CartDatabase project within the solution. It has 2 tables.<br/>
<img src="https://github.com/MFarooqTahir/ECommerceTest/blob/master/Cartdb%20Tables.png?raw=true"></img>
```SQL
CREATE TABLE [dbo].[Cart]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserID] INT NOT NULL
)
CREATE TABLE [dbo].[CartItems]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CartID] INT NOT NULL, 
    [ProductID] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    CONSTRAINT [FK_CartItems_ToTable] FOREIGN KEY ([CartID]) REFERENCES [Cart] ([Id])
)
```
5.  Create one repository and one controller for add to cart part.  
6.  Do  RabbitMQ  integration (dummy).  
5 and 6 are implemented in the same code.


Repository Code
```Csharp
namespace ECommerceTest.CartApi.Repositories
{
    public interface ICartRepository
    {
        Task<bool> AddToCart(int productID, int quantity);
    }
}
```
```Csharp
namespace ECommerceTest.CartApi.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDatabaseContext _cartContext;
 
        public CartRepository(CartDatabaseContext cartContext)
        {
            _cartContext = cartContext;
        }
 
        public async Task<bool> AddToCart(int productID, int quantity)
        {
            //The dbcontext can be filterred with the user with identity claims, logically not implemented here
            var currentUserID = 0;//id taken as 0 for example
            var cart = await _cartContext.Cart.Where(x => x.UserId == currentUserID).Include(x => x.CartItems).FirstOrDefaultAsync();
            if (cart is not null)
            {
                cart.CartItems.Add(new()
                {
                    ProductId = productID,
                    Quantity = quantity,
                });
                await _cartContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
```

Controller Code
```Csharp
namespace ECommerceTest.CartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cart;
        private readonly IConfiguration _configuration;
        public CartController(ICartRepository cart, IConfiguration configuration)
        {
            _cart = cart;
            _configuration = configuration;
        }
 
        [Route("Add")]
        public async Task<bool> Add([FromBody] AddProductToCartDto newProduct)
        {
            //RabbitMQ dummy implementations
            string RabbitmqHost = _configuration.GetValue<string>("RabbitMQHost");
            var factory = new ConnectionFactory() { HostName = RabbitmqHost };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "locationSampleQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                //user ID from the claims
                int userID = 0;
                string message = $"Added To Cart=> ID:{newProduct.productID}, Quantity: {newProduct.quantity},UserID:{userID}";
                var body = Encoding.UTF8.GetBytes(message);
 
                channel.BasicPublish(exchange: "",
                                     routingKey: "ProductAddQueue",
                                     basicProperties: null,
                                     body: body);
            }
            return await _cart.AddToCart(newProduct.productID, newProduct.quantity);
        }
 
    }
}
```

7.  Adding a testing unite is a plus.
A simple unit test has been added for the cart add endpoint
```Csharp
namespace ECommerceTest.CartApiTests.Controllers
{
    public class CartControllerTests
    {
        [Fact]
        public async Task Add_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(Environment.ProcessPath)//path of current project
        .AddJsonFile("appsettings.json", optional: true)
        .AddEnvironmentVariables()
        .Build();
 
            var options = new DbContextOptionsBuilder<CartDatabaseContext>();
            options.UseSqlServer(config.GetValue<string>("CartDbLink"));
 
            var CartContext = new CartDatabaseContext(options.Options);
            ICartRepository cartRepository = new CartRepository(CartContext);
            AddProductToCartDto newProduct = new(1, 3);
            var cartController = new CartController(cartRepository, config);
            // Act
            var result = await cartController.Add(
                newProduct);
 
            // Assert
            Assert.True(false);
        }
    }
}
```

>The contents of this repository are implemented in a naive manner, and the implementation of Rabbit MQ, Database designs and the projects themselves does not always represent how it would be done in a real life situation. The solution structure shows how it will be implemented.
>Currently, the Cart database, Cart db context, Cart repository and the cart api have been implemented for adding a new product, other essential implementations such as authorization, identity,RPC etc are left without implementation as they were not required. 
>Unit test has been written for only the implemented add item to cart endpoint
