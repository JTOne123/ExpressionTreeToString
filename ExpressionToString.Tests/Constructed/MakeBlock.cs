﻿using Xunit;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;
using System.Linq.Expressions;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeBlock {
        [Fact]
        public void BlockNoVariables() => RunTest(
            Block(
                Constant(true),
                Constant(true)
            ),
            @"true;
true;",
            @"True
True"
        );

        [Fact]
        public void BlockSingleVariable() => RunTest(
            Block(
                new[] { i },
                Constant(true),
                Constant(true)
            ),
            @"{
    int i;
    true;
    true;
}",
            @"Block
    Dim i As Integer
    True
    True
End Block"
        );

        [Fact]
        public void BlockMultipleVariable() => RunTest(
            Block(
                new[] { i,s1 },
                Constant(true),
                Constant(true)
            ),
            @"{
    int i;
    string s1;
    true;
    true;
}",
    @"Block
    Dim i As Integer
    Dim s1 As String
    True
    True
End Block"
        );

        [Fact]
        public void EmptyBlock() => RunTest(
            Block(),
            "",
            ""
        );

        [Fact]
        public void BlockOnlyVariables() => RunTest(
            Block(
                new[] { i, s1 },
                new Expression[] { }
            ),
            @"{
    int i;
    string s1;
}",
            @"Block
    Dim i As Integer
    Dim s1 As String
End Block"
        );



    }
}
