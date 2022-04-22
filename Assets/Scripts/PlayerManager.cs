using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {get; private set;}

    public GameObject player;
    public GameObject cam;
    private PlayerInput pMove;
    private Interactor pInteract;
    private MouseLook pCamX;
    private MouseLook pCamY;

    public void Startup()
    {
        Debug.Log("Player manager starting...");
        pMove = player.GetComponent<PlayerInput>();
        pInteract = player.GetComponent<Interactor>();
        pCamX = player.GetComponent<MouseLook>();
        pCamY = cam.GetComponent<MouseLook>();
        status = ManagerStatus.Started;
    }

    public void Hold()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        pMove.canMove = false;
        pInteract.canInteract = false;
        pCamX.canMove = false;
        pCamY.canMove = false;

    }

    public void Release()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pMove.canMove = true;
        pInteract.canInteract = true;
        pCamX.canMove = true;
        pCamY.canMove = true;
    }
}
