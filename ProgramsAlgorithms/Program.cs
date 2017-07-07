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
            //Add files with graph to the Debug folder and then put theit names into the list below.
            //All the output files will be prepared in a folder with timestamp in the Debug directory
            List<string> fileNames = new List<string>() { "In.txt", "PetersenGraph.txt" };
            string mainFolderWithTimestamp = DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss");
            Directory.CreateDirectory(mainFolderWithTimestamp);
            foreach (var inputFileName in fileNames)
            {
                Graph graphToColor;
                try
                {
                    graphToColor = GraphFiles.GetGraphFromFile(inputFileName);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"File has not been found: {inputFileName}");
                    Console.ReadLine();
                    return;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Wrong data in the file: {inputFileName}");
                    Console.ReadLine();
                    continue;
                }

                List<ColoringGraphs> coloringAgorithms = new List<ColoringGraphs>() { new LargestFirst(), new SmallestLast(), new DSatur() };
                foreach (var algorithm in coloringAgorithms)
                {
                    string[] splitInputFileName = inputFileName.Split(new char[] { '.' });
                    string outputFileName = $@"{mainFolderWithTimestamp}\" + splitInputFileName[0] + algorithm.Name + ".txt";
                    using (StreamWriter writer = new StreamWriter(outputFileName))
                    {
                        int[] verticesColros = algorithm.GetPaintedVertices(graphToPaint: graphToColor, limit: 3, verbose: true);
                        writer.WriteLine($"{graphToColor.VerticesCount} {algorithm.ColorsUsedCount}");
                        for (int i = 0; i < graphToColor.VerticesCount; i++)
                        {
                            writer.WriteLine($"{i} {verticesColros[i]}");
                        }
                    }
                }
            }
            Console.WriteLine("Press any key to close");
            Console.ReadLine();
        }
    }
}
