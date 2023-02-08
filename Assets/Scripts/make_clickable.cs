using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class make_clickable : MonoBehaviour
{
    [SerializeField] private GameObject Maskenhelm;
    // GameObject Maskenhelm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        if(TryGetComponent(out BoxCollider2D boxcol)) {
            boxcol.enabled = false;
        }
        Maskenhelm.SetActive(true);
    }
}
