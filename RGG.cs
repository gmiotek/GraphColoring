using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Graphs;

namespace GraphColoringAlgorithms
{
    public class RGG
    {
        private static Random rnd = new Random();
        private static RandomGraphGenerator rgg = new RandomGraphGenerator();

        public static Graph path()
        {
            Graph cyclee = cycle();
            int vert = rnd.Next(0, cyclee.VerticesCount);
            Edge e = cyclee.OutEdges(vert).ElementAt(0);
            cyclee.DelEdge(e.From, e.To);
            return cyclee;
        }
        public static Graph cycle()
        {
            int verticesCount = rnd.Next(3, 100);
            Graph cycle = new AdjacencyListsGraph<SimpleAdjacencyList>(false, verticesCount);
            for (int i = 1; i < verticesCount; i++)
            cycle.AddEdge(i - 1, i); 
                cycle.AddEdge(0, verticesCount -1);
            cycle = changeVerticesNumeration(cycle);
            //FOR USER - checking changeVerticesNumeration
            /*
            if (cycle.VerticesCount < 11)
            {
                int vert = 0;
                int vold = -1;
                int v = 0;
                Console.Write("vert count = " + verticesCount);
                Console.WriteLine();
                Edge e; ;
                do
                {
                    Console.Write(v + " ");
                    e = cycle.OutEdges(v).ToArray()[0];
                    if (e.To == vold || e.From == vold)
                        e = cycle.OutEdges(v).ToArray()[1];
                    vold = v;
                    if (vold == e.To)
                        v = e.From;
                    else
                        v = e.To;
                }
                while (v != vert);
                Console.WriteLine();
            }
            */
            return cycle;
        }
        public static Graph tree()
        {
            int verticesCount = rnd.Next(1, 1000);
            int density = 1;
            Graph treeGraph = rgg.TreeGraph(typeof(AdjacencyListsGraph<SimpleAdjacencyList>), verticesCount, density);
            return changeVerticesNumeration(treeGraph);
        }
        public static Graph monocyclic()
        {
            Graph treeGraph = tree();
            int n = treeGraph.VerticesCount;
            int a, b;
            do
            {
                a = rnd.Next(n);
                b = rnd.Next(n);
            }
            while (contains(treeGraph.OutEdges(a), new Edge(a, b)) || contains(treeGraph.OutEdges(b), new Edge(b, a)) ); 
            treeGraph.AddEdge(a, b);
            return treeGraph;
        }
        public static Graph monocyclic(Graph tree)
        {
            int n = tree.VerticesCount;
            int a, b;
            do
            {
                a = rnd.Next(n);
                b = rnd.Next(n);
            }
            while (contains(tree.OutEdges(a), new Edge(a, b)) || contains(tree.OutEdges(b), new Edge(b, a)));
            tree.AddEdge(a, b);
            return tree;
        }
        public static Graph bicyclic()
        {
            Graph monocyclicc = monocyclic();
            monocyclicc = monocyclic(monocyclicc);
            return monocyclicc;
        }   
        public static Graph wheel()
        {
            int veticesCount = rnd.Next(4, 101);
            Graph wheel = new AdjacencyListsGraph<SimpleAdjacencyList>(false, veticesCount);            
            for (int i = 1; i<veticesCount; i++)
            {
                wheel.AddEdge(0, i);
                wheel.AddEdge(i - 1, i);
            }
            wheel.AddEdge(1, veticesCount - 1);
            return changeVerticesNumeration(wheel);
        }
        public static Graph rawHelm()
        {
            int verticesCount = rnd.Next(4, 1000);
            if (verticesCount % 2 == 0)
                verticesCount++;
            Graph helm = new AdjacencyListsGraph<SimpleAdjacencyList>(false, verticesCount);
            int i;
            int ringSize = verticesCount / 2;
            for (i = 1; i <= ringSize; i++)   // 1:ringSize is first cycle; ringsize+1:2*ringSize is second cycle
            {
                helm.AddEdge(new Edge(i - 1, i));
                helm.AddEdge(new Edge(0, i));
                helm.AddEdge(new Edge(i, ringSize + i));     // connecting outer vertices
            }
            helm.AddEdge(new Edge(ringSize, 1));
            return helm;
        }
        public static Graph helm()
        {
            Graph helm = rawHelm();
            return changeVerticesNumeration(helm);
        }
        public static Graph closedHelm()
        {
            Graph helm = rawHelm();
            int ringSize = helm.VerticesCount / 2;
            int i;
            for (i = ringSize + 1; i < 2 * ringSize; i++)   // 1:ringSize is first cycle; ringSize+1:2*ringSize is second cycle
            {
                helm.AddEdge(new Edge(i, i + 1));
            }
            helm.AddEdge(new Edge(2 * ringSize, ringSize + 1));
            return changeVerticesNumeration(helm);
        }
        public static Graph webGraph()
        {
            int nOfCycles = rnd.Next(2, 15);
            int ringSize = rnd.Next(3, 1000 / nOfCycles );
            int verticesCount = nOfCycles * ringSize;
            Graph web = new AdjacencyListsGraph<SimpleAdjacencyList>(false, verticesCount);
            int i,j;
            for (j = 0; j < nOfCycles; j++)
                for (i = 1; i < ringSize; i++)   // 0:ringsize-1 denotes first cycle; ringsize:2*ringsize-1 is second cycle
                    web.AddEdge(new Edge(j * ringSize + i - 1, j * ringSize + i));
            for (j = 0; j < nOfCycles; j++)     // closing cycles
                web.AddEdge(new Edge(j * ringSize, (j + 1) * ringSize - 1));  
            for (j = 0; j < nOfCycles - 1; j++) // connecting cycles 
                for (i = 0; i < ringSize; i++)
                    web.AddEdge(new Edge(j * ringSize + i, (j + 1) * ringSize + i));
            for (j = 0; j < nOfCycles - 1; j++) // connecting last vertices of cycles
                web.AddEdge(new Edge(( j + 1 ) * ringSize - 1, (j + 2) * ringSize - 1));                   
            return changeVerticesNumeration(web);
        }
        public static Graph treeOfPolygons()
        {
            int verticesCount = rnd.Next(3, 1000);
            Graph poligonTree = new AdjacencyListsGraph<SimpleAdjacencyList>(false, verticesCount);
            List<Edge> freeEdges = new List<Edge>();
            Edge e = new Edge(0, 1);
            poligonTree.Add(e);
            freeEdges.Add(e);
            int i = 1;
            while (i < verticesCount - 1)
            {
                int polygonsize = rnd.Next(3, 7);
                polygonsize = Math.Min(polygonsize, verticesCount - i - 1);
                if (polygonsize > 2)
                {                    
                    poligonTree.AddEdge(e.To, ++i); freeEdges.Add(new Edge(e.To, i));
                    for (int j = 0; j < polygonsize - 1; j++)
                    {
                        poligonTree.AddEdge(i, ++i);
                        freeEdges.Add(new Edge(i-1, i));
                    }
                    poligonTree.AddEdge(i, e.From);
                    freeEdges.Add(new Edge(i, e.From));
                }
                else
                    break;
                e = freeEdges[rnd.Next(freeEdges.Count)];
                freeEdges.Remove(e);
            }
            return changeVerticesNumeration(poligonTree);
        }
        public static Graph cactus()
        {
            int verticesCount = rnd.Next(3, 1000);
            Graph cactus = new AdjacencyListsGraph<SimpleAdjacencyList>(false, verticesCount);
            int i = 0; int connectingNode = 0;
            while (i < verticesCount - 1)
            {
                int polygonsize = 1;
                if (rnd.Next(2) > 0)
                    cactus.AddEdge(connectingNode, ++i);
                else
                {
                    polygonsize = rnd.Next(1, 10);
                    polygonsize = Math.Min(polygonsize, verticesCount - i - 1);
                    cactus.AddEdge(connectingNode, ++i);
                    for (int j = 1; j < polygonsize; j++)
                        cactus.AddEdge(i, ++i);
                    cactus.AddEdge(i, connectingNode);
                }
                if (rnd.Next(2) > 0)   // chance for changing connecting node == 0.5
                    connectingNode = i - rnd.Next(polygonsize);
            }
            return changeVerticesNumeration(cactus);
        }
        public static Graph twotree()
        {
            int verticesCount = rnd.Next(3, 1000);
            Graph twotree = new AdjacencyListsGraph<SimpleAdjacencyList>(false, verticesCount);
            twotree.AddEdge(new Edge(0, 1)); twotree.AddEdge(new Edge(1, 0));
            twotree.AddEdge(new Edge(1, 2)); twotree.AddEdge(new Edge(2, 1));
            twotree.AddEdge(new Edge(2, 0)); twotree.AddEdge(new Edge(0, 2));
            for (int i = 3; i < verticesCount; i++)
            {
                int a = rnd.Next(i);
                List<Edge> neighbours = twotree.OutEdges(a).ToList();
                int b = rnd.Next(neighbours.Count);
                if (neighbours[b].To == a)
                    b = neighbours[b].From;
                else
                    b = neighbours[b].To;
                twotree.AddEdge(i, a);
                twotree.AddEdge(i, b);
            }
            return changeVerticesNumeration(twotree);
        }
        public static Graph necklace()
        {
            int k, l, r; // k - size of a bead; l - length of path; r - nb of beads
            k = rnd.Next(3, 7);
            l = rnd.Next(2, 7);
            r = rnd.Next(3, 15);
            int verticesCount = (k + l) * r; 
            Graph necklace = new AdjacencyListsGraph<SimpleAdjacencyList>(false, verticesCount);
            int i = 0; int toPathStart = k / 2 - 1;
            for (int bead = 0; bead < r; bead++)
            {
                for (int j = 0; j < k; j++)
                    necklace.AddEdge(i, ++i);
                necklace.AddEdge(i, i - k);   // bead done
                necklace.AddEdge(i - toPathStart, ++i);
                if (bead == r - 1)
                    l--;
                for (int j = 0; j < l - 1; j++)
                    necklace.AddEdge(i, ++i);       // path done    
            }
            necklace.AddEdge(verticesCount - 1, 0);  // for the last bead the last vertice connected with vertice 0
            return changeVerticesNumeration(necklace);
        }
        public static Graph eulerian()
        {
            int verticesCount = rnd.Next(4, 1000);
            double density = rnd.Next(verticesCount,1000);
            density /= 1000;
            Graph eulerian = rgg.EulerGraph(typeof(AdjacencyListsGraph<SimpleAdjacencyList>), false, verticesCount, density);
            return changeVerticesNumeration(eulerian);
        }
        public static Graph hamiltonian()
        {            
            Graph hamiltonian = random();
            hamiltonian.AddEdge(0, hamiltonian.VerticesCount - 1);
            for (int i = 1; i < hamiltonian.VerticesCount; i++)
                hamiltonian.AddEdge(i - 1, i);            
            return changeVerticesNumeration(hamiltonian);  
        }
        public static Graph eulerian2()
        {
            int verticesCount = rnd.Next(4, 1000);
            Graph hamiltonian = new AdjacencyListsGraph<SimpleAdjacencyList>(false, verticesCount);
            int timesInStartVertice = rnd.Next(1, verticesCount / 2);
            int timesInStartv = 0;
            int v = 0; int t;
            while (timesInStartv < timesInStartVertice)
            {
                do
                {
                    t = rnd.Next(0, verticesCount);
                } while (t == v);
                hamiltonian.AddEdge(v, t);
                v = t;
                if (v == 0)
                    timesInStartv += 2;
            }
            return changeVerticesNumeration(hamiltonian);
        }        
        public static Graph semieulerian()
        {
            int verticesCount = rnd.Next(40, 1000);
            double density = rnd.Next(1,1000);
            density /= 1000;
            Graph semieulerian = rgg.SemiEulerGraph(typeof(AdjacencyListsGraph<SimpleAdjacencyList>), false, verticesCount, density);
            return changeVerticesNumeration(semieulerian);
        }
        public static Graph bipartite()
        {
            int n = rnd.Next(3, 500);
            int m = rnd.Next(3, 500);
            double density = rnd.Next(3, 1000);
            density /= 1000;            
            Graph bipartite = rgg.BipariteGraph(typeof(AdjacencyListsGraph<SimpleAdjacencyList>), n, m, density);
            return changeVerticesNumeration(bipartite);
        }
        public static Graph random()
        {
            int verticesCount = rnd.Next(2, 1000);
            double density = rnd.Next(1, 1000);
            density /= 1000;
            Graph random = rgg.UndirectedGraph(typeof(AdjacencyListsGraph<SimpleAdjacencyList>), verticesCount, density);
            return changeVerticesNumeration(random);
        }
        public static bool contains(IEnumerable<Edge> set, Edge e)
        {
            foreach (Edge ee in set)
            {
                if (ee.From == e.From && ee.To == e.To)
                    return true;
            }
            return false;
        }
        public static int[] permutation(int n)
        {
            int[] permutation = new int[n];            
            for (int i = 0; i < n; i++)
                permutation[i] = i;
            permutation = permutation.OrderBy(x => rnd.Next()).ToArray();
            return permutation;
        }
        public static Graph changeVerticesNumeration(Graph g)
        {
            int[] perm = permutation(g.VerticesCount);
            Graph gnew = new AdjacencyListsGraph<SimpleAdjacencyList>(false, g.VerticesCount);
            for (int i = 0; i < g.VerticesCount; i++)
                foreach (Edge e in g.OutEdges(i))
                    gnew.AddEdge( perm[e.From], perm[e.To] );
            return gnew;
        }
    }
}
