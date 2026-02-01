using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class MouseManager : MonoBehaviour
{
    [Header("Only Visible for Debugging")]
    [SerializeField] private Vector2 mousePos;
    [SerializeField] private PartyGoerBrain selectedPerson;
    [SerializeField] private Mask selectedMask;
    [SerializeField] private Vector2 infoPanelOffset;
    [SerializeField] private bool showInfo;

    public BoardManager boardmanager;

    [Header("Assign this stuff!!")]
    [SerializeField] private GameObject infoPanel; private TextMeshProUGUI infoText;

    void Start()
    {
        infoPanel.SetActive(false);
        infoText = infoPanel.GetComponentInChildren<TextMeshProUGUI>();
        //Debug.Log(LayerMask.GetMask("Chairs"));
    }

    void Update()
    {
        Vector2 fakePos = Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(fakePos.x, fakePos.y, 0));
        // The mouse position is equal to the main camera converting the raw mouse data from screen pixels to world coordinates 

        RayCasting();

        if (showInfo) // Sorry :(
        {
            infoPanel.SetActive(true);
        } else
        {
            infoPanel.SetActive(false);
        }

    }

    void InfoPanel(Mask mask)
    {
        infoPanel.transform.position = new Vector3(mousePos.x + infoPanelOffset.x, mousePos.y + infoPanelOffset.y, 0);

        if (mousePos.x > 0)
        {
            infoPanelOffset.x = -4;
        }
        else
        {
            infoPanelOffset.x = 1;
        }

        if (mousePos.y > 0)
        {
            infoPanelOffset.y = -3;
        }
        else
        {
            infoPanelOffset.y = 1;
        }

        string text = "";
        if (mask.type == Mask.Type.mood)
        {
            if (mask.myMood == PartyGoerBrain.Mood.happy)
            {
                text = "Happy Mask";
            }
            if (mask.myMood == PartyGoerBrain.Mood.neutral)
            {
                text = "Neutral Mask";
            }
            if (mask.myMood == PartyGoerBrain.Mood.sad)
            {
                text = "Sad Mask";
            }
            if (mask.myMood == PartyGoerBrain.Mood.angry)
            {
                text = "Angry Mask";
            }
        }
        if (mask.type == Mask.Type.style)
        {
            if (mask.myStyle == PartyGoerBrain.Style.plain)
            {
                text = "Plain Mask";
            }
            if (mask.myStyle == PartyGoerBrain.Style.professional)
            {
                text = "Professional Mask";
            }
            if (mask.myStyle == PartyGoerBrain.Style.fancy)
            {
                text = "Fancy Mask";
            }
        }
        if (mask.type == Mask.Type.lonely)
        {
            text = "Lonely Mask";
        }

        if (mask.type == Mask.Type.red)
        {
            text = "Red Mask";
        }
        if (mask.type == Mask.Type.oni)
        {
            text = "Oni Mask";
        }
        infoPanel.GetComponentInChildren<TMP_Text>().text = text;
    }

    void InfoPanel(PartyGoerBrain partyGoer) 
    {
        infoPanel.transform.position = new Vector3(mousePos.x + infoPanelOffset.x, mousePos.y + infoPanelOffset.y, 0);

        if (mousePos.x > 0)
        {
            infoPanelOffset.x = -4;
        } else
        {
            infoPanelOffset.x = 1;
        }

        if (mousePos.y > 0)
        {
            infoPanelOffset.y = -3;
        } else
        {
            infoPanelOffset.y = 1;
        }

        string text = "";
        for (int i = 0; i < partyGoer.wants.Count; i++) //display wants
        {
            if (text != "")
            {
                text += "\n";
            }

            switch (partyGoer.wants[i])
            {
                case PartyGoerBrain.Want.sit_with_someone_with_mood_happy:
                    text += "I want to sit next to someone happy!";
                    break;
                case PartyGoerBrain.Want.sit_with_someone_with_mood_neutral:
                    text += "I want to sit next to someone neutral!";
                    break;
                case PartyGoerBrain.Want.sit_with_someone_with_mood_sad:
                    text += "I want to sit next to someone sad!";
                    break;
                case PartyGoerBrain.Want.sit_with_someone_with_style_plain:
                    text += "I want to sit next to someone who dresses plainly!";
                    break;
                case PartyGoerBrain.Want.dont_sit_with_someone_with_style_plain:
                    text += "I DONT want to sit next to someone who dresses plainly!";
                    break;
                case PartyGoerBrain.Want.sit_with_someone_with_style_fancy:
                    text += "I want to sit next to someone who dresses fancily!";
                    break;
                case PartyGoerBrain.Want.sit_with_someone_with_style_professional:
                    text += "I want to sit next to someone who dresses professionally!";
                    break;
                case PartyGoerBrain.Want.talk_with_someone:
                    text += "I want to talk to someone!";
                    break;
                case PartyGoerBrain.Want.eat_with_someone:
                    text += "I want to share food with someone!";
                    break;
                case PartyGoerBrain.Want.drink_with_someone:
                    text += "I want to drink with someone!!";
                    break;
                case PartyGoerBrain.Want.be_alone:
                    text += "I want to be alone!";
                    break;
                case PartyGoerBrain.Want.partnered:
                    text += "I want to sit with my partner!";
                    break;
                case PartyGoerBrain.Want.limited_number_of_people_at_table:
                    text += "I want to sit at a table with exactly " + partyGoer.limited_number_of_people_at_table_limit + " people!";
                    break;
                case PartyGoerBrain.Want.sit_next_to_only_x_people:
                    text += "I want to interact with exactly " +partyGoer.sit_next_to_only_x_people_x + " people!";
                    break;
                case PartyGoerBrain.Want.circle_table:
                    text += "I want to sit at a circle table!";
                    break;
                case PartyGoerBrain.Want.square_table:
                    text += "I want to sit at a square table!";
                    break;
                case PartyGoerBrain.Want.end_of_a_table:
                    text += "I want to sit at the end of a table!";
                    break;
                case PartyGoerBrain.Want.soft_seat:
                    text += "I want to sit on a soft seat!";
                    break;
                case PartyGoerBrain.Want.wood_seat:
                    text += "I want to sit on a wooden seat!";
                    break;
                case PartyGoerBrain.Want.drink_wine: //jesus exclusive
                    text += "I will drink fine wine.";
                    break;
                case PartyGoerBrain.Want.center_of_table: //jesus exclusive
                    text += "I will sit in the center of the table.";
                    break;
                case PartyGoerBrain.Want.important: //caesar exclusive
                    text += "I will sit in the most important seat!";
                    break;
                case PartyGoerBrain.Want.everyone_happy:
                    text += "I want everyone around me to be happy!";
                    break;
                case PartyGoerBrain.Want.assassination:
                    text += "I want to assassinate Caesar!";
                    break;
                case PartyGoerBrain.Want.not_angry:
                    text += "I don't want to sit next to angry people!";
                    break;
                case PartyGoerBrain.Want.phantom_of_the_opera:
                    text += "I want to be the only one with Christine!";
                    break;
                case PartyGoerBrain.Want.no_phantoms:
                    text += "I don't want to sit next to the phantom!";
                    break;
                case PartyGoerBrain.Want.kill_red_mask:
                    text += "I want to kill the one wearing the red mask!";
                    break;
                case PartyGoerBrain.Want.dont_sit_with_someone_with_style_professional:
                    text += "I DONT want to sit next to someone who dresses professionally!!";
                    break;
                case PartyGoerBrain.Want.no_sad:
                    text += "I don't want anyone around me to be sad!";
                    break;
                case PartyGoerBrain.Want.safe:
                    text += "I want to keep everyone safe!";
                    break;
            }
        }
        if (text == "")
        {
            text = "I have no preference";
        }
        infoPanel.GetComponentInChildren<TMP_Text>().text = text;

    }

    void RayCasting()
    {
        Collider2D thingHovering = Physics2D.OverlapPoint(mousePos);

        if (thingHovering != null && thingHovering.GetComponent<PartyGoerBrain>() != null) //if a person, show info about them :)
        {
            showInfo = true;
            InfoPanel(thingHovering.GetComponent<PartyGoerBrain>());
        }
        else if(thingHovering != null && thingHovering.GetComponent<Mask>() != null) //if a mask, show info about it :)
        {
            showInfo = true;
            InfoPanel(thingHovering.GetComponent<Mask>());
        } 
        else
        {
            showInfo = false;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (thingHovering != null && thingHovering.GetComponent<Mask>() != null)
            {
                selectedMask = thingHovering.GetComponent<Mask>();

                BoxCollider2D blockRaycasts = selectedMask.GetComponent<BoxCollider2D>();
            }
            else if (thingHovering != null && thingHovering.GetComponent<PartyGoerBrain>() != null)
            {
                selectedPerson = thingHovering.GetComponent<PartyGoerBrain>();

                BoxCollider2D blockRaycasts = selectedPerson.GetComponent<BoxCollider2D>();
            }


        }

        if (Mouse.current.leftButton.isPressed)
        {
            if (selectedPerson)
            {
               selectedPerson.transform.position = mousePos;
            }

            if (selectedMask)
            {
                selectedMask.transform.position = mousePos;
            }


        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {

            if (!selectedPerson && !selectedMask) // no selected object = do nothing
            {
                return;
            }
            GameObject chair = null;
            GameObject maskTarget = null;
            //bool waiting = false;
            //preferably change it so the overlap is around the person instead (more intuitive but can be done later)
            List<Collider2D> objects = new List<Collider2D>(); //should only be 1 at any given time
            if (selectedMask)
            {
                Physics2D.OverlapCollider(selectedMask.GetComponent<BoxCollider2D>(), objects);
            } else //selected person
            {
                Physics2D.OverlapCollider(selectedPerson.GetComponent<BoxCollider2D>(), objects);
            }
            
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].transform.GetComponent<ChairBrain>() != null) // if it has a chair brain it is a chair
                {
                   chair = objects[i].transform.gameObject;
                }
                if (objects[i].transform.GetComponent<PartyGoerBrain>() != null) // if it has a partygoer brain, you can put a mask on it.
                {
                    maskTarget = objects[i].transform.gameObject;
                }

            }
             
           

                //Physics2D.OverlapPoint(mousePos, 8).gameObject; // 8 as in the layer 3 which is chairs only
            // This line makes an error but not a bad one ^^^^^
            if (maskTarget != null && selectedMask)
            {
                PartyGoerBrain person = maskTarget.GetComponent<PartyGoerBrain>();
                if (person.mask) //swap places if they're already wearing a mask
                {
                    if (selectedMask.transform.parent) //mask is already on soemone
                    {
                        selectedMask.transform.parent.GetComponent<PartyGoerBrain>().mask = person.mask; //current person gets their new mask
                        person.mask.transform.SetParent(selectedMask.transform.parent);
                        person.mask.transform.localPosition = new Vector2(0, 0);

                        selectedMask.transform.SetParent(person.transform, false);
                        selectedMask.transform.localPosition = new Vector2(0, 0);
                        person.mask = selectedMask;
                    }
                    else //selected mask is unused
                    {
                        Mask theirmask = person.mask;
                        theirmask.transform.SetParent(null);
                        theirmask.transform.position = theirmask.true_origin;
                        

                        selectedMask.transform.SetParent(person.transform, false);
                        selectedMask.transform.localPosition = new Vector2(0, 0); // Sit down....
                        person.mask = selectedMask;


                    }
                } else
                {
                    
                    if (selectedMask.transform.parent) // if mask is already on someone
                    {
                        selectedMask.transform.parent.GetComponent<PartyGoerBrain>().mask = null;// take mask off
                    }
                    //put on mask without issue
                    selectedMask.transform.SetParent(person.transform);
                    selectedMask.transform.localPosition = new Vector2(0, 0);
                    person.mask = selectedMask;
                }

                } else if (selectedMask)
                {
                
                if (selectedMask.transform.parent) //remove self from old wearer
                {
                    selectedMask.transform.parent.GetComponent<PartyGoerBrain>().mask = null;
                    selectedMask.transform.SetParent(null);
                    selectedMask.transform.position = selectedMask.true_origin;
                }
                else
                {
                    selectedMask.transform.position = selectedMask.true_origin;
                }

                selectedMask = null;
            }


            if (chair != null && selectedPerson)// if chair is already taken
            {
                ChairBrain chairbrain = chair.GetComponent<ChairBrain>();
                if (chairbrain.myPerson != null) //swap places
                {
                    if (selectedPerson.currentChair != null) // if the selected person was previously in a chair
                    {
                        // move the person who you're dropping onto, into the chair you moved the selected person out of

                        selectedPerson.currentChair.myPerson = chairbrain.myPerson;
                        chairbrain.myPerson.currentChair = selectedPerson.currentChair;
                        
                        chairbrain.myPerson.transform.SetParent(selectedPerson.currentChair.transform, false);
                        chairbrain.myPerson.transform.localPosition = new Vector2(0, 1);




                        selectedPerson.transform.SetParent(chair.transform, false);
                        selectedPerson.transform.localPosition = new Vector2(0, 1); // Sit down....
                        selectedPerson.currentChair = chairbrain;
                        chairbrain.myPerson = selectedPerson;
                    } else // the selectedperson was still waiting for a seat
                    {
                        chairbrain.myPerson.satisfied = false;
                        chairbrain.myPerson.currentChair = null;
                        chairbrain.myPerson.transform.SetParent(null);
                        chairbrain.myPerson.transform.position = chairbrain.myPerson.true_origin;

                        selectedPerson.transform.SetParent(chair.transform, false);
                        selectedPerson.transform.localPosition = new Vector2(0, 1); // Sit down....
                        selectedPerson.currentChair = chairbrain;
                        chairbrain.myPerson = selectedPerson;
                    }
                } else // chair is empty
                {
                    if (selectedPerson.currentChair) //remove self from old chair
                    {
                        selectedPerson.currentChair.myPerson = null;
                    }

                    selectedPerson.transform.SetParent(chair.transform, false);
                    selectedPerson.transform.localPosition = new Vector2(0, 1); // Sit down....
                    selectedPerson.currentChair = chairbrain;
                    chairbrain.myPerson = selectedPerson;

                    
                }
                


                selectedPerson = null;
            } /*else if (waiting){ //if colliding with waiting area, return to true origin
                if (selectedPerson.currentChair) //remove self from old chair
                {
                    selectedPerson.currentChair.myPerson = null;
                }
                selectedPerson.currentChair = null;
                selectedPerson.transform.SetParent(null);
                selectedPerson.transform.position = selectedPerson.true_origin;
            }*/  else if (selectedPerson) //dragged somewhere with no chair
            {
                selectedPerson.transform.SetParent(null);
                if (selectedPerson.currentChair) //remove self from old chair
                {
                    selectedPerson.currentChair.myPerson = null;
                    selectedPerson.currentChair = null;
                    selectedPerson.transform.position = selectedPerson.true_origin;
                } else
                {
                    selectedPerson.transform.position = selectedPerson.true_origin;
                }
                selectedPerson.satisfied = false; //cannot be satisfied if not seated.

                
            }


            boardmanager.updateBoard();
            selectedMask = null;
            selectedPerson = null;
        }
    }


}
