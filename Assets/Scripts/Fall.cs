using System;
using UnityEngine;

namespace BallsFall
{
    public class Fall : MonoBehaviour
    {
        public float speed { get; set; }

        private void Update()
        {
            transform.position += Vector3.down * Time.deltaTime * speed;
        }
    }
}