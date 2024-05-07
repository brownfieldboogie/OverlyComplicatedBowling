using Newtonsoft.Json;

namespace OverlyComplicatedBowling.Infrastructure.Serialization
{
    public static class JsonSettings
    {
        public static readonly JsonSerializerSettings TypeNameHandlingAuto = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto
        };
    }
}
