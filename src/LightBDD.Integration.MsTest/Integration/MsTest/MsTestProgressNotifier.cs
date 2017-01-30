﻿using System;
using System.Diagnostics;
using LightBDD.Core.Notification;
using LightBDD.Notification;

namespace LightBDD.Integration.MsTest
{
    [DebuggerStepThrough]
    internal class MsTestProgressNotifier
    {
        public static IFeatureProgressNotifier CreateFeatureProgressNotifier()
        {
            return ParallelProgressNotifierProvider.Default.CreateFeatureProgressNotifier(Console.WriteLine);
        }

        public static IScenarioProgressNotifier CreateScenarioProgressNotifier()
        {
            return ParallelProgressNotifierProvider.Default.CreateScenarioProgressNotifier(Console.WriteLine);
        }
    }
}
