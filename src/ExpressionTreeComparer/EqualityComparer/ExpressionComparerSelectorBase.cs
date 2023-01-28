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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using ExpressionTreeComparer.ExpressionComparers;

namespace ExpressionTreeComparer.EqualityComparer
{
    public abstract class ExpressionComparerSelectorBase
    {
        protected Dictionary<Type, IExpressionComparer> _expressionComparerMap = new Dictionary<Type, IExpressionComparer>();
        
        public ExpressionComparerSelectorBase()
        {
            _expressionComparerMap.Add(typeof(UnaryExpression), new UnaryExpressionComparer());
            _expressionComparerMap.Add(typeof(BinaryExpression), new BinaryExpressionComparer());
            _expressionComparerMap.Add(typeof(ConstantExpression), new ConstantExpressionComparer());
            _expressionComparerMap.Add(typeof(MemberExpression), new MemberExpressionComparer());
            _expressionComparerMap.Add(typeof(MethodCallExpression), new MethodCallExpressionComparer());
            _expressionComparerMap.Add(typeof(ParameterExpression), new ParameterExpressionComparer());
            _expressionComparerMap.Add(typeof(NewExpression), new NewExpressionComparer());
        }

        /// <summary>
        /// Returns an ExpressionComparer based on the given Expression type
        /// </summary>
        /// <param name="expressionType">Type of Expression</param>
        /// <returns>Implementation of IExpressionComparer</returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IExpressionComparer GetExpressionComparer(Type expressionType)
        {
            var expressionComparerMapping = _expressionComparerMap.SingleOrDefault(kpv => expressionType == kpv.Key || expressionType.IsSubclassOf(kpv.Key));
            if (expressionComparerMapping.Value is null)
            {
                throw new NotImplementedException($"Expression type {expressionType.Name} is not supported.");
            }
            else
            {
                return expressionComparerMapping.Value;
            }
        }
    }
}