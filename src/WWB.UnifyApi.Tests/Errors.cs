using System;
using System.ComponentModel;
using Xunit;

namespace WWB.UnifyApi.Tests
{
    public enum Errors
    {
        [Description("Î´Öª´íÎó")]
        None,

        TestNotDesc,

        [Description("Î´Öª´íÎó{0}{1}")]
        TestWithFormat,
    }
}