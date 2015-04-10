using System;
using UnityEngine;
using System.Collections;

namespace Mapping
{
    public enum TileType
    {
        Grass,
        Tree,
        Path,
        Dirt,
        Wall
    }

    public struct Tile
    {
        public TileType type;
        public GameObject obj;
    }

    public class Map
    {
        private Tile[,] tiles;
        private int length;
        private int width;

        public Map(TileType[,] data)
        {
            length = data.GetLength(0);
            width = data.GetLength(1);
            tiles = new Tile[length, width];

            // create the map using the data
            BuildMap(data);
        }

        public Tile GetTile(int x, int y)
        {
            return tiles[x, y];
        }

        public void SetTile(int x, int y, TileType tileType)
        {
            Debug.Log(tileType);
            tiles[x, y].type = tileType;
            tiles[x, y].obj.GetComponent<Renderer>().material.color = Color.red;
        }

        private void BuildMap(TileType[,] data)
        {
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
            tilePlane.GetComponent<Renderer>().material.color = Color.green;

            tiles[x, y].obj = tilePlane;
        }
    }
}
