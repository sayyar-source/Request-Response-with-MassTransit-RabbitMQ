
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Contracts;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRequestClient<ProductInfoRequest> _requestClient;
        public ProductController(IRequestClient<ProductInfoRequest> requestClient)
        {
            _requestClient= requestClient;

        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductController>/5
        [HttpGet("get-product-by-slug")]
        public async Task<IActionResult> GetProductBySlug([FromQuery] string slug)
        {
            //Product p = null;
            //// request from the remote service
            //using (var request = _client.Create(new ProductInfoRequest { Slug = slug, Delay = timeout }))
            //{
            //    var response = await request.GetResponse<ProductInfoResponse>();
            //    p = response.Message.Product;
            //}
            var request=_requestClient.Create(new ProductInfoRequest { Slug = slug});
            var response=await request.GetResponse<ProductInfoResponse>();
            var product=response.Message.Product;

            return Ok(product);

        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
