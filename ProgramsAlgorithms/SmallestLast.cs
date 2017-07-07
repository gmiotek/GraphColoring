using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Graphs;

namespace ProgramsAlgorithms
{
    public class SmallestLast : ColoringGraphs
    {
        public override string Name => "SmallestLast";

        public override int[] GetPaintedVertices(Graph graphToPaint, int limit, bool verbose)
        {
            Graph graphCopy = graphToPaint.Clone();
            return PaintGraph(graphCopy, limit, verbose);
        }

        private int[] PaintGraph(Graph graphToPaint, int limit, bool verbose)
        {
            int[] sortedVertices = SortSmallestDegreeFirst(graphToPaint);
            int[] verticesColors = PaintVerticesGreedily(graphToPaint, limit, sortedVertices);
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
            for (int vertex = 0; vertex < graphToSort.VerticesCount; vertex++)
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
