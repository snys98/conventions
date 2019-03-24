using System;
using System.Reflection;
using GraphQL.Conventions;
using GraphQL.Conventions.Adapters;
using GraphQL.Conventions.Builders;
using GraphQL.Conventions.Web;
using GraphQL.Subscription;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Conventions.Extensions
{
    public static class Utilities
    {
        public static void AddSchema<TSchemaDefinition>(this IServiceCollection services)
        {
            if (!typeof(TSchemaDefinition).Name.Contains("SchemaDefinition"))
            {
                throw new ArgumentException("TSchemaDefinition must inherit from SchemaDefinition");
            }
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            var typeAdapter = new GraphTypeAdapter();
            var constructor = new SchemaConstructor<ISchema, IGraphType>(typeAdapter);
            var schema = constructor.Build(
                typeof(TSchemaDefinition));
            services.AddSingleton(schema);
        }
        public static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        public static bool IsContextParam(this ParameterInfo parameterInfo)
        {
            return parameterInfo.ParameterType == typeof(ResolveEventStreamContext) || parameterInfo.ParameterType == typeof(ResolveFieldContext);
        }

        public static string IdentifierForTypeOrNull<T>(this string id) =>
            id.IsIdentifierForType<T>() ? id.IdentifierForType<T>() : null;

        public static string IdentifierForTypeOrNull<T>(this NonNull<string> id) =>
            id.IsIdentifierForType<T>() ? id.IdentifierForType<T>() : null;

        public static string IdentifierForType<T>(this string id) =>
            new Id(id).IdentifierForType<T>();

        public static string IdentifierForType<T>(this NonNull<string> id) =>
            id.Value.IdentifierForType<T>();

        public static bool IsIdentifierForType<T>(this string id) =>
            new Id(id).IsIdentifierForType<T>();

        public static bool IsIdentifierForType<T>(this NonNull<string> id) =>
            id.Value.IsIdentifierForType<T>();
    }
}
