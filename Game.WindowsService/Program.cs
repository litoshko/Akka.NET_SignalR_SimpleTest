using System;
using Akka.Actor;
using Game.ActorModel.Actors;
using Topshelf;

namespace Game.WindowsService
{
    public class GameStateService
    {
        private ActorSystem ActorSystemInstance;

        public void Start()
        {
            ActorSystemInstance = ActorSystem.Create("GameSystem");

            var gameController = ActorSystemInstance.ActorOf<GameControllerActor>("GameController");
        }

        public void Stop()
        {
            ActorSystemInstance.Terminate()
                .Wait(TimeSpan.FromSeconds(2));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(gameService =>
            {
                gameService.Service<GameStateService>(s =>
                {
                    s.ConstructUsing(game => new GameStateService());
                    s.WhenStarted(game => game.Start());
                    s.WhenStopped(game => game.Stop());
                });

                gameService.RunAsLocalSystem();
                gameService.StartAutomatically();

                gameService.SetDescription("PSDemo Game Topshelf Service");
                gameService.SetDisplayName("PSDemoGame");
                gameService.SetServiceName("PSDemoGame");
            });

        }
    }
}
