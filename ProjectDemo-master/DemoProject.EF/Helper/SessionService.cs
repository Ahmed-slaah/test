using DemoProject.Core.Extention_Methods;
using DemoProject.Core.IHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoProject.EF.Helper
{
    public class SessionService: ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            
            _httpContextAccessor = httpContextAccessor;
        }

        public bool CreateSession(Dictionary<string, string> cookies)
        {
            try
            {
                foreach (var cooke in cookies)
                {
                    _httpContextAccessor.HttpContext.Session.SetString(cooke.Key, cooke.Value);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public bool CreateSession(string key,string value)
        {
            try
            {
                _httpContextAccessor.HttpContext.Session.SetString(key, value);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public string GetSession(string Key)
        {
            try
            {
                string value = _httpContextAccessor.HttpContext.Session.GetString(Key);
                return value;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }


}
