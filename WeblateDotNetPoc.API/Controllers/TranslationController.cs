using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WeblateDotNetPoc.API.Controllers
{
    [RoutePrefix("api/Translations")]
    public class TranslationController : ApiController
    {
        public TranslationController()
        {
        }
        
        [HttpGet]
        [Route("{languageCode}")]
        public IHttpActionResult GetTranslation(string languageCode)
        {
            
            return Ok();
        }

        
    }
}
