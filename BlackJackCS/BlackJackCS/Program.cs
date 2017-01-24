using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Card card = new Card();
    static List<Card> deck = new List<Card>();
    static List<Card> player = new List<Card>();
    static List<Card> dealer = new List<Card>();
    static bool playAgain = true;
    static int money = 1000;

    static void Main()
    {
        makeDeck(deck);
        RandomShuffle(deck);
        
        Console.WriteLine("Hello! You have 1000$ to play Black Jack for!");

        dealCard(deck, player, 2);
        dealCard(deck, dealer, 2);

        do {
            //Make bet
            Console.WriteLine("How much do you want to bet?");
            Console.WriteLine("Money: " + money);
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

            //Player hand
            Console.Write("Your hand contains: ");
            foreach (Card card in player)
            {
                Console.Write("[" + card.name + "] ");
                if ((card.name == "Ace") && (calculateHand(player) > 21))
                {
                    card.value = 1;
                }
            }
            Console.WriteLine("Total: " + (calculateHand(player)));

            //Dealer hand
            Console.Write("\nDealer hand contains: ");
            foreach (Card card in dealer)
            {
                Console.Write("[" + card.name + "] ");
                if ((card.name == "Ace") && (calculateHand(dealer) > 21))
                {
                    card.value = 1;
                }
            }
            Console.WriteLine("Total: " + (calculateHand(dealer)));

            if (calculateHand(player) == 21)
            {
                Console.WriteLine("Black Jack!");
                money = money + bet;
                player.Clear();
                dealer.Clear();

                dealCard(deck, player, 2);
                dealCard(deck, dealer, 2);
            }

            if (calculateHand(player) != 21)
            {
                //Ask for another card
                Console.WriteLine("\nDo you want another card? (y/n)");
                string input = Console.ReadLine();
                if (input == "y")
                {
                    dealCard(deck, player, 1);
                    if (calculateHand(player) > 21)
                    {
                        foreach (Card card in player)
                        {
                            Console.Write("[" + card.name + "] ");
                        }
                        Console.WriteLine("Your total is: " + calculateHand(player));
                        Console.WriteLine("\nYou got FAT!\nYou lose!\n");
                        money = money - bet;
                        player.Clear();
                        dealer.Clear();

                        dealCard(deck, player, 2);
                        dealCard(deck, dealer, 2);

                        if (money <= 0)
                        {
                            Console.WriteLine("You're out of money! Please leave!");
                            break;
                        }
                    }
                    else if (calculateHand(player) == 21)
                    {
                        Console.WriteLine("Black Jack!");
                        money = money + bet;
                        player.Clear();
                        dealer.Clear();

                        dealCard(deck, player, 2);
                        dealCard(deck, dealer, 2);
                    }
                }

                else if (input == "n")
                {
                    while (calculateHand(dealer) < 17)
                    {
                        dealCard(deck, dealer, 1);
                        if (calculateHand(dealer) <= 21 && calculateHand(dealer) > 17 && calculateHand(dealer) > calculateHand(player))
                        {
                            Console.WriteLine("Dealer win!");
                            money = money - bet;
                            player.Clear();
                            dealer.Clear();

                            dealCard(deck, player, 2);
                            dealCard(deck, dealer, 2);
                            break;
                        }
                        else if(calculateHand(dealer) > 21)
                        {
                            Console.WriteLine("Dealer is fat! You win!");
                            money = money + bet;
                            player.Clear();
                            dealer.Clear();

                            dealCard(deck, player, 2);
                            dealCard(deck, dealer, 2);
                            break;
                        }
                        else if(calculateHand(dealer) < calculateHand(player))
                        {
                            Console.WriteLine("You win!");
                            money = money + bet;
                            player.Clear();
                            dealer.Clear();

                            dealCard(deck, player, 2);
                            dealCard(deck, dealer, 2);
                            break;
                        }
                    }
                }
            }
        } while (playAgain);

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
                    tmpInt = 11;
                    break;
                case 11:
                    tmpString = "Jack";
                    tmpInt = 10;
                    break;
                case 12:
                    tmpString = "Queen";
                    tmpInt = 10;
                    break;
                case 13:
                    tmpString = "King";
                    tmpInt = 10;
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

    static int calculateHand(List<Card> hand)
    {
        int total = 0;
        for (int i = 0; i < hand.Count; i++)
        {
            total = hand[i].value + total;
        }
        return total;
    }
}
