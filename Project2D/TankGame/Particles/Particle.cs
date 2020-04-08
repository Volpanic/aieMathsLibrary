using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2D.Scenes;
using Raylib;
using static Raylib.Raylib;

namespace Project2D.TankGame.Particles
{
    using MathClasses;
    public class Particle : Component
    {
        public int MaxParticleLife = 120;
        public float ParticleLifeTimer = 0;
        bool Fade = false;
        public int AnimationTime = 15;

        public Vector2 Velocity = new Vector2();

        public Particle(GameScene _gS, Texture2D _partSprite,Vector2 pos, Vector2 _velocity, float _rotation,bool _fade = false,int _maxPartTime = 120, int _animSpeed = 0) : base(_gS)
        {
            Sprite = _partSprite;
            Velocity = _velocity;
            Rotation = _rotation;
            MaxParticleLife = _maxPartTime;
            AnimationTime = _animSpeed;
            Position = pos;
            Fade = _fade;
            Dimensions = new Vector2(Sprite.width,Sprite.height);
        }

        public override void Create()
        {
            
        }

        public override void Update()
        {
            
        }

        public override void Draw()
        {

            Position += Velocity;
            if (Fade)
            {
                ParticleLifeTimer += Game.deltaTime;
                Blend.a = (byte)(255 - (240 * (ParticleLifeTimer / MaxParticleLife)));
            }

            if (ParticleLifeTimer > MaxParticleLife)
            {
                Active = false;
            }

            DrawSelf();


        }
    }
}
