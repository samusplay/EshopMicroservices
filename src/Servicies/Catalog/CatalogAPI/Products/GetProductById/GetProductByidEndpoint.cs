
namespace CatalogAPI.Products.GetProductById;

//Debe ser exactamente igual  al handler la respuesta
//En este caso devolvemos el mismo tipo de dato Product que es el resultado del handler
public record GetProductByIdResponse(Product product);
//Implementamos Icarter para registrar las rutas de la aplicacion
//no hya necesidad registralas en el progrma.cs
public class GetProductByidEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        //1.Paso registramos la ruta
        //es una funcion asincronca que recibe el id y el Isender
        //sera el mensajero para el Respectivo Handler
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            //En esta variable enviaremos la Query al Hnandler que es GetProductByIdHandler
            //Requiere el parametri dek id
            var result = await sender.Send(new GetProductByIdQuery(id));

            //devolvemos la respuesta la adaptamaos al record para separar responsabilidades
            var response = result.Adapt<GetProductByIdResponse>();

            //Utilizamos el metodo de Ok envolvemos en un reposne para un 201 Created
            //L
            return Results.Ok(response);
            ;
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Product By Id")
         .WithDescription("Get Product By Id");
    }
}

