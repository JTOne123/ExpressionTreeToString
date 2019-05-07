﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static ExpressionToString.Util.Functions;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {

        [Fact]
        public void InstanceMethod0Arguments() => RunTest(
            Call(s, GetMethod(() => "".ToString())),
            "s.ToString()",
            "s.ToString"
        );

        [Fact]
        public void StaticMethod0Arguments() => RunTest(
            Call(GetMethod(() => Dummy.DummyMethod())),
            "Dummy.DummyMethod()",
            "Dummy.DummyMethod"
        );

        [Fact]
        public void ExtensionMethod0Arguments() => RunTest(
            Call(GetMethod(() => ((List<string>)null).Count()), lstString),
            "lst.Count()",
            "lst.Count"
        );

        [Fact]
        public void InstanceMethod1Argument() => RunTest(
            Call(s, GetMethod(() => "".CompareTo("")), Constant("")),
            "s.CompareTo(\"\")",
            "s.CompareTo(\"\")"
        );

        [Fact]
        public void StaticMethod1Argument() => RunTest(
            Call(GetMethod(() => string.Intern("")), Constant("")),
            "string.Intern(\"\")",
            "String.Intern(\"\")"
        );

        [Fact]
        public void ExtensionMethod1Argument() => RunTest(
            Call(GetMethod(() => (null as List<string>).Take(1)), lstString, Constant(1)),
            "lst.Take(1)",
            "lst.Take(1)"
        );


        [Fact]
        public void InstanceMethod2Arguments() => RunTest(
            Call(
                s,
                GetMethod(() => "".IndexOf('a', 2)),
                Constant('a'),
                Constant(2)
            ),
            "s.IndexOf('a', 2)",
            "s.IndexOf(\"a\"C, 2)"
        );

        [Fact]
        public void StaticMethod2Arguments() => RunTest(
            Call(
                GetMethod(() => string.Join(",", new[] {"a","b" })),
                Constant(","),
                NewArrayInit(typeof(string), Constant("a"), Constant("b"))
            ),
            "string.Join(\",\", new [] { \"a\", \"b\" })",
            "String.Join(\",\", { \"a\", \"b\" })"
        );

        [Fact]
        public void ExtensionMethod2Arguments() {
            var x = Parameter(typeof(string), "x");
            RunTest(
                Call(
                    GetMethod(() => (null as List<string>).OrderBy(y => y, StringComparer.OrdinalIgnoreCase)),
                    lstString,
                    Lambda(x, x),
                    MakeMemberAccess(null, typeof(StringComparer).GetMember("OrdinalIgnoreCase").Single())
                ),
                "lst.OrderBy((string x) => x, StringComparer.OrdinalIgnoreCase)",
                "lst.OrderBy(Function(x As String) x, StringComparer.OrdinalIgnoreCase)"
            );
        }

        [Fact]
        public void StringConcat() => RunTest(
            Call(
                GetMethod(() => string.Concat("", "")),
                s1,
                s2
            ),
            "s1 + s2",
            "s1 + s2"
        );
    }
}
