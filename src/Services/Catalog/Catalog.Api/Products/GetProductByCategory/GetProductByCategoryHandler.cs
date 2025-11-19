using Catalog.Api.Products.GetProductById;

namespace Catalog.Api.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string  Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);


public class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetProductByCategoryQueryHandler.Handle called with {query}.");
        var product = await session.Query<Product>().Where(c=>c.Category.Contains(query.Category)).ToListAsync();

        if (product is null || product.Count ==0)
        {
            throw new ProductNotFoundException($"Product with category {query.Category} not found!!!");
        }
        return new GetProductByCategoryResult(product);
    }
}
