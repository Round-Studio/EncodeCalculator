using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodeCalculator.SuffixExpressionsCalculating.Interface
{
    /// <summary>
    /// 节点接口（步骤节点），定义了一组属性
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// 节点标题（节点说明、节点标识）
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 计算过程说明
        /// </summary>
        public string? Process { get; set; }

        /// <summary>
        /// 此步骤原算式（中缀的）
        /// </summary>
        public string? Formula { get; set; }

        /// <summary>
        /// 此步骤原算式（后缀的）
        /// </summary>
        public string? FormulaSuffix { get; set; }

        /// <summary>
        /// 索引（优先级，默认从-1开始，等于大于零代表正确）
        /// </summary>
        public int Index { get; set; }

    }
}
