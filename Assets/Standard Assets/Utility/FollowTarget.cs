using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0f, 7.5f, 0f);
        public float speed = 2f;
        public void Start()
        {
            if(!target)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset,Time.deltaTime*speed);
        }

        public void ResetPosition()
        {
            transform.position = transform.position + offset;
        }
    }
}
