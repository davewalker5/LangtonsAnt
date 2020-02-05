using System;
using LangtonsAnt.Core.Entities;

namespace LangtonsAnt.Core.Events
{
    public class AntPositionChangedEventArgs : EventArgs
    {
        public int StepNumber { get; set; }
        public Coordinate Position { get; set; }
    }
}
