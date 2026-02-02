using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    public static IntroScript instance;
    public static int party = 0;
    private int goUp = 0;
    [SerializeField] public TextMeshProUGUI introText;

    private string vampireText = "Oh mascareri~ My dearest mascareri! How fare you on this fine winter's eve? I sincerely hope your health has been flourishing and your spirits remain high. Oh my dearest mascareri, I am so excited for the upcoming Carnevale! Every year, I ask the moon of you and you fetch me both the moon and her stars! However, I fear this year will be a little different. Oftentimes when I have a new request for you, I ask that you make me a mask for my own benefit. But this time, I would ask you for a mask catering to others' preferences. As you know, my partner and I have finally celebrated our individual coming of age ceremonies and shall be wed by the start of Spring so long as I can get the approval of Dracula, ruling head of the Vampires."; 
    private string werewolfText = "Oh mascareri~ My dearest mascareri! How fare you on this fine winter's eve? I sincerely hope your health has been flourishing and your spirits remain high. Oh my dearest mascareri, I am so excited for the upcoming Carnevale! Every year, I ask the moon of you and you fetch me both the moon and her stars! However, I fear this year will be a little different. Oftentimes when I have a new request for you, I ask that you make me a mask for my own benefit. But this time, I would ask you for a mask catering to others' preferences. As you know, my partner and I have finally celebrated our individual coming of age ceremonies and shall be wed by the start of Spring so long as I can get the approval of Fenrir, head Alpha of the Werewolves.";
    private string faeText = "Oh mascareri~ My dearest mascareri! How fare you on this fine winter's eve? I sincerely hope your health has been flourishing and your spirits remain high. Oh my dearest mascareri, I am so excited for the upcoming Carnevale! Every year, I ask the moon of you and you fetch me both the moon and her stars! However, I fear this year will be a little different. Oftentimes when I have a new request for you, I ask that you make me a mask for my own benefit. But this time, I would ask you for a mask catering to others' preferences. As you know, my partner and I have finally celebrated our individual coming of age ceremonies and shall be wed by the start of Spring so long as I can get the approval of Calypso, Sorceress and Head Diplomat of the Sirens.";
    private string sirenText = "Oh mascareri~ My dearest mascareri! How fare you on this fine winter's eve? I sincerely hope your health has been flourishing and your spirits remain high. Oh my dearest mascareri, I am so excited for the upcoming Carnevale! Every year, I ask the moon of you, and you fetch me both the moon and her stars! However, I fear this year will be a little different. Oftentimes, when I have a new request for you, I ask that you make me a mask for my own benefit. But this time, I would ask you for a mask catering to others' preferences. As you know, my partner and I have finally celebrated our individual coming of age ceremonies and shall be wed by the start of Spring so long as I can get the approval of Titania, Queen of the Fae.";
    private string[] commonText = {"Oh mascareri, I know this is a momentous task for you to complete, but I beg of you; it is imperative that I get their approval in order to be wed and who else to ask but you; you who knows all about the preferences of the supernatural that exist in our world.", "In a week's time I will be attending my first masquerade ball of Carnevale and I will need the finest mask to impress the special guests there and continue to get invited to other balls!", "So mascareri, I humbly ask: will you assist me?", "You will?! Oh my goodness thank you so much! I will owe you till the end of time itself!"};
    void Start()
    {
        party = UnityEngine.Random.Range(0, 3);
        instance = this;
        DontDestroyOnLoad(this);

        // 0 = vampire, 1 = werewolves, 2 = fae, 3 = sirens

switch (party){
        case 0:
            introText.text = vampireText;
            break;
        case 1:
            introText.text = werewolfText;
            break;
        case 2:
            introText.text = faeText;
            break;
        case 3:
            introText.text = sirenText;
            break;
            
        }

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            introText.text = commonText[goUp];
            goUp++;
        } else if (goUp == 3)
        {
            SceneManager.LoadScene("Ball Scene");
        }
    }
}
