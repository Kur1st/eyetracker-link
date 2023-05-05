using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorGame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float mass;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cursorPos = TobiiHelper.getWorldPoint();
        transform.position += (cursorPos - transform.position).sqrMagnitude * (cursorPos - transform.position).normalized / mass;
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
    }
}
