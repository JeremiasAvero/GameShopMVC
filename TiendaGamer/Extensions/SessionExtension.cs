
using Newtonsoft.Json;

namespace TiendaGamer.Extensions
{
    public static class SessionExtension
    {
        public static void SetObject(this ISession session, String Key, object value)
        {
            String data = JsonConvert.SerializeObject(value);
            session.SetString(Key, data);   
        }

        public static T GetObject<T>(this ISession session, String Key)
        {
            String data = session.GetString(Key);
            if(data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
