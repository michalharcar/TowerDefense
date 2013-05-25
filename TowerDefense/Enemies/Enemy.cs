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
        protected float speed;
        public float CurrentHealth { get; set; }
        public float HealthPercentage {
            get { return CurrentHealth / startHealth; }
        }
        public int GoldGiven { get; protected set; }

        protected bool alive;
        public bool IsDead {
            get { return !alive; }
        }
        
        protected Direction EnemyDirection { get; set; }
        private Queue<Vector2> waypoints = new Queue<Vector2>();
        public float DistanceToDestination {
            get { return Vector2.Distance(position, waypoints.Peek()); }
        }

        // slow motion
        private float modifierDuration;
        public float ModifierDuration {
            set {
                modifierDuration = value;
                modifierCurrentTime = 0;
            }
        }
        private float modifierCurrentTime;
        public float SpeedModifier { get; set; } 

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
                        Move(gameTime);
                    }
                    if(direction.X < 0) {
                        EnemyDirection = Direction.WEST;
                        Move(gameTime);
                    }
                    if(direction.Y > 0) {
                        EnemyDirection = Direction.SOUTH;
                        Move(gameTime);
                    }
                    if(direction.Y < 0) {
                        EnemyDirection = Direction.NORTH;
                        Move(gameTime);
                    }
                    direction.Normalize();
                    // Store the original speed.
                    float temporarySpeed = speed;
                    // If the modifier has finished,
                    if (modifierCurrentTime > modifierDuration)  {
                        // reset the modifier.
                        SpeedModifier = 0;
                        modifierCurrentTime = 0;
                    }
                    if (SpeedModifier != 0 && modifierCurrentTime <= modifierDuration)  {
                        temporarySpeed *= SpeedModifier;
                        modifierCurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    velocity = Vector2.Multiply(direction, temporarySpeed);
                    position += velocity;
                }
            }
            else
                alive = false;
            if (CurrentHealth <= 0)
                alive = false;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (alive) {
               Rectangle frame = new Rectangle(32 * currentFrame, 0, 32, 32);
               base.Draw(spriteBatch, frame);
            }
        }

        protected virtual void Move(GameTime gameTime) {
            switch(EnemyDirection) {
                case Direction.NORTH:
                    if(currentFrame < 0 || currentFrame > 3)
                        currentFrame = 0;
                    timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                    if(timer > interval) {
                        currentFrame++;
                        if(currentFrame > 3)
                            currentFrame = 0;
                        timer = 0f;
                    }
                    break;

                case Direction.EAST:
                    if(currentFrame < 4 || currentFrame > 7)
                        currentFrame = 4;
                    timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                    if(timer > interval) {
                        currentFrame++;
                        if(currentFrame > 7)
                            currentFrame = 4;
                        timer = 0f;
                    }
                    break;

                case Direction.WEST:
                    if(currentFrame < 8 || currentFrame > 11)
                        currentFrame = 8;
                    timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                    if(timer > interval) {
                        currentFrame++;
                        if(currentFrame > 11)
                            currentFrame = 8;
                        timer = 0f;
                    }
                    break;

                case Direction.SOUTH:
                    if(currentFrame < 12 || currentFrame > 15)
                        currentFrame = 12;
                    timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                    if(timer > interval) {
                        currentFrame++;
                        if(currentFrame > 15)
                            currentFrame = 12;
                        timer = 0f;
                    }
                    break;
            }

        }

    }
}
