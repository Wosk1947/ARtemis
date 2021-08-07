

using System.Threading;

namespace HandARSample
{
    using UnityEngine;
    using System.Collections.Generic;
    using HuaweiARUnitySDK;
    using System.Collections;
    using System;
    using Common;
    public class HandARController : MonoBehaviour
    {

	    private List<GameObject> enemyParts = new List<GameObject>();
	    private List<Vector3> randomVectors = new List<Vector3>();
	    private float partMoveSpeed = 5;
	    private float partRotationSpeed = 90;
	    private bool explosion = false;

	    private float timer = 0.0f;
        private GameObject marker;
        private List<GameObject> markers = new List<GameObject>();

        private int nextcoin;
        private int endgame = 1;

        private ARAnchor anchor;
        private List<ARHitResult> newPoints = new List<ARHitResult>();
        private Pose enemyPose;

        private bool inputOn = true;

        private Pose startpose;
        private Pose newstartpose;
        private List<Pose> markerpose = new List<Pose>();

        private GameObject toy;

        private bool hud = true;

        public Texture crosshair;
        public Texture defaultCrosshair;

        private int x = 0;
        private int y = 0;

        private int bs = 0;

        private bool markerOn=false;
        private bool markerSet = false;

        private int positionNum = 0;

        private List<Vector2> points = new List<Vector2>();
        private Pose target;
        private bool targetSet=false;
        private bool dummySet=false;

        private float distance = 0;

        private double theta, psy, gamma;
        private double defTilt=0;
        private double tilt;

        public Texture gyro;
        public Texture gyroMark;
        private float markX=-100;
        private float markY=-100;

        private GameObject enemy;

        private bool counting = false;
        private List<int> data = new List<int>();

        private float currentAcceleretaion;
        private float maxAcceleration = 0;

        private String message="";
        private String message2="";
        private String message3="";
        private String message4="";
        private int counter;
        private bool foundBigAcceleration = false;
        private bool probableShot = false;

        private Camera camera;
        Vector3 torso;
        private Vector2 torso2dOrigin;
        private float radius;
        private Vector3 circleBorder;

        private bool dummyBuilt = false;
        private List<GameObject> enemyPartsBase = new List<GameObject>();





        public void Start()
        {
	        marker= GameObject.Find("Marker2");
            marker.SetActive(false);

            for (int i = 1; i <= 16; i++)
            {
	            GameObject go = GameObject.Find("Part"+i);
	            go.SetActive(true);
	            enemyPartsBase.Add(Instantiate(go,go.transform.localPosition,go.transform.localRotation));
	            enemyPartsBase[i-1].SetActive(false);
	            go.SetActive(false);
	            randomVectors.Add(new Vector3(UnityEngine.Random.value,UnityEngine.Random.value,UnityEngine.Random.value));
            }




            for (int i = 0; i < 10; i++)
            {
	            data.Add(0);
            }
            counter = 0;
            counting = false;
            message = "";
            foundBigAcceleration = false;
            probableShot = false;

            camera=Camera.main;

			torso = Vector3.zero;
			torso2dOrigin = Vector2.zero;
			radius = 0;
			circleBorder = Vector3.zero;

        }


        public void Update()
        {

	        theta = ARFrame.GetPose().rotation.eulerAngles.z;

	        tilt = Math.Abs(theta - defTilt);

	        if (targetSet)
	        {
		        distance = Vector3.Distance(ARFrame.GetPose().position, target.position);
	        }

	        if (dummySet)
	        {
		        if (!dummyBuilt)
		        {
			        int a = enemyParts.Count;
			        for (int i = 0; i < a; i++)
			        {
				        Destroy(enemyParts[0]);
				        enemyParts.RemoveAt(0);
			        }
			        Vector3 pos = ARFrame.GetPose().position;
			        for (int i = 0; i <= 15; i++)
			        {
				        enemyPartsBase[i].SetActive(true);
				        enemyParts.Add(Instantiate(enemyPartsBase[i],enemyPartsBase[i].transform.position+pos
					        +new Vector3(0,-1.5f,0),enemyPartsBase[i].transform.rotation));
				        enemyPartsBase[i].SetActive(false);
				        enemyParts[i].SetActive(true);
			        }

			        dummyBuilt = true;
		        }

		        torso = camera.WorldToScreenPoint(enemyParts[0].transform.position);
		        torso2dOrigin = new Vector2(torso.x, Screen.height - torso.y);
		        circleBorder = camera.WorldToScreenPoint(new Vector3(enemyParts[0].transform.position.x,enemyParts[0].transform.position.y+0.6f,enemyParts[0].transform.position.z));
		        radius = Vector2.Distance(torso2dOrigin,
			        new Vector2(circleBorder.x, Screen.height-circleBorder.y));
	        }

	        if (markerOn&&!markerSet)
	        {
		        marker.SetActive(true);
		        markers.Add(Instantiate(marker,
			        new Vector3(ARFrame.GetPose().position.x, ARFrame.GetPose().position.y-1.0f,
				        ARFrame.GetPose().position.z), Quaternion.Euler(0, 0, 0)));
		        marker.SetActive(false);
		        markerSet = true;

	        }

	        if (explosion)
	        {
		        dummySet = false;
		        for (int i = 0; i <= 15; i++)
		        {
			        enemyParts[i].SetActive(true);
			        enemyParts[i].transform.Translate(partMoveSpeed*Time.deltaTime*randomVectors[i]);
			        enemyParts[i].transform.Rotate(partRotationSpeed*Time.deltaTime*randomVectors[i]);
		        }
	        }

	        for (int i = 0; i < 5; i++)
	        {
		        Vector3 acceleration = Input.acceleration;
		        currentAcceleretaion = acceleration.magnitude;
		        data.RemoveAt(0);
		        data.Add((int) Math.Round(currentAcceleretaion));

		        if (currentAcceleretaion > 4.5f)
		        {
			        int sum=0;
			        for (int j = 0; j < 9; j++)
			        {
				        sum+=data[i];
			        }

			        if (sum <= 10 && sum >= 8)
			        {
				        probableShot = true;
				        if (dummySet)
				        {
					       if (positionNum > 0)
					       {
						       if (Vector2.Distance(torso2dOrigin, points[0]) < radius)
						       {
							       explosion = true;
						       }
					       }
				        }
			        }
			        else
			        {
				        probableShot = false;
			        }
		        }
		        Thread.Sleep(5);
	        }


        }

        private void OnGUI()
        {
	        GUIStyle bb = new GUIStyle();
	        bb.normal.background = null;
	        bb.normal.textColor = new Color(1, 0, 0);
	        bb.fontSize = 40;

	        bs = Screen.width / 12;



	        if (
		        GUI.Button(new Rect(Screen.width/2-5*bs-50, 100, 2*bs, 2*bs), "Hud"))
	        {
		        hud = !hud;
	        }


	        if (hud)
	        {
		        if (
			        GUI.Button(new Rect(Screen.width/2-bs, Screen.height-100-6*bs, 2*bs, 2*bs), "Up"))
		        {
			        y -= 5;
		        }
		        if (
			        GUI.Button(new Rect(Screen.width/2-bs, Screen.height-100-2*bs, 2*bs, 2*bs), "Down"))
		        {
			        y += 5;
		        }
		        if (
			        GUI.Button(new Rect(Screen.width/2+bs, Screen.height-100-4*bs, 2*bs, 2*bs), "Right"))
		        {
			        x += 5;
		        }
		        if (
			        GUI.Button(new Rect(Screen.width/2-3*bs, Screen.height-100-4*bs, 2*bs, 2*bs), "Left"))
		        {
			        x -= 5;
		        }


		        if (
			        GUI.Button(new Rect(Screen.width / 2 - 5 * bs - 50, Screen.height - 100 - 14 * bs, 2 * bs, 2 * bs),
				        "Dummy"))
		        {
			        explosion = false;
			        dummySet = true;
			        dummyBuilt = false;
		        }
		        if (
			        GUI.Button(new Rect(Screen.width / 2 - 5 * bs - 50, Screen.height - 100 - 12 * bs, 2 * bs, 2 * bs),
				        "Target"))
		        {

			        target = ARFrame.GetPose();
			        targetSet = true;

		        }
		        if (
			        GUI.Button(new Rect(Screen.width / 2 - 5 * bs - 50, Screen.height - 100 - 10 * bs, 2 * bs, 2 * bs),
				        "Clear All Markers"))
		        {
			        if (!markerOn)
			        {
				        int a = markers.Count;
				        for (int i = 0; i < a; i++)
				        {
					        Destroy(markers[0]);
					        markers.RemoveAt(0);
				        }

				        points.Clear();
				        x = 0;
				        y = 0;
				        positionNum = 0;
			        }
		        }

		        if (
			        GUI.Button(new Rect(Screen.width / 2 - 5 * bs - 50, Screen.height - 100 - 8 * bs, 2 * bs, 2 * bs),
				        "Set"))
		        {
			        markerOn = true;
		        }

		        if (
			        GUI.Button(new Rect(Screen.width / 2 - 5 * bs - 50, Screen.height - 100 - 6 * bs, 2 * bs, 2 * bs),
				        "Ready"))
		        {
			        points.Add(new Vector2(Screen.width / 2 - bs / 2 + x, Screen.height / 2 - bs / 2 + y));
			        x = 0;
			        y = 0;
			        positionNum++;
			        markerOn = false;
			        markerSet = false;
		        }

		        if (
			        GUI.Button(new Rect(Screen.width / 2 - 5 * bs - 50, Screen.height - 100 - 4 * bs, 2 * bs, 2 * bs),
				        "Reset Last"))
		        {
			        if (!markerOn)
			        {
				        Destroy(markers[markers.Count - 1]);
				        markers.RemoveAt(markers.Count - 1);
				        points.RemoveAt(points.Count - 1);
				        x = 0;
				        y = 0;
				        positionNum--;
				        markerOn = true;
			        }
		        }

		        if (
			        GUI.Button(new Rect(Screen.width / 2 - 5 * bs - 50, Screen.height - 100 - 2 * bs, 2 * bs, 2 * bs),
				        "Calibrate Gyro"))
		        {

			        defTilt = ARFrame.GetPose().rotation.eulerAngles.z;
			        markX = (float) (Screen.width / 2 - 4.2 * bs * Math.Sin(defTilt / 180 * Math.PI) - bs);
			        markY = (float) (Screen.height / 2 + 4.2 * bs * Math.Cos(defTilt / 180 * Math.PI) - bs);

		        }


		        GUI.DrawTexture(new Rect(Screen.width / 2 - bs, Screen.height / 2 - bs, 2 * bs, 2 * bs),
			        defaultCrosshair, ScaleMode.ScaleToFit, true);

		        GUI.DrawTexture(new Rect(Screen.width / 2 - 4 * bs, Screen.height / 2 - 4 * bs, 8 * bs, 8 * bs), gyro,
			        ScaleMode.ScaleToFit, true);

		        GUI.DrawTexture(
			        new Rect((float) (Screen.width / 2 - 4 * bs * Math.Sin(theta / 180 * Math.PI) - bs),
				        (float) (Screen.height / 2 + 4 * bs * Math.Cos(theta / 180 * Math.PI) - bs), 2 * bs, 2 * bs),
			        gyroMark, ScaleMode.ScaleToFit, true);
		        GUI.DrawTexture(new Rect(markX, markY, 2 * bs, 2 * bs), gyroMark, ScaleMode.ScaleToFit, true);

		        if (markerSet)
		        {
			        GUI.DrawTexture(new Rect(Screen.width / 2 + x - bs / 2, Screen.height / 2 + y - bs / 2, bs, bs),
				        crosshair, ScaleMode.ScaleToFit, true);
		        }



		        GUI.Label(new Rect(100, 100, 1000, 100), "Distance to target: " + distance, bb);
		        bb.fontSize = 20;
		        GUI.Label(new Rect(100, 200, 1000, 100), "Shot: " + probableShot, bb);
		        bb.fontSize = 40;


		        if (dummySet)
		        {
			        GUI.DrawTexture(new Rect(torso2dOrigin.x, torso2dOrigin.y, bs, bs), crosshair, ScaleMode.ScaleToFit,
				        true);
		        }


	        }

	        for (int i = 0; i < positionNum; i++)
	        {
		        GUI.DrawTexture(new Rect(points[i].x, points[i].y, bs, bs), crosshair, ScaleMode.ScaleToFit, true);
	        }


        }


    }
}
