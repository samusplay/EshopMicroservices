

namespace CatalogAPI.Products.GetProductById;
//Creamos un record para eviar un query al handler con un id 
//Esperamos que esperemos de GetproductResult
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
//devolvemos el resultado con el tipo de dato Product
public record GetProductByIdResult(Product product);
//generamos la clase que es interna para que no se acceda de otro lugar
//Tomamos el primer parametro de la firma que es el query y el segundo es el resultado
internal class GetProductByIdQueryHandler 
    //Inyectamos dependencias en el Constructor
    //Idocumeent es la conversacion de Marten a la DB
    //LOGGER los logs que esta pasando en nuestra app
    (IDocumentSession session,ILogger<GetProductByIdQueryHandler> logger)

    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    //Metodo que vamos Gestionar la Logica de negocio
    //pasamos parametro de query
    public  async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        //Log para obtener informacion 
        logger.LogInformation("GetProductByIdQueryHandler.Handler called with {@query}", query);
        //Creamos una variable para guardar la consulta de la DB con el Id 
        //es una llamada asincronica porque esperamos la db
        //Ve busca la db con el id
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        //creamos una validacion si el Producto no existe
        if(product is null)
        {
            throw new ProductNotFoundException();
        }
        //si pasa este codigo retornamos el resultado
        //Product vinee de LoadAsync method
        //Retornamos segun el contrato
        return new GetProductByIdResult(product);

    }
}

