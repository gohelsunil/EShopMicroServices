namespace Catalog.Api.Products.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool Success);


internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting product with ID: {request.ProductId}");
        var product = await session.LoadAsync<Product>(request.ProductId, cancellationToken);
        if (product == null)
        {
            logger.LogWarning($"Product with ID: {request.ProductId} not found");
            return new DeleteProductResult(false);
        }
        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);
        logger.LogInformation($"Product with ID: {request.ProductId} deleted successfully");
        return new DeleteProductResult(true);
    }

}
