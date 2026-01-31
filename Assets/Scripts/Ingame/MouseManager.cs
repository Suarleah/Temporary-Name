using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    [Header("Only Visible for Debugging")]
    [SerializeField] private Vector2 mousePos;
    [SerializeField] private PartyGoerBrain selectedPerson;
    [SerializeField] private Vector2 infoPanelOffset;
    [SerializeField] private bool showInfo;

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

    void InfoPanel(PartyGoerBrain partyGoer)
    {
        infoPanel.transform.position = new Vector3(mousePos.x + infoPanelOffset.x, mousePos.y + infoPanelOffset.y, 0);
        // info panel text = partyGoer. blah blah

    }

    void RayCasting()
    {
        Collider2D thingHovering = Physics2D.OverlapPoint(mousePos);

        if (thingHovering != null && thingHovering.GetComponent<PartyGoerBrain>() != null) //if a person, show info about them :)
        {
            showInfo = true;
            InfoPanel(thingHovering.GetComponent<PartyGoerBrain>());
        } else
        {
            showInfo = false;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (thingHovering.GetComponent<PartyGoerBrain>() != null)
            {
                selectedPerson = thingHovering.GetComponent<PartyGoerBrain>();

                BoxCollider2D blockRaycasts = selectedPerson.GetComponent<BoxCollider2D>();
            }
        }

        if (Mouse.current.leftButton.isPressed)
        {
            selectedPerson.transform.position = mousePos;
            
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            GameObject chair = Physics2D.OverlapPoint(mousePos, 8).gameObject; // 8 as in the layer 3 which is chairs only
            // This line makes an error but not a bad one ^^^^^
            
            if (chair != null)
            {
                selectedPerson.transform.SetParent(chair.transform, false);
                selectedPerson.transform.localPosition = new Vector2(0,1); // Sit down....


                selectedPerson = null;
            }
            selectedPerson = null;
        }
    }


}
