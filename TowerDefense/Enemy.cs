using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public class Enemy : Sprite {
        protected float startHealth;
        protected float currentHealth;
        public float HealthPercentage {
            get { return currentHealth / startHealth; }
        }
        protected bool alive = true;
        protected float speed = 0.5f;
        protected int bountyGiven;
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

        public Enemy(Texture2D texture, Vector2 position, float health, int bountyGiven, float speed) : base(texture, position){
            this.startHealth = health;
            this.currentHealth = startHealth;
            this.bountyGiven = bountyGiven;
            this.speed = speed;
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
                base.Draw(spriteBatch);
            }
        }


    }
}
