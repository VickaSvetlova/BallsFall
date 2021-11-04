using System;
using UnityEngine;

namespace BallsFall.Model
{
    public class BallModel
    {
        public int Reward { get; set; }
        public Color Color { get; set; }
        public float Speed { get; set; }
        public float Damage { get; set; }

        public BallModel(int reward, Color color, float speed, float damage)
        {
            Reward = reward;
            Color = color;
            Speed = speed;
            Damage = damage;
        }
    }
}