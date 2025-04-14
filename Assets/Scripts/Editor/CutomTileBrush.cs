using DualGrid;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Editor
{
    [CustomGridBrush(false, true, true, "Custom Tile Brush")]
    public class CustomTileBrush : GridBrush
    {
        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            base.Paint(gridLayout, brushTarget, position);
            DualGridTilemap.OnTileChanged?.Invoke(position);
        }

        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            base.Erase(gridLayout, brushTarget, position);
            DualGridTilemap.OnTileChanged?.Invoke(position);
        }

        public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
        {
            base.BoxFill(gridLayout, brushTarget, position);
            for (var i = position.xMin; i <= position.xMax; i++)
            {
                for (var j = position.yMin; i <= position.yMax; i++)
                {
                    DualGridTilemap.OnTileChanged?.Invoke(new Vector3Int(i, j));
                }
            }
        }

        public override void BoxErase(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
        {
            base.BoxErase(gridLayout, brushTarget, position);
            for (var i = position.xMin; i <= position.xMax; i++)
            {
                for (var j = position.yMin; i <= position.yMax; i++)
                {
                    DualGridTilemap.OnTileChanged?.Invoke(new Vector3Int(i, j));
                }
            }
        }
    }
}
