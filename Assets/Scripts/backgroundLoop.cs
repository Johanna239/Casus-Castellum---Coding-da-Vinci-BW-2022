using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundLoop : MonoBehaviour{
    public GameObject[] levels;
    private Camera mainCamera;
    private Vector2 screenBounds;
    public float choke;
    public float scrollSpeed;

    void Start(){
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = GetWorldPositionOnPlane(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z), 20);
        foreach(GameObject obj in levels){
            loadChildObjects(obj);
        }
    }
    void loadChildObjects(GameObject obj){
        // calculate length of sprites
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke;

        // calculate how many of them are needed to fill the screen
        int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);

        // instantiate as many
        GameObject clone = Instantiate(obj) as GameObject;
        for(int i = 0; i <= childsNeeded; i++){
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    void repositionChildObjects(GameObject obj){

        // check how many instances have been created
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if(children.Length > 1){

            // get first and last element
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - choke;

            // if next object is needed on the right, move first (leftmost) object to the right
            if(transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth){
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
            
            // if next object is needed on the left, move last (rightmost) object to the left
            }else if(transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth){
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);
            }
        }
    }


    void Update() {

    }
    void LateUpdate(){
        foreach(GameObject obj in levels){
            repositionChildObjects(obj);
        }
    }

        public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
