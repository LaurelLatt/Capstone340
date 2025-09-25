using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public class MoveDownCommand : Command
    {
        private MoveObject moveObject;


        public MoveDownCommand(MoveObject moveObject)
        {
            this.moveObject = moveObject;
        }


        public override void Execute()
        {
            moveObject.MoveDown();
        }


        //Undo is just the opposite
        public override void Undo()
        {
            moveObject.MoveUp();
        }
    }
}
