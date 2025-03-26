using System;
using System.CodeDom;
using System.Threading;
using System.Web.Compilation;
using System.Web.UI;

namespace WeblateDotNetPoc
{
    [ExpressionPrefix("Resources")]
    public class ResourceExpressionBuilder : ExpressionBuilder
    {
        public ResourceExpressionBuilder()
        { }
        
        public override bool SupportsEvaluate
        {
            get { return true; }
        }

     
        public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData,
            ExpressionBuilderContext context)
        {
            if ((parsedData != null && ReferenceEquals(parsedData.GetType(), typeof (string))))
            {
                return GetRequestedValue(Convert.ToString(parsedData));
            }
            return base.EvaluateExpression(target, entry, parsedData, context);
        }

        public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData,
            ExpressionBuilderContext context)
        {
            CodeExpression[] inputParams = {new CodePrimitiveExpression(entry.Expression.Trim())};
            return new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(GetType()), "GetRequestedValue",
                inputParams);
        }


        public static object GetRequestedValue(string expression)
        {
            var key = expression.Split(',')[1].Trim();
            return TranslationMediator.GetTranslationByKey(key);
        }
    }
}