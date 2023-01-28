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

namespace ExpressionTreeComparer.IntegrationTests.Tests.MethodCallExpressionTests
{
    public class MethodCallExpressionTests : TestBase
    {
        [Fact]
        public void When_Same_Type_Calls_Same_Method_With_Same_Parameter_Then_Expressions_Should_Be_Equal()
        {
            //Arrange
            Expression<Func<Person, bool>> expression1 = p => p.Do(0);
            Expression<Func<Person, bool>> expression2 = person => person.Do(0);

            //Act
            var equal = expression1.IsEquivalentTo(expression2);

            //Assert
            equal.Should().BeTrue();
        }

        [Fact]
        public void When_Same_Type_Calls_Same_Method_With_Different_Parameter_Then_Expressions_Should_NOT_Be_Equal()
        {
            //Arrange
            Expression<Func<Person, bool>> expression1 = p => p.Do(0);
            Expression<Func<Person, bool>> expression2 = person => person.Do(2);

            //Act
            var equal = expression1.IsEquivalentTo(expression2);

            //Assert
            equal.Should().BeFalse();
        }

        [Fact]
        public void When_Different_Types_Call_Same_Methods_Then_Expressions_Should_NOT_Be_Equal_1()
        {
            //Arrange
            Expression<Func<Person, string>> expression1 = p => p.ToText();
            Expression<Func<Person, string>> expression2 = p => p.Age.ToText();

            //Act
            var equal = expression1.IsEquivalentTo(expression2);

            //Assert
            equal.Should().BeFalse();
        }

        [Fact]
        public void When_Different_Types_Call_Same_Methods_Then_Expressions_Should_NOT_Be_Equal_2()
        {
            //Arrange
            Expression<Func<string>> expression1 = () => true.ToString();
            Expression<Func<string>> expression2 = () => 2.ToString();

            //Act
            var equal = expression1.IsEquivalentTo(expression2);

            //Assert
            equal.Should().BeFalse();
        }

        [Fact]
        public void When_Same_Types_Call_Same_Method_With_Different_Parameter_Expressions_Then_Expressions_Should_NOT_Be_Equal()
        {
            //Arrange
            Expression<Func<Person, bool>> expression1 = p => p.Do(p.Age.YO + 2);
            Expression<Func<Person, bool>> expression2 = p => p.Do(2);

            //Act
            var equal = expression1.IsEquivalentTo(expression2);

            //Assert
            equal.Should().BeFalse();
        }

        [Fact]
        public void When_Same_Types_Call_Same_Method_With_Different_Parameter_Expressions_ThatAre_Equivalent_And_Can_Be_Simplified_Then_Expressions_Should_Be_Equal()
        {
            //Arrange
            Expression<Func<Person, bool>> expression1 = p => p.Do(2 + 2);
            Expression<Func<Person, bool>> expression2 = p => p.Do(4);

            //Act
            var equal = expression1.IsEquivalentTo(expression2);

            //Assert
            equal.Should().BeTrue(because : "2 + 2 is simplified to 4 before execution.");
        }

        [Fact]
        public void When_Same_Types_Call_Same_Method_With_Different_Parameter_Expressions_ThatAre_Equivalent_And_Can_Be_Simplified_Then_Expressions_Should_Be_Equal_2()
        {
            //Arrange
            Expression<Action<Person>> expression1 = p => p.SetStatus(true);
            Expression<Action<Person>> expression2 = p => p.SetStatus(!false);

            //Act
            var equal = expression1.IsEquivalentTo(expression2);

            //Assert
            equal.Should().BeTrue(because: "!false is simplified to true before execution.");
        }
    }
}
