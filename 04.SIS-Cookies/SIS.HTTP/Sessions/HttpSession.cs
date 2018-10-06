using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        private Dictionary<string, object> parameters;

        public HttpSession(string id)
        {
            this.Id = id;
            this.parameters = new Dictionary<string, object>();
        }

        public string Id { get; }

        public void AddParameter(string name, object parameter)
        {
            if(name != null && parameter != null)
            {
                this.parameters[name] = parameter;
            }
        }

        public void ClearParameters()
        {
            this.parameters.Clear();
        }

        public bool ContainsParameter(string name)
        {
            if(name != null)
            {
                return this.parameters.ContainsKey(name);
            }

            return false;
        }

        public object GetParameter(string name)
        {
            if(name != null && this.parameters.ContainsKey(name))
            {
                return this.parameters[name];
            }

            return null;
        }
    }
}
