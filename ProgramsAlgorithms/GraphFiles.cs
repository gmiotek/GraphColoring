using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Graphs;

namespace ProgramsAlgorithms
{
    public static class GraphFiles
    {
        public static Graph GetGraphFromFile(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                bool directed = bool.Parse(reader.ReadLine());
                int verticesCount = int.Parse(reader.ReadLine());
                Graph graphToReturn = new AdjacencyListsGraph<AVLAdjacencyList>(directed, verticesCount);
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] edge = line.Split(new char[] { ' ' });
                    graphToReturn.Add(new Edge(int.Parse(edge[0]), int.Parse(edge[1])));
                }
                return graphToReturn;
            }
        }
    }

}
