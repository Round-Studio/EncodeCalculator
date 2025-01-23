using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodeCalculator.SuffixExpressionsCalculating.Exception
{
    public class ParseInvalidException : System.Exception
    {
        // 构造函数
        public ParseInvalidException() : base("解析时遇到无效符号") {

        }

        public ParseInvalidException(string message) : base(message) {

        }

        public ParseInvalidException(string message, System.Exception innerException) : base(message, innerException) {

        }
    }
}
