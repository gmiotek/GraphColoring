using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Graphs;

namespace GraphColoringAlgorithms
{
    public class LargestFirst : ColoringGraphs
    {
        public override string Name => "Largest First";

        public override int[] GetPaintedVertices(Graph graphToPaint, int limit, bool verbose, out int nbOfColorsUsed)
        {
            Graph graphCopy = graphToPaint.Clone();
            int[] ret = PaintGraph(graphCopy, limit, verbose, out nbOfColorsUsed);
            return ret; 
        }       
        private int[] PaintGraph(Graph graphToPaint, int limit, bool verbose, out int nbOfColorsUsed) 
        {
            int[] sortedVertices;
            BottomUpMergeSort(graphToPaint, out sortedVertices, verbose: false);            
            int[] verticesColors = PaintVerticesGreedily(graphToPaint, limit, sortedVertices, out nbOfColorsUsed); 
            PrintColors(verbose, sortedVertices, verticesColors);
            return verticesColors;
        }
    }
}
