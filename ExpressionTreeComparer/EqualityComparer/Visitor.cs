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
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ExpressionTreeComparer.UnitTests", AllInternalsVisible = true)]
namespace ExpressionTreeComparer.EqualityComparer
{
    /// <summary>
    /// Visits all expression nodes.
    /// </summary>
    internal class Visitor : ExpressionVisitor
    {
        /// <summary>
        /// Stores visited nodes.
        /// </summary>
        public Queue<Expression> ExpressionNodes { get; set; }

        public Visitor()
        {
            ExpressionNodes = new Queue<Expression>();
        }

        /// <inheritdoc />
        protected override Expression VisitParameter(ParameterExpression node)
        {
            ExpressionNodes.Enqueue(node);
            return node;
        }

        /// <inheritdoc />
        protected override Expression VisitMember(MemberExpression node)
        {
            ExpressionNodes.Enqueue(node);
            base.Visit(node.Expression);
            return node;
        }

        /// <inheritdoc />
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            ExpressionNodes.Enqueue(node);
            base.Visit(node.Object);
            foreach (var argument in node.Arguments)
            {
                base.Visit(argument);
            }
            return node;
        }

        /// <inheritdoc />
        protected override Expression VisitBinary(BinaryExpression node)
        {
            ExpressionNodes.Enqueue(node);
            base.Visit(node.Left);
            base.Visit(node.Right);
            return node;
        }

        /// <inheritdoc />
        protected override Expression VisitConstant(ConstantExpression node)
        {
            ExpressionNodes.Enqueue(node);
            return node;
        }

        /// <inheritdoc />
        protected override Expression VisitUnary(UnaryExpression node)
        {
            ExpressionNodes.Enqueue(node);
            base.Visit(node.Operand);
            return node;
        }

        /// <inheritdoc />
        protected override Expression VisitNew(NewExpression node)
        {
            ExpressionNodes.Enqueue(node);
            foreach (var argument in node.Arguments)
            {
                base.Visit(argument);
            }
            return node;
        }

        /// <inheritdoc />
        protected override Expression VisitInvocation(InvocationExpression node)
        {
            ExpressionNodes.Enqueue(node);
            return node;
        }

        /// <inheritdoc />
        [return: NotNullIfNotNull("node")]
        public override Expression? Visit(Expression? node)
        {
            return base.Visit(node);
        }

        #region NotImplemented

        /// <inheritdoc />
        protected override Expression VisitExtension(Expression node)
        {
            ExpressionNodes.Enqueue(node);
            return node;
        }

        /// <inheritdoc />
        protected override Expression VisitBlock(BlockExpression node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitDefault(DefaultExpression node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitDynamic(DynamicExpression node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override ElementInit VisitElementInit(ElementInit node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitGoto(GotoExpression node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitIndex(IndexExpression node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitLabel(LabelExpression node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        [return: NotNullIfNotNull("node")]
        protected override LabelTarget? VisitLabelTarget(LabelTarget? node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitListInit(ListInitExpression node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitLoop(LoopExpression node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitSwitch(SwitchExpression node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitTry(TryExpression node)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}