using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    //This class handles all methods that moves the object it's attached to
    public class MoveObject : MonoBehaviour
    {
        //Speed of the object
        private const float MOVE_STEP_DISTANCE = 0.02f;


        //These methods will be executed by their own command
        public void MoveUp()
        {
            Move(Vector3.up);
        }

        public void MoveDown()
        {
            Move(Vector3.down);
        }

        public void MoveLeft()
        {
            Move(Vector3.left);
        }

        public void MoveRight()
        {
            Move(Vector3.right);
        }


        //Help method to make it more general
        private void Move(Vector3 dir)
        {
            transform.Translate(dir * MOVE_STEP_DISTANCE);
        }
    }
}
