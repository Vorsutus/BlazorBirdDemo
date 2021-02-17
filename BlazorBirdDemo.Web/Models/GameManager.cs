using System;
using System.Collections.Generic;
using System.Linq;
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
        //public PipeModel Pipe { get; set; }
        public List<PipeModel> Pipes { get; set; } //need a collection of pipes, not just one

        public bool IsRunning { get; set; } = false;

        public GameManager()
        {
            //Construct new instances of the Pipe and Bird Models
            Bird = new BirdModel();
            //Pipe = new PipeModel();
            Pipes = new List<PipeModel>();
        }

        public async void MainLoop()
        {
            IsRunning = true;

            //while the game is running..
            while (IsRunning)
            {
                MoveObjects();

                CheckForCollisions();

                ManagePipes();

                //send a notification that a property changed on the Bird
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bird))); ****REFACTORED

                //send a notification that a property changed on the Pipe
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pipe))); ****REFACTORED

                //raising an event that triggers the listener in the game container
                MainLoopCompleted?.Invoke(this, EventArgs.Empty); //not supplying info, just raising an event

                //wait for 20 milliseconds
                await Task.Delay(20);
            }
        }

        public void ManagePipes()
        {
            //if the list of pipes don't have any objects in it...
            //OR if the last pipe in the list is halfway across the screen...
            if (!Pipes.Any() || Pipes.Last().DistanceFromLeft <= 250)
            {
                //add another pipe to the list
                Pipes.Add(new PipeModel());
            }

            //if it is true that the first pipe in the list if off the screen...
            if (Pipes.First().IsOffScreen())
            {
                //remove the pipe from the list of pipes
                Pipes.Remove(Pipes.First());
            }
        }

        public void MoveObjects()
        {
            //make the Bird fall
            Bird.Fall(_gravity);
            //make the Pipe mode
            //Pipe.Move(_speed);

            //Move each pipe in the collection of pipes
            //foreach(var pipe in Pipes)
            //{
            //    pipe.Move(_speed);
            //}
            Pipes.ForEach(x => x.Move(_speed)); //shorthand
        }

        public void CheckForCollisions()
        {
            //check if the bird is on the ground
            if (Bird.IsOnGround())
            {
                GameOver();
            }

            //the first pipe in pipes that has a value of true for IsCentered() (otherwise var is null value)
            var centeredPipe = Pipes.FirstOrDefault(p => p.IsCentered());

            //if there is a pipe in the center collision zone...
            if(centeredPipe != null)
            {
                //true if the bottom of the bird's distance to ground is beneath the bottom of the gap minus the height of the ground
                bool hasCollidedWithBottom = Bird.DistanceFromGround < centeredPipe.GapBottom - 150;
                //true if the top of the bird's distance to the ground is greater than the top of the gap minus the ground height
                bool hasCollidedWithTop = Bird.DistanceFromGround + 45 > centeredPipe.GapTop - 150;

                if(hasCollidedWithBottom || hasCollidedWithTop)
                {
                    GameOver();
                }
            }
        }

        public void Jump()
        {
            //if the game isn't over...
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
                //Pipe = new PipeModel();
                Pipes = new List<PipeModel>();
                MainLoop();
            }
        }

        public void GameOver()
        {
            IsRunning = false;
        }
    }
}
