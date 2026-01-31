using UnityEngine;
using System.Collections.Generic;


public class BoardManager : MonoBehaviour
{

    public List<TableBrain> tables;
    public List<PartyGoerBrain> people;



    public void updateBoard()
    {
        for (int j = 0; j < tables.Count; j++) //super giga loops but basically it loops through all the partygoers and then checks if they're satisfied, drinking, eating, etc.
        {
            TableBrain table = tables[j];
            for (int i = 0; i < table.myChairs.Length; i++)
            {
                ChairBrain chair = table.myChairs[i];

                if (chair.myPerson)
                {
                    PartyGoerBrain person = chair.myPerson;
                    bool satisfied = true;
                    for (int x = 0; x < person.wants.Count; x++)
                    {
                        switch (person.wants[x])
                        {
                            case PartyGoerBrain.Want.sit_with_someone_with_mood_happy:
                                satisfied = satisfied && searchMood(table.type, i, table.myChairs, PartyGoerBrain.Mood.happy);
                                break;
                            case PartyGoerBrain.Want.be_alone:
                                satisfied = satisfied && !searchPeople(table.type, i, table.myChairs);
                                break;
                        }
                    }
                    person.satisfied = satisfied;
                }
            }
        }
        for (int i = 0; i < people.Count; i++)
        {
            if (!people[i].satisfied)
            {
                return;
            }
        }
        Win();
    }

    public void Win()
    {
        Debug.Log("Winned!");
    }

    //helper functions to check if person is satisfied
    public bool searchMood(TableBrain.Type tableType, int chairIndex, ChairBrain[] chairs, PartyGoerBrain.Mood mood)
    {
        if (chairs[(chairIndex - 1 + chairs.Length) % chairs.Length].myPerson != null && chairs[(chairIndex -1 + chairs.Length) % chairs.Length].myPerson.myMood == mood)
        {
            return true;
        }
        if (chairs[(chairIndex + 1) % chairs.Length].myPerson != null && chairs[(chairIndex + 1) % chairs.Length].myPerson.myMood == mood)
        {
            return true;
        }
        if (tableType == TableBrain.Type.square)
        {
            if (chairs[(chairIndex - chairs.Length/2) % chairs.Length].myPerson != null && chairs[(chairIndex + chairs.Length/2) % chairs.Length].myPerson.myMood == mood)
            {
                return true;
            }
        }
        return false;
    }

    public bool searchStyle(TableBrain.Type tableType, int chairIndex, ChairBrain[] chairs, PartyGoerBrain.Style style)
    {
        if (chairs[(chairIndex - 1 + chairs.Length) % chairs.Length].myPerson!= null && chairs[(chairIndex - 1 + chairs.Length) % chairs.Length].myPerson.myStyle == style)
        {
            return true;
        }
        if (chairs[(chairIndex + 1) % chairs.Length].myPerson != null && chairs[(chairIndex + 1) % chairs.Length].myPerson.myStyle == style)
        {
            return true;
        }
        if (tableType == TableBrain.Type.square)
        {
            if (chairs[(chairIndex + chairs.Length/2) % chairs.Length].myPerson != null && chairs[(chairIndex + chairs.Length / 2) % chairs.Length].myPerson.myStyle == style)
            {
                return true;
            }
        }
        return false;
    }

    public bool searchWants(TableBrain.Type tableType, int chairIndex, ChairBrain[] chairs, PartyGoerBrain.Want want)
    {
        if (chairs[(chairIndex - 1 + chairs.Length) % chairs.Length].myPerson)
        {
            List<PartyGoerBrain.Want> curWants = chairs[(chairIndex - 1 + chairs.Length) % chairs.Length].myPerson.wants;
            for (int i = 0; i < curWants.Count; i++)
            {
                if (curWants[i] == want)
                {
                    return true;
                }
            }
        }

        if (chairs[(chairIndex + 1) % chairs.Length].myPerson)
        {
            List<PartyGoerBrain.Want> curWants = chairs[(chairIndex + 1) % chairs.Length].myPerson.wants;
            for (int i = 0; i < curWants.Count; i++)
            {
                if (curWants[i] == want)
                {
                    return true;
                }
            }
        }

        if (tableType == TableBrain.Type.square)
        {
            if (chairs[(chairIndex + chairs.Length/2) % chairs.Length].myPerson)
            {
                List<PartyGoerBrain.Want> curWants = chairs[(chairIndex + chairs.Length/2) % chairs.Length].myPerson.wants;
                for (int i = 0; i < curWants.Count; i++)
                {
                    if (curWants[i] == want)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool searchPeople(TableBrain.Type tableType, int chairIndex, ChairBrain[] chairs)//just checks if people exist
    {
        if (chairs[(chairIndex - 1 + chairs.Length) % chairs.Length].myPerson != null)
        {
            return true;
        }
        if (chairs[(chairIndex + 1) % chairs.Length].myPerson != null)
        {
            return true;
        }
        if (tableType == TableBrain.Type.square)
        {
            if (chairs[(chairIndex + chairs.Length / 2) % chairs.Length].myPerson != null)
            {
                return true;
            }
        }

        return false;
    }
}
