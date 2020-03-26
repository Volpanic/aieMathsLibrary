using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2D.Scenes.LevelEditorTools
{
    public abstract class LevelEditorTool
    {
        public string Name = "No Name Tool";

        public LevelEditor levelScene;

        public LevelEditorTool(LevelEditor _ls)
        {
            levelScene = _ls;
        }

        public abstract void Update();
        public abstract void Draw();

        public virtual void AllDraw() { }
    }
}
