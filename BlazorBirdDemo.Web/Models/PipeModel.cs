using System;

namespace BlazorBirdDemo.Web.Models
{
    public class PipeModel
    {
        //will render just outside the game container (500px wide)
        public int DistanceFromLeft { get; private set; } = 500; 

        //whenever there is a new pipe created, it will be given a random height between 0 and 60 pixels
        public int DistanceFromGround { get; private set; } = new Random().Next(0, 60);

        public void Move(int Speed)
        {
            //How much we want the pipe to move by
            DistanceFromLeft -= Speed;
        }
    }
}
