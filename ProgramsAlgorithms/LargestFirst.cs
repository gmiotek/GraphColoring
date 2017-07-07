using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Graphs;

namespace ProgramsAlgorithms
{
    public class LargestFirst : ColoringGraphs
    {
        public override string Name => "LargestFirst";

        public override int[] GetPaintedVertices(Graph graphToPaint, int limit, bool verbose)
        {
            Graph graphCopy = graphToPaint.Clone();
            return PaintGraph(graphCopy, limit, verbose);
        }

        private int[] PaintGraph(Graph graphToPaint, int limit, bool verbose)
        {
            int[] sortedVertices;
            BottomUpMergeSort(graphToPaint, out sortedVertices, verbose: false);
            int[] verticesColors = PaintVerticesGreedily(graphToPaint, limit, sortedVertices);
            PrintColors(verbose, sortedVertices, verticesColors);
            return verticesColors;
        }
    }
}
