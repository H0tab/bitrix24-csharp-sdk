﻿using System;
using Flurl;
using Flurl.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using Bitrix24RestApiClient.Core.Models.Enums;
using System.Threading;

namespace Bitrix24RestApiClient.Core.Client;

/// <summary>
/// Bitrix24 client for sending Http POST responses
/// </summary>
public class Bitrix24Client: IBitrix24Client
{
    private static DateTimeOffset _lastRequestDate = DateTimeOffset.UtcNow;
    private static readonly TimeSpan DelayBetweenSeqRequests = TimeSpan.FromMilliseconds(1000);
    private static readonly SemaphoreSlim ImportEntitiesSemaphore = new (1, 1);
        
    private readonly string webhookUrl;
    private ILogger<Bitrix24Client> logger;

    /// <summary>
    /// Create new Bitrix24Client
    /// </summary>
    /// <param name="webhookUrl">bitrix webhook url</param>
    /// <param name="logger">logger</param>
    public Bitrix24Client(string webhookUrl, ILogger<Bitrix24Client> logger)
    {
        this.webhookUrl = webhookUrl;
        this.logger = logger;
    }

    public async Task<TResponse> SendPostRequest<TArgs, TResponse>(EntryPointPrefix entityTypePrefix, EntityMethod entityMethod, TArgs args, CancellationToken ct = default) where TResponse : class
    {
        await ImportEntitiesSemaphore.WaitAsync(ct);

        var passedTime = DateTimeOffset.UtcNow - _lastRequestDate;
        if (DelayBetweenSeqRequests > passedTime)
        {
            var delay = DelayBetweenSeqRequests - passedTime;
            await Task.Delay(delay, ct);
        }
        _lastRequestDate = DateTimeOffset.UtcNow;

        string responseBodyStr;

        try
        {
            var response = await webhookUrl
                .AppendPathSegment(GetMethod(entityTypePrefix, entityMethod))
                .PostJsonAsync(args, cancellationToken: ct);

            var responseBody = await response.GetJsonAsync<TResponse>();
            responseBodyStr = JsonConvert.SerializeObject(responseBody);
                
            logger.LogInformation("Got response body: {responseBodyStr}", responseBodyStr);
                
            return responseBody;
        }
        catch (FlurlHttpException ex)
        {
            if (ex.Call.Response == null)
                throw;

            responseBodyStr = Regex.Unescape(await ex.Call.Response.GetStringAsync());
            throw new Exception(responseBodyStr, ex);
        }
        finally
        {
            ImportEntitiesSemaphore.Release();
        }
    }

    private static string GetMethod(EntryPointPrefix entityTypePrefix, EntityMethod method) => 
        method.Value == EntityMethod.None.Value 
            ? entityTypePrefix.Value 
            : $"{entityTypePrefix.Value}.{method.Value}";
}