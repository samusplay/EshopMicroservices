namespace CatalogAPI.Products.UpdateProduct;

//Como es un command esta vez si es necesario un record de Request
//De igual forma le debemos proporcionar los parametros 
//Luego lo vamos Mapear Con Mapster porque deben ser los mismos parametros
public record UpdateProductRequest(
     Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
    );
//de igual forma recibira un Bool si es False o True
public record UpdateProductResponse(bool IsSuccess);

//Heredamos ICarter
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        //Utilizamos Map Products
        //le pasamos el parametro que vamsoa actualizar en este caso ek Record completo
        //Pasamos Isender va actuar como mensajero ahcia el  handler Correspondiente
        app.MapPut("/products", async (UpdateProductRequest request,ISender sender) =>
        {
            //Adaptamos la respuesta del Parmetro de Creacion del command
            var command = request.Adapt<UpdateProductCommand>();
            
            //esperamos la Respuesta y le pasamos el command
            var result = await sender.Send(command);

            //adaptamos la respuesta a nuestra firma con maspeter
            //Este adapt nos va dar separacion de responsabilidades
            //Sacamos la respuesta al mundo web en HTTP
            var response = result.Adapt<UpdateProductResponse>();

            //Retornmaos el resultado con results que nos permite dar el codigo
            return Results.Ok(response);

        })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
             .WithDescription("Update Product");
    }
}

