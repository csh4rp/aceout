using Aceout.Tools.Helpers;
using System;
using Xunit;
using ProtoBuf;
using System.Text;

namespace Aceout.Tests.Tools
{
    public class SerializationTests
    {
        [Fact]
        public void Serialization_Json_CanSerialize()
        {
            var obj = new TestClass
            {
                Id = 12,
                Name = "Name"
            };

            var json = SerializationHelper.SerializeJson(obj);
            var objDeserialized = SerializationHelper.DeserializeJson<TestClass>(json);

            Assert.Equal(12, objDeserialized.Id);
        }

        [Fact]
        public void Serialization_Binary_CanSerialize()
        {
            var obj = new TestClass
            {
                Id = 12,
                Name = "Name"
            };

            var data = SerializationHelper.SerializeBinary(obj);
            var objDeserialized = SerializationHelper.DeserializeBinary<TestClass>(data);

            Assert.Equal(12, objDeserialized.Id);
        }

        [Fact]
        public void Serialization_Xml_CanSerialize()
        {
            var obj = new TestClass
            {
                Id = 12,
                Name = "Name"
            };

            var data = SerializationHelper.SerializeXml(obj);
            var objDeserialized = SerializationHelper.DeserializeXml<TestClass>(data);

            Assert.Equal(12, objDeserialized.Id);
        }
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllFields), Serializable]
    public class TestClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
