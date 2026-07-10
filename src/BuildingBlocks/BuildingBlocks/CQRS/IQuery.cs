

using MediatR;

namespace BuildingBlocks.CQRS;

//Interfaz de consulta y no puede ser nula
//El generic nos devulve un tipo de dato
    public interface IQuery<out TResponse>:IRequest<TResponse>
    where TResponse:notnull
    {
    }

