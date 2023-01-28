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
    internal class MethodCallExpressionComparer : IExpressionComparer
    {
        /// <inheritdoc />
        public bool Equals(Expression expression1, Expression expression2)
        {
            var method1 = ((MethodCallExpression)expression1).Method;
            var method2 = ((MethodCallExpression)expression2).Method;

            var haveSameNumberOfTypeParameters = MethodsHaveSameNumberOfTypeParameters(method1, method2);
            if (!haveSameNumberOfTypeParameters) return false;

            var haveSameNumberOfParameters = MethodsHaveSameNumberOfParameters(method1, method2);
            if (!haveSameNumberOfParameters) return false;

            var haveSameParameters = MethodsHaveSameParameters(method1, method2);
            if (!haveSameParameters) return false;

            return method1.Name == method2.Name && method1.ReturnParameter == method2.ReturnParameter;
        }

        private bool MethodsHaveSameNumberOfParameters(MethodInfo method1, MethodInfo method2) => method1.GetParameters().Length == method2.GetParameters().Length;

        private bool MethodsHaveSameNumberOfTypeParameters(MethodInfo method1, MethodInfo method2) => method1.GetGenericArguments().Length == method2.GetGenericArguments().Length;

        private bool MethodsHaveSameParameters(MethodInfo method1, MethodInfo method2)
        {
            var method1Parameters = method1.GetParameters();
            var method2Parameters = method2.GetParameters();

            for (int i = 0; i < method1Parameters.Length; i++)
            {
                if (method1Parameters[i].Name != method2Parameters[i].Name ||
                    method1Parameters[i].ParameterType != method2Parameters[i].ParameterType ||
                    method1Parameters[i].IsIn != method2Parameters[i].IsIn ||
                    method1Parameters[i].IsOut != method2Parameters[i].IsOut ||
                    method1Parameters[i].IsLcid != method2Parameters[i].IsLcid)
                {
                    return false;
                }
            }

            return true;
        }
    }
}