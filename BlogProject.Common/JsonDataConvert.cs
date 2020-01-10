using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace BlogProject.Common
{
    public static class JsonDataConvert
    {
        public static string SerializeObject<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }
        public static T DeSerializeObject<T>(string json)
        {
            var setting = new JsonSerializerSettings
            {
                ContractResolver = new PublicContractResolver()
            };
            return JsonConvert.DeserializeObject<T>(json, setting);
        }

        private sealed class PublicContractResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var jProperty = base.CreateProperty(member, memberSerialization);
                jProperty.Writable = true;
                return jProperty;
            }
        }
    }
}
