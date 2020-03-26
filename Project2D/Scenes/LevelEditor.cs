using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathClasses;
using Project2D.TankGame;
using Raylib;
using static Raylib.Raylib;

namespace Project2D.Scenes
{
    using MathClasses;
    using Project2D.Scenes.LevelEditorTools;
    using Project2D.TankGame.Tiles;

    public class LevelEditor : Scene
    {
        public int ToolSelected = 0; // Draw - CameraZone - Settings

        public List<LevelEditorTool> toolBelt = new List<LevelEditorTool>();

        public TileGrid tileGrid;

        //Component Placing
        public List<Component> componentList = new List<Component>();

        //WorldScrolling
        Vector2 InitalClickPos = null;
        public float Zoom = 1f;

        //Mouse
        public Vector2 MousePos { get { return GetRelativeMousePosition(); } }
        public Vector2 GUIMousePos { get {return (new Vector2(GetMousePosition().x, GetMousePosition().y) / Program.GameZoom) / Zoom; } }
        public Vector2 GridMousePos { get { return MousePos / 16; } }

        public LevelEditor(Game _game) : base(_game)
        {
            //Fill grid empty
            int w = (int)Math.Ceiling((double)(Program.GameWidth / 16));
            int h = (int)Math.Ceiling((double)(Program.GameHeight / 16)) + 1;

            int[,] tempGrid = new int[w * 2, h * 2];

            for (int xx = 0; xx < tempGrid.GetLength(0); xx++)
            {
                for (int yy = 0; yy < tempGrid.GetLength(1); yy++)
                {
                    tempGrid[xx, yy] = 0;
                }
            }

            tileGrid = new TileGrid(tempGrid, 16, 16);

            //Tools
            toolBelt.Add(new TilePen(this));
            toolBelt.Add(new CameraZoneTool(this));
            toolBelt.Add(new EntityPenTool(this));
        }

        private Vector2 GetRelativeMousePosition()
        {
            Vector2 RefMousePos = (new Vector2(GetMousePosition().x, GetMousePosition().y) / Program.GameZoom);
            Vector2 CamOffSet = new Vector2(Program.camera.offset.x, Program.camera.offset.y);

            RefMousePos -= CamOffSet;

            RefMousePos /= Zoom;

            return RefMousePos;
        }

        private Vector2 GetMousePosNoOffset()
        {
            Vector2 RefMousePos = (new Vector2(GetMousePosition().x, GetMousePosition().y) / Program.GameZoom);

            RefMousePos /= Zoom;

            return RefMousePos;
        }

        //For Scroll zooming etx
        public void WorldNavigation()
        {
            //World scrolling
            if (IsMouseButtonPressed(MouseButton.MOUSE_MIDDLE_BUTTON))
            {
                InitalClickPos = MousePos;
            }

            if (IsMouseButtonDown(MouseButton.MOUSE_MIDDLE_BUTTON))
            {
                Vector2 newPos = InitalClickPos - (GetMousePosNoOffset());
                Program.camera.offset = -new Raylib.Vector2(newPos.x, newPos.y);
            }

            if (IsMouseButtonReleased(MouseButton.MOUSE_MIDDLE_BUTTON))
            {
                InitalClickPos = InitalClickPos - (MousePos);
            }

            if(GetMouseWheelMove() != 0)
            {
                Zoom += (float)(GetMouseWheelMove()) / 8;
                Zoom = Clamp(Zoom, .25f, 2);

                Program.camera.zoom = Zoom;
            }

            //PanicButton
            if(IsKeyPressed(KeyboardKey.KEY_F1))
            {
                Program.camera.offset = new Raylib.Vector2(0,0);

                Zoom = 1;
                Program.camera.zoom = Zoom;
            }
            
        }

        public override void Update()
        {
            WorldNavigation();

            //Tool belt update
            toolBelt[ToolSelected].Update();
        }

        public override void Draw()
        {
            //Draw 2d Grid for tiles
            for (int xx = 0; xx < tileGrid.TileGridValues.GetLength(0); xx++)
            {
                for (int yy = 0; yy < tileGrid.TileGridValues.GetLength(1); yy++)
                {
                    DrawRectangleLinesEx(new Rectangle(xx * 16, yy * 16, 16, 16), 1, Color.LIGHTGRAY);
                }
            }

            //Draw 2D grid for room
            int w = (int)Math.Ceiling((double)(Program.GameWidth / 16));
            int h = (int)Math.Ceiling((double)(Program.GameHeight / 16));

            for (int xx = 0; xx <= tileGrid.TileGridValues.GetLength(0) / w; xx++)
            {
                for (int yy = 0; yy < tileGrid.TileGridValues.GetLength(1) / h; yy++)
                {
                    DrawRectangleLinesEx(new Rectangle(xx * Program.GameWidth, yy * Program.GameHeight, Program.GameWidth, Program.GameHeight), 2, Color.BLACK);
                }
            }

            //Tool switching
            if(IsKeyPressed(KeyboardKey.KEY_RIGHT))
            {
                ToolSelected++;
            }

            if (IsKeyPressed(KeyboardKey.KEY_LEFT))
            {
                ToolSelected--;
            }

            if(ToolSelected < 0)
            {
                ToolSelected = toolBelt.Count - 1;
            }

            if (ToolSelected > toolBelt.Count-1)
            {
                ToolSelected = 0;
            }

            //DrawTiles
            tileGrid.DrawTiles();

            toolBelt[ToolSelected].Draw();

            foreach(LevelEditorTool tool in toolBelt)
            {
                tool.AllDraw();
            }

        }
    }
}
