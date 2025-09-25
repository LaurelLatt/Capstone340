using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public class MoveRightCommand : Command
    {
        private MoveObject moveObject;
    

        public MoveRightCommand(MoveObject moveObject)
        {
            this.moveObject = moveObject;
        }


        public override void Execute()
        {
            moveObject.MoveRight();
        }


        //Undo is just the opposite
        public override void Undo()
        {
            moveObject.MoveLeft();
        }
    }
}
