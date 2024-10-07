using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1.0f;

    [SerializeField]
    private float moveSpeed = 10.0f;

    [SerializeField]
    private float zoomIncrement = 0.5f;

    [SerializeField]
    private Transform cameraHolderTransform;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private float minCameraDistance;
    [SerializeField]
    private float maxCameraDistance;

    [SerializeField]
    private Transform focusObjectTransform;

    // Update is called once per frame
    void Update()
    {
        Vector3 newRotation = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            float newYPosition = this.cameraHolderTransform.position.y - (this.moveSpeed * Time.deltaTime);

            if (newYPosition >= 0.0f)
            {
                this.cameraHolderTransform.position = new Vector3(this.cameraHolderTransform.position.x, newYPosition, this.cameraHolderTransform.position.z);
            }

            Vector3 newCameraPosition = this.cameraTransform.forward;

            if (Vector3.Distance(this.focusObjectTransform.position, this.cameraTransform.position) > this.minCameraDistance)
            {
                newCameraPosition = this.cameraTransform.position + (this.cameraTransform.forward * this.zoomIncrement * Time.deltaTime);
                this.cameraTransform.position = newCameraPosition;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            float newYRotation = this.transform.rotation.eulerAngles.y + (this.rotationSpeed * Time.deltaTime);
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, newYRotation, this.transform.rotation.eulerAngles.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Vector3.Distance(this.focusObjectTransform.position, this.cameraTransform.position) < this.maxCameraDistance)
            {
                Vector3 newCameraPosition = this.cameraTransform.forward;
                newCameraPosition = this.cameraTransform.position - (this.cameraTransform.forward * this.zoomIncrement * Time.deltaTime);
                this.cameraTransform.position = newCameraPosition;

                float newYPosition = this.cameraHolderTransform.position.y + (this.moveSpeed * Time.deltaTime);
                this.cameraHolderTransform.position = new Vector3(this.cameraHolderTransform.position.x, newYPosition, this.cameraHolderTransform.position.z);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            float newYRotation = this.transform.rotation.eulerAngles.y - (this.rotationSpeed * Time.deltaTime);
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, newYRotation, this.transform.rotation.eulerAngles.z);
        }
        /*
        if (Input.mouseScrollDelta.y != 0.0f)
        {
            Vector3 newCameraPosition = this.cameraTransform.forward;


            if (Input.mouseScrollDelta.y > 0.0f && Vector3.Distance(this.cameraTransform.position, this.focusObjectTransform.position) > this.minCameraDistance)
            {
                newCameraPosition = this.cameraTransform.position + (this.cameraTransform.forward * this.zoomIncrement * Time.deltaTime);
                this.cameraTransform.position = newCameraPosition;
            }
            if (Input.mouseScrollDelta.y < 0.0f && Vector3.Distance(this.cameraTransform.position, this.focusObjectTransform.position) < this.maxCameraDistance)
            {
                newCameraPosition = this.cameraTransform.position - (this.cameraTransform.forward * this.zoomIncrement);
                this.cameraTransform.position = newCameraPosition;
            }
        }
        */
    }
}
