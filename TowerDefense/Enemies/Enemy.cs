using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.Enemies{

    public enum Direction {
        NORTH,
        SOUTH,
        WEST, 
        EAST
    }

    public class Enemy : Sprite {
        protected float startHealth;
        protected float currentHealth;
        public float HealthPercentage {
            get { return currentHealth / startHealth; }
        }
        protected bool alive;
        protected float speed;
        protected int bountyGiven;
        protected Direction EnemyDirection { get;  set; }
        private Queue<Vector2> waypoints = new Queue<Vector2>();
        private float speedModifier;
        private float modifierDuration;
        private float modiferCurrentTime;
        public float SpeedModifier {
            get { return speedModifier; }
            set { speedModifier = value; }
        }
        public float ModifierDuration {
            get { return modifierDuration; }
            set   {
                modifierDuration = value;
                modiferCurrentTime = 0;
            }
        }
        public float DistanceToDestination  {
            get { return Vector2.Distance(position, waypoints.Peek()); }
        }
        public float CurrentHealth {
            get { return currentHealth; }
            set { currentHealth = value; }
        }
        public bool IsDead {
            get { return !alive; }
        }
        public int BountyGiven {
            get { return bountyGiven; }
        }

        //Animated enemy
        private new Texture2D texture;
        protected float timer = 0f;
        protected float interval = 200f;
        protected int currentFrame = 0;

        public Enemy(Texture2D texture, Vector2 position) : base(texture, position){
            this.texture = texture;
            alive = true;
        }

        public void SetWaypoints(Queue<Vector2> waypoints) {
            foreach (Vector2 waypoint in waypoints)
                this.waypoints.Enqueue(waypoint);
            this.position = this.waypoints.Dequeue();

        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (waypoints.Count > 0) {
                if (DistanceToDestination < speed) {
                    position = waypoints.Peek();
                    waypoints.Dequeue();
                }
                else   {
                    Vector2 direction = waypoints.Peek() - position;
                    if(direction.X > 0) {
                        EnemyDirection = Direction.EAST;
                        MoveRight(gameTime);
                    }
                    if(direction.X < 0) {
                        EnemyDirection = Direction.WEST;
                        MoveLeft(gameTime);
                    }
                    if(direction.Y > 0) {
                        EnemyDirection = Direction.SOUTH;
                        MoveDown(gameTime);
                    }
                    if(direction.Y < 0) {
                        EnemyDirection = Direction.NORTH;
                        MoveUp(gameTime);
                    }
                    direction.Normalize();
                    // Store the original speed.
                    float temporarySpeed = speed;
                    // If the modifier has finished,
                    if (modiferCurrentTime > modifierDuration)  {
                        // reset the modifier.
                        speedModifier = 0;
                        modiferCurrentTime = 0;
                    }
                    if (speedModifier != 0 && modiferCurrentTime <= modifierDuration)  {
                        // Modify the speed of the enemy.
                        temporarySpeed *= speedModifier;
                        // Update the modifier timer.
                        modiferCurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    velocity = Vector2.Multiply(direction, temporarySpeed);
                    position += velocity;
                }
            }
            else
                alive = false;

            if (currentHealth <= 0)
                alive = false;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (alive) {
               Rectangle rectangle = new Rectangle(32 * currentFrame, 0, 32, 32);
                base.Draw(spriteBatch, rectangle);
            }
        }

        public virtual void MoveRight(GameTime gameTime){        
	        if(currentFrame< 4 || currentFrame > 7)
            currentFrame = 4;	    	 
	        timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;	 
	    if (timer > interval) {
	        currentFrame++;         
	        if (currentFrame > 7) 
	            currentFrame = 4;        
	        timer = 0f;
	    }
	}
        public virtual void MoveUp(GameTime gameTime) {
            if(currentFrame < 0 || currentFrame > 3)
            currentFrame = 0;
            timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            if(timer > interval) {
                currentFrame++;
                if(currentFrame > 3)
                    currentFrame = 0;
                timer = 0f;
            }
        }
        public virtual void MoveDown(GameTime gameTime) {
            if(currentFrame < 12 || currentFrame > 15)
            currentFrame = 12;
            timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            if(timer > interval) {
                currentFrame++;
                if(currentFrame > 15)
                    currentFrame = 12;
                timer = 0f;
            }
        }
        public virtual void MoveLeft(GameTime gameTime) {
            if(currentFrame < 8 || currentFrame > 11)
            currentFrame = 8;
            timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            if(timer > interval) {
                currentFrame++;
                if(currentFrame > 11)
                    currentFrame = 8;
                timer = 0f;
            }
        }

    }
}
