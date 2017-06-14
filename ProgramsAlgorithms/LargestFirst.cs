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
        public override string Name => "Largest First";

        public override int[] GetPaintedVertices(Graph graphToPaint, int limit, bool verbose)
        {
            Graph graphCopy = graphToPaint.Clone();
            return PaintGraph(graphCopy, limit, verbose);
        }

        private int[] PaintGraph(Graph petersenGraph, int limit, bool verbose)
        {
            int[] sortedVertices;
            BottomUpMergeSort(petersenGraph, out sortedVertices, verbose: false);
            int[] verticesColors = PaintVerticesGreedily(petersenGraph, limit, sortedVertices);
            PrintColors(verbose, sortedVertices, verticesColors);
            return verticesColors;
        }
    }
}
