using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {
	public GameObject ap1;

	private Apply ap;
	public Slider s1;
	public Slider s2;
	public Slider s3;

	public void Start(){
      
        ap = ap1.GetComponent<Apply> ();
	}
	public void Pull(){
		ap.current = "Pull";
        Debug.Log("Pull Called");
	}
	public void Push(){
		ap.current = "Push";
		Debug.Log("Push Called");
	}
	public void Flatten(){
		ap.current = "Flatten";
		Debug.Log("Flatten Called");
	}
	public void BuildUp(){
		ap.current = "BuildUp";
		Debug.Log("BuildUp Called");
	}
	public void Symmetry(bool state){
		ap.sym = state;
		Debug.Log("Symmetry Called");
	}
	public void Save(){
		Debug.Log("YEHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
		ap.SaveIt ();
		Debug.Log("Save Called......................................................................................");
	}
	public void Load(){
		ap.LoadIt ();
		Debug.Log("Load Called");
	}
	public void Update(){
		Slider();
	}

	public void Slider(){
		this.ap.gameObject.transform.rotation=Quaternion.Euler(s1.value,s2.value,s3.value);

	}


}
