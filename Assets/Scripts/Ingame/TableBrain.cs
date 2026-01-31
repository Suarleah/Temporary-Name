using UnityEngine;
using System.Collections.Generic;

public class TableBrain : MonoBehaviour
{
    public enum Type {circle, square};
    public Type type;
    public ChairBrain[] myChairs; // for a rectangle table, chairs are listed in order from top to bottom, left to right, so the second row would be the same index + size/2
    // 0,1,2,3
    //public PartyGoerBrain[] peopleAtMyTable;
    void Start()
    {
        
    }
    void PersonSatHere(PartyGoerBrain partyGoerBrain)
    {

    }
    
}
