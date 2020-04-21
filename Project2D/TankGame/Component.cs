using Raylib;
using static Raylib.Raylib;

namespace Project2D.TankGame
{
    using MathClasses;
    using Project2D.Scenes;

    public abstract class Component
    {
        public Texture2D Sprite;  // Sprite
        public Vector2 Position;  // World Pos
        public float Rotation;    // Rotation for the sprite
        public Vector2 Origin;    // Draw Origin
        public Vector2 Dimensions;// Width and height of the sprite / a sprites cell
        public Vector2 Scale;     // Size
        public Color Blend;       // Colour

        public int ImageIndex = 0;// Decides which cell of animation to draw from a sprite sheet
        public bool Active = true;

        protected GameScene gameScene;

        public Component(GameScene _gameScene)
        {
            //Set blanks
            Position = Vector2.Zero;
            Rotation = 0.0f;
            Origin = Vector2.Zero;
            Dimensions = Vector2.Zero;
            Scale = new Vector2(1, 1);

            gameScene = _gameScene;

            Blend = Color.WHITE;

            //Runs create which allows children to set sprites and stuff
            Create();
        }

        //Abstracts
        public abstract void Create();

        //Virtuals

        public virtual void Update() { }
        public virtual void Draw() { }
        public virtual void OnDestroy() { } //Called when an objects Alive is set to false (Called from the scene running the code)

        public virtual Rectangle GetCollisionRectangle() // Returns a collision rectangle
        {
            Rectangle rect = new Rectangle(Position.x - Origin.x, Position.y - Origin.y, Dimensions.x, Dimensions.y);

            return rect;
        }

        public void DrawSelf() // Default draw, draws sprites from a sprite sheet( Which isn't actually used in game)
        {
            Rectangle destRectangle = new Rectangle(Position.x, Position.y, Dimensions.x * Scale.x, Dimensions.y * Scale.y); // Scales the drawn image

            if (new Vector2(Sprite.width, Sprite.height) == Dimensions) //Draw flat sprite, i.e not a sprite sheet
            {
                // new Raylib.Vector2 was the best work around i could find, i could use the basic draw class
                // but that doesnt allow rotation and stuff

                Rectangle imageRect = new Rectangle(0, 0, Dimensions.x + 1, Dimensions.y);
                DrawTexturePro(Sprite, MathMore.toRayRect(imageRect), MathMore.toRayRect(destRectangle), new Raylib.Vector2(Origin.x, Origin.y), Rotation, Blend);
            }
            else
            {
                float sheetXPos = Dimensions.x * ImageIndex;

                if (sheetXPos > Sprite.width) // Sets the animation back to first frame
                {
                    sheetXPos = 0;
                    ImageIndex = 0;
                }

                Rectangle imageRect = new Rectangle(sheetXPos, 0, Dimensions.x + 1, Dimensions.y);
                DrawTexturePro(Sprite, MathMore.toRayRect(imageRect), MathMore.toRayRect(destRectangle), new Raylib.Vector2(Origin.x, Origin.y), Rotation, Blend);
            }
        }
    }
}
