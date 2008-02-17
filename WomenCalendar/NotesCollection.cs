using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace WomenCalendar
{
    [XmlRoot("Notes")]
    public class NotesCollection
        : Dictionary<DateTime, string>, IXmlSerializable
    {
        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public NotesCollection()
        {
        }

        public NotesCollection(string xml) : base(FromXml(xml))
        {
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty)
                return;

            XmlSerializer keySerializer = new XmlSerializer(typeof(DateTime));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(string));

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("Note");
                this.Add((DateTime)keySerializer.Deserialize(reader), (string)valueSerializer.Deserialize(reader));
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }



        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(DateTime));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(string));
            foreach (KeyValuePair<DateTime, string> item in this)
            {
                writer.WriteStartElement("Note");
                keySerializer.Serialize(writer, item.Key);
                valueSerializer.Serialize(writer, item.Value);
                writer.WriteEndElement();
            }
        }

        public string ToXml()
        {
            StringWriter sw = new StringWriter();
            XmlSerializer xs = new XmlSerializer(this.GetType());
            xs.Serialize(sw, this);
            string Result = sw.ToString();
            sw.Close();
            return Result;
        }

        static public NotesCollection FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return default(NotesCollection);
            StringReader reader = new StringReader(xml);
            XmlSerializer sr = new XmlSerializer(typeof(NotesCollection));
            return (NotesCollection)sr.Deserialize(reader);
        }
        #endregion

    }
}