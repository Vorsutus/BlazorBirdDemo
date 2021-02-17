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
            if(DistanceFromGround <= 530)
            {
                DistanceFromGround += JumpStrength;
            }
        }
    }
}
