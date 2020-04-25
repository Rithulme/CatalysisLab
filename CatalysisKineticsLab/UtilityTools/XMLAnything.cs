using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace UtilityTools
{
    //This class is needed to allow the use of interfaces in a class that needs to be able to be serialized in an XML-file
    //To use:
    //  - Instead of a property public IFoo ...,  use public XmlAnything<IFoo> Foo...
    //  - To declare this class: new XmlAnything<T>(t);
    //  - Use via: Foo.Value. ...
    //  - The implementation of IFoo needs a constructor without parameters

    public sealed class XmlAnything<T> : IXmlSerializable
    {
        public XmlAnything() { }
        public XmlAnything(T t) { this.Value = t; }
        public T Value { get; set; }

        public void WriteXml(XmlWriter writer)
        {
            if (Value == null)
            {
                writer.WriteAttributeString("type", "null");
                return;
            }
            Type type = this.Value.GetType();
            XmlSerializer serializer = new XmlSerializer(type);
            writer.WriteAttributeString("type", type.AssemblyQualifiedName);
            serializer.Serialize(writer, this.Value);
        }

        public void ReadXml(XmlReader reader)
        {
            if (!reader.HasAttributes)
                throw new FormatException("expected a type attribute!");
            string type = reader.GetAttribute("type");
            reader.Read(); // consume the value
            if (type == "null")
                return;// leave T at default value
            XmlSerializer serializer = new XmlSerializer(Type.GetType(type));
            this.Value = (T)serializer.Deserialize(reader);
            reader.ReadEndElement();
        }

        public XmlSchema GetSchema() { return (null); }
    }
}
