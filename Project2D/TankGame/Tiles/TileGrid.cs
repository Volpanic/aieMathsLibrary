﻿using MathClasses;
using System;
using static Raylib.Raylib;


namespace Project2D.TankGame.Tiles
{
    public class TileGrid
    {
        public int[,] TileGridValues;

        public int CellWidth = 16;
        public int CellHeight = 16;

        public TileGrid(int[,] _tileGrid, int _cellWidth = 16, int _cellHeight = 16)
        {
            TileGridValues = _tileGrid;
            CellWidth = _cellWidth;
            CellHeight = _cellHeight;
        }

        public void DrawTiles()
        {
            for (int xx = 0; xx < TileGridValues.GetLength(0); xx++)
            {
                for (int yy = 0; yy < TileGridValues.GetLength(1); yy++)
                {
                    if (TileGridValues[xx, yy] == 1)
                    {
                        DrawRectangleRec(MathMore.toRayRect(new Rectangle(xx * CellWidth, yy * CellHeight, CellWidth, CellHeight)), MathMore.toRayColour(Colour.Gray));
                        DrawRectangleLinesEx(MathMore.toRayRect(new Rectangle(xx * CellWidth, yy * CellHeight, CellWidth, CellHeight)), 2, MathMore.toRayColour(Colour.Black));
                    }
                }
            }
        }

        public bool RectTileCollision(Rectangle rect)
        {
            // Get rounded tile positions
            float minx = (float)Math.Floor(rect.x / CellWidth) - 1;
            float miny = (float)Math.Floor(rect.y / CellHeight) - 1;

            float maxx = (float)Math.Ceiling((rect.x + rect.width) / CellWidth) + 1;
            float maxy = (float)Math.Ceiling((rect.y + rect.height) / CellHeight) + 1;

            Vector2 min = new Vector2(minx, miny);
            Vector2 max = new Vector2(maxx, maxy);

            //Set range of tile view
            float MinSpaceX = Math.Max(min.x, 0);
            float MinSpaceY = Math.Max(min.y, 0);

            float MaxSpaceX = Math.Min(max.x, TileGridValues.GetLength(0) - 1);
            float MaxSpaceY = Math.Min(max.y, TileGridValues.GetLength(1) - 1);

            //Loop thorugh tile pos
            for (int xx = (int)MinSpaceX; xx <= MaxSpaceX; xx++)
            {
                for (int yy = (int)MinSpaceY; yy <= MaxSpaceY; yy++)
                {
                    if (TileGridValues[xx, yy] == 1)
                    {
                        //Creates a rect representing the tile
                        Rectangle TileRect = new Rectangle(xx * CellWidth, yy * CellHeight, CellWidth, CellHeight);

                        if (rect.CollidingWith(TileRect))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
