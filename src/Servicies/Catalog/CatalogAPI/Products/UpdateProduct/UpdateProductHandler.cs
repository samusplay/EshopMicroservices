namespace CatalogAPI.Products.UpdateProduct;

//utilizaremos comand ya que tenemos que editar la peticion y con el parametro del id
//Heredamos iComand y esperamos el Tipo de resultado de UpdateProductResult
//OJO CON LOS NOMBRES la mejor practica es decir si es un comand o un query
public record UpdateProductCommand(
    //Estas propiedades provienen del modelo la utilizamos para actualizar el producto
    Guid Id, string Name, List<string>Category,string description,string ImageFile, decimal Price
    ) : ICommand<UpdateProductResult>;
//Como respuesta le pasaremos solo un booleano o True o False si se actulizo el producto
public record UpdateProductResult(bool IsSuccess);
//Pasamos dos parametros el de crear ene ste caso comand y su respuesta
internal class UpdateProductCommandHandler 
    //Inyectamos la dapendecnias para poder hablar con la base de datos
    (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    //ojo en definir los nombres de parametros y es command
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with ID: {ProductId}", command.Id);

        //lOGICA Real
        //para acatulizar en base de datos de documentos
        //primero necesitamos la informacion en la memoria
        //luego lo sobreescribimos con lo que provenga del command
        //le pasamos el parametrio del comand id para quye encuentre el producto
        //el cancellation token sera para cancelar en caso de emergencia
        var product=await session.LoadAsync<Product>(command.Id, cancellationToken);

        //hacemos una validacion previa de que el producto no puede ser null
        if(product is null)
        {
            //Mandamos llamar nuestra funcion de que no se encontro
            throw new ProductNotFoundException();
        }
        //Se puede mapear con Mapster
        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.description;
        product.ImageFile=command.ImageFile;
        product.Price = command.Price;

        //mandamos llamar Session con el metodo update y le pasamos la entidad
        session.Update(product);

        //Guardamos en la base de datos
        await session.SaveChangesAsync(cancellationToken);
        //retornar el reusltado y le pasaremos el parametro de la firma
        return new UpdateProductResult(true);

    }
}

