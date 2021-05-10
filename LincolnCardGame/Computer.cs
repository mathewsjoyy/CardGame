﻿using System;

namespace LincolnCardGame
{
    internal class Computer : Player
    {
        // Constructor which calls base (abstract class) constructor
        public Computer(Deck deck, int id) : base(deck, id)
        {
            Console.WriteLine($"Hello I'm Computer (PLAYER {Id}) Who You Will Try Beat You!" +
                " I Have 10 Cards In My Hand!\n");
        }

        // Computer picks 2 random cards to play,removing them from the Hand
        public override Tuple<Card, Card> Play2Cards()
        {
            if (PlayerHand.AHand.Count <= 1)
            {
                Console.WriteLine($"Not enough cards in Player {Id} Hand...");
                return null;
            }

            Random random = new Random();

            Card card1 = PlayerHand.AHand[random.Next(PlayerHand.AHand.Count - 1)];
            PlayerHand.AHand.Remove(card1);
            Card card2 = PlayerHand.AHand[random.Next(PlayerHand.AHand.Count - 1)];
            PlayerHand.AHand.Remove(card2);

            return Tuple.Create(card1, card2);
        }
    }
}