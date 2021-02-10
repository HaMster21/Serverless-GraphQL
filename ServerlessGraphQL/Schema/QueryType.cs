using System;
using System.Collections.Generic;
using System.Text;
using HotChocolate.Types;

namespace ServerlessGraphQL.Schema
{
    class QueryType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("greeting")
                      .Resolver(() => "hello world");
        }
    }
}
