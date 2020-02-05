using System;
using LangtonsAnt.Core.Entities;
using LangtonsAnt.Core.Events;

namespace LangtonsAnt.Core
{
    public class Simulator
    {
        // For a given cell colour and direction, this array
        // determines the X and Y motion of the Virtual Ant
        private static int[] _movementArray = { -1,  0, 1,  1,  0, 3,
                                                 0, -1, 2,  0,  1, 0,
                                                 1,  0, 3, -1,  0, 1,
                                                 0,  1, 0,  0, -1, 2 };

        public event EventHandler<AntPositionChangedEventArgs> AntPositionChanged;
        public event EventHandler<CellColourChangedEventArgs> CellColourChanged;

        public DateTime Started { get; private set; }
        public DateTime Completed { get; private set; }
        public long RunTime { get; private set; }
        public int Steps { get; private set; }
        public decimal TimePerStep { get; private set; }

        /// <summary>
        /// Run the simulation
        /// </summary>
        public void Run(Universe universe, int maximumSteps)
        {
            // Initial ant position and direction, at the center of the universe
            // and pointing down
            Ant ant = new Ant
            {
                Position = new Coordinate { X = universe.Size / 2, Y = universe.Size / 2 },
                Direction = 2
            };

            // Report the initial position of the ant
            Steps = 0;
            AntPositionChanged?.Invoke(this, new AntPositionChangedEventArgs
            {
                Position = ant.Position,
                StepNumber = Steps
            });

            bool simulationCompleted = false;
            Started = DateTime.Now;
            while (!simulationCompleted)
            {
                Steps++;

                // Determine the current cell position and colour
                int currentX = ant.Position.X;
                int currentY = ant.Position.Y;
                int index = universe.ConvertCoordinateToIndex(ant.Position);
                int currentCellColour = universe.Matrix[index];

                // Move the ant
                int movementOffset = ant.Direction * 6 + currentCellColour * 3;
                ant.Position.X += _movementArray[movementOffset];
                ant.Position.Y += _movementArray[movementOffset + 1];
                ant.Direction = _movementArray[movementOffset + 2];

                AntPositionChanged?.Invoke(this, new AntPositionChangedEventArgs
                {
                    Position = ant.Position,
                    StepNumber = Steps
                });

                // Invert the previous cell
                universe.Matrix[index] = Math.Abs(currentCellColour - 1);
                CellColourChanged?.Invoke(this, new CellColourChangedEventArgs
                {
                    Position = new Coordinate { X = currentX, Y = currentY },
                    StepNumber = Steps,
                    Colour = universe.Matrix[index]
                });

                simulationCompleted = universe.IsOutOfBounds(ant.Position) ||
                                        ((Steps >= maximumSteps) && (maximumSteps > 0));
            }

            Completed = DateTime.Now;
            RunTime = (long)(Completed - Started).TotalMilliseconds;
            TimePerStep = (decimal)RunTime / (decimal)Steps;
        }
    }
}
