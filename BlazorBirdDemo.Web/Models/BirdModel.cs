namespace BlazorBirdDemo.Web.Models
{
    public class BirdModel
    {
        //vertical position change over time
        public int DistanceFromGround { get; set; } = 100;

        public int JumpStrength { get; set; } = 50;

        public void Fall(int gravity)
        {
            //how far the bird will fall each time this method is called
            DistanceFromGround -= gravity;
        }

        public void Jump()
        {
            //if the bird is less than the height of the screen (mostly)...
            if(DistanceFromGround <= 530)
            {
                DistanceFromGround += JumpStrength;
            }
        }

        public bool IsOnGround()
        {
            //return whether the bird position is at or below where the ground starts or not
            return DistanceFromGround <= 0;
        }
    }
}
