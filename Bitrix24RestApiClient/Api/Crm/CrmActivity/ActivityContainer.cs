using Bitrix24RestApiClient.Api.Crm.CrmActivity.Models;
using Bitrix24RestApiClient.Core;
using Bitrix24RestApiClient.Core.Client;
using Bitrix24RestApiClient.Core.Models.Enums;

namespace Bitrix24RestApiClient.Api.Crm.CrmActivity;

/// <summary>
/// Activity
/// </summary>
public class ActivityContainer: AbstractEntities<Activity>
{
	public ActivityContainer(IBitrix24Client client)
		:base(client, EntryPointPrefix.Activity)
	{
	}
}