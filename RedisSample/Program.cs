﻿using System;
using RedisSample;
using WeihanLi.Common.Helpers;
using WeihanLi.Common.Logging;

LogHelper.ConfigureLogging(builder => builder.AddConsole());

//await SimpleStreamUsage.MainTest();
await StreamConsumerGroupSample.MainTest();

Console.WriteLine("Completed");
Console.ReadLine();
