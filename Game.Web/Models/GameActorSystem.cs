using System;
using Akka.Actor;
using Game.ActorModel.Actors;

namespace Game.Web.Models
{
    public static class GameActorSystem
    {
        private static ActorSystem ActorSystem;

        public static void Create()
        {
            ActorSystem = Akka.Actor.ActorSystem.Create("GameSystem");

            ActorReferences.GameController = 
                ActorSystem.ActorOf<GameControllerActor>();
        }

        public static void Shutdown()
        {
            ActorSystem.Terminate()
                .Wait(TimeSpan.FromSeconds(1));
        }



        public static class ActorReferences
        {
            public static IActorRef GameController { get; set; }
        }
    }
}