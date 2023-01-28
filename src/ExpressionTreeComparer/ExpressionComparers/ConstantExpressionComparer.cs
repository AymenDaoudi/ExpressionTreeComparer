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

namespace ExpressionTreeComparer.ExpressionComparers
{
    internal class ConstantExpressionComparer : IExpressionComparer
    {
        /// <inheritdoc />
        public bool Equals(Expression expression1, Expression expression2)
        {
            var areTypesEqual = ((ConstantExpression)expression1).Type.Name == ((ConstantExpression)expression2).Type.Name;

            var value1 = ((ConstantExpression)expression1).Value;
            var value2 = ((ConstantExpression)expression2).Value;

            if (value1 is null ^ value2 is null) return false;
            else if (value1 is null && value2 is null) return areTypesEqual;
            else if (value1.GetType().Namespace != "System" || value2.GetType().Namespace != "System") return true;

            var areValuesEqual = value1.Equals(value2);

            return areTypesEqual && areValuesEqual;
        }
    }
}