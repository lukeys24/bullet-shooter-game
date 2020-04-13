﻿using System;
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
    class Grunt1Creator : EntityCreator
    {
        ScriptReader sr = new ScriptReader();

        Vector2 startPos = new Vector2(60, 20), endPos = new Vector2(1500, 200);
        int health = 25;
        bool speedMode = true;
        GameSprite sprite;
        Texture2D bulletTexture;

        public Grunt1Creator(ContentManager content)
        {
            this.content = content;
            int startPosition = sr.xStart;
            
            sprite = new GameSprite(content.Load<Texture2D>("Entities/grunt1"), startPos, Color.White);

            bulletTexture = content.Load<Texture2D>("Bullets/blast2-down");
        }

        public override Entity GetEntity()
        {
            return new Grunt1(startPos, endPos, health, speedMode, sprite, bulletTexture,content, new MoveLikeGrunt1(), new ShootLikeGrunt());
        }
    }
}
