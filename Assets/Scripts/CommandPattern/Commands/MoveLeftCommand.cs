using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public class MoveLeftCommand : Command
    {
        private MoveObject moveObject;


        public MoveLeftCommand(MoveObject moveObject)
        {
            this.moveObject = moveObject;
        }


        public override void Execute()
        {
            moveObject.MoveLeft();
        }


        //Undo is just the opposite
        public override void Undo()
        {
            moveObject.MoveRight();
        }
    }
}
