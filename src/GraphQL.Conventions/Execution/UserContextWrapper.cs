using System;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQL.Conventions.Execution
{
    public class UserContextWrapper
    {
        public static UserContextWrapper Create(IUserContext userContext, IServiceProvider serviceProvider)
        {
            if (userContext is IDataLoaderContextProvider)
            {
                return new UserContextWithDataLoaderContextProvider
                {
                    UserContext = userContext,
                    ServiceProvider = serviceProvider
                };
            }

            return new UserContextWrapper
            {
                UserContext = userContext,
                ServiceProvider = serviceProvider
            };
        }

        public IUserContext UserContext { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        private class UserContextWithDataLoaderContextProvider : UserContextWrapper, IDataLoaderContextProvider
        {
            public Task FetchData(CancellationToken token) => (UserContext as IDataLoaderContextProvider)?.FetchData(token) ?? Task.FromResult(0);
        }
    }
}
