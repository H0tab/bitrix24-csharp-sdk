﻿using Bitrix24ApiClient.src.Builders;
using Bitrix24ApiClient.src.Models;
using Bitrix24ApiClient.src.Models.Crm.Timeline.Comment;
using Bitrix24RestApiClient.src.Models.Crm.Core.Client;
using System;
using System.Threading.Tasks;

namespace Bitrix24ApiClient.src
{
    public class TimelineComments
    {
        private IBitrix24Client client;
        public TimelineComments(IBitrix24Client client)
        {
            this.client = client;
        }

        public async Task<AddResponse> Add(string entityType, int entityId, string comment)
        {
            var builder = new RequestBuilder<TimelineComment>();
            return await builder
                .WithClient(client)
                .WithEntityType(EntityType.TimelineComment)
                .SetField(TimelineCommentFields.EntityType, entityType)
                .SetField(TimelineCommentFields.EntityId, entityId.ToString())
                .SetField(TimelineCommentFields.Comment, comment)
                .Add();
        }
    }
}
