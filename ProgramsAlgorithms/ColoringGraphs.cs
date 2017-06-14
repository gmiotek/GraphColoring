using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Graphs;

namespace ProgramsAlgorithms
{
    public abstract class ColoringGraphs
    {
        public abstract int[] GetPaintedVertices(Graph graphToPaint, int limit, bool verbose);
        public abstract string Name { get; }
        protected int[] PaintVerticesGreedily(Graph graphToPaint, int limit, int[] sortedVertices)
        {
            int[] verticesColors = new int[graphToPaint.VerticesCount];
            for (int i = 0; i < verticesColors.Length; i++)
            {
                verticesColors[i] = -1;
            }
            //how many vertices are painted in particular color
            List<int> verticesInSameColorCount = new List<int>();
            foreach (var currentVertex in sortedVertices)
            {
                int colorsUsedCount = verticesInSameColorCount.Count;
                HashSet<int> neighbourColors = new HashSet<int>();
                foreach (var neighbour in graphToPaint.OutEdges(currentVertex))
                {
                    int colorOfNeighbour = neighbour.To;
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
                //If could not paint vertex in any color already used
                if (verticesColors[currentVertex] == -1)
                {
                    verticesInSameColorCount.Add(1);
                    verticesColors[currentVertex] = verticesInSameColorCount.Count - 1;
                }
            }

            return verticesColors;
        }

        protected void BottomUpMergeSort(Graph graphToSort, out int[] outputVertices, bool verbose)
        {
            int i;
            int n = graphToSort.VerticesCount;
            int[] degrees = new int[n]; // tablica do posortowania
            for (i = 0; i < n; i++)
                degrees[i] = graphToSort.OutDegree(i);
            outputVertices = new int[n];      // docelowo tablica indeksow i posortowana wg A[i] malejąco
            for (i = 0; i < n; i++)
                outputVertices[i] = i;
            // Algorytm zapisuje w CA coraz dluzsze posortowane ciagi 2, 4, 8, 16... 
            // aż tablica A jest posortowana. Zwraca też tablicę kolejnosci wierzcholkow CA
            for (int width = 1; width < n; width *= 2)
            {
                for (i = 0; i < n; i += 2 * width)
                {
                    // złącz dwa ciągi: A[i:i+width-1] and A[i+width:i+2*width-1] i wstaw do B[] 
                    // przy laczeniu zapisz tablicę numerow wierzcholkow z CA do CB	   
                    // lub skopiuj A[i:n-1] to B[] ( if(i+width >= n) )
                    BottomUpMerge(degrees, i, Math.Min(i + width, n), Math.Min(i + 2 * width, n), outputVertices, out outputVertices);
                }
            }
            if (verbose)
                for (i = 0; i < outputVertices.Length; i++)
                    Console.WriteLine(" CA[ " + i + " ] = " + outputVertices[i]);
        }
        //  lewy ciąg to A[CA[iLeft :iRight-1]].
        // prawy ciąg to A[CA[iRight:iEnd-1]].
        private void BottomUpMerge(int[] degrees, int iLeft, int iRight, int iEnd, int[] CA, out int[] CB)
        {
            int i, j, k; CB = new int[degrees.Length]; for (i = 0; i < CA.Length; i++) CB[i] = CA[i];
            i = iLeft; j = iRight;
            for (k = iLeft; k < iEnd; k++)
            {
                // jeśli head lewego ciągu istnieje i jest <= head prawego ciągu
                if (i < iRight && (j >= iEnd || degrees[CA[i]] >= degrees[CA[j]]))
                {
                    CB[k] = CA[i];
                    i = i + 1;
                }
                else
                {
                    CB[k] = CA[j];
                    j = j + 1;
                }
            }
        }

        protected void PrintColors(bool verbose, int[] sortedVertices, int[] verticesColors)
        {
            if (verbose)
            {
                Console.WriteLine($"Coloring algorithm {Name}");
                foreach (var vertex in sortedVertices)
                {
                    Console.WriteLine($"Vertex number {vertex} colored in color {verticesColors[vertex]}");
                }
                Console.WriteLine();
            }
        }
    }
}
