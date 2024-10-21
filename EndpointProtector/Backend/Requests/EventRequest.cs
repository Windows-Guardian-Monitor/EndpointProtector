using Database.Models.Reports;

namespace EndpointProtector.Backend.Requests
{
	public class EventRequest
	{
		public List<DbProcessFinishedEvent> ProcessFinishedEvents { get; set; }

		public EventRequest(List<DbProcessFinishedEvent> processFinishedEvents)
		{
			ProcessFinishedEvents = processFinishedEvents;
		}
	}
}
