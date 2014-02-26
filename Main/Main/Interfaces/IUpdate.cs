using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main.Interfaces
{
    interface IUpdate
    {
        void Update(GameTime gameTime);
       void Update(GameTime gameTime, Player player);
        
    }
}
