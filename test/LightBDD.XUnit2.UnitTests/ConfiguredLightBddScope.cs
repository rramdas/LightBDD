﻿using System.Collections.Concurrent;
using LightBDD.Core.Configuration;
using LightBDD.Framework.Notification;
using LightBDD.Framework.Notification.Configuration;
using LightBDD.XUnit2.UnitTests;

[assembly: ConfiguredLightBddScope]
namespace LightBDD.XUnit2.UnitTests
{
    class ConfiguredLightBddScope : LightBddScopeAttribute
    {
        public static readonly ConcurrentQueue<string> CapturedNotifications = new ConcurrentQueue<string>();
        protected override void OnConfigure(LightBddConfiguration configuration)
        {
            var defaultProvider = configuration.ScenarioProgressNotifierConfiguration().NotifierProvider;

            configuration
                .ScenarioProgressNotifierConfiguration()
                .UpdateNotifierProvider<object>(feature => new DelegatingScenarioProgressNotifier(defaultProvider(feature), new DefaultProgressNotifier(x => CapturedNotifications.Enqueue(x))));
        }
    }
}