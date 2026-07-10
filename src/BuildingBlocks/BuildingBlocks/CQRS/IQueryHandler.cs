using MediatR;


namespace BuildingBlocks.CQRS;

//Interfaz Handler de Iquery
    public interface IQueryHandler<in TQuery,TResponse>
    :IRequestHandler<TQuery,TResponse>
    where TQuery:IQuery<TResponse>
    where TResponse:notnull
    {
    }

