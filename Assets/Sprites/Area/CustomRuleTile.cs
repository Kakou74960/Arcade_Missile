using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class CustomRuleTile : RuleTile<CustomRuleTile.Neighbor> {
    public RuleTile ruleTileOverride;

    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int AlienWall = 3;
        public const int NotNull = 4;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.AlienWall: return tile == ruleTileOverride;
            case Neighbor.NotNull: return tile != null;
        }
        return base.RuleMatch(neighbor, tile);
    }
}