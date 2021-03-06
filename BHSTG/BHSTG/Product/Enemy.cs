﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BHSTG.States;

namespace BHSTG.Product
{
    public class Enemy : Entity
    {
        private Vector2 start, end;
        public double angle = 0, radius = 100;
        public int count = 0, baseX = 200;
        Random r = new Random();
        int shoot = 0;
        Texture2D bulletTexture;

		//iterator used to move enemy movement pattern horizontal
		float moveH;

        public Enemy(Vector2 position,Vector2 end, int health, bool speed, GameSprite sprite, Texture2D newBulletTexture) : base(position, health, speed, sprite)

        {
            position = new Vector2(0, 0);
            health = 25;
            speed = false;
            start.X = sprite.position.X; //setting up enemy start point
            start.Y = sprite.position.Y; //setting up enemy start point
            this.end.X = end.X; //setting up enemy start point
            this.end.Y = end.Y; //setting up enemy start point
            bulletTexture = newBulletTexture;
        }

        /*public void updateBullets(GameTime gameTime)
        {
            characterShooting.updateBullets(gameTime);
        }*/


        public override void Move(GameTime gameTime)
        {
            speedRate = 6;
			moveH = 2;
            characterMovements.circlePattern(ref gameTime, ref sprite, ref speedRate, ref start, ref end, moveH);
            //position = characterMovements.PlayerMovement(ref gameTime, ref sprite, ref speedRate);

        }

        //Will be used to help the enemy shoot from a certain location.
        public override List<Bullet> Shoot()
        {
            //characterShooting.ShootLikeGrunt1(toPosition, position, Texture, bulletTexture);
            return characterShooting.ShootLikeGrunt1(position, Texture, bulletTexture);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
            characterShooting.Draw(spriteBatch);

        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            position = sprite.position;
            shoot = gameTime.TotalGameTime.Milliseconds;

            /*if (shoot == 500)
            {
                Shoot();
            }*/
            //updateBullets(gameTime);

        }

        ~Enemy()
        {
            
        }
    }

}
