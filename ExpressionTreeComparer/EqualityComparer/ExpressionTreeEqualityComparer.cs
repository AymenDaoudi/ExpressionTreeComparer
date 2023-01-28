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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

using ExpressionTreeComparer.ExpressionComparers;

namespace ExpressionTreeComparer.EqualityComparer
{
    /// <summary>
    /// Represents a comparer between two expression trees. Provides an Equals method that does the comparison.
    /// </summary>
    /// <typeparam name="TDelegate">Func or Action</typeparam>
    internal class ExpressionTreeEqualityComparer<TDelegate> : IEqualityComparer<Expression<TDelegate>>
    {
        private readonly ExpressionComparerSelectorBase _expressionComparerSelector;

        internal ExpressionTreeEqualityComparer(ExpressionComparerSelectorBase expressionComparerSelector)
        {
            _expressionComparerSelector = expressionComparerSelector;
        }

        /// <inheritdoc />
        public bool Equals(Expression<TDelegate>? expression1, Expression<TDelegate>? expression2)
        {
            if (expression1 is null ^ expression2 is null) return false;
            if (expression1 is null && expression2 is null) return true;
            if (expression1.Compile()?.GetType() != expression1.Compile()?.GetType()) return false;
            
            return IsParametersEqual(expression1, expression2) && IsBodyEqual(expression1, expression2);
        }

        /// <summary>
        /// Compares the body of two expressions
        /// </summary>
        /// <param name="expression1">expression1</param>
        /// <param name="expression2">expression2</param>
        /// <returns>true if bodies are equivalent, false otherwise</returns>
        private bool IsBodyEqual(Expression<TDelegate> expression1, Expression<TDelegate> expression2)
        {
            var expression1Visitor = new Visitor();
            var expression2Visitor = new Visitor();

            expression1Visitor.Visit(expression1.Body);
            expression2Visitor.Visit(expression2.Body);

            var expression1Nodes = expression1Visitor.ExpressionNodes;
            var expression2Nodes = expression2Visitor.ExpressionNodes;

            if (expression1Nodes.Count != expression2Nodes.Count) return false;

            var expressionsCount = expression1Nodes.Count;
            IExpressionComparer expressionComparer;

            for (int i = 0; i < expressionsCount; i++)
            {
                var expression1Node = expression1Nodes.Dequeue();
                var expression2Node = expression2Nodes.Dequeue();

                if (expression1Node.NodeType != expression2Node.NodeType) return false;

                if (expression1Node.Type != expression2Node.Type)
                {
                    if (!(expression1Node.NodeType is ExpressionType.Constant && expression2Node.NodeType is ExpressionType.Constant)) return false;
                }

                expressionComparer = _expressionComparerSelector.GetExpressionComparer(expression1Node.GetType());
                var areExpressionsEqual = expressionComparer.Equals(expression1Node, expression2Node);

                if (!areExpressionsEqual) return false;
            }

            return true;
        }

        /// <summary>
        /// Compares parameters of two expressions
        /// </summary>
        /// <param name="expression1">expression1</param>
        /// <param name="expression2">expression2/param>
        /// <returns>true if same parameters, false otherwise</returns>
        private bool IsParametersEqual(Expression<TDelegate> expression1, Expression<TDelegate> expression2)
        {
            if (expression1.Parameters.Count != expression2.Parameters.Count) return false;
            var parametersCount = expression1.Parameters.Count;

            for (int i = 0; i < parametersCount; i++)
            {
                if (expression1.Parameters[i].Type != expression2.Parameters[i].Type) return false;
            }

            return true;
        }

        /// <inheritdoc />
        public int GetHashCode([DisallowNull] Expression<TDelegate> obj) => obj.Parameters.Count.GetHashCode();
    }
}
