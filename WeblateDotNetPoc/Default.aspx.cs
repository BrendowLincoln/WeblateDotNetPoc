using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeblateDotNetPoc
{
    public partial class _Default : Page
    {
        private readonly ITranslationMediator _mediator;

        public _Default()
        {
            _mediator = new TranslationMediator();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            var context = HttpContext.Current;
            if (context == null || context.Session == null)
                return;

            string userLang = context.Session["UserCulture"] as string;
            if (!string.IsNullOrWhiteSpace(userLang))
            {
                var culture = new CultureInfo(userLang);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        protected void btnCarregarTraducao_Click(object sender, EventArgs e)
        {
            string languageCode = txtLanguageCode.Text?.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(languageCode))
            {
                Session["UserCulture"] = languageCode;

                _mediator.InitializeLanguages(languageCode);
            }
        }
    }
}