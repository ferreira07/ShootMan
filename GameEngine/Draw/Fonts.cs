using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Draw
{
    public class Fonts
    {
        private static Dictionary<EFontType, SpriteFont> _FontDict;

        public static void Load(ContentManager content)
        {
            _FontDict = new Dictionary<EFontType, SpriteFont>();
            _FontDict.Add(EFontType.Font1, content.Load<SpriteFont>("Arial"));            

        }

        public static SpriteFont GetFont(EFontType fontType)
        {
            return _FontDict[fontType];
        }
    }
}
