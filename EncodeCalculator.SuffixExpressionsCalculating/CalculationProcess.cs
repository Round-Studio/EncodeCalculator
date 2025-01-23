using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodeCalculator.SuffixExpressionsCalculating
{
    /// <summary>
    /// 计算过程的数据结构
    /// </summary>
    public class CalculationProcess
    {
        private List<Node> nodes = new List<Node>();

        public CalculationProcess() {

        }

        /// <summary>
        /// 节点个数（长度）
        /// </summary>
        public int Count { get { return nodes.Count; } }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="node">节点</param>
        public void Add(Node node) {
            if (this.Count == 0) {
                node.Index = -1;
            }
            else {
                node.Index = nodes[nodes.Count - 1].Index + 1;
            }
            if (node.Index == -1)
                node.Index++;

            nodes.Add(node);
        }


        /// <summary>
        /// 删除节点操作，根据传入的引用进行删除。
        /// </summary>
        /// <param name="node">节点引用</param>
        public void Remove(Node node) {
            nodes.Remove(node);
        }

        /// <summary>
        /// 删除节点操作，根据传入的索引删除。
        /// </summary>
        /// <param name="index">要删除元素的索引</param>
        public void Remove(int index) {
            nodes.RemoveAt(index);
        }


        /// <summary>
        /// 删除节点操作，后置删除（将序列中最后一个节点删除）。
        /// </summary>
        public void Remove() {
            nodes.RemoveAt(this.Count - 1);
        }

        /// <summary>
        /// 清空节点（删除所有节点）
        /// </summary>
        public void Clear() {
            nodes.Clear();
        }
    }
}
