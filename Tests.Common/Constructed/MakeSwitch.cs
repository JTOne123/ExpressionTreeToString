﻿using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        static readonly SwitchCase singleValueCase = SwitchCase(
            Block(writeLineTrue, writeLineTrue),
            Constant(5)
        );
        static readonly SwitchCase multiValueCase = SwitchCase(
            Block(writeLineTrue, writeLineTrue),
            Constant(5),
            Constant(6)
        );

        [Fact]
        [Trait("Category", SwitchCases)]

        public void SingleValueSwitchCase() => RunTest(
            singleValueCase,
            @"case 5:
    Console.WriteLine(true);
    Console.WriteLine(true);
    break;",
            @"Case 5
    Console.WriteLine(True)
    Console.WriteLine(True)",
            @"SwitchCase(
    Block(
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(true)
        ),
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(true)
        )
    ),
    Constant(5)
)"

        );

        [Fact]
        [Trait("Category", SwitchCases)]
        public void MultiValueSwitchCase() => RunTest(
            multiValueCase,
            @"case 5:
case 6:
    Console.WriteLine(true);
    Console.WriteLine(true);
    break;",
            @"Case 5, 6
    Console.WriteLine(True)
    Console.WriteLine(True)",
            @"SwitchCase(
    Block(
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(true)
        ),
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(true)
        )
    ),
    Constant(5),
    Constant(6)
)"
        );

        [Fact]
        [Trait("Category", SwitchCases)]
        public void SingleValueSwitchCase1() => RunTest(
            SwitchCase(writeLineTrue, Constant(5)),
            @"case 5:
    Console.WriteLine(true);
    break;",
            @"Case 5
    Console.WriteLine(True)", 
            @"SwitchCase(
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        Constant(true)
    ),
    Constant(5)
)"
        );

        [Fact]
        [Trait("Category", SwitchCases)]
        public void MultiValueSwitchCase1() => RunTest(
            SwitchCase(writeLineTrue, Constant(5), Constant(6)),
            @"case 5:
case 6:
    Console.WriteLine(true);
    break;",
            @"Case 5, 6
    Console.WriteLine(True)",
            @"SwitchCase(
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        Constant(true)
    ),
    Constant(5),
    Constant(6)
)"
        );

        [Fact]
        [Trait("Category", SwitchCases)]
        public void SwitchOnExpressionWithDefaultSingleStatement() => RunTest(
            Switch(i, Empty(), SwitchCase(
                    writeLineTrue,
                    Constant(4)
                ), SwitchCase(
                    writeLineFalse,
                    Constant(5)
                )
            ),
            @"switch (i) {
    case 4:
        Console.WriteLine(true);
        break;
    case 5:
        Console.WriteLine(false);
        break;
    default:
        default(void);
}", @"Select Case i
    Case 4
        Console.WriteLine(True)
    Case 5
        Console.WriteLine(False)
    Case Else
        CType(Nothing, Void)
End Select", 
            @"Switch(i,
    Empty(),
    SwitchCase(
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(true)
        ),
        Constant(4)
    ),
    SwitchCase(
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(false)
        ),
        Constant(5)
    )
)"
        );

        [Fact]
        [Trait("Category", SwitchCases)]
        public void SwitchOnExpressionWithDefaultMultiStatement() => RunTest(
    Switch(i, Block(
            typeof(void),
            Constant(true),
            Constant(true)
        ), SwitchCase(
            writeLineTrue,
            Constant(4)
        ), SwitchCase(
            writeLineFalse,
            Constant(5)
        )
    ),
    @"switch (i) {
    case 4:
        Console.WriteLine(true);
        break;
    case 5:
        Console.WriteLine(false);
        break;
    default:
        true;
        true;
}", @"Select Case i
    Case 4
        Console.WriteLine(True)
    Case 5
        Console.WriteLine(False)
    Case Else
        True
        True
End Select",
    @"Switch(i,
    Block(
        typeof(void),
        Constant(true),
        Constant(true)
    ),
    SwitchCase(
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(true)
        ),
        Constant(4)
    ),
    SwitchCase(
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(false)
        ),
        Constant(5)
    )
)"
        );

        [Fact]
        [Trait("Category", SwitchCases)]
        public void SwitchOnMultipleStatementsWithDefault() => RunTest(
            Switch(Block(i, j), Block(
                    typeof(void),
                    Constant(true),
                    Constant(true)
                ), SwitchCase(
                    writeLineTrue,
                    Constant(4)
                ), SwitchCase(
                    writeLineFalse,
                    Constant(5)
                )
            ),
            @"switch (
    i,
    j
) {
    case 4:
        Console.WriteLine(true);
        break;
    case 5:
        Console.WriteLine(false);
        break;
    default:
        true;
        true;
}", @"Select Case Block
        i
        j
    End Block
    Case 4
        Console.WriteLine(True)
    Case 5
        Console.WriteLine(False)
    Case Else
        True
        True
End Select", 
            @"Switch(
    Block(i, j),
    Block(
        typeof(void),
        Constant(true),
        Constant(true)
    ),
    SwitchCase(
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(true)
        ),
        Constant(4)
    ),
    SwitchCase(
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(false)
        ),
        Constant(5)
    )
)"
        );

        [Fact]
        [Trait("Category", SwitchCases)]
        public void SwitchOnExpressionWithoutDefault() => RunTest(
            Switch(i, SwitchCase(
                    writeLineTrue,
                    Constant(4)
                ), SwitchCase(
                    writeLineFalse,
                    Constant(5)
                )
            ),
            @"switch (i) {
    case 4:
        Console.WriteLine(true);
        break;
    case 5:
        Console.WriteLine(false);
        break;
}", @"Select Case i
    Case 4
        Console.WriteLine(True)
    Case 5
        Console.WriteLine(False)
End Select", 
            @"Switch(i,
    SwitchCase(
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(true)
        ),
        Constant(4)
    ),
    SwitchCase(
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(false)
        ),
        Constant(5)
    )
)"
        );

        [Fact]
        [Trait("Category", SwitchCases)]
        public void SwitchOnMultipleStatementsWithoutDefault() => RunTest(
            Switch(Block(i, j), SwitchCase(
                    writeLineTrue,
                    Constant(4)
                ), SwitchCase(
                    writeLineFalse,
                    Constant(5)
                )
            ),
            @"switch (
    i,
    j
) {
    case 4:
        Console.WriteLine(true);
        break;
    case 5:
        Console.WriteLine(false);
        break;
}", @"Select Case Block
        i
        j
    End Block
    Case 4
        Console.WriteLine(True)
    Case 5
        Console.WriteLine(False)
End Select", 
            @"Switch(
    Block(i, j),
    SwitchCase(
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(true)
        ),
        Constant(4)
    ),
    SwitchCase(
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(false)
        ),
        Constant(5)
    )
)"
        );
    }
}
