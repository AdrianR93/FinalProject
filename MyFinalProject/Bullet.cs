using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFinalProject
{
    class Bullet : GameObject
    {
        public Bullet(Vector2f startPosition) : base("Sprites" + Path.DirectorySeparatorChar + "tiro.png", startPosition) { }
    }
}
