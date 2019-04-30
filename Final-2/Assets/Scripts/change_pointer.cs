using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class change_pointer : MonoBehaviour {
	public Texture2D cse;
	public bool brush_change;
	public CursorMode mode = CursorMode.Auto;
	public Vector2 hitmap = Vector2.zero;


	public GameObject ColorPickedPrefab;
	private ColorPickerTriangle CP;
	private bool isPaint = false;
	private GameObject go;
	private Color mat;



	void Start () {
		brush_change = false;

	}
	
	// Update is called once per frame
	void Update()
	{
		if (isPaint) {	
			Debug.Log ("Called");
			var a= this.GetComponent<Button> ().colors;
			a.normalColor = CP.TheColor;
			this.GetComponent<Button> ().colors = a;


		}
	}
	public void cursor_change(){
		if (brush_change) {
			//Cursor.SetCursor (null, hitmap, mode);
			brush_change = false;
			StopPaint ();
		} 
		else {
			//Cursor.SetCursor (cse, hitmap, mode);
			brush_change = true;
			StartPaint ();
		}
	}
	private void StartPaint()
	{
		Vector3 temp=new Vector3 (-10.4f, 5.0f, 2.0f);

		go = (GameObject)Instantiate (ColorPickedPrefab,temp,Quaternion.identity);
			go.transform.localScale = Vector3.one * 2.0f;
			go.transform.LookAt (Camera.main.transform);
			CP = go.GetComponent<ColorPickerTriangle> ();
			CP.SetNewColor (mat);
			isPaint = true;
		
	}

	private void StopPaint()
	{
		Destroy(go);
		isPaint = false;
	}










}
