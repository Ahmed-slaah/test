using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Core.IHelper
{
    public interface ISessionService
    {
        public bool CreateSession(Dictionary<string, string> cookies);
        public bool CreateSession(string key, string value);
        public string GetSession(string Key);
    }
}
