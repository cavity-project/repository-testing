namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
#if !NET20
    using System.Linq;
#endif
    using System.Reflection;

    public sealed class RepositoryExpectations<T>
        where T : new()
    {
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This design is required to make activation work.")]
        public void VerifyAll<TRepository>() where TRepository : IRepository<T>
        {
            VerifyAll<TRepository>(GetType().Assembly);
        }

        private static void VerifyAll<TRepository>(Assembly assembly) where TRepository : IRepository<T>
        {
#if NET20
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract)
                {
                    continue;
                }

                if (type.IsInterface)
                {
                    continue;
                }

                if (null == type.GetInterface(typeof(IVerifyRepository<T>).Name))
                {
                    continue;
                }

                var obj = Activator.CreateInstance(type.MakeGenericType(typeof(T))) as IVerifyRepository<T>;
                if (null != obj)
                {
                    obj.Verify(Activator.CreateInstance<TRepository>());
                }
            }
#else
            foreach (var obj in assembly.GetTypes()
                                        .Where(x => !x.IsAbstract && !x.IsInterface && null != x.GetInterface(typeof(IVerifyRepository<T>).Name))
                                        .Select(type => Activator.CreateInstance(type.MakeGenericType(typeof(T)))).OfType<IVerifyRepository<T>>())
            {
                obj.Verify(Activator.CreateInstance<TRepository>());
            }

#endif
        }
    }
}