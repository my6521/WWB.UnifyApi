using System;
using System.ComponentModel;
using Xunit;

namespace WWB.UnifyApi.Tests
{
    public enum Errors
    {
        [Description("δ֪����")]
        None,

        TestNotDesc,

        [Description("δ֪����{0}{1}")]
        TestWithFormat,
    }
}