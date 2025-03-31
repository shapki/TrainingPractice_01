using System;

namespace Shapkin_Task_8
{
    public class CellState : ICloneable
    {
        public State State { get; set; } = State.Healthy;
        public int InfectionTime { get; set; } = 0;
        public int ImmuneTime { get; set; } = 0;

        public CellState Clone()
        {
            return (CellState)MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
