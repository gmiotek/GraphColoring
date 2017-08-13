using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Graphs;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Windows.Forms;
using System.Reflection;
using System.Text.RegularExpressions;

namespace GraphColoringAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = Directory.GetCurrentDirectory();
            string[] lines = System.IO.File.ReadAllLines(dir + "\\settings.txt");
            int i;
            List<ColoringGraphs> methods = new List<ColoringGraphs>();
            if (bool.Parse(Regex.Split(lines[0], " ")[1]))
                methods.Add(new DSatur());
            if (bool.Parse(Regex.Split(lines[1], " ")[1]))
                methods.Add(new LargestFirst());
            if (bool.Parse(Regex.Split(lines[2], " ")[1]))
                methods.Add(new SmallestLast());
            if ( methods.Count == 0)
                return;
            string[] str = Regex.Split(lines[3], " ");
            int[] limits = new int[str.Length - 1];
            for (i = 1; i < str.Length; i++)
                limits[i - 1] = int.Parse(str[i]);           
            int GraphTypeCount = int.Parse(Regex.Split(lines[4], " ")[1]);

            Graph[] eulerians = new Graph[GraphTypeCount];
            bool eulerian = bool.Parse(Regex.Split(lines[5], " ")[1]);
            Graph[] semieulerians = new Graph[GraphTypeCount];
            bool semieulerian = bool.Parse(Regex.Split(lines[6], " ")[1]);
            Graph[] hamiltonians = new Graph[GraphTypeCount];
            bool hamiltonian = bool.Parse(Regex.Split(lines[7], " ")[1]);
            Graph[] randoms = new Graph[GraphTypeCount];
            bool random = bool.Parse(Regex.Split(lines[8], " ")[1]);
            Graph[] cycles = new Graph[GraphTypeCount];
            bool cycle = bool.Parse(Regex.Split(lines[9], " ")[1]);
            Graph[] monocyclics = new Graph[GraphTypeCount];
            bool monocyclic = bool.Parse(Regex.Split(lines[10], " ")[1]);
            Graph[] bicyclics = new Graph[GraphTypeCount];
            bool bicyclic = bool.Parse(Regex.Split(lines[11], " ")[1]);
            Graph[] trees = new Graph[GraphTypeCount];
            bool tree = bool.Parse(Regex.Split(lines[12], " ")[1]);
            Graph[] twotrees = new Graph[GraphTypeCount];
            bool twotree = bool.Parse(Regex.Split(lines[13], " ")[1]);
            Graph[] webGraphs = new Graph[GraphTypeCount];
            bool webGraph = bool.Parse(Regex.Split(lines[14], " ")[1]);
            Graph[] helms = new Graph[GraphTypeCount];
            bool helm = bool.Parse(Regex.Split(lines[15], " ")[1]);
            Graph[] closedHelms = new Graph[GraphTypeCount];
            bool closedHelm = bool.Parse(Regex.Split(lines[16], " ")[1]);
            Graph[] treeOfPolygons = new Graph[GraphTypeCount];
            bool treeOfPolygon = bool.Parse(Regex.Split(lines[17], " ")[1]);
            Graph[] necklaces = new Graph[GraphTypeCount];
            bool necklace = bool.Parse(Regex.Split(lines[18], " ")[1]);
            Graph[] cactuses = new Graph[GraphTypeCount];
            bool cactus = bool.Parse(Regex.Split(lines[19], " ")[1]);
            Graph[] petersen = new Graph[1];
            bool peterse = bool.Parse(Regex.Split(lines[20], " ")[1]);
            Graph[] bipartites = new Graph[GraphTypeCount];
            bool bipartite = bool.Parse(Regex.Split(lines[21], " ")[1]);
            Graph[] wheels = new Graph[GraphTypeCount];
            bool wheel = bool.Parse(Regex.Split(lines[22], " ")[1]);
            Graph[] paths = new Graph[GraphTypeCount];
            bool path = bool.Parse(Regex.Split(lines[23], " ")[1]);
            for (i = 0; i < GraphTypeCount; i++)
            {
                if (eulerian)
                    eulerians[i] = RGG.eulerian();
                if ( semieulerian)
                    semieulerians[i] = RGG.semieulerian();
                if ( hamiltonian)
                    hamiltonians[i] = RGG.hamiltonian();
                if ( random )
                    randoms[i] = RGG.random();
                if ( cycle)
                    cycles[i] = RGG.cycle();
                if ( monocyclic )
                    monocyclics[i] = RGG.monocyclic();
                if ( bicyclic )
                    bicyclics[i] = RGG.bicyclic();
                if ( tree)
                    trees[i] = RGG.tree();
                if ( twotree)
                    twotrees[i] = RGG.twotree();
                if ( webGraph)
                    webGraphs[i] = RGG.webGraph();
                if ( helm )
                    helms[i] = RGG.helm();
                if ( closedHelm )
                    closedHelms[i] = RGG.closedHelm();
                if ( treeOfPolygon )
                    treeOfPolygons[i] = RGG.treeOfPolygons();
                if ( necklace )
                    necklaces[i] = RGG.necklace();
                if ( cactus )
                    cactuses[i] = RGG.cactus();
                if ( bipartite )
                    bipartites[i] = RGG.bipartite();
                if ( wheel )
                    wheels[i] = RGG.wheel();
                if ( path )
                    paths[i] = RGG.path();                
            }
            Graph petersenGraph =
                  new AdjacencyMatrixGraph(false, 10)
                  { new Edge(0,1), new Edge(0,4), new Edge(0,7), new Edge(1,2), new Edge(1,5), new Edge(2,3),
                    new Edge(2,9), new Edge(9,8), new Edge(9,7), new Edge(5,6), new Edge(6,7), new Edge(5,8),
                    new Edge(3,4), new Edge(3,6), new Edge(8,4)};
            petersen[0] = petersenGraph;

            Dictionary< string, Graph[]> graphTestSets = new Dictionary<string, Graph[]>();
            if (eulerian)
                graphTestSets.Add("eulerian", eulerians);
            if (semieulerian)
                graphTestSets.Add("semieulerian", semieulerians);
            if (hamiltonian)
                graphTestSets.Add("hamiltonian", hamiltonians);
            if (random)
                graphTestSets.Add("random", randoms);
            if (cycle)
                graphTestSets.Add("cycle", cycles);
            if (monocyclic)
                graphTestSets.Add("monocyclic", monocyclics);
            if (bicyclic)
                graphTestSets.Add("bicyclic", bicyclics);
            if (tree)
                graphTestSets.Add("tree", trees);
            if (twotree)
                graphTestSets.Add("twotree", twotrees);
            if (webGraph)
                graphTestSets.Add("webGraph", webGraphs);
            if (helm)
                graphTestSets.Add("helm", helms);
            if (closedHelm)
                graphTestSets.Add("closedHelm", closedHelms);
            if (treeOfPolygon)
                graphTestSets.Add("treeOfPolygons", treeOfPolygons);
            if (necklace)
                graphTestSets.Add("necklace", necklaces);
            if (cactus)
                graphTestSets.Add("cactus", cactuses);
            if (peterse)
                graphTestSets.Add("petersen", petersen);
            if (bipartite)
                graphTestSets.Add("bipartite", bipartites);
            if (wheel)
                graphTestSets.Add("wheel", wheels);
            if (path)
                graphTestSets.Add("path", paths);
            
            foreach (KeyValuePair<string, Graph[]> graphSet in graphTestSets)
            {
                for (i = 0; i < limits.Length; i++)
                {
                    string file_name = "results" + limits[i] + ".txt";
                    File.AppendAllText(file_name, graphSet.Key + Environment.NewLine);
                }
                compareMethods(methods, graphSet.Value, limits);
            }
        }
        public static void compareMethods(List<ColoringGraphs> listOfMethods, Graph[] graphs, int[] limits)
        {
            foreach ( int limit in limits)
            {
                string file_name = "results" + limit + ".txt";
                File.AppendAllText(file_name, limit.ToString() + Environment.NewLine);
                for (int i = 0; i < graphs.Length; i++)
                {
                    int maxDegree = 0;
                    for (int j = 0; j < graphs[i].VerticesCount; j++)
                        if (graphs[i].OutDegree(j) > maxDegree)
                            maxDegree = graphs[i].OutDegree(j);
                    int nbOfColorsUsed;
                    List<long> elapsedMs = new List<long>();
                    foreach (ColoringGraphs method in listOfMethods)
                    {
                        var watch = System.Diagnostics.Stopwatch.StartNew();
                        method.GetPaintedVertices(graphs[i], limit, false, out nbOfColorsUsed);
                        watch.Stop();
                        elapsedMs.Add(watch.ElapsedMilliseconds);
                        File.AppendAllText(file_name, nbOfColorsUsed.ToString() + " ");
                    }
                    File.AppendAllText(file_name, maxDegree.ToString() + " ");
                    foreach (long eMS in elapsedMs)
                        File.AppendAllText(file_name, eMS.ToString() + " ");
                    File.AppendAllText(file_name, Environment.NewLine);
                }
            }
        }             
    }        
}
