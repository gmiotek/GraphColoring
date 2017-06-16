using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Graphs;

namespace ProgramsAlgorithms
{
    class DSatur : ColoringGraphs
    {
        public override string Name => "DSatur";

        public override int[] GetPaintedVertices(Graph graphToPaint, int limit, bool verbose)
        {
            Graph graphCopy = graphToPaint.Clone();
            return PaintGraph(graphToPaint, limit, verbose);
        }

        private int[] PaintGraph(Graph graphToPaint, int limit, bool verbose)
        {
            int verticesCount = graphToPaint.VerticesCount;
            int[] sortedVertices;
            BottomUpMergeSort(graphToPaint, out sortedVertices, verbose: false);
            int[] DSaturValues = new int[verticesCount];
            int[] verticesColors = new int[verticesCount];
            for (int i = 0; i < verticesColors.Length; i++)
            {
                verticesColors[i] = -1;
            }
            //how many vertices are painted in particular color
            List<int> verticesInSameColorCount = new List<int>();
            int currentVertex = sortedVertices[0];
            for (int verticesHandled = 0; verticesHandled < verticesCount;)
            {
                int colorsUsedCount = verticesInSameColorCount.Count;
                HashSet<int> neighbourColors = new HashSet<int>();
                foreach (var edge in graphToPaint.OutEdges(currentVertex))
                {
                    DSaturValues[edge.To]++;
                    int colorOfNeighbour = edge.To;
                    if (verticesColors[colorOfNeighbour] == -1) continue;
                    if (!neighbourColors.Contains(verticesColors[colorOfNeighbour]))
                        neighbourColors.Add(verticesColors[colorOfNeighbour]);
                }
                for (int currentColor = 0; currentColor < colorsUsedCount; currentColor++)
                {
                    if (neighbourColors.Contains(currentColor) || verticesInSameColorCount[currentColor] >= limit) continue;
                    else
                    {
                        verticesColors[currentVertex] = currentColor;
                        verticesInSameColorCount[currentColor]++;
                        break;
                    }
                }
                //If could not color vertex in any color already used
                if (verticesColors[currentVertex] == -1)
                {
                    verticesInSameColorCount.Add(1);
                    verticesColors[currentVertex] = verticesInSameColorCount.Count - 1;
                }
                verticesHandled++;
                if (verticesHandled == verticesCount) break;
                int firstNotColored;
                for (firstNotColored = 0; firstNotColored < verticesCount; firstNotColored++)
                {
                    if (verticesColors[sortedVertices[firstNotColored]] == -1) break;
                }
                currentVertex = sortedVertices[firstNotColored];
                int maxDSaturValue = DSaturValues[currentVertex];
                for (int i = firstNotColored + 1; i < verticesCount; i++)
                {
                    if(verticesColors[sortedVertices[i]] == -1 && maxDSaturValue < DSaturValues[sortedVertices[i]])
                    {
                        maxDSaturValue = DSaturValues[sortedVertices[i]];
                        currentVertex = sortedVertices[i];
                    }
                }
            }
            PrintColors(verbose, sortedVertices, verticesColors);
            return verticesColors;
        }
    }
}
