using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class delete_holz : MonoBehaviour
{
    [SerializeField]
    private UnityEvent panzerAktivieren;
    public bool clickable = false;

    public void setclickable()
    {
        clickable = true;
    }

    void OnMouseUp()
    {
        if (clickable)
        {
            Destroy(gameObject);
            if (panzerAktivieren != null)
            {
                panzerAktivieren.Invoke();
            }
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
