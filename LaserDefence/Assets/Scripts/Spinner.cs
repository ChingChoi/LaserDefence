using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

    [SerializeField] float speedOfRotation = 720f;

    private void Update()
    {
        transform.Rotate(0, 0, speedOfRotation * Time.deltaTime);
    }
}
