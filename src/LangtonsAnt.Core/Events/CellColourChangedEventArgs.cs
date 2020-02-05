using LangtonsAnt.Core.Entities;

namespace LangtonsAnt.Core.Events
{
    public class CellColourChangedEventArgs
    {
        public int StepNumber { get; set; }
        public Coordinate Position { get; set; }
        public int Colour { get; set; }
    }
}
