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
        public int ParticleLifeTimer = 0;

        public int AnimationTime = 15;

        public Vector2 Velocity = new Vector2();

        public Particle(GameScene _gS, Texture2D _partSprite, Vector2 _velocity, float _rotation,int _maxPartTime = 120, int _animSpeed = 15) : base(_gS)
        {
            Sprite = _partSprite;
            Velocity = _velocity;
            Rotation = _rotation;
            MaxParticleLife = _maxPartTime;
            AnimationTime = _animSpeed;
        }

        public override void Create()
        {
            
        }

        public override void Update()
        {
            if(ParticleLifeTimer > MaxParticleLife)
            {
                Active = false;
            }

            if(AnimationTime % AnimationTime == 0)
            {
                ImageIndex++;
            }

            ParticleLifeTimer++;
        }

        public override void Draw()
        {
            DrawSelf();
        }
    }
}
