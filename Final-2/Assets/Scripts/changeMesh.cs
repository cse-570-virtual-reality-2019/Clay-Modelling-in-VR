using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMesh : MonoBehaviour {

	public GameObject obj;

	public void sphere(){
		GameObject p=GameObject.CreatePrimitive(PrimitiveType.Sphere);
		p.GetComponent<Transform>().position=new Vector3(0,100f,0);
		obj.GetComponent<MeshFilter>().mesh = p.GetComponent<MeshFilter>().mesh;
	}
	public void cylinder(){
		GameObject p=GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		p.GetComponent<Transform>().position=new Vector3(0,100f,0);
		obj.GetComponent<MeshFilter>().mesh = p.GetComponent<MeshFilter>().mesh;
	}

	public void cube(){
		GameObject p=GameObject.CreatePrimitive(PrimitiveType.Cube);
		p.GetComponent<Transform>().position=new Vector3(0,100f,0);
		obj.GetComponent<MeshFilter>().mesh = p.GetComponent<MeshFilter>().mesh;
	}

	public void capsule(){
		GameObject p=GameObject.CreatePrimitive(PrimitiveType.Capsule);
		p.GetComponent<Transform>().position=new Vector3(0,100f,0);
		obj.GetComponent<MeshFilter>().mesh = p.GetComponent<MeshFilter>().mesh;
	}
}
