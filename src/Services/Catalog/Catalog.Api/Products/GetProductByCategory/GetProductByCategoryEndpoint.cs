
namespace Catalog.Api.Products.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);
public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender, ILogger<GetProductByCategoryEndpoint> logger, CancellationToken cancellationToken) =>
        {
            logger.LogInformation($"GetProductByCategoryEndpoint called with category: {category}");
            var result = await sender.Send(new GetProductByCategoryQuery(category));
            return Results.Ok(result.Adapt<GetProductByCategoryResponse>());
        })
        .WithName("GetProductsByCategory")
        .WithTags("Products")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Gets products by category.")
        .WithSummary("Get Products by Category");
    }
}
