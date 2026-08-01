namespace CatalogAPI.Products.GetProducts;

//como buena Practica Requerimos el record de Respuesta
//Usamos record asi es mas facil hacer cambios a futuro
//IEnumerbale es una coleccion que se puede recorrer
//utilizamos el generic pra prometerle que solo hay entidades de Product
public record GetProductResponse(IEnumerable<Product> Products);

//Heredamos Icarter Module para definir los endpoints
//con Carter no hay nececisidad de registra las rutas en el program.cs
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        //registramos el endpoint
        //Regla cualquier cosa que haner con un servicio o DB externa es asincronica

        //Isender es el mensajero hacia el Handler
        //Usamos segregacion de interfaces utilizamos la herramientas Exacta
        app.MapGet("/products", async (ISender sender) =>
        {
            //mandamos la query al Hanlder con Send metodo de Isender
            //llama al Handler por eso new GetProductQuery()
            var result = await sender.Send(new GetProductQuery());

            //retornamos la respuesta adaptandola segun los parametros
            //separamos resposnabilidades
            var response = result.Adapt<GetProductResponse>();

            //lo devolvemos en Formato Json con las Minimal APIS
            return Results.Ok(response);
        })
        // --- PROPIEDADES DEL ENDPOINT (SWAGGER) ---
        .WithName("GetProducts")
        .Produces<GetProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");

    }
}

