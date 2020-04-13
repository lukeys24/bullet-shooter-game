using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BHSTG.Product;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using BHSTG.MoveStrategy;
using BHSTG.ShootStrategy;

namespace BHSTG.Factory
{
    class MidBossCreator : EntityCreator
    {
        Vector2 startPos = new Vector2(0,300), endPos = new Vector2(1500,200);
        int health = 100;
        bool speedMode = true;
        GameSprite sprite;
        Texture2D bulletTexture;

        public MidBossCreator(ContentManager content)
        {
            this.content = content;
            sprite = new GameSprite(content.Load<Texture2D>("Entities/mid_boss"), startPos, Color.White);

            bulletTexture = content.Load<Texture2D>("Bullets/BossBullets");
        }

        public override Entity GetEntity()
        {
            return new MidBoss(startPos, endPos, health, speedMode, sprite, bulletTexture,content, new MoveLikeMid(), new ShootLikeMid());
        }
    }
}
