using Newtonsoft.Json;
using System.IO;
using ProtoBuf;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text;
using System.Xml;

namespace Aceout.Tools.Helpers
{
    public class SerializationHelper
    {
        public static string SerializeJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T DeserializeJson<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static byte[] SerializeBinary<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T DeserializeBinary<T>(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                return Serializer.Deserialize<T>(ms);
            }
        }

        public static XElement SerializeXml<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            var nameSpaces = new XmlSerializerNamespaces(new XmlQualifiedName[]
            {
                new XmlQualifiedName("","")
            });

            using (var ms = new MemoryStream())
            {
                serializer.Serialize(ms, obj, nameSpaces);
                var str = Encoding.UTF8.GetString(ms.ToArray());
                return XElement.Parse(str);
            }

        }

        public static T DeserializeXml<T>(XElement data)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var ms = new StringReader(data.ToString()))
            {
                return (T)serializer.Deserialize(ms);
            }
        }
    }
}
