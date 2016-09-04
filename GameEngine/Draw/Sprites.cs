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

        public static void Load(ContentManager content, string folderName)
        {
            _SpriteDict = new Dictionary<ESpriteType, Sprite>();
            _SpriteDict.Add(ESpriteType.char1, LoadCharacter(content, folderName+"char0"));
            _SpriteDict.Add(ESpriteType.char2, LoadCharacter(content, folderName + "char1"));
            _SpriteDict.Add(ESpriteType.char3, LoadCharacter(content, folderName + "char2"));
            _SpriteDict.Add(ESpriteType.Title, LoadImage(content, folderName + "titulo"));
            _SpriteDict.Add(ESpriteType.Bullet, Load(content, folderName + "bullet1", 10));
            _SpriteDict.Add(ESpriteType.Bullet2, Load(content, folderName + "bullet2", 20));
            _SpriteDict.Add(ESpriteType.Bullet3, LoadTimeAnimated(content, folderName + "bullet3", 10, 10, 4, 0.5f));
            _SpriteDict.Add(ESpriteType.Fireball, LoadTimeAnimated(content, folderName + "fireball", 16,16,4, 0.3f));
            _SpriteDict.Add(ESpriteType.RollingStone, LoadAnimated(content, folderName + "rollingstone", 32,32,4, 1));

            _SpriteDict.Add(ESpriteType.Poison, Load(content, folderName + "poison", 25));
            _SpriteDict.Add(ESpriteType.Grass1, Load(content, folderName + "grass1", 32));
            _SpriteDict.Add(ESpriteType.Grass2, Load(content, folderName + "grass2", 32));

            _SpriteDict.Add(ESpriteType.Fada, LoadDirectionTimeAnimated(content, folderName + "fairy", 24, 32, 4, 1));
            _SpriteDict.Add(ESpriteType.EmergingStone, LoadTimeAnimated(content, folderName + "emergingstone", 32, 32, 13, 0.1f));

            _SpriteDict.Add(ESpriteType.Water, LoadTimeAnimated(content, folderName + "water", 32, 32, 2, 1.5f));

            _SpriteDict.Add(ESpriteType.HpBar, Load(content, folderName + "hpbar", 1, 2));
            _SpriteDict.Add(ESpriteType.MpBar, Load(content, folderName + "mpbar", 1, 2));
            _SpriteDict.Add(ESpriteType.BarBackground, Load(content, folderName + "BarBackground", 1));
            _SpriteDict.Add(ESpriteType.ColisionArea, Load(content, folderName + "ColisionArea", 1));
            _SpriteDict.Add(ESpriteType.Crate, Load(content, folderName + "crate", 32));
            _SpriteDict.Add(ESpriteType.RedGem, LoadTimeAnimated(content, folderName + "redgem", 14,14,4, 1));
            _SpriteDict.Add(ESpriteType.BlueGem, LoadTimeAnimated(content, folderName + "bluegem", 14, 14, 4, 1));
            _SpriteDict.Add(ESpriteType.GreenGem, LoadTimeAnimated(content, folderName + "greengem", 14, 14, 4, 1));
            _SpriteDict.Add(ESpriteType.barrier1, LoadList(content, folderName + "barrier1", new Rectangle(0,0,32,32), new Rectangle(32, 0, 32, 32)));
            _SpriteDict.Add(ESpriteType.barrier2, LoadList(content, folderName + "barrier2", new Rectangle(0, 0, 32, 32), new Rectangle(32, 0, 32, 32)));
            _SpriteDict.Add(ESpriteType.barrier3, LoadList(content, folderName + "barrier3", new Rectangle(0, 0, 32, 32), new Rectangle(32, 0, 32, 32)));
        }

        private static Sprite LoadTimeAnimated(ContentManager content, string imagePath, int w, int h, int count, float time)
        {
            Rectangle[] positions = new Rectangle[count];
            for (int i = 0; i < count; i++)
            {
                positions[i] = new Rectangle(i * w, 0, w, h);
            }
            return new TimeChangeSprite(content.Load<Texture2D>(imagePath), positions, time);
        }

        private static Sprite LoadCharacter(ContentManager content, string imagePath)
        {
            return LoadAnimated(content, imagePath, 32, 48, 4);
        }

        private static Sprite LoadAnimated(ContentManager content, string imagePath, int w, int h, int count, int directionType = 0)
        {
            return new BasicPlayerSprite(content.Load<Texture2D>(imagePath), w, h, count, directionType);
        }
        private static Sprite LoadDirectionTimeAnimated(ContentManager content, string imagePath, int w, int h, int count, int directionType = 0)
        {
            return new DirectionTimeChangeSprite(content.Load<Texture2D>(imagePath), w, h, count, directionType);
        }
        
        public static Sprite GetSprite(ESpriteType spriteType)
        {
            return _SpriteDict[spriteType].Clone();
        }

        private static Sprite LoadImage(ContentManager content, string imagePath)
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
