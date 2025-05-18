using AutoMapper;

namespace ToDo.Application.Common.Mapping
{
    public interface IMapped
    {
        void ConfigureMapping(Profile profile);
    }
}
