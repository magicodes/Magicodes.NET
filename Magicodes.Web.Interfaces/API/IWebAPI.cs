using Magicodes.Web.Interfaces.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.API
{
    public interface IWebAPI
    {
        OperationResult OperationResult{ get; set; }
        // GET api/T
        OperationResult Get(string[] paramsStrs);

        // GET api/T/5
        OperationResult Get(string id);

        // POST api/T
        OperationResult Post(string json);

        // PUT api/T/5
        OperationResult Put(string id, string json);

        // DELETE api/T/5
        OperationResult Delete(string id);
    }
}
