using Bitrix24RestApiClient.Api.Calendar;
using Bitrix24RestApiClient.Api.Crm;
using Bitrix24RestApiClient.Api.Task;
using Bitrix24RestApiClient.Api.User;
using Bitrix24RestApiClient.Core.Client;

namespace Bitrix24RestApiClient.Api
{
    /// <summary>
    /// Контейнер для АПИ битрикс24. Все общение с битрикс24 ведется через него.
    /// </summary>
    public class Bitrix24
    {
        public Bitrix24(IBitrix24Client client)
        {
            Users = new Users(client);
            Crm = new CrmContainer(client);
            Tasks = new Tasks(client);
            Calendar = new CalendarContainer(client);
        }

        public Users Users { get; private set; }
        public CrmContainer Crm { get; private set; }
        public Tasks Tasks { get; private set; }
        public CalendarContainer Calendar { get; private set; }
    }
}
