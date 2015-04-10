using System;
using UnityEngine;
using System.Collections;

namespace Mapping
{
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

        public Tile GetTile(int x, int y)
        {
            return tiles[x, y];
        }

        public int GetTileCost(int x, int y)
        {
            return GetTile(x, y).cost;
        }

        public void FindTile(TileType tileType, out Tile tile)
        {
            tile = new Tile();
            // go through all the tiles and find the first one with a matching type.
            // linear search
            int i = 0, j = 0;
            for (; i < length; i++)
            {
                for (j = 0; j < width; j++)
                {
                    if (tiles[i, j].type == tileType)
                    {
                        tile = tiles[i, j];
                        // force break out of outer loop
                        i = length + 1;
                        break;
                    }
                }
            }
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

        public void SetTileColor(Tile tile, Color color)
        {
            tile.obj.GetComponent<Renderer>().material.color = color;
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
