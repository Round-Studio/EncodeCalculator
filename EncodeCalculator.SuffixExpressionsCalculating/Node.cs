using EncodeCalculator.SuffixExpressionsCalculating.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodeCalculator.SuffixExpressionsCalculating
{
    public class Node : INode
    {
        public string? Title { get; set; }
        public string? Process { get; set; }
        public string? Formula { get; set; }
        public string? FormulaSuffix { get; set; }
        public int Index { get; set; } = -10;
    }
}
