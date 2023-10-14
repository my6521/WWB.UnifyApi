using Microsoft.AspNetCore.Mvc;
using WWB.UnifyApi.Attributes;

namespace WWB.UnifyApi.Tests
{
    public class LogFilterTests : OperateFilter
    {
        public override void Log(ActionContext context, OperateModel operateModel)
        {
            base.Log(context, operateModel);
        }
    }
}