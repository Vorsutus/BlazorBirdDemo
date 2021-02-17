using System;

namespace BlazorBirdDemo.Web.Models
{
    public class PipeModel
    {
        //will render just outside the game container (500px wide)
        public int DistanceFromLeft { get; private set; } = 500; 

        //whenever there is a new pipe created, it will be given a random height between 0 and 60 pixels
        public int DistanceFromGround { get; private set; } = new Random().Next(0, 150);

        //distance between the top and bottom pipe
        public int Gap { get; set; } = 130;

        //pipe position height plus pipe height
        public int GapBottom => DistanceFromGround + 300;

        //bottom of the gap plus the gap height
        public int GapTop => GapBottom + Gap;

        public void Move(int Speed)
        {
            //How much we want the pipe to move by
            DistanceFromLeft -= Speed;
        }

        public bool IsOffScreen()
        {
            //return if the distance makes the pipe off screen on the left
            return DistanceFromLeft <= -60; ;
        }

        public bool IsCentered()
        {
            //check if the pipe is less/equal to half the game width plus half the bird width
            bool hasEnteredCenter = DistanceFromLeft <= (500 / 2) + (60 / 2);
            //check if the pipe is less/equal to half the game width minus half the bird minus the pipe width
            bool hasExitedCenter = DistanceFromLeft <= (500 / 2) - (60 / 2) - 60;

            //check if we have entered the middle zone and have not exited it yet
            return hasEnteredCenter && !hasExitedCenter;
        }
    }
}
