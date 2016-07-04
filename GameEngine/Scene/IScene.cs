﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Scene
{
    public interface IScene
    {
        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        event EventHandler<IScene> ChangeScene;
    }
}
