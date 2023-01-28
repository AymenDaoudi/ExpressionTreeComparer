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

using System.ComponentModel;

namespace ExpressionTreeComparer.UnitTests.MockData
{
    public class Person
    {
        [Description("The color used on nodes containing errors.")]
        [DefaultValue(null)]
        public Person()
        {

        }

        public Person(string name)
        {

        }

        public string Name { get; set; }

        public string Address { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsDisabled { get; set; }

        public Age Age { get; set; }

        public bool Do(int i) => true;

        public bool Do() => true;

        public object GetObject() => null;

        public string ToText() => $"{Name} : {Address}";

        public bool SetStatus(bool enabled) 
        {
            return IsDisabled = !enabled;
        }
    }
}
