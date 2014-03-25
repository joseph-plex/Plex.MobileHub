using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace MobileHub.Repositories
{
    public sealed class ResponseCompound : Response
    {
        Dictionary<int, ResponseComponent> components = new Dictionary<int, ResponseComponent>();

        public ResponseCompound()
        {
            IsComplete = false;
        }
        public bool IsComplete
        {
            get;
            set;

        }

        public override string Resp
        {
            get
            {
                if (!IsComplete) throw new InvalidOperationException("ResponseCompound LastComponent Flag has not been set");
                return base.Resp;
            }
            set
            {
                base.Resp = value;
            }
        }

        public void Add(ResponseComponent Component)
        {
            components.Add(Component.ComponentId, Component);
            if (Component.IsLastComponent)
            {
                string response = string.Empty;
                for (int i = 0; i < components.Values.Count; i++)
                    if (!components.ContainsKey(i)) throw new FormatException("Response is missing a component");
                    else response += components[i].Resp;
                Resp = response;
                IsComplete = true;
            }
        }
    }
}