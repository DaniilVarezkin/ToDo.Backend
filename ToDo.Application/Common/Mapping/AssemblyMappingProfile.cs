using AutoMapper;
using System.Reflection;

namespace ToDo.Application.Common.Mapping
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly) =>
            ApplyMappingFromAssembly(assembly);

        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            var typesIMapped = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i == typeof(IMapped)))
                .ToList();

            foreach (var type in typesIMapped)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("ConfigureMapping");
                methodInfo?.Invoke(instance, new object[] { this } );
            }
        }
    }
}
