using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BoardManager : MonoBehaviour
{

    public List<TableBrain> tables;
    public List<PartyGoerBrain> people;

    public string nextLevel;
    public float fadeDelay = 0.5f; // Time after everyone is done celebrating to fade
    public float celebrateDelay = 0.2f; // Time in between each celebration

    private CameraFade cameraFade;

    private void Start()
    {
        cameraFade = FindFirstObjectByType<CameraFade>();
        updateBoard();
    }

    public void updateBoard()
    {

        for (int i = 0; i < people.Count; i++) // apply masks
        {
            ApplyMask(people[i]);
        }

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
                            case PartyGoerBrain.Want.sit_with_someone_with_mood_neutral:
                                satisfied = satisfied && searchMood(table.type, i, table.myChairs, PartyGoerBrain.Mood.neutral);
                                break;
                            case PartyGoerBrain.Want.sit_with_someone_with_mood_sad:
                                satisfied = satisfied && searchMood(table.type, i, table.myChairs, PartyGoerBrain.Mood.sad);
                                break;
                            case PartyGoerBrain.Want.sit_with_someone_with_style_plain:
                                satisfied = satisfied && searchStyle(table.type, i, table.myChairs, PartyGoerBrain.Style.plain);
                                break;
                            case PartyGoerBrain.Want.dont_sit_with_someone_with_style_plain:
                                satisfied = satisfied && !searchStyle(table.type, i, table.myChairs, PartyGoerBrain.Style.plain);
                                break;
                            case PartyGoerBrain.Want.sit_with_someone_with_style_fancy:
                                satisfied = satisfied && searchStyle(table.type, i, table.myChairs, PartyGoerBrain.Style.fancy);
                                break;
                            case PartyGoerBrain.Want.sit_with_someone_with_style_professional:
                                satisfied = satisfied && searchStyle(table.type, i, table.myChairs, PartyGoerBrain.Style.professional);
                                break;
                            case PartyGoerBrain.Want.talk_with_someone:
                                satisfied = satisfied && searchWants(table.type, i, table.myChairs, PartyGoerBrain.Want.talk_with_someone);
                                break;
                            case PartyGoerBrain.Want.eat_with_someone:
                                satisfied = satisfied && searchWants(table.type, i, table.myChairs, PartyGoerBrain.Want.eat_with_someone);
                                break;
                            case PartyGoerBrain.Want.drink_with_someone:
                                satisfied = satisfied && searchWants(table.type, i, table.myChairs, PartyGoerBrain.Want.drink_with_someone);
                                break;
                            case PartyGoerBrain.Want.partnered:
                                satisfied = satisfied && searchWants(table.type, i, table.myChairs, PartyGoerBrain.Want.partnered);
                                break;
                            case PartyGoerBrain.Want.be_alone:
                                satisfied = satisfied && !searchPeople(table.type, i, table.myChairs);
                                break;
                            case PartyGoerBrain.Want.limited_number_of_people_at_table:
                                satisfied = satisfied && (TablePopulation(table)==person.limited_number_of_people_at_table_limit);
                                break;
                            case PartyGoerBrain.Want.sit_next_to_only_x_people:
                                satisfied = satisfied && (CountNeighbors(table.type, i, table.myChairs) == person.sit_next_to_only_x_people_x);
                                break;
                            case PartyGoerBrain.Want.circle_table:
                                satisfied = satisfied && table.type == TableBrain.Type.circle;
                                break;
                            case PartyGoerBrain.Want.square_table:
                                satisfied = satisfied && table.type==TableBrain.Type.square;
                                break;
                            case PartyGoerBrain.Want.end_of_a_table:
                                satisfied = satisfied && checkSeatAttribute(i, table.myChairs, ChairBrain.Attribute.end_of_table);
                                break;
                            case PartyGoerBrain.Want.drink_wine: //jesus exclusive
                                satisfied = satisfied && checkSeatAttribute(i, table.myChairs, ChairBrain.Attribute.wine);
                                break;
                            case PartyGoerBrain.Want.center_of_table: //jesus exclusive
                                satisfied = satisfied && checkSeatAttribute(i, table.myChairs, ChairBrain.Attribute.center_of_table);
                                break;
                            case PartyGoerBrain.Want.soft_seat:
                                satisfied = satisfied && checkSeatType(i, table.myChairs, ChairBrain.Type.soft);
                                break;
                            case PartyGoerBrain.Want.wood_seat:
                                satisfied = satisfied && checkSeatType(i, table.myChairs, ChairBrain.Type.wood);
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
       StartCoroutine(Win());
    }
    
    private IEnumerator Win()
        {
            foreach (PartyGoerBrain person in people)
            {
                Animator anim = person.gameObject.GetComponentInChildren<Animator>();
                anim.SetTrigger("win");
                yield return new WaitForSeconds(celebrateDelay);
            }
            // Debug.Log("Winned!");
            yield return new WaitForSeconds(fadeDelay);
            cameraFade.FadeOut(nextLevel);
        }

    //helper functions to check if person is satisfied
    public bool searchMood(TableBrain.Type tableType, int chairIndex, ChairBrain[] chairs, PartyGoerBrain.Mood mood)
    {
        if (tableType == TableBrain.Type.square)
        {
            if (chairs[(chairIndex + chairs.Length / 2) % chairs.Length].myPerson != null && chairs[(chairIndex + chairs.Length / 2) % chairs.Length].myPerson.myMood == mood)
            {
                return true;
            }
            if (chairIndex != 0 && chairIndex != chairs.Length / 2)
            {
                if (chairs[chairIndex - 1].myPerson  && chairs[chairIndex-1].myPerson.myMood == mood)
                {
                    return true;
                }
            }
            if (chairIndex != chairs.Length-1 && chairIndex != chairs.Length / 2-1)
            {
                if (chairs[chairIndex + 1].myPerson && chairs[chairIndex + 1].myPerson.myMood == mood)
                {
                    return true;
                }
            }

            return false;
            
        }
        if (chairs[(chairIndex - 1 + chairs.Length) % chairs.Length].myPerson != null && chairs[(chairIndex -1 + chairs.Length) % chairs.Length].myPerson.myMood == mood)
        {
            return true;
        }
        if (chairs[(chairIndex + 1) % chairs.Length].myPerson != null && chairs[(chairIndex + 1) % chairs.Length].myPerson.myMood == mood)
        {
            return true;
        }
        
        return false;
    }

    public bool searchStyle(TableBrain.Type tableType, int chairIndex, ChairBrain[] chairs, PartyGoerBrain.Style style)
    {
        if (tableType == TableBrain.Type.square)
        {
            if (chairs[(chairIndex + chairs.Length / 2) % chairs.Length].myPerson != null && chairs[(chairIndex + chairs.Length / 2) % chairs.Length].myPerson.myStyle == style)
            {
                return true;
            }
            if (chairIndex != 0 && chairIndex != chairs.Length / 2)
            {
                if (chairs[chairIndex - 1].myPerson && chairs[chairIndex - 1].myPerson.myStyle == style)
                {
                    return true;
                }
            }
            if (chairIndex != chairs.Length - 1 && chairIndex != chairs.Length / 2 - 1)
            {
                if (chairs[chairIndex + 1].myPerson && chairs[chairIndex + 1].myPerson.myStyle == style)
                {
                    return true;
                }
            }

            return false;

        }
        if (chairs[(chairIndex - 1 + chairs.Length) % chairs.Length].myPerson!= null && chairs[(chairIndex - 1 + chairs.Length) % chairs.Length].myPerson.myStyle == style)
        {
            return true;
        }
        if (chairs[(chairIndex + 1) % chairs.Length].myPerson != null && chairs[(chairIndex + 1) % chairs.Length].myPerson.myStyle == style)
        {
            return true;
        }
        return false;
        
    }

    public bool searchWants(TableBrain.Type tableType, int chairIndex, ChairBrain[] chairs, PartyGoerBrain.Want want)
    {
        if (tableType == TableBrain.Type.square)
        {
            if (chairs[(chairIndex + chairs.Length / 2) % chairs.Length].myPerson)
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
            if (chairIndex != 0 && chairIndex != chairs.Length / 2)
            {   if (chairs[chairIndex - 1].myPerson)
                {
                    List<PartyGoerBrain.Want> curWants = chairs[chairIndex - 1].myPerson.wants;
                    for (int i = 0; i < curWants.Count; i++)
                    {
                        if (curWants[i] == want)
                        {
                            return true;
                        }
                    }
                }
                
            }
            if (chairIndex != chairs.Length - 1 && chairIndex != chairs.Length / 2 - 1)
            {
                if (chairs[chairIndex + 1].myPerson)
                {
                    List<PartyGoerBrain.Want> curWants = chairs[chairIndex + 1].myPerson.wants;
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

        
        return false;
    }

    public bool searchPeople(TableBrain.Type tableType, int chairIndex, ChairBrain[] chairs)//just checks if people exist
    {
        if (tableType == TableBrain.Type.square)
        {
            if (chairs[(chairIndex + chairs.Length / 2) % chairs.Length].myPerson != null)
            {
                return true;
            }
            if (chairIndex != 0 && chairIndex != chairs.Length / 2)
            {
                if (chairs[chairIndex - 1].myPerson)
                {
                    return true;
                }
               
            }
            if (chairIndex != chairs.Length - 1 && chairIndex != chairs.Length / 2 - 1)
            {
                if (chairs[chairIndex + 1].myPerson)
                {
                    return true;
                }
            }

            return false;

        }
        if (chairs[(chairIndex - 1 + chairs.Length) % chairs.Length].myPerson != null)
        {
            return true;
        }
        if (chairs[(chairIndex + 1) % chairs.Length].myPerson != null)
        {
            return true;
        }
        

        return false;
    }

    public bool checkSeatType(int chairIndex, ChairBrain[] chairs, ChairBrain.Type type)// checks chair type
    {
        if (chairs[chairIndex].type == type)
        {
            return true;
        }
        

        return false;
    }

    public bool checkSeatAttribute(int chairIndex, ChairBrain[] chairs, ChairBrain.Attribute attribute)// checks chair type
    {
        List<ChairBrain.Attribute> list = chairs[chairIndex].attributes;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == attribute)
            {
                return true;
            }
        }
        


        return false;
    }

    public int TablePopulation (TableBrain table)
    {
        int sum = 0;
        for (int i = 0; i < table.myChairs.Length; i++)
        {
            if (table.myChairs[i].myPerson)
            {
                sum++;
            }
        }

        return sum;
    }

    public int CountNeighbors(TableBrain.Type tableType, int chairIndex, ChairBrain[] chairs)//just checks if people exist
    {
        int sum = 0;

        if (tableType == TableBrain.Type.square)
        {
            if (chairs[(chairIndex + chairs.Length / 2) % chairs.Length].myPerson != null)
            {
                sum++;
            }
            if (chairIndex != 0 && chairIndex != chairs.Length / 2)
            {
                if (chairs[chairIndex-1].myPerson)
                {
                    sum++;
                }
                
            }
            if (chairIndex != chairs.Length - 1 && chairIndex != chairs.Length / 2 - 1)
            {
                if (chairs[chairIndex + 1].myPerson)
                {
                    sum++;
                }
            }

            return sum;

        }
        if (chairs[(chairIndex - 1 + chairs.Length) % chairs.Length].myPerson != null)
        {
            sum++;
        }
        if (chairs[(chairIndex + 1) % chairs.Length].myPerson != null)
        {
            sum++;
        }
        if (tableType == TableBrain.Type.square)
        {
            if (chairs[(chairIndex + chairs.Length / 2) % chairs.Length].myPerson != null)
            {
                sum++;
            }
        }

        return sum;
    }


    public void ApplyMask(PartyGoerBrain person)
    {
        person.myMood = person.baseMood;
        person.myStyle = person.baseStyle;
        person.wants = new List<PartyGoerBrain.Want>(); // copy over base wants to current wants
        for (int i = 0; i < person.baseWants.Count; i++)
        {
            person.wants.Add(person.baseWants[i]);
        }
        
        if (person.mask)
        {
            switch (person.mask.type)
            {
                case (Mask.Type.mood):
                    person.myMood = person.mask.myMood;
                    break;
                case (Mask.Type.style):
                    person.myStyle = person.mask.myStyle;
                    break;
                case (Mask.Type.want):
                    person.wants.Add(person.mask.myWant);
                    break;
            }

        }
    }
}
