using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PartyGoerBrain : MonoBehaviour
{
    public Vector2 true_origin; // This is where the partygoer started on screen (in the waiting area)
    //public Vector2 origin; // used to lock partygoard into place, noticed it was not used

    public Wardrobe wardrobe; // contains all the outfits a character can wear

    public enum Mood {happy, neutral, sad, angry};
    public Mood myMood;
    public Mood baseMood;

    public enum Want {talk_with_someone, drink_with_someone, eat_with_someone, be_alone, dont_want_noise, sit_with_someone_with_mood_angry, sit_with_someone_with_mood_happy, sit_with_someone_with_mood_neutral, sit_with_someone_with_mood_sad, sit_with_someone_with_style_plain, dont_sit_with_someone_with_style_plain, sit_with_someone_with_style_fancy, sit_with_someone_with_style_professional, partnered, limited_number_of_people_at_table, sit_next_to_only_x_people, square_table, circle_table, soft_seat, wood_seat, end_of_a_table, drink_wine, center_of_table, important, assassination, everyone_happy, not_angry, phantom_of_the_opera, no_phantoms, kill_red_mask, dont_sit_with_someone_with_style_professional }; //styles are placeholders, styles are just for fun
    public List<Want> wants;
    public List<Want> baseWants;

    public int limited_number_of_people_at_table_limit; //if they only want to sit at a tavle with limited people this is the limit
    public int sit_next_to_only_x_people_x; //if they only want to sit next to a certain number of people, this is the number

    public enum Style {plain, fancy, professional };
    public Style myStyle;
    public Style baseStyle;

    public bool satisfied; //visual cue + used for logic

    public bool drinking; // visual cue

    public bool eating; // visual cue

    public bool talking; // visual cue

    public bool too_noisy; // visual cue

    public bool not_alone; // visual cue

    public Mask mask;
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

    public void updateVisual() //called by boardManager, updates  outfit when a mask is applied
    {
        
    }


}
