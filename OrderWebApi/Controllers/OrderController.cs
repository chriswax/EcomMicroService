using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OrderWebApi.Models;

namespace OrderWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public OrderController()
        {
            //Databae setup/connection locally
            //var dbHost = "localhost";
            //var dbName = "dms_order";

            //use for mongo Docker
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var conneectionString = $"mongodb://{dbHost}:27017/{dbName}";

            var mongoUrl = MongoUrl.Create(conneectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _orderCollection = database.GetCollection<Order>("order");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _orderCollection.Find(Builders<Order>.Filter.Empty).ToListAsync();
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrderById(string orderId)
        {
            var filterOrder = Builders<Order>.Filter.Eq(x => x.OrderId, orderId);
            return await _orderCollection.Find(filterOrder).SingleOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Order order)
        {
            await _orderCollection.InsertOneAsync(order);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(Order order)
        {
            var filterOrder = Builders<Order>.Filter.Eq(x =>x.OrderId, order.OrderId);
            await _orderCollection.ReplaceOneAsync(filterOrder, order);
            return Ok();
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> Delete(string orderId)
        {
            var filterOrder = Builders<Order>.Filter.Eq(x => x.OrderId, orderId);
            await _orderCollection.DeleteManyAsync(filterOrder);
            return Ok();
        }
    }
}
