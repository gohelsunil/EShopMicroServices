
namespace Catalog.Api.Products.UpdateProduct;


public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, List<string> Category) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);
internal class UpdateProductCommandHandler (IDocumentSession session,ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling UpdateProductCommand for Product Id: {request.Id}");
        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
        if(product == null)
        {
            logger.LogWarning($"Product with Id: {request.Id} not found.");
            return new UpdateProductResult(IsSuccess: false);
        }
        product.Name = request.Name;    
        product.Description = request.Description;  
        product.Price = request.Price;  
        product.Category = request.Category;  
        product.ImageFile = product.ImageFile ?? "default.png"; 
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        logger.LogInformation($"Product with Id: {request.Id} updated successfully.");
        return new UpdateProductResult(IsSuccess: true);
    }
}
