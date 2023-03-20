using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGMovementComponent : HOGLogicMonoBehaviour
    {
        [SerializeField]
        private float speed = 1;

        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            StartMovement();
        }

        public void StopMovement()
        {
            rb.velocity = Vector2.zero;
        }

        public void StartMovement()
        {
            rb.velocity = transform.forward * speed;
        }
    }
}

