using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WomenCalendar
{
    public abstract class SerializableEventsCollection<DataT> : Dictionary<DateTime, DataT>, IXmlSerializable, ICloneable
    {
        private string OneNodeName;
        public SerializableEventsCollection(string oneNodeName)
        {
            OneNodeName = oneNodeName;
        }

        #region IXmlSerializable Members
        public virtual System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(System.Xml.XmlReader reader)
        {
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty)
                return;

            XmlSerializer keySerializer = new XmlSerializer(typeof(DateTime));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(DataT));

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement(OneNodeName);
                this.Add((DateTime)keySerializer.Deserialize(reader), (DataT)valueSerializer.Deserialize(reader));
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public virtual void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(DateTime));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(DataT));

            foreach (KeyValuePair<DateTime, DataT> item in this)
            {
                writer.WriteStartElement(OneNodeName);
                keySerializer.Serialize(writer, item.Key);
                valueSerializer.Serialize(writer, item.Value);
                writer.WriteEndElement();
            }
        }
        #endregion

        #region ICloneable Members

        public virtual object Clone()
        {
            var copy = Activator.CreateInstance(this.GetType()) as SerializableEventsCollection<DataT>;
            foreach (var item in this)
            {
                copy.Add(item.Key, item.Value);
            }
            return copy;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            SerializableEventsCollection<DataT> secondValue = obj as SerializableEventsCollection<DataT>;
            if (secondValue.Count != this.Count)
            {
                return false;
            }
            foreach (var item in secondValue)
            {
                if (!this.ContainsKey(item.Key) || !this[item.Key].Equals(item.Value))
                { // a key is absent or value is not equal
                    return false;
                }
            }
            return true;
        }
    }
}
