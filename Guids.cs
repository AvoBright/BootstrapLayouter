// Guids.cs
// MUST match guids.h
using System;

namespace AvoBright.BootstrapLayouter
{
    static class GuidList
    {
        public const string guidBootstrapLayouterPkgString = "e8060ce4-9c06-42dd-8e18-9ce62256f344";
        public const string guidBootstrapLayouterCmdSetString = "96bb7fcf-6167-4836-99c8-cae1976de3fb";

        public static readonly Guid guidBootstrapLayouterCmdSet = new Guid(guidBootstrapLayouterCmdSetString);
    };
}