using System.IO;

namespace LangtonsAnt.Core.Entities
{
    public class Universe
    {
        public int[] Matrix { get; private set; }
        public int Size { get; set; }

        public Universe(int size)
        {
            Matrix = new int[size * size];
            Size = size;
        }

        /// <summary>
        /// Reset the matrix
        /// </summary>
        public void Reset()
        {
            Matrix = new int[Size * Size];
        }

        /// <summary>
        /// Convert an X,Y coordinate into an index into the matrix
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int ConvertCoordinateToIndex(Coordinate c)
        {
            return c.X * Size + c.Y;
        }

        /// <summary>
        /// Return true if the specified point is off the matrix
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool IsOutOfBounds(Coordinate c)
        {
            return (c.X < 0) || (c.X >= Size) || (c.Y < 0) || (c.Y >= Size);
        }

        /// <summary>
        /// Write the universe data as a scatter plot
        /// </summary>
        /// <param name="writer"></param>
        public void WriteScatterPlot(StreamWriter writer)
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    int universeOffset = y * Size + x;
                    if (Matrix[universeOffset] == 1)
                    {
                        writer.WriteLine("{0},{1}", x, y);
                    }
                }
            }
        }
    }
}
