using BHSTG.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BHSTG.Controls;
using BHSTG.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using BHSTG.ShootStrategy;
using BHSTG.MoveStrategy;

namespace BHSTG.Factory
{
    class PlayerCreator : EntityCreator
    {

        Vector2 position;
        int health = 100;
        bool speedMode = false;
        GameSprite sprite;
        Texture2D bulletTexture;
        

        public PlayerCreator(ContentManager content) 
        {
            this.content = content;
            position = new Vector2(1920 / 2, 750);
            sprite = new GameSprite(content.Load<Texture2D>("Entities/fly-right-left"), new Vector2(1920 / 2, 750), Color.White);

            bulletTexture = content.Load<Texture2D>("Bullets/blast-up");
        }

		public override Entity GetEntity()
		{
			return new Player(position, health, speedMode, sprite, bulletTexture,content, new MoveLikePlayer(), new ShootLikePlayer(this.content));
		}







		//public override Entity makeEntity(GameSprite sprte, Vector2 pos, Vector2 end, int Health, bool spdMode)
		//{
		//	return new Player(pos, end, Health, spdMode, sprte);

		//}




	}
}
