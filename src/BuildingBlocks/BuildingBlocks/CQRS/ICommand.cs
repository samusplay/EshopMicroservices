using MediatR;
namespace BuildingBlocks.CQRS;
 
//Interfaz que no va devolver Nada
    public interface Icommand:ICommand<Unit>
{

}
//Interfaz de command que devuelve
    public interface ICommand<out TResponse>:IRequest<TResponse>
    {

    }

