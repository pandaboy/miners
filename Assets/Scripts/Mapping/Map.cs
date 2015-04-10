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

    public struct Tile
    {
        public TileType type;
        public GameObject obj;
        public int cost;

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
            switch(tileType)
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

    public class Map
    {
        private Tile[,] tiles;
        private GameObject tile_group;

        private int length;
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        private int width;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public Map(TileType[,] data)
        {
            // create the map using the data
            BuildMap(data);
        }

        public Tile GetTile(int x, int y)
        {
            return tiles[x, y];
        }

        public int GetTileCost(int x, int y)
        {
            return GetTile(x, y).cost;
        }

        public void SetTile(int x, int y, TileType tileType)
        {
            tiles[x, y].type = tileType;
            tiles[x, y].obj.GetComponent<Renderer>().material.color = Color.red;
        }

        public void SetTileColor(int x, int y, Color color)
        {
            tiles[x, y].obj.GetComponent<Renderer>().material.color = color;
        }

        private void BuildMap(TileType[,] data)
        {
            // store dimensions based on array given.
            length = data.GetLength(0);
            width = data.GetLength(1);
            
            // create 2D array
            tiles = new Tile[length, width];

            // used to organize the tiles.
            tile_group = new GameObject();
            tile_group.name = "Tiles";

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    BuildTile(i, j, data[i, j]);
                }
            }
        }

        private void BuildTile(int x, int y, TileType tileType)
        {
            GameObject tilePlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            tilePlane.transform.position = new Vector3(
                x * 10,
                0,
                y * 10
            );
            tilePlane.GetComponent<Renderer>().material.color = Tile.GetColor(tileType);

            // grouped to tile_group
            tilePlane.transform.parent = tile_group.transform;

            // update the tile array data
            tiles[x, y].type = tileType;
            tiles[x, y].cost = Tile.GetCost(tileType);
            tiles[x, y].obj = tilePlane;
        }
    }
}
