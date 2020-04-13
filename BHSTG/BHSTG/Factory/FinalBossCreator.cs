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
    class FinalBossCreator : EntityCreator
    {
        Vector2 startPos = new Vector2(60, 200), endPos = new Vector2(1500, 200);
        int health = 150;
        bool speedMode = true;
        GameSprite sprite;
        Texture2D bulletTexture;


        public FinalBossCreator(ContentManager content)
        {
            this.content = content;
            sprite = new GameSprite(content.Load<Texture2D>("Entities/final_boss"), startPos, Color.White);

            bulletTexture = content.Load<Texture2D>("Bullets/BossBullets");
        }


        public override Entity GetEntity()
        {
            return new FinalBoss(startPos, endPos, health, speedMode, sprite, bulletTexture,content, new MoveLikeFinal(), new ShootLikeFinal());
        }

    }
}
