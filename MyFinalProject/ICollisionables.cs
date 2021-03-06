using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinalProject
{
    interface IColisionable
    {
        public FloatRect GetBounds();

        public void OnCollisionEnter(IColisionable other);
        public void OnCollisionStay(IColisionable other);
        public void OnCollisionExit(IColisionable other);

    }
}
