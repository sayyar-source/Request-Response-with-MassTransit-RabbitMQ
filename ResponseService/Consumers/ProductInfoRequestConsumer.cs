
using MassTransit;
using Model.Contracts;
using ResponseService.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ResponseService.Consumers
{

    public class ProductInfoRequestConsumer:IConsumer<ProductInfoRequest>
    {

        readonly ICatalogService _catalogService;

        public ProductInfoRequestConsumer(ICatalogService catalogService)
        {
            _catalogService= catalogService;
        }

        public async Task Consume(ConsumeContext<ProductInfoRequest> context)
        {
            var slug = context.Message.Slug;

            // get the product from ProductService
            var product = _catalogService.GetProductBySlug(slug);

            // this responds via the queue to our client
            await context.RespondAsync(new ProductInfoResponse
            {
                Product = product
            });
        }
    }
}
