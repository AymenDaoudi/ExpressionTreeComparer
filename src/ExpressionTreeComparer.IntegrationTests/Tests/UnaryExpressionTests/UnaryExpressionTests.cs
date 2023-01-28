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
using ExpressionTreeComparer.IntegrationTests.Common;
using ExpressionTreeComparer.IntegrationTests.MockData;
using FluentAssertions;

namespace ExpressionTreeComparer.IntegrationTests.Tests.UnaryExpressionTests
{
    public class UnaryExpressionTests : TestBase
    {
        [Fact]
        public void When_Same_Unary_Operator_With_Same_Operands_Then_Expressions_Should_Be_Equal()
        {
            //Arrange
            Expression<Func<Person, bool>> expression1 = p => !p.IsDeleted;
            Expression<Func<Person, bool>> expression2 = p => !p.IsDeleted;

            //Act
            var equal = expression1.IsEquivalentTo(expression2);

            //Assert
            equal.Should().BeTrue();
        }

        [Fact]
        public void When_Same_Unary_Operator_With_Different_Operands_Then_Expressions_Should_NOT_Be_Equal()
        {
            //Arrange
            Expression<Func<Person, bool>> expression1 = p => !p.IsDeleted;
            Expression<Func<Person, bool>> expression2 = p => !p.IsDisabled;

            //Act
            var equal = expression1.IsEquivalentTo(expression2);

            //Assert
            equal.Should().BeFalse();
        }

        [Fact]
        public void When_Different_Unary_Operator_With_Same_Operands_Then_Expressions_Should_NOT_Be_Equal()
        {
            //Arrange
            Expression<Func<Person, object>> expression1 = p => !p.IsDeleted;
            Expression<Func<Person, object>> expression2 = p => nameof(p.IsDeleted);

            //Act
            var equal = expression1.IsEquivalentTo(expression2);

            //Assert
            equal.Should().BeFalse();
        }

        [Fact]
        public void When_Different_Unary_Operator_With_Different_Operands_Then_Expressions_Should_NOT_Be_Equal()
        {
            //Arrange
            Expression<Func<Person, object>> expression1 = p => !p.IsDeleted;
            Expression<Func<Person, object>> expression2 = p => nameof(p.IsDisabled);

            //Act
            var equal = expression1.IsEquivalentTo(expression2);

            //Assert
            equal.Should().BeFalse();
        }
    }
}
