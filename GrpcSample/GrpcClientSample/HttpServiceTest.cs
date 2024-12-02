﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WeihanLi.Common.Helpers;

namespace GrpcClientSample
{
    internal class HttpServiceTest
    {
        public static async Task MainTest()
        {
            using var client = new HttpClient()
            {
                DefaultRequestVersion = HttpVersion.Version20,
                DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher,
            };
            await InvokeHelper.TryInvokeAsync(async () =>
            {
                var responseText = await client.GetStringAsync("https://localhost:5001/v1/greeter/test");
                Console.WriteLine($"Response from https endpoint: {responseText}");
            });
            await InvokeHelper.TryInvokeAsync(async () =>
            {
                var responseText = await client.GetStringAsync("http://localhost:5000/v1/greeter/test");
                Console.WriteLine($"Response from http endpoint: {responseText}");
            });

            //
            await InvokeHelper.TryInvokeAsync(async () =>
            {
                var responseText = await client.GetStringAsync("http://localhost:5000/v1/todo");
                Console.WriteLine($"Response from todo endpoint: {responseText}");
            });
        }
    }
}
