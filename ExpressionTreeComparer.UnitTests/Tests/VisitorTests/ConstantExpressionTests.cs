//------------------------------------------------------------------------------------------------------------------------------------
// Author: Aymen Daoudi.
// License: The MIT License (MIT).
// Copyright (c) 2023 Aymen Daoudi.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation 
// files, to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished 
// to do so, subject to the following conditions:
//   • The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//   • THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//		 TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
//		 THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//	     TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//------------------------------------------------------------------------------------------------------------------------------------

using System.Linq.Expressions;

using ExpressionTreeComparer.EqualityComparer;
using ExpressionTreeComparer.UnitTests.Common;
using FluentAssertions;

namespace ExpressionTreeComparer.UnitTests.Tests.VisitorTests
{
    public class ConstantExpressionTests : TestBase
    {
        [Fact]
        public void When_Constant_Expression_Visited_All_Nodes_Are_Visited()
        {
            //Arrange
            var visitor = new Visitor();
            Expression<Func<int>> expression1 = () => 2;

            //Act
            Func<Expression> visitAction = () => visitor.Visit(expression1.Body);

            //Assert
            visitAction
                .Should()
                .NotThrow<NotImplementedException>();
            visitor
                .ExpressionNodes
                .Dequeue()
                .Should()
                .BeAssignableTo<ConstantExpression>();
        }
    }
}