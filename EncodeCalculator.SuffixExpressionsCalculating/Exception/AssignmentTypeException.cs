using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodeCalculator.SuffixExpressionsCalculating.Exception
{
    public class AssignmentTypeException : System.Exception
    {
        // 构造函数
        public AssignmentTypeException() : base("赋值时类型错误") {

        }

        public AssignmentTypeException(string message) : base(message) {

        }

        public AssignmentTypeException(string message, System.Exception innerException) : base(message, innerException) {

        }
    }
}
