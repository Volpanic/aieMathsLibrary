using Raylib;

namespace Project2D.TankGame
{
    using MathClasses;
    using Project2D.Scenes;

    public class SpindleBullet : Component
    {
        public Vector2 Velocity = new Vector2();

        private int MaxLifeTime = 120;
        private float LifeTimer = 0;

        public SpindleBullet(GameScene _gS, Texture2D _sprite) : base(_gS)
        {
            Sprite = _sprite;
            Dimensions = new Vector2(Sprite.width, Sprite.height);
            Origin = Dimensions / 2;
        }

        public override void Create()
        {

        }

        public override Rectangle GetCollisionRectangle()
        {
            Rectangle rect = new Rectangle((Position.x - Origin.x) + 2, (Position.y - Origin.y) + 2, Dimensions.x - 2, Dimensions.y - 2);

            return rect;
        }

        public override void Update()
        {
            //XCheck
            if (gameScene.tileGrid.RectTileCollision(GetCollisionRectangle()))
            {
                Active = false;
            }

            //YCheck
            if (gameScene.tileGrid.RectTileCollision(GetCollisionRectangle()))
            {
                Active = false;
            }

            Position += (Velocity * Game.deltaTime);

            LifeTimer += Game.deltaTime;

            if (LifeTimer >= MaxLifeTime)
            {
                Active = false;
            }
        }

        public override void Draw()
        {
            DrawSelf();
        }



    }
}
