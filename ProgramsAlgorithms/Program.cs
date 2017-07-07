using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Graphs;

namespace ProgramsAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph petersenGraph;
            try
            {
                petersenGraph = GraphFiles.GetGraphFromFile("in.txt");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File has not been found");
                Console.ReadLine();
                return;
            }
            catch(FormatException)
            {
                Console.WriteLine("Wrong data in the file");
                Console.ReadLine();
                return;
            }
            //new AdjacencyMatrixGraph(false, 11)
            //{ new Edge(0,1), new Edge(0,3), new Edge(0,4), new Edge(0,5), new Edge(1,2), new Edge(1,7),
            //    new Edge(1,9), new Edge(2,3), new Edge(2,6), new Edge(3,8), new Edge(3,10), new Edge(4,6),
            //    new Edge(4,9), new Edge(5,6), new Edge(5,10), new Edge(6,7), new Edge(6,8), new Edge(7,10),
            //    new Edge(8,9), new Edge(9,10)};

            List<ColoringGraphs> coloringAgorithms = new List<ColoringGraphs>() { new LargestFirst(), new SmallestLast(), new DSatur() };
            foreach (var algorithm in coloringAgorithms)
            {
                algorithm.GetPaintedVertices(graphToPaint: petersenGraph, limit: 3, verbose: true);
            }

            Console.ReadLine();
        }
    }
}
