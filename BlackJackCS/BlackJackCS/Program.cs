using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Card card = new Card();
    static List<Card> deck = new List<Card>();
    static List<Card> player = new List<Card>();
    static List<Card> dealer = new List<Card>();

    static void Main()
    {
        makeDeck(deck);
        RandomShuffle(deck);

        Console.WriteLine("Hello! You have 1000$ to play Black Jack for!");
        Console.WriteLine("How much do you want to bet?");

        int money = 1000;
        string tmpBet = "";

        tmpBet = Console.ReadLine();
        int bet;
        Int32.TryParse(tmpBet, out bet);

        while (bet > money)
        {
            Console.WriteLine("Whoa! You don't have that much money!\nTry again!");
            tmpBet = Console.ReadLine();
            Int32.TryParse(tmpBet, out bet);
        }

        dealCard(deck, player, 2);

        foreach (Card card in player)
        {
            Console.WriteLine(card.name);
        }

        //foreach (Card card in deck)
        //{
        //    Console.WriteLine(card.name + " " + card.value);
        //}

        Console.ReadLine();
    }

    static void makeDeck(List<Card> deck)
    {
        string tmpString = "";
        for (int i = 0; i < 52; i++)
        {
            int tmpInt = (i % 13) + 1;
            switch (tmpInt)
            {
                case 1:
                    tmpString = "Ace";
                    break;
                case 11:
                    tmpString = "Jack";
                    break;
                case 12:
                    tmpString = "Queen";
                    break;
                case 13:
                    tmpString = "King";
                    break;
                default:
                    tmpString = tmpInt.ToString();
                    break;
            }
            deck.Add(new Card(tmpString, tmpInt));
        }  
    }

    static void RandomShuffle<T>(List<T> list)
    {
        Random rng = new Random();
        int i = list.Count;

        while (i > 1)
        {
            i--;
            int next = rng.Next(i + 1);
            T temp = list[next];
            list[next] = list[i];
            list[i] = temp;
        }
    }
    
    static void dealCard(List<Card> deck, List<Card> hand, int it)
    {
        for (int i = 0; i < it; i++)
        {
            hand.Add(deck.First());
            deck.Remove(deck.First());
        }
    }
}
