using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class ChairBrain : MonoBehaviour
{
    public TableBrain myTable;
    public PartyGoerBrain myPerson;


    public enum Type {soft, wood, beanbag}
    public Type type;

    public enum Attribute {food, wine, end_of_table, center_of_table} //stuff that might be at/near the chair
    public List<Attribute> attributes;
}
