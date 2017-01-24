using System;
using System.Collections.Generic;


public class Card
{
    public string name;
    public int value;

    public Card()
    {
        name = "Empty";
        value = 0;
    }

    public Card(string nm, int val)
    {
        name = nm;
        value = val;
    }
}