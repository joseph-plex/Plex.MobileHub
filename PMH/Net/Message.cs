using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Plex.PMH.Net
{
    public class Message
    {
        public Int32 Id;
        public String Name;

        public Message()
        {
        }
        public Message(XmlDocument document) : this()
        {
            Id = Convert.ToInt32(document.DocumentElement.Attributes["Id"].Value);
            Name = document.DocumentElement.Name;
        }
        public Message(String document) : this(new XmlDocument(){InnerXml = document}) { }
        public Message(Byte[] document) : this(Encoding.ASCII.GetString(document)) { }

        /// <summary>
        /// Converts the current object into an Xml Document and convert's the xml Document to bytes.
        /// </summary>
        /// <returns>returns it's ASCII conversion in bytes</returns>
        public Byte[] GetBytes()
        {
            return Encoding.ASCII.GetBytes(ToString());
        }

        /// <summary>
        /// Convert's this class into a customized XML Document
        /// </summary>
        /// <returns>Xml Document representing this class </returns>
        public override string ToString()
        {
            StringWriter sw = new StringWriter();
            XmlDocument x = new XmlDocument();

            x.AppendChild(x.CreateElement(this.Name)).Attributes.Append(x.CreateAttribute("Id")).Value = Id.ToString();
            x.Save(sw);
           
            return sw.ToString();
        }
    }
}
