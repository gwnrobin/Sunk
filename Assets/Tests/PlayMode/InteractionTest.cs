using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class InteractionTest
{
    [UnityTest]
    public IEnumerator InteractText()
    {
        SceneManager.LoadScene(0);

        yield return new WaitForFixedUpdate();

        GameObject player = GameObject.Find("Player");

        PlayerInteract interact = player.GetComponent<PlayerInteract>();

        Assert.AreNotEqual("", interact._currentInteractable.InteractText);
    }

    [UnityTest]
    public IEnumerator Interaction()
    {
        SceneManager.LoadScene(0);

        yield return new WaitForFixedUpdate();

        GameObject player = GameObject.Find("Player");

        PlayerInteract interact = player.GetComponent<PlayerInteract>();

        interact.Interact();

        Assert.AreNotEqual(0, interact.interactCD);
    }

    [UnityTest]
    public IEnumerator FlickerTest()
    {
        SceneManager.LoadScene(0);

        yield return new WaitForFixedUpdate();

        GameObject light = GameObject.Find("SpecialLight");

        Flickering flicker = light.GetComponent<Flickering>();

        flicker.Flicker(3);

        Assert.AreNotEqual(3, flicker.times);
    }

    [UnityTest]
    public IEnumerator Screenshak()
    {
        SceneManager.LoadScene(0);

        yield return new WaitForFixedUpdate();

        GameObject camera = GameObject.Find("Camera");

        Vector3 originPosition = camera.transform.position;

        CameraShake screenshake = camera.GetComponent<CameraShake>();

        screenshake.Shake();

        yield return new WaitForSeconds(2);

        Assert.IsTrue((originPosition == camera.transform.position));
    }

    [UnityTest]
    public IEnumerator MaxMovementSpeed()
    {
        SceneManager.LoadScene(0);

        yield return new WaitForFixedUpdate();

        GameObject player = GameObject.Find("Player");

        PlayerMovement movement = player.GetComponent<PlayerMovement>();

        movement.DoMove(new Vector2(0, 1));
        movement.DoMove(new Vector2(0, 1));
        movement.DoMove(new Vector2(0, 1));
        movement.DoMove(new Vector2(0, 1));
        movement.DoMove(new Vector2(0, 1));

        Assert.IsTrue(movement._movementSpeed <= 5);
    }

    [UnityTest]
    public IEnumerator UltimaTest()
    {
        SceneManager.LoadScene(0);

        yield return new WaitForFixedUpdate();

        GameObject player = GameObject.Find("Player");
        GameObject light = GameObject.Find("SpecialLight");
        GameObject camera = GameObject.Find("Camera");

        PlayerInteract interact = player.GetComponent<PlayerInteract>();
        Flickering flicker = light.GetComponent<Flickering>();
        CameraShake screenshake = camera.GetComponent<CameraShake>();
        PlayerMovement movement = player.GetComponent<PlayerMovement>();

        flicker.Flicker(3);

        Assert.AreNotEqual(3, flicker.times);

        yield return new WaitForFixedUpdate();

        Assert.AreNotEqual("", interact._currentInteractable.InteractText);

        interact.Interact();

        Assert.AreNotEqual(0, interact.interactCD);

        Vector3 originPositionCam = camera.transform.position;

        screenshake.Shake();

        yield return new WaitForSeconds(2);

        Assert.IsTrue((originPositionCam == camera.transform.position));

        movement.DoMove(new Vector2(0, 1));
        movement.DoMove(new Vector2(0, 1));
        movement.DoMove(new Vector2(0, 1));
        movement.DoMove(new Vector2(0, 1));
        movement.DoMove(new Vector2(0, 1));

        Assert.IsTrue(movement._movementSpeed <= 5);
    }

}
