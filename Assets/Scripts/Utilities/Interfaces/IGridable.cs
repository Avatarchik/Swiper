using UnityEngine;
using System.Collections;

namespace Minigames
{
    public interface IGridable
    {
        Direction Direction { get; set; }

        Pivot Pivot { get; set; }

        float Size { get; set; }

        float Shift { get; set; }

        void RecalculateGrid();
    }
}