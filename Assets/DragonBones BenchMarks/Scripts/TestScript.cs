using System;
using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Transform = UnityEngine.Transform;

public class TestScript : MonoBehaviour
{

    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject prefab2;
    [SerializeField] private Transform parent;

    private int counter;

    [SerializeField] private InputField inputField;

    [SerializeField] private Text countText;
    [SerializeField] private Text fpsText;

    [SerializeField] private float k;
    [SerializeField] private float p;

    public void Spawn()
    {
        int countToSpawn = int.Parse(inputField.text);

        for (int i = 0; i < countToSpawn; i++)
        {
            Vector2 posToSpawn = Random.insideUnitCircle.normalized * k * Mathf.Pow(counter, p);

            Instantiate(prefab, posToSpawn, Quaternion.identity, parent);
            counter++;
        }

        countText.text = counter.ToString();


    }

    public void Spawn2()
    {
        int countToSpawn = int.Parse(inputField.text);

        for (int i = 0; i < countToSpawn; i++)
        {
            Vector2 posToSpawn = Random.insideUnitCircle.normalized * k * Mathf.Pow(counter, p);

            Instantiate(prefab2, posToSpawn, Quaternion.identity, parent);
            counter++;
        }

        countText.text = counter.ToString();


    }

    public void DisableAll()
    {
        foreach (Transform obj in parent)
        {
            obj.gameObject.SetActive(false);
        }

        //parent.GetChild(0).gameObject.SetActive(true);

    }

    public void EnableAll()
    {
        foreach (Transform obj in parent)
        {
            obj.gameObject.SetActive(true);
        }
    }

    public void DestroyAll()
    {
        counter = 0;
        countText.text = counter.ToString();

        foreach (Transform obj in parent)
        {
            Destroy(obj.gameObject);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CleanUp()
    {
        Resources.UnloadUnusedAssets();
        GC.Collect();        
    }

    public void TryDisableDB1()
    {
        foreach (Transform obj in parent)
        {
            UnityArmatureComponent db = obj.GetComponentInChildren<UnityArmatureComponent>();
            db.animation.Reset();
        }
    }

    public void TryDisableDB2()
    {
        foreach (Transform obj in parent)
        {
            UnityArmatureComponent db = obj.GetComponentInChildren<UnityArmatureComponent>();
            db.animation.Stop();
        }
    }

    public void TryDisableDB3()
    {
        foreach (Transform obj in parent)
        {
            UnityArmatureComponent db = obj.GetComponentInChildren<UnityArmatureComponent>();
            db.animation.Play();
        }
    }

    void Start()
    {
        countText.text = counter.ToString();
    }




    //===========================

    private Camera camera;

    private bool dragging;

    private Vector3 dragOrigin;
    private Vector3 camDragOrigin;

    void Awake()
    {
        camera = Camera.main;
    }

    private int frameCount;
    private float elapsedTime;
    private double frameRate;

    void Update()
    {
        frameCount++;
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 0.1f)
        {
            frameRate = System.Math.Round(frameCount / elapsedTime, 1, System.MidpointRounding.AwayFromZero);
            frameCount = 0;
            elapsedTime = 0;

            fpsText.text = frameRate.ToString();
        }

        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) //Only if DOWN, set that we are dragging and remember it
        {
            dragging = true;

            dragOrigin = Input.mousePosition;
            camDragOrigin = camera.transform.position;
        }


        if (dragging)
        {
            if (Input.GetMouseButton(1) || Input.GetMouseButton(2)) //Only while buttons are held
            {
                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);

                float camAspect = (float)Screen.width / Screen.height;

                //Since camera has different size in x direction, multiplying by the aspect
                Vector3 move = 2 * camera.orthographicSize * new Vector3(camAspect * pos.x, pos.y);

                camera.transform.position = camDragOrigin - move;

            }
            else    //If they get unheld, stop dragging
            {
                dragging = false;
            }


        }

        camera.orthographicSize -= 3 * Input.GetAxis("Mouse ScrollWheel");






    }











}
