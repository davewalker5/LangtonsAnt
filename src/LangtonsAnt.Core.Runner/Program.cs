using System;
using System.IO;
using LangtonsAnt.Core.Entities;
using LangtonsAnt.Core.Events;

namespace LangtonsAnt.Core.Runner
{
    public class Program
    {
        private static StreamWriter _trackWriter;
        private static StreamWriter _colourWriter;

        public static void Main(string[] args)
        {
            Universe universe = new Universe(100);
            Simulator simulator = new Simulator();
            simulator.AntPositionChanged += OnAntPositionChanged;
            simulator.CellColourChanged += OnCellColourChanged;

            using (_trackWriter = new StreamWriter("anttrack.csv"))
            {
                using (_colourWriter = new StreamWriter("antcolour.csv"))
                {
                    _trackWriter.WriteLine("Step,X,Y");
                    _colourWriter.WriteLine("Step,X,Y,Colour");

                    simulator.Run(universe, 100000);

                    using (StreamWriter scatter = new StreamWriter("antscatter.csv"))
                    {
                        scatter.WriteLine("X,Y");
                        universe.WriteScatterPlot(scatter);
                    }
                }
            }

            Console.WriteLine($"Size\t\t: {universe.Size}");
            Console.WriteLine($"Started\t\t: {simulator.Started}");
            Console.WriteLine($"Completed\t: {simulator.Completed}");
            Console.WriteLine($"Steps\t\t: {simulator.Steps}");
            Console.WriteLine($"Run time\t: {simulator.RunTime} ms");
            Console.WriteLine($"Time Per Step\t: {decimal.Round(simulator.TimePerStep, 6)} ms");
        }

        private static void OnAntPositionChanged(object sender, AntPositionChangedEventArgs e)
        {
            _trackWriter.WriteLine($"{e.StepNumber},{e.Position.X},{e.Position.Y}");
        }

        private static void OnCellColourChanged(object sender, CellColourChangedEventArgs e)
        {
            _colourWriter.WriteLine($"{e.StepNumber},{e.Position.X},{e.Position.Y},{e.Colour}");
        }
    }
}
