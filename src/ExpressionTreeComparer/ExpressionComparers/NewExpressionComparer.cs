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
using System.Reflection;

namespace ExpressionTreeComparer.ExpressionComparers
{
    internal class NewExpressionComparer : IExpressionComparer
    {
        /// <inheritdoc />
        public bool Equals(Expression expression1, Expression expression2)
        {
            var const1 = ((NewExpression)expression1).Constructor;
            var const2 = ((NewExpression)expression2).Constructor;

            var haveSameType = ConstructorsHaveSameType(const1, const2);
            if (!haveSameType) return false;

            var haveSameNumberOfTypeParameters = ConstructorHaveSameNumberOfTypeParameters(const1, const2);
            if (!haveSameNumberOfTypeParameters) return false;

            var haveSameNumberOfParameters = ConstructorsHaveSameNumberOfParameters(const1, const2);
            if (!haveSameNumberOfParameters) return false;

            var haveSameParameters = ConstructorsHaveSameParameters(const1, const2);
            if (!haveSameParameters) return false;

            return true;
        }

        private bool ConstructorsHaveSameType(ConstructorInfo const1, ConstructorInfo const2) => const1.DeclaringType == const2.DeclaringType;

        private bool ConstructorHaveSameNumberOfTypeParameters(ConstructorInfo const1, ConstructorInfo const2)
        {
            if (const1.IsGenericMethod ^ const2.IsGenericMethod) return false;
            if (!const1.IsGenericMethod && !const2.IsGenericMethod) return true;
            return const1.GetGenericArguments().Length == const2.GetGenericArguments().Length;
        }

        private bool ConstructorsHaveSameNumberOfParameters(ConstructorInfo const1, ConstructorInfo const2) => const1.GetParameters().Length == const2.GetParameters().Length;

        private bool ConstructorsHaveSameParameters(ConstructorInfo const1, ConstructorInfo const2)
        {
            var expression1Parameters = const1.GetParameters();
            var expression2Parameters = const2.GetParameters();

            for (int i = 0; i < expression1Parameters.Length; i++)
            {
                if (expression1Parameters[i].Name != expression2Parameters[i].Name ||
                    expression1Parameters[i].ParameterType != expression2Parameters[i].ParameterType ||
                    expression1Parameters[i].IsIn != expression2Parameters[i].IsIn ||
                    expression1Parameters[i].IsOut != expression2Parameters[i].IsOut ||
                    expression1Parameters[i].IsLcid != expression2Parameters[i].IsLcid)
                {
                    return false;
                }
            }

            return true;
        }
    }
}