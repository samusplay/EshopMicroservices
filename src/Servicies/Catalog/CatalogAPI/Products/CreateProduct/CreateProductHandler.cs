using BuildingBlocks.CQRS;
using CatalogAPI.Models;


namespace CatalogAPI.Products.CreateProduct;

//clases que va Manejar Mediatr
//comand la clase con los campos
public record CreateProductComand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
   ):ICommand<CreateProductResult>;

//Lo que va devolver luego de crear el Comand
public record CreateProductResult(Guid Id);

//hacemos enfasis de que hanlder del comand le pasamos el comand y el result
internal class CreateProductComandHandler 
    : ICommandHandler<CreateProductComand, CreateProductResult>

{//Implementamos los metodos de Irequest que son de la libreria 
    public  async Task<CreateProductResult> Handle(CreateProductComand command, CancellationToken cancellationToken)
    {
        //Logica de Negocio para Crear un Producto
        //Crear el producto entidad como objeto
        //toca mapear se Command para la entidad
        var product = new Product
        {
            //mapear propiedades
            Name=command.Name,
            Category=command.Category,
            Description=command.Description,
            ImageFile=command.ImageFile,
            Price=command.Price,
        };
        //Guardar En la Base de datos

        //devolvemos el resultado
        return  new CreateProductResult(Guid.NewGuid());
    }
}

