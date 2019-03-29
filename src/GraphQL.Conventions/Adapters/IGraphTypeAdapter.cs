using System;
using GraphQL.Conventions.Types.Descriptors;
using GraphQL.Types;

namespace GraphQL.Conventions.Adapters
{
    public interface IGraphTypeAdapter
    {
        ISchema DeriveSchema(GraphSchemaInfo schemaInfo);

        IGraphType DeriveType(GraphTypeInfo typeInfo);

        void RegisterScalarType<TType>(string name);
    }
}
