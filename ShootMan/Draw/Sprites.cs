using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMan.Draw
{
    public class Sprites
    {
        private static Dictionary<ESprite, Sprite> _SpriteDict;

        public static void Load(ContentManager content)
        {
            _SpriteDict = new Dictionary<ESprite, Sprite>();
            _SpriteDict.Add(ESprite.char1, Load(content, "Images\\char0"));
            _SpriteDict.Add(ESprite.char2, Load(content, "Images\\char1"));
            _SpriteDict.Add(ESprite.char3, Load(content, "Images\\char2"));
        }

        private static Sprite Load(ContentManager content, string imagePath)
        {
            return new Sprite()
            {
                Texture = content.Load<Texture2D>(imagePath),
                SourceRectangle = new Rectangle(0, 0, 32, 48)
            };
        }

        public static Sprite GetSprite(ESprite spriteType)
        {
            return _SpriteDict[spriteType];
        }
    }
}
