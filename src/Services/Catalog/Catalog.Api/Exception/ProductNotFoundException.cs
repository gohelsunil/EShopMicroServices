namespace Catalog.Api.Exception;

public class ProductNotFoundException : IOException
{
    public ProductNotFoundException(string ErrorMessage): base(ErrorMessage)
    {
            
    }
}
