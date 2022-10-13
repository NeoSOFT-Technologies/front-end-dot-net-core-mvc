using MVC.Boilerplate.Application.Helper.ApiHelper;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.Event.Commands;
using MVC.Boilerplate.Models.Event.Queries;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MVC.Boilerplate.Service
{
    public class EventService : IEventService
    {
        private readonly IApiClient<Events> _client;
        private readonly IApiClient<Guid> _postClient;
        public readonly ILogger<EventService> _logger;
        public EventService(IApiClient<Events> client, IApiClient<Guid> postClient, ILogger<EventService> logger)
        {
            _client = client;
            _logger = logger;
            _postClient = postClient;
        }
        public async Task<IEnumerable<Events>> GetEventList()
        {

            _logger.LogInformation("GetEventList Service initiated");
            var Events = await _client.GetAllAsync("Events");
            _logger.LogInformation("GetEventList Service conpleted");
            return Events.Data;
        }
        public async Task<Guid> CreateEvent(CreateEvent createEvent)
        {
            _logger.LogInformation("CreateEvents Service initiated");
            var Events = await _postClient.PostAsync("Events", createEvent);
            _logger.LogInformation("CreateEvents Service conpleted");
            return Events.Data;
        }
    }

    

}
