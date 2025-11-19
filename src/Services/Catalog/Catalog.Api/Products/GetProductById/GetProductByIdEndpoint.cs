
namespace Catalog.Api.Products.GetProductById;

public record GetProductByIdReponse(Product Product);
public class GetProductByIdEndpoint

    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetProductByIdQuery(id);
            var result =  await sender.Send(query);
            return Results.Ok(result.Adapt<GetProductByIdReponse>());
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdReponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get a product by its unique identifier.")
        .WithDescription("Retrieves the details of a specific product using its unique identifier (GUID).")
        .WithTags("Products");
    }
}
