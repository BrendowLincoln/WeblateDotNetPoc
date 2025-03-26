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
        private static ITranslationMediator _translationMediator;
        public TranslationController()
        {
            _translationMediator = new TranslationMediator();
        }
        
        [HttpGet]
        [Route("{languageCode}")]
        public IHttpActionResult GetTranslation(string languageCode)
        {
            var result = _translationMediator.GetTranslationsByLanguage(languageCode);
            return Ok(result);
        }

        
    }
}
