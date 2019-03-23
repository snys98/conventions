using System;
using GraphQL.Subscription;
using GraphQL.Types;

namespace GraphQL.Conventions.Abstractions
{
    public abstract class SubscriptionBase
    {
        /// <summary>
        /// subscription执行的上下文
        /// </summary>
        public ResolveEventStreamContext ResolveEventStreamContext { get; set; }

        /// <summary>
        /// 参数获取的上下文
        /// </summary>
        public ResolveFieldContext ResolveFieldContext { get; set; }
    }
}