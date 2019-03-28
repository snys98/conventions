using System;
using GraphQL.Conventions.Types.Descriptors;

namespace GraphQL.Conventions.Adapters
{
    public interface IGraphTypeAdapter<TSchemaType, TGraphType>
    {
        IServiceProvider ServiceProvider { get; }
        TSchemaType DeriveSchema(GraphSchemaInfo schemaInfo);

        TGraphType DeriveType(GraphTypeInfo typeInfo);

        void RegisterScalarType<TType>(string name);
    }
}
