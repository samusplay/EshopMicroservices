

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
//Idocument es la absatracion inversion de depenecias solo  es la abstraccion
internal class CreateProductComandHandler(IDocumentSession session) 
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
        //usamos el objeto de session.store lo tenemos listo para guardar en la DB 
        session.Store(product);
        //el token de cancelacion es para evitar requets incompletos
        //esperamos que se guarden los datos
        await session.SaveChangesAsync(cancellationToken);

        //devolvemos el resultado Con su id generado en la db
        return  new CreateProductResult(product.Id);
    }
}

