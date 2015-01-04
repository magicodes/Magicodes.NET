using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Magicodes.Core.Web.Editor
{
    public abstract class Handler
    {
        public HttpRequest Request { get; private set; }
        public HttpResponse Response { get; private set; }
        public HttpContext Context { get; private set; }
        public HttpServerUtility Server { get; private set; }
        protected Handler(HttpContext context)
        {
            Request = context.Request;
            Response = context.Response;
            Context = context;
            Server = context.Server;
        }
        public abstract void Process();
        protected void WriteJson(object response)
        {
            string jsonpCallback = Request["callback"],
            json = JsonConvert.SerializeObject(response);
            if (String.IsNullOrWhiteSpace(jsonpCallback))
            {
                Response.AddHeader("Content-Type", "text/plain");
                Response.Write(json);
            }
            else
            {
                Response.AddHeader("Content-Type", "application/javascript");
                Response.Write(String.Format("{0}({1});", jsonpCallback, json));
            }
            Response.End();
        }
    }
}
