namespace CatalogAPI.Exceptions;
//Clase personalizada

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException():base("Product not found!")
    {
    }
}

