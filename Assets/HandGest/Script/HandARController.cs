

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

	    private float timer = 0.0f;
        private GameObject marker;
        private List<GameObject> markers = new List<GameObject>();

        private int i = 0;
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

        private bool hud = false;

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
        private bool targetSet=true;

        private float distance = 0;

        private double theta, psy, gamma;
        private double defTilt=0;
        private double tilt;

        public Texture gyro;
        public Texture gyroMark;
        private float markX=-100;
        private float markY=-100;

        public void Start()
        {
	        marker= GameObject.Find("Marker2");
            marker.SetActive(true);
        }

        public void Update()
        {

	        theta = ARFrame.GetPose().rotation.eulerAngles.z;

	        tilt = Math.Abs(theta - defTilt);

	        if (!targetSet)
	        {
		        distance = Vector3.Distance(ARFrame.GetPose().position, target.position);
	        }

	        if (markerOn&&!markerSet)
	        {
		        markers.Add(Instantiate(marker,
			        new Vector3(ARFrame.GetPose().position.x, ARFrame.GetPose().position.y-1.0f,
				        ARFrame.GetPose().position.z), Quaternion.Euler(0, 0, 0)));
		        markerSet = true;

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
		        GUI.Button(new Rect(Screen.width/2-5*bs-50, Screen.height-100-8*bs, 2*bs, 2*bs), "Set"))
	        {
		        markerOn = true;

	        }
	        if (
		        GUI.Button(new Rect(Screen.width/2-5*bs-50, Screen.height-100-6*bs, 2*bs, 2*bs), "Ready"))
	        {
		        points.Add(new Vector2(Screen.width/2-bs/2+x,Screen.height/2-bs/2+y));
		        x = 0;
		        y = 0;
		        positionNum++;
		        markerOn = false;
		        markerSet = false;
	        }
	        if (
		        GUI.Button(new Rect(Screen.width/2-5*bs-50, Screen.height-100-4*bs, 2*bs, 2*bs), "Target")&&targetSet)
	        {

		        target = ARFrame.GetPose();
		        targetSet = false;

	        }

	        if (
		        GUI.Button(new Rect(Screen.width/2-5*bs-50, Screen.height-100-2*bs, 2*bs, 2*bs), "Calibrate Gyro"))
	        {

		        defTilt=ARFrame.GetPose().rotation.eulerAngles.z;
		        markX = (float) (Screen.width / 2 - 4.2 * bs * Math.Sin(defTilt / 180 * Math.PI) - bs);
		        markY= (float) (Screen.height / 2 + 4.2 * bs * Math.Cos(defTilt / 180 * Math.PI) - bs);

	        }


	        GUI.DrawTexture(new Rect(Screen.width/2-bs, Screen.height/2-bs, 2*bs, 2*bs), defaultCrosshair, ScaleMode.ScaleToFit, true);

	        GUI.DrawTexture(new Rect(Screen.width/2-4*bs, Screen.height/2-4*bs, 8*bs, 8*bs), gyro, ScaleMode.ScaleToFit, true);

	        GUI.DrawTexture(new Rect((float)(Screen.width/2-4*bs*Math.Sin(theta/180*Math.PI)-bs), (float)(Screen.height/2+4*bs*Math.Cos(theta/180*Math.PI)-bs), 2*bs, 2*bs), gyroMark, ScaleMode.ScaleToFit, true);
	        GUI.DrawTexture(new Rect(markX, markY, 2*bs, 2*bs), gyroMark, ScaleMode.ScaleToFit, true);

	        if (markerSet)
	        {
		        GUI.DrawTexture(new Rect(Screen.width/2+x-bs/2,Screen.height/2+y-bs/2, bs, bs), crosshair, ScaleMode.ScaleToFit, true);
	        }

	        for (int i = 0; i < positionNum; i++)
	        {
		        GUI.DrawTexture(new Rect(points[i].x,points[i].y, bs, bs), crosshair, ScaleMode.ScaleToFit, true);
	        }

	        GUI.Label(new Rect(100, 100, 1000, 100), "Distance to target: "+distance.ToString(), bb);

        }


    }
}
