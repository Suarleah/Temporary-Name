using UnityEngine;
using System.Collections.Generic;
public class Mask : MonoBehaviour
{
    public enum Type {mood, style, want, lonely, red} 
    public Type type;

    public PartyGoerBrain.Mood myMood;

    public PartyGoerBrain.Want myWant; //only one to keep it simple/understandable.

    public PartyGoerBrain.Style myStyle;

    public Vector2 true_origin;

    private void Start()
    {
        true_origin = transform.position; //record position when scene starts as true origin
    }

}
