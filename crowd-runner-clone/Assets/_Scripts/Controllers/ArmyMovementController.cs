using System;
using _Scripts.Models;
using _Scripts.Utilities;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Controllers
{
    public class ArmyMovementController : MonoBehaviour
    {
        private Touch touch;
        private Vector3 touchDown;
        private Vector3 touchUp;
        private bool dragStarted;
        private Rigidbody rb;
        public bool isGamePaused;
        [SerializeField] private bool editorTest;
        
        private void Start()
        {
            rb = this.GetComponent<Rigidbody>();
        }
        void FixedUpdate()
        {
            if (isGamePaused != false) return;
            
            //CLAMP X POS
            var pos = transform.position;
            pos.x =  Mathf.Clamp(transform.position.x, -Constants.CLAMP_MODIFIER, Constants.CLAMP_MODIFIER);
            transform.position = pos;
            
            // TRANSFORM FORWARD
            this.transform.Translate(transform.forward * Constants.SPEED_COEFFICIENT);

            
            if (!editorTest)
            {
                TouchControl();
            }
            else
            {
                EditorControl();
            }

        }

        private void TouchControl()
        {
            // rb.velocity = (transform.forward + CalculateDirection()) * Constants.MOVEMENT_SPEED;
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    dragStarted = true;
                    touchUp = touch.position;
                    touchDown = touch.position;
                }
                ChangeMovementDirection(CalculateDirection());
            }
            if (dragStarted)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    touchDown = touch.position;
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    touchDown = touch.position;
                    dragStarted = false;
                }
            }

        }

        private void EditorControl()
        {

            if(Input.GetMouseButton(0))
            {
                // rb.velocity =  (transform.forward + CalculateDirection()) * Constants.MOVEMENT_SPEED;
                ChangeMovementDirection(CalculateDirection());
                dragStarted = true;
                touchDown = Input.mousePosition;

            }
            if (Input.GetMouseButtonUp(0))
            {
                touchUp = Input.mousePosition;
                dragStarted = false;
            }
        }
        
        private void ChangeMovementDirection(Vector3 dir)
        {
            this.transform.Translate(dir * Constants.SENSITIVITY_COEFFICIENT);
        }

        private Vector3 CalculateDirection()
        {
            Vector3 temp = (touchDown - touchUp).normalized;
            temp.z = 0;
            temp.y = 0;
            return temp;
        }
    }
}
