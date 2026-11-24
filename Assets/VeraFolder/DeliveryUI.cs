using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DeliveryUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;
    public string deliveryMessage;
    public string questMessage;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialoguePanel.gameObject.SetActive(false);

        TakeQuest();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Settlement"))
        {
            dialoguePanel.gameObject.SetActive(true);
            dialogueText.text = deliveryMessage;
        }
    }

    public void TakeQuest()
    {
        dialoguePanel.gameObject.SetActive(true);
        dialogueText.text = questMessage;
    }

}
