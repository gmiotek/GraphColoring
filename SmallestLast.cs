using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Graphs;

namespace GraphColoringAlgorithms
{
    public class SmallestLast : ColoringGraphs
    {
        public override string Name => "Smallest Last";
        public override int[] GetPaintedVertices(Graph graphToPaint, int limit, bool verbose, out int nbOfColorsUsed)
        {
            Graph graphCopy = graphToPaint.Clone();
            int[] ret = PaintGraph(graphCopy, limit, verbose, out nbOfColorsUsed);
            return ret;
        }
        private int[] PaintGraph(Graph graphToPaint, int limit, bool verbose, out int nbOfColorsUsed)
        {
            int[] sortedVertices = SortSmallestDegreeFirst(graphToPaint);
            int[] verticesColors = PaintVerticesGreedily(graphToPaint, limit, sortedVertices, out nbOfColorsUsed);
            PrintColors(verbose, sortedVertices, verticesColors);
            return verticesColors;
        }
        private int[] SortSmallestDegreeFirst(Graph graphToSort)
        {
            Stack<int> smallestFirstVertices = new Stack<int>();
            List<int> remainingVertices = new List<int>();
            int[] degrees = new int[graphToSort.VerticesCount];
            for (int i = 0; i < graphToSort.VerticesCount; i++)
            {
                degrees[i] = graphToSort.OutDegree(i);
            }
            for (int vertex = graphToSort.VerticesCount-1; vertex >= 0; vertex--)
            {
                remainingVertices.Add(vertex);
            }
            while (remainingVertices.Count>0)
            {
                int minDegree = int.MaxValue;
                int minDegreeVertex = -1;
                foreach (var vertex in remainingVertices)
                {
                    if(degrees[vertex] < minDegree)
                    {
                        minDegreeVertex = vertex;
                        minDegree = degrees[vertex];
                    }
                }
                smallestFirstVertices.Push(minDegreeVertex);
                remainingVertices.Remove(minDegreeVertex);
                foreach (var neighbourEdge in graphToSort.OutEdges(minDegreeVertex))
                {
                    degrees[neighbourEdge.To]--;
                }
            }
            int[] sortedVertices = new int[graphToSort.VerticesCount];
            for (int i = 0; i < sortedVertices.Length; i++)
            {
                sortedVertices[i] = smallestFirstVertices.Pop();
            }
            return sortedVertices;
        }
    }
}
