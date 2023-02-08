using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TMP_Text))]
public class OpenHyperlink : MonoBehaviour, IPointerClickHandler
{
    private TMP_Text m_textMeshPro;
        private string _URL = "https://www.limesmuseum.de/digitale-sammlung";
        private string _swordURL = "https://www.limesmuseum.de/digitale-sammlung/objekt/schwertknauf-eines-langschwertes-spatha-1959-0030-0001-0001#f3375461332023320";

    void Start()
    {
        m_textMeshPro = GetComponent<TMP_Text>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_textMeshPro, eventData.position, null);
        
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = m_textMeshPro.textInfo.linkInfo[linkIndex];
            
            // manual switching needed, since TextMeshPro LinkID can only contain 120 characters
            string URL; 
            switch(linkInfo.GetLinkID()) {
            case "sword":
                URL = this._swordURL;
                break;
            default:
                Debug.Log("URL not recognized");
                URL = this._URL;
                break;      
        }
            Application.OpenURL(URL);
        }
    }
}

