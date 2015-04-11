using System;
using UnityEngine;
using System.Collections;

namespace Mapping
{
    public enum TileType
    {
        Grass,
        Tree,
        Wall,
        Home,
        Bank,
        Pub,
        Mine
    }

    // would have been a struct but there are some issues with by pass by 
    // value/reference for some methods e.g. Mapping.Map.FindTile can't return null for a struct
    public struct Tile
    {
        public TileType type;
        public GameObject obj;
        public int cost;

        public int x;
        public int y;

        public static int GetCost(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Grass: return 1;
                case TileType.Tree: return -1;
                case TileType.Wall: return -1;
                case TileType.Home: return 0;
                case TileType.Bank: return 0;
                case TileType.Pub: return 0;
                case TileType.Mine: return 0;
            }

            return 0;
        }

        public static Color GetColor(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Grass: return Color.green;
                case TileType.Tree: return Color.black;
                case TileType.Wall: return Color.black;
                case TileType.Home: return Color.blue;
                case TileType.Bank: return Color.blue;
                case TileType.Pub: return Color.blue;
                case TileType.Mine: return Color.blue;
            }

            return Color.gray;
        }
    }
}
