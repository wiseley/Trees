using System;
using System.Collections.Generic;
using Trees.Services;

namespace Trees.Models.Stateful
{
    public class Table
    {
        public Table(Guid guid, Deck<Land> lands, Deck<Tree> trees, Deck<Event> events, List<Player> players) 
        {            
            LandDeck = lands;
            TreeDeck = trees;
            EventDeck = events;
            Players = players;
            Id = guid;
        }
        public Guid Id { get; set; }
        public Deck<Land> LandDeck { get; private set; } 
        public Deck<Tree> TreeDeck { get; set; } 
        public Deck<Event> EventDeck { get; set; }
        public List<Player> Players { get; set; } 
        public int CurrentPlayer { get; private set; } = 0;
        public int NextPlayer()
        {
            CurrentPlayer++;
            if (CurrentPlayer == Players.Count) CurrentPlayer = 0;
            return CurrentPlayer;
        }
        public Player GetCurrentPlayer()
        {
            return this.Players[this.CurrentPlayer];
        }
        public Event CurrentEvent { get; set; }
        public List<Grove> Groves { get; set; } = new List<Grove>();
        public List<string> TurnLog { get; set; } = new List<string>();
        public List<string> GameLog { get; set; } = new List<string>();
        public GameState GameState { get; set; } = GameState.EventProcessing;
    }
}