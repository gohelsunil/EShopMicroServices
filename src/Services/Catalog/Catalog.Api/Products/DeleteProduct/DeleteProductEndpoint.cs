
namespace Catalog.Api.Products.DeleteProduct;

public record DeleteProductResponse(bool Success);
public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(id));
            return Results.Ok(result.Adapt<DeleteProductResponse>());
        })
        .WithName("DeleteProduct")
        .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete a product by its unique identifier.")
        .WithDescription("Deletes a specific product from the catalog using its unique identifier (GUID).")
        .WithTags("Products");
    }
}
