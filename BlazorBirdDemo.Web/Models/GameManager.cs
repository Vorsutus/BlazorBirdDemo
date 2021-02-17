using System;
using System.Threading.Tasks;

namespace BlazorBirdDemo.Web.Models
{
    //Monitors and controls whats happening at a high level
    //implementing interface NotifyPropertyChanged so that we can do something when it happens
    public class GameManager //: INotifyPropertyChanged ****REFACTORED
    {
        private readonly int _gravity = 2;
        private readonly int _speed = 2;

        //public event PropertyChangedEventHandler PropertyChanged; ****REFACTORED

        //generic EventHandler to listen for in the MainLoop to re-render screen when the loop completes
        public event EventHandler MainLoopCompleted; 

        public BirdModel Bird { get; set; }
        public PipeModel Pipe { get; set; }
        public bool IsRunning { get; set; } = false;

        public GameManager()
        {
            //Construct new instances of the Pipe and Bird Models
            Bird = new BirdModel();
            Pipe = new PipeModel();
        }

        public async void MainLoop()
        {
            IsRunning = true;

            //while the game is running..
            while (IsRunning)
            {
                //make the Bird fall
                Bird.Fall(_gravity);

                //send a notification that a property changed on the Bird
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bird))); ****REFACTORED

                //make the Pipe mode
                Pipe.Move(_speed);

                //send a notification that a property changed on the Pipe
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pipe))); ****REFACTORED

                //check is the bird is on the ground
                if(Bird.DistanceFromGround <= 0)
                {
                    GameOver();
                }

                //raising an event that triggers the listener in the game container
                MainLoopCompleted?.Invoke(this, EventArgs.Empty); //not supplying info, just raising an event

                //wait for 20 milliseconds
                await Task.Delay(20);
            }
        }

        public void Jump()
        {
            if (IsRunning)
            {
                Bird.Jump();
            }
        }

        public void StartGame()
        {           
            if (!IsRunning)
            {
                Bird = new BirdModel();
                Pipe = new PipeModel();
                MainLoop();
            }
        }

        public void GameOver()
        {
            IsRunning = false;
        }
    }
}
