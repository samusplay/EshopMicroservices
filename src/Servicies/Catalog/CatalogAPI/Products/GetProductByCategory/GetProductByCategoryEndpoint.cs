namespace CatalogAPI.Products.GetProductByCategory;
//Por buenas praticas definimos el Record de la request para separar responsabilidades
public record GetProductByCategoryRequest();
//El de response ya que vamos devolver una lista de Productos por eso usamos Ienumerable
public record GetProductByCategoryResponse(IEnumerable<Product> products);

//Heredamos Icarterpara registra las rutas sin el program cs
public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        //Utilizamos el metodo de Mapget que nos da ICarter
        //le Pasamos Isender para que envie la Query al respectivo handler y en este caso le 
        //Pasamos el parametro de category
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            //Creamos un a variable utilizamos el metodo de send y le pasamos el 
            //Nuevo objeto con el parametro de Creacion ose GetProductCategoryQuery no el handler
            //y le pasamos el paametro de category  ya que nosotros definimos esa firma
            var result = await sender.Send(new GetProductByCategoryQuery(category));
            //adaptamos la varaible de result al record para mejor practica seprar la Responsabilidad
            var response = result.Adapt<GetProductByCategoryResponse>();

            //enviamos el 200k al cliente con el response ya respuest adaptada
            //El results nos los da ASP net para envair de forma limpia sin controller
            return Results.Ok(response);
        })
         .WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");
    }
}

