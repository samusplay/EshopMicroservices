namespace CatalogAPI.Products.GetProducts;
//1.Paso editar recors 
//2.Paso crear handler y heredar interfza en inyectar dependencias
//3.Crear Logica
//Utilizamos Record porque no debe cambiar el mensaje
//Usamos Generic para devolver el tipo de dato
//Creamos una Firma
public record GetProductQuery() : IQuery<GetProductsResult>;

//La Respuesta de Nuestra solictud
public record GetProductsResult(IEnumerable<Product> Products);

//la mejor pratica separar de quey y comand
//es una clase que solo se puede acceder desde este archivo
//el Primer parametro despues del Gneric es lo que necesita
//la interface de Query y su resultado
internal class GetProductsQueryHandler
    //Inyectamos las dependencias con el constructor primario
    (IDocumentSession session,ILogger<GetProductsQueryHandler> logger)
    //Es necesario implentar la interfaz
    : IQueryHandler<GetProductQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        //mandar logs para mandar informacion de que esta pasando
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}",query);

        //definimos la consulta
        var products = await session.Query<Product>().ToListAsync(cancellationToken);

        //Retornamos un resultado proveniente de nuestra Firma
        //Se Usa new para crar un objeto en memoria
        return new GetProductsResult(products);
        
    }
}

