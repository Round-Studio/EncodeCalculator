using EncodeCalculator.SuffixExpressionsCalculating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EncodeCalculator.SuffixExpressionsCalculating.FormulaType
{
    /// <summary>
    /// 常量类型（x∈R  .........    且拥有限制，Int64和Double类型限制一致）
    /// </summary>
    public struct FormulaConstant
    {
        private object value; // 使用 object 存储值

        // 隐式类型转换：从 int 到 Constant
        public static implicit operator FormulaConstant(int intValue) {
            return new FormulaConstant(intValue);
        }

        // 隐式类型转换：从 double 到 Constant
        public static implicit operator FormulaConstant(double doubleValue) {
            return new FormulaConstant(doubleValue);
        }

        public static implicit operator FormulaConstant(string StringValue) {
            return new FormulaConstant(StringValue);
        }

        // 私有构造函数：接受 object 类型
        private FormulaConstant(object value) {
            if (!(value is int || value is double || value is string)) {
                throw new ArgumentException("Constant can only be assigned an int or double value.");
            }
            this.value = value;
        }

        /// <summary>
        /// 获取类型，可以用于调试以及里氏转换与比较。
        /// </summary>
        public Type ValueType => value.GetType();

        // 重写 ToString 方法，方便打印
        public override string ToString() {
            return value.ToString();
        }

        // 重写相等性方法
        public override readonly bool Equals(object obj) {
            if (obj is FormulaConstant other) {
                return value.Equals(other.value);
            }
            return false;
        }

        public override int GetHashCode() {
            return value.GetHashCode();
        }
    }
}
