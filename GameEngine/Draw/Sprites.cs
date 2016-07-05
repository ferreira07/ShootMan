using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Draw
{
    public class Sprites
    {
        private static Dictionary<ESpriteType, Sprite> _SpriteDict;

        public static void Load(ContentManager content)
        {
            _SpriteDict = new Dictionary<ESpriteType, Sprite>();
            _SpriteDict.Add(ESpriteType.char1, Load(content, "Images\\char0"));
            _SpriteDict.Add(ESpriteType.char2, Load(content, "Images\\char1"));
            _SpriteDict.Add(ESpriteType.char3, Load(content, "Images\\char2"));
            _SpriteDict.Add(ESpriteType.Title, Load(content, "Images\\titulo"));
            _SpriteDict.Add(ESpriteType.Bullet, Load(content, "Images\\bullet1", 10));
            _SpriteDict.Add(ESpriteType.Bullet2, Load(content, "Images\\bullet2", 20));

            _SpriteDict.Add(ESpriteType.barrier11, LoadList(content, "Images\\Barrier1", new Rectangle(0,0,32,32), new Rectangle(32, 0, 32, 32)));
        }

        public static Sprite GetSprite(ESpriteType spriteType)
        {
            return _SpriteDict[spriteType].Clone();
        }

        private static Sprite Load(ContentManager content, string imagePath)
        {
            return Load(content, imagePath, 32, 48);
        }
        private static Sprite Load(ContentManager content, string imagePath, int size)
        {
            return Load(content, imagePath, size, size);
        }
        private static Sprite Load(ContentManager content, string imagePath, int w, int h)
        {
            return new Sprite()
            {
                Texture = content.Load<Texture2D>(imagePath),
                SourceRectangle = new Rectangle(0, 0, w, h)
            };
        }
        private static DamageChangeSprite LoadList(ContentManager content, string imagePath, params Rectangle[] sourceRectangles)
        {
            DamageChangeSprite ret = new DamageChangeSprite()
            {
                Texture = content.Load<Texture2D>(imagePath),
                Positions = sourceRectangles,
                SourceRectangle = sourceRectangles.FirstOrDefault()
            };
            return ret;
        }
    }
}
