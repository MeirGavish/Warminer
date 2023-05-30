using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGMovementComponent : HOGLogicMonoBehaviour
    {
        [SerializeField] private float speed = 1;

        [SerializeField] private Rigidbody2D rigid_body;

        void Start()
        {
            StartMovement();
        }

        public void StopMovement()
        {
            rigid_body.velocity = Vector2.zero;
        }

        public void StartMovement()
        {
            rigid_body.velocity = transform.up * speed;
        }
    }
}

