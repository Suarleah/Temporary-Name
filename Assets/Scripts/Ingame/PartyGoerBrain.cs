using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PartyGoerBrain : MonoBehaviour
{
    public Vector2 true_origin; // This is where the partygoer started on screen (in the waiting area)
    //public Vector2 origin; // used to lock partygoard into place, noticed it was not used

    public GameObject facevisualobj;
    public GameObject maskvisualobj;
    public GameObject stylevisualobj;

    // visuals for some of 
    public GameObject drinkingvisual; //eating and drinking are mutually exclusive
    public GameObject eatingvisual;
    public GameObject talkingvisual;



    public Wardrobe wardrobe; // contains all the outfits a character can wear

    public enum Mood {happy, neutral, sad, angry};
    public Mood myMood;
    public Mood baseMood;

    public enum Want {talk_with_someone, drink_with_someone, eat_with_someone, be_alone, dont_want_noise, sit_with_someone_with_mood_angry, sit_with_someone_with_mood_happy, sit_with_someone_with_mood_neutral, sit_with_someone_with_mood_sad, sit_with_someone_with_style_plain, dont_sit_with_someone_with_style_plain, sit_with_someone_with_style_fancy, sit_with_someone_with_style_professional, partnered, limited_number_of_people_at_table, sit_next_to_only_x_people, square_table, circle_table, soft_seat, wood_seat, end_of_a_table, drink_wine, center_of_table, important, assassination, everyone_happy, not_angry, phantom_of_the_opera, no_phantoms, kill_red_mask, dont_sit_with_someone_with_style_professional, no_sad, safe}; //styles are placeholders, styles are just for fun
    public List<Want> wants;
    public List<Want> baseWants;

    public int limited_number_of_people_at_table_limit; //if they only want to sit at a tavle with limited people this is the limit
    public int sit_next_to_only_x_people_x; //if they only want to sit next to a certain number of people, this is the number

    public enum Style {plain, fancy, professional };
    public Style myStyle;
    public Style baseStyle;

    public bool satisfied; //visual cue + used for logic

    //these visual cues are only used if the person has the respective want.
    public bool drinking; // visual cue

    public bool eating; // visual cue

    public bool talking; // visual cue

    //public bool too_noisy; // visual cue


    public Mask mask;
    [SerializeField] GameObject heart;

    //public PartyGoerBrain[] myNeighbors;
    public ChairBrain currentChair;

    [Header("Icon Animators")]

    [SerializeField] private Animator wineAnim;
    [SerializeField] private Animator utensilAnim;
    [SerializeField] private Animator talkAnim;

    private void Start()
    {
        myMood = baseMood;
        myStyle = baseStyle;
        true_origin = transform.position; //record position when scene starts as true origin
        updateVisual();
        
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


        if (drinkingvisual) //if this person has the drinking visuala, AKA wants to drink with someone
        {
            if (drinking)
            {
                wineAnim.SetBool("cheers", true);
            }
            else
            {
               wineAnim.SetBool("cheers", false);
            }
        }

        if (eating) //if this person has the eating visuala, AKA wants to drink with someone
        {
            if (eating)
            {
                //if their want is fulfilled, make the icon brighter, and move a little bit as though they're cheersing
            }
            else
            {
                //else, the icon is darkened and still
            }
        }

        if (talkingvisual) //if this person has the talking visuala, AKA wants to drink with someone
        {
            if (talking)
            {
                //if their want is fulfilled, make the icon brighter, and make the dots bounce up and down as though they're in dialogue
            }
            else
            {
                //else, the icon is darkened and still
            }
        }
    }

    public void updateVisual() //called by boardManager, updates  outfit when a mask is applied
    {
        SpriteRenderer facevisual = facevisualobj.GetComponent<SpriteRenderer>();
        SpriteRenderer maskvisual = maskvisualobj.GetComponent<SpriteRenderer>();
        SpriteRenderer stylevisual = stylevisualobj.GetComponent<SpriteRenderer>();
        if (wardrobe)
        {
            switch (myMood)
            {
                case Mood.happy:
                    if (wardrobe.face_happy)
                    {
                        facevisual.sprite = wardrobe.face_happy;
                    }
                    break;
                case Mood.neutral:
                    if (wardrobe.face_neutral)
                    {
                        facevisual.sprite = wardrobe.face_neutral;
                    }
                    break;
                case Mood.sad:
                    if (wardrobe.face_sad)
                    {
                        facevisual.sprite = wardrobe.face_sad;
                    }
                    break;
                case Mood.angry:
                    if (wardrobe.face_angry)
                    {
                        facevisual.sprite = wardrobe.face_angry;
                    }
                    break;
            }

            switch (myStyle)
            {
                case Style.plain:
                    if (wardrobe.style_plain)
                    {
                        stylevisual.sprite = wardrobe.style_plain;
                    }
                    break;
                case Style.professional:
                    if (wardrobe.style_professional)
                    {
                        stylevisual.sprite = wardrobe.style_professional;
                    }
                    break;
                case Style.fancy:
                    if (wardrobe.style_fancy)
                    {
                        stylevisual.sprite = wardrobe.style_fancy;
                    }
                    break;
            }

            if (!mask)
            {
                if (wardrobe.mask_default)
                {
                    maskvisual.sprite = wardrobe.mask_default;
                } else
                {
                    maskvisual.sprite = null;
                }
            } else
            {
                if (mask.type == Mask.Type.mood)
                {
                    if (mask.myMood == Mood.happy && wardrobe.mask_happy)
                    {
                        maskvisual.sprite = wardrobe.mask_happy;
                    }
                    if (mask.myMood == Mood.neutral && wardrobe.mask_neutral)
                    {
                        maskvisual.sprite = wardrobe.mask_neutral;
                    }
                    if (mask.myMood == Mood.sad && wardrobe.mask_sad)
                    {
                        maskvisual.sprite = wardrobe.mask_sad;
                    }
                    if (mask.myMood == Mood.angry && wardrobe.mask_angry)
                    {
                        maskvisual.sprite = wardrobe.mask_angry;
                    }
                }
                if (mask.type == Mask.Type.style)
                {
                    if (mask.myStyle == Style.plain && wardrobe.mask_plain)
                    {
                        maskvisual.sprite = wardrobe.mask_plain;
                    }
                    if (mask.myStyle == Style.professional && wardrobe.mask_professional)
                    {
                        maskvisual.sprite = wardrobe.mask_professional;
                    }
                    if (mask.myStyle == Style.fancy && wardrobe.mask_fancy)
                    {
                        maskvisual.sprite = wardrobe.mask_fancy;
                    }
                }
                if (mask.type == Mask.Type.lonely)
                {
                    if (wardrobe.mask_lonely)
                    {
                        maskvisual.sprite = wardrobe.mask_lonely;
                    }
                }

                if (mask.type == Mask.Type.red)
                {
                    if (wardrobe.mask_red_mask)
                    {
                        maskvisual.sprite = wardrobe.mask_red_mask;
                    }
                }
                if (mask.type == Mask.Type.oni)
                {
                    if (wardrobe.mask_oni)
                    {
                        maskvisual.sprite = wardrobe.mask_oni;
                    }
                }
            }
            
        }
    }


}
