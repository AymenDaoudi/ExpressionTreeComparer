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
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionTreeComparer.ExpressionComparers
{
    internal class MemberExpressionComparer : IExpressionComparer
    {
        /// <inheritdoc />
        public bool Equals(Expression expression1, Expression expression2)
        {
            if (((MemberExpression)expression1).Expression.NodeType is ExpressionType.Constant
             && ((MemberExpression)expression2).Expression.NodeType is ExpressionType.Constant)
            {
                var val1 = GetMemberValue((MemberExpression)expression1);
                var val2 = GetMemberValue((MemberExpression)expression2);

                return val1 == val2;
            }
            return ((MemberExpression)expression1).Member.Name == ((MemberExpression)expression2).Member.Name &&
                   ((MemberExpression)expression1).Member.MemberType == ((MemberExpression)expression2).Member.MemberType;
        }

        private object GetMemberValue(MemberExpression memberExpression)
        {
            var member = memberExpression.Member;
            var memberExpressionValue = ((ConstantExpression)memberExpression.Expression).Value;

            var memberValue = member.MemberType switch
            {
                MemberTypes.Field => memberExpressionValue.GetType().GetField(member.Name).GetValue(memberExpressionValue),
                MemberTypes.Property => memberExpressionValue.GetType().GetProperty(member.Name).GetValue(memberExpressionValue),
                _ => throw new NotImplementedException($"Couldn't identify type of member {member.Name}"),
            };

            return memberValue;
        }
    }
}