using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PartyGoerBrain : MonoBehaviour
{
    public Vector2 true_origin; // This is where the partygoer started on screen (in the waiting area)
    //public Vector2 origin; // used to lock partygoard into place, noticed it was not used

    public enum Mood {happy, neutral, sad};
    public Mood myMood;

    public enum Want {talk_with_someone, drink_with_someone, eat_with_someone, be_alone, dont_want_noise, sit_with_someone_with_mood_happy, sit_with_someone_with_mood_neutral, sit_with_someone_with_mood_sad, sit_with_someone_with_style_plain }; //styles are placeholders, styles are just for fun
    public List<Want> wants;

    public enum Style {plain, fancy, professional };
    public Style myStyle;

    public bool satisfied; //visual cue + used for logic

    public bool drinking; // visual cue

    public bool eating; // visual cue

    public bool talking; // visual cue

    public bool too_noisy; // visual cue

    public bool not_alone; // visual cue


    [SerializeField] GameObject heart;

    //public PartyGoerBrain[] myNeighbors;
    public ChairBrain currentChair;

    private void Start()
    {
        true_origin = transform.position; //record position when scene starts as true origin
    }

    private void Update()
    {
        if (satisfied)
        {
            heart.SetActive(true);
        } else
        {
           heart.SetActive(false);
        }
    }


}
