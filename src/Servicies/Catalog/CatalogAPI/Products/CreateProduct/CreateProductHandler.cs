using MediatR;

namespace CatalogAPI.Products.CreateProduct;

//clases que va Manejar Mediatr
//comand la clase con los campos
public record CreateProductComand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
    //Utilizamos mediatr para comprobar y pasar al handler
   ):IRequest<CreateProductResult>;

//Lo que va devolver luego de crear el Comand
public record CreateProductResult(Guid Id);

//hacemos enfasis de que hanlder del comand le pasamos el comand y el result
internal class CreateProductComandHandler : IRequestHandler<CreateProductComand, CreateProductResult>

{//Implementamos los metodos de Irequest que son de la libreria 
    public Task<CreateProductResult> Handle(CreateProductComand request, CancellationToken cancellationToken)
    {
        //Logica de Negocio para Crear un Producto
        throw new NotImplementedException();
    }
}

