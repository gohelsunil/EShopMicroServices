using System.Net;

namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id, string Name, string Description, decimal Price, List<string> Category);
public record UpdateProductResponse(bool IsSuccess);
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender, ILogger<UpdateProductEndpoint> logger) =>
        {
            logger.LogInformation($"Received UpdateProductRequest for Product Id: {request.Id}");
            var command = request.Adapt<UpdateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();

            if (!result.IsSuccess)
            {
                logger.LogWarning($"Failed to update Product with Id: {request.Id}");
                return Results.NotFound(new UpdateProductResponse(IsSuccess: false));
            }

            logger.LogInformation($"Successfully updated Product with Id: {request.Id}");
            return Results.Ok(new UpdateProductResponse(IsSuccess: true));
        })
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}
