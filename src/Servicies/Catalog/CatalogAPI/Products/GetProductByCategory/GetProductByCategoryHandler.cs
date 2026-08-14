namespace CatalogAPI.Products.GetProductByCategory;

// creamos un record para enviar
// Especificamos que es una query este record
//le pasamos el parametro ya que lo buscaremos por Category
//utilizamos Generic porque devolveremos el tipo de dato en este Caso GetProductByCategoryResult
public record GetProductByCategoryQuery(string Category):IQuery<GetProductByCategoryResult>;
//Definimos el record que es el resultado de la query
//Utilizamos IEnumerable apra que recorra esa lista de Products

public record GetProductByCategoryResult(IEnumerable<Product> products);
//Aqui nos centramos en la logica de negocio
//Denominamos QUERYHANDLER para mejores practcias
//Luego pasar el parametro para crear la query y el resltado que esperamos es nuestra firma
internal class GetProductByCategoryQueryHandler
    //pasaremos el constructor para inyectar las depencias
    (IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
    : IRequestHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    //query le cambiamos el parametro
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        //logs de que se mando llamar 
        logger.LogInformation("GetProductByCategoryQueryHandler.Handle Called with{@Query}",query);
        //Creamos una varibale que va comunicarse con la base de datos con el metodo Query
        var products = await session.Query<Product>()
            //Utilizamos una funcion lamda
            //p signifca el valor que va ir consultando a la base de datos mientras lo recorre
            //contains signifca solo busca los que envio el usuario en la query
            .Where(p=>p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);
        //devolvemos el nuevo objeto
        return new GetProductByCategoryResult(products);

    }
}

