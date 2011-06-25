using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DrWPF.Windows.Data;
using System.Collections.Specialized;

namespace WomenCalendar
{
    /// <summary>
    /// The base class for meny business logic collections. This one is very important. 
    /// It can (de-)serialize to xml and clone itself.
    /// </summary>
    /// <typeparam name="DataT">The type we are going to store in the collection.</typeparam>
    public abstract class SerializableEventsCollection<DataT> : ObservableDictionary<DateTime, DataT>, IXmlSerializable, ICloneable
    {
        /// <summary>
        /// This is the single node name. Used for nice looking name in xml file.
        /// </summary>
        private string oneNodeName;

        /// <summary>
        /// The constructor of the collection.
        /// </summary>
        /// <param name="oneNodeName">XML node name of one collection item.</param>
        public SerializableEventsCollection(string oneNodeName)
        {
            this.oneNodeName = oneNodeName;
        }

        #region IXmlSerializable Members
        /// <summary>
        /// Returns null.
        /// </summary>
        /// <returns>null because of no use.</returns>
        public virtual System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Read xml data from the given stream.
        /// </summary>
        /// <param name="reader">The reader object (stream).</param>
        public virtual void ReadXml(System.Xml.XmlReader reader)
        {
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty)
            {
                return;
            }

            XmlSerializer keySerializer = new XmlSerializer(typeof(DateTime));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(DataT));

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement(this.oneNodeName);
                this.Add((DateTime)keySerializer.Deserialize(reader), (DataT)valueSerializer.Deserialize(reader));
                reader.ReadEndElement();
                reader.MoveToContent();
            }

            reader.ReadEndElement();
        }

        /// <summary>
        /// Writes xml data to the given stream.
        /// </summary>
        /// <param name="writer">The writer object (stream).</param>
        public virtual void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(DateTime));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(DataT));

            foreach (KeyValuePair<DateTime, DataT> item in this)
            {
                writer.WriteStartElement(this.oneNodeName);
                keySerializer.Serialize(writer, item.Key);
                valueSerializer.Serialize(writer, item.Value);
                writer.WriteEndElement();
            }
        }
        #endregion

        #region ICloneable Members

        /// <summary>
        /// Creates copy of itself.
        /// </summary>
        /// <returns>The object copy.</returns>
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

        /// <summary>
        /// Compares collection to the given one.
        /// </summary>
        /// <param name="obj">Collection to compare with.</param>
        /// <returns>True if all the data is equal.</returns>
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

        /// <summary>
        /// Calc hash code.
        /// </summary>
        /// <returns>Object hash code.</returns>
        public override int GetHashCode()
        {
            return this.Count ^ this.Aggregate(0, (seed, pair) => seed ^ pair.Key.GetHashCode() ^ pair.Value.GetHashCode());
        }
    }
}
