

using MediatR;

namespace BuildingBlocks.CQRS;

//Hay operaciones que no necesitamos respuestas
public interface ICommandHandler<in TCommand>
    //Unit es el avoid de los Generic
    :ICommandHandler<TCommand,Unit>
    where TCommand : ICommand<Unit>
{

}

//solo acepta commands para eso sirve la sentencia de where y va llegar una respuesta
    public interface ICommandHandler<in TCommand,TResponse>
    :IRequestHandler<TCommand,TResponse>
    where TCommand:ICommand<TResponse>
    {
    }

