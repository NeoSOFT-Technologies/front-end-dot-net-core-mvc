
using MVC.Boilerplate.Models.Event.Commands;
using MVC.Boilerplate.Models.Event.Queries;

namespace MVC.Boilerplate.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Events>> GetEventList();
        Task<Guid> CreateEvent(CreateEvent events);

    }
}
