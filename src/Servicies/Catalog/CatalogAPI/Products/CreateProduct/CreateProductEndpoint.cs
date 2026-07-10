namespace CatalogAPI.Products.CreateProduct;

//El Record es una estrctura de datos que no se puede modificar es como Un dto 
//Tener en cuenta que aqui MediatR es el mensajero hacia el Handler
public record CreateProductRequest(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price

);
//Record de respuesta
public record CreateProductResponse(Guid Id);

//Usamos la Implementacion de la Interfaz de Carter ya que lo dividimos en Modulos
public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        //aqui Vamosa definir el metodo http
        app.MapPost("/products",
            async (CreateProductRequest request, ISender sender) =>
            {
                //Usamos Mapster para Mapear el objeto en la spo
                var command = request.Adapt<CreateProductComand>();

                //enviamos el resultado transportamos el Isender 
                //No hay necesidad  de llamar al Handler se Lo pasamos
                //MediatR que es el mensajero
                var result = await sender.Send(command);

                //damos una respuesta usamos en este caso el Objeto Result
                var response = result.Adapt<CreateProductResponse>();


                //Devolvemos un 202 Created y la ruta donde se encuentra
                return Results.Created($"/products/{response.Id}", response);

            })
          .WithName("CreateProduct")
    .Produces<CreateProductResponse>(StatusCodes.Status201Created)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithSummary("Create Product")
    .WithDescription("Create Product");
    }
}

