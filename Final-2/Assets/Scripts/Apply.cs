using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Apply : MonoBehaviour {

	private float radius = 0.05f;
	private float pull = 0.7f;
	private float push=0.7f;
	private float rotationSpeed=1.0f;
	int[] triangles;
	public string current;
	public MeshFilter filter;
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 lastpos;
	public bool sym;
	private GameObject g;
	private Mesh myMesh;
	public Material mat;

	//VRTK
	public VRTK_Pointer point;
	public VRTK_ControllerEvents control;
	public bool pressed;
	
	public void Start(){
		sym = true;
		current = "";
		pressed = true;
	}

	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

		offset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

	}
	public void SaveIt()
		{
		
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			System.IO.FileStream fs = new System.IO.FileStream(Application.dataPath + "SaveMesh.dat", System.IO.FileMode.Create);

			Serialize_Mesh  Ser_Mesh = new Serialize_Mesh (filter.mesh);
			bf.Serialize(fs, Ser_Mesh);
			fs.Close();
			
			float red=this.GetComponent<Renderer>().material.color.r;
			float green=this.GetComponent<Renderer>().material.color.g;
			float blue=this.GetComponent<Renderer>().material.color.b;
			float alpha=this.GetComponent<Renderer>().material.color.a;
		
			string mycolor=red+" "+green+" "+blue+" "+alpha;
			Debug.Log(mycolor+"......................................");
		string path=Application.dataPath+"file.txt";

		System.IO.File.WriteAllText(path,mycolor);
			
		}

	public void LoadIt()
		{
		if (System.IO.File.Exists (Application.dataPath + "SaveMesh.dat")) {

			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter ();
			System.IO.FileStream fs = new System.IO.FileStream (Application.dataPath + "SaveMesh.dat", System.IO.FileMode.Open);
			Serialize_Mesh Ser_Mesh = (Serialize_Mesh)bf.Deserialize (fs);
			myMesh = Ser_Mesh.Construct ();

			fs.Close ();

			filter.mesh = myMesh;
			filter.gameObject.GetComponent<Renderer> ().material = mat;
			filter.mesh.RecalculateNormals ();
			filter.mesh.RecalculateBounds ();
			filter.gameObject.GetComponent<MeshCollider> ().sharedMesh = filter.mesh;
			
			string path=Application.dataPath+"file.txt";
			string tmp=System.IO.File.ReadAllText(path);
			string[] arr=tmp.Split(char.Parse(" "));
			float r=float.Parse(arr[0]);
			float g=float.Parse(arr[1]);
			float b=float.Parse(arr[2]);
			float a=float.Parse(arr[3]);

			
			this.GetComponent<Renderer>().material.color=new Color(r,g,b,a);
		} 
		else {
			Debug.Log ("No file");
		}

	}


				


	void Update(){
		if (Input.GetKeyDown (KeyCode.S)) {

			if (sym == false) {
				sym = true;
			} 
			else {
				sym = false;
			}
			Debug.Log ("Pressed");
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Pressed sPACE");
			SaveIt ();
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			Debug.Log ("L pressed");
			LoadIt ();
		}
		


		

	}
	void OnMouseDrag()
	{
		if (Input.GetKey(KeyCode.RightShift)) {
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);
			transform.position = curPosition;
		}
		if(Input.GetKey(KeyCode.LeftShift)){
		float XaxisRotation = Input.GetAxis("Mouse X")*rotationSpeed;
		float YaxisRotation = Input.GetAxis("Mouse Y")*rotationSpeed;
		transform.RotateAround (Vector3.down, XaxisRotation);
		transform.RotateAround (Vector3.right, YaxisRotation);
		}
	}

	void FixedUpdate () {

        if (point != null) { 
            RaycastHit hit = point.pointerRenderer.GetDestinationHit ();
            if (hit.transform != null) { 
            if (hit.transform.gameObject.tag=="Stone") {
				filter = hit.collider.GetComponent<MeshFilter> ();

                    if (filter)
                    {
                        if (current.Equals("Pull"))
                        {

                            Vector3 relativePoint = filter.transform.InverseTransformPoint(hit.point);
                            Vector3 inversePoint = relativePoint;
                            inversePoint.x = -relativePoint.x;
                            if (pressed)
                            {
                                Pull(filter.mesh, relativePoint, pull * Time.deltaTime, radius);
                                if (sym)
                                {
                                    Pull(filter.mesh, inversePoint, push * Time.deltaTime, radius);
                                }
                                filter.gameObject.GetComponent<MeshCollider>().sharedMesh = filter.mesh;
                            }
                        }
                        if (current.Equals("Push"))
                        {

                            Vector3 relativePoint = filter.transform.InverseTransformPoint(hit.point);
                            Vector3 inversePoint = relativePoint;
                            inversePoint.x = -relativePoint.x;
                            if (pressed)
                            {
                                Push(filter.mesh, relativePoint, push * Time.deltaTime, radius);
                                if (sym)
                                {
                                    Push(filter.mesh, inversePoint, push * Time.deltaTime, radius);
                                }
                                filter.gameObject.GetComponent<MeshCollider>().sharedMesh = filter.mesh;
                            }
                        }
                        if (current.Equals("Flatten"))
                        {

                            Vector3 relativePoint = filter.transform.InverseTransformPoint(hit.point);
                            Vector3 inversePoint = relativePoint;
                            inversePoint.x = -relativePoint.x;
                            if (pressed)
                            {
                                Flatten(filter.mesh, relativePoint, push * Time.deltaTime, radius);
                                if (sym)
                                {
                                    Flatten(filter.mesh, inversePoint, push * Time.deltaTime, radius);
                                }
                                filter.gameObject.GetComponent<MeshCollider>().sharedMesh = filter.mesh;
                            }
                        }
                        if (current.Equals("BuildUp"))
                        {

                            Vector3 relativePoint = filter.transform.InverseTransformPoint(hit.point);
                            Vector3 inversePoint = relativePoint;
                            inversePoint.x = -relativePoint.x;
                            if (pressed)
                            {
                                BuildUp(filter.mesh, relativePoint, push * Time.deltaTime, radius);

                                if (sym)
                                {
                                    BuildUp(filter.mesh, inversePoint, push * Time.deltaTime, radius);
                                }

                                filter.gameObject.GetComponent<MeshCollider>().sharedMesh = filter.mesh;
                            }
                        }
						if(current.Equals("Drag")){
							
                        //     Vector3 relativePoint = filter.transform.InverseTransformPoint(hit.point);
                        
						// Vector3 curScreenPoint = new Vector3 (relativePoint.x, relativePoint.y, screenPoint.z);
						// Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);
						// transform.position = curPosition;
						}

                    }
                }







		}
	}
	}

	public void BuildUp(Mesh mesh, Vector3 position, float power, float Radius){
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;

		Vector3 avg_normal = Vector3.zero;
		Vector3 avg_position = Vector3.zero;
		for (int i = 0; i < vertices.Length; i++) {
			float difference = Vector3.Distance(vertices [i],position);
			if (difference < Radius) {
				avg_normal += normals [i];
				avg_position += vertices [i];
			} 
		}


		avg_normal = avg_normal.normalized;
		avg_position = avg_position.normalized;
		avg_position = avg_position + pull * avg_normal;
		float d=Vector3.Dot (avg_position, avg_normal);

		for (int i = 0; i < vertices.Length; i++) {
			float difference = Vector3.Distance(vertices [i],position);
			if (difference < Radius) {
				List<int> ans=find_same_vertices(mesh,vertices[i]);
				Debug.Log (ans.Count);
				for (int j = 0; j < ans.Count; j++) {
					float l=Vector3.Dot (avg_normal, vertices [ans[j]]) + d;
					float rho = power * weight (vertices [ans[j]], position);
					vertices [ans[j]] = vertices [ans[j]] + rho*l*normals [i];
				}
			} 
		}
		mesh.vertices = vertices;
		mesh.RecalculateNormals ();
		mesh.RecalculateBounds ();
	
	
	}
	public float smooth_step(float t){
		float ans=6 * Mathf.Pow (t, 5) - 15 * Mathf.Pow (t, 4) + 10 * Mathf.Pow (t, 3);
		return ans;
	}

	public float weight(Vector3 vertex,Vector3 p){
		Vector3 v = vertex - p;
		float t=(v.magnitude / radius);
		float x=smooth_step (t);
		float ans = 1 - x;
		return ans;
		
	}
	public void Flatten(Mesh mesh, Vector3 position, float power, float Radius){
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;
	
		Vector3 avg_normal = Vector3.zero;
		Vector3 avg_position = Vector3.zero;
		for (int i = 0; i < vertices.Length; i++) {
			float difference = Vector3.Distance(vertices [i],position);
			if (difference < Radius) {
				avg_normal += normals [i];
				avg_position += vertices [i];
			} 
		}
		avg_normal = avg_normal.normalized;
		avg_position = avg_position.normalized;
		float d=Vector3.Dot (avg_position, avg_normal);

		for (int i = 0; i < vertices.Length; i++) {
			float difference = Vector3.Distance(vertices [i],position);
			if (difference < Radius) {
				List<int> ans=find_same_vertices(mesh,vertices[i]);
				Debug.Log (ans.Count);
				for (int j = 0; j < ans.Count; j++) {
					float l=Vector3.Dot (avg_normal, vertices [ans[j]]) + d;
					float rho = power * weight (vertices [ans [j]], position);
					vertices [ans [j]] = vertices [ans [j]] -l*rho*normals [i];
				}
			} 
		}
		mesh.vertices = vertices;
		mesh.RecalculateNormals ();
		mesh.RecalculateBounds ();
	}
	public void Push(Mesh mesh, Vector3 position, float power, float Radius){
		power = -1 * power;
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;
		for (int i = 0; i < vertices.Length; i++) {
			float difference = Vector3.Distance (vertices [i], position);
			if (difference < Radius) {
				List<int> ans=find_same_vertices(mesh,vertices[i]);
				Debug.Log (ans.Count);
				for (int j = 0; j < ans.Count; j++) {

					float rho = power * weight (vertices [ans [j]], position);
					vertices [ans [j]] = vertices [ans [j]] + normals [i] * rho;
				}

			
			}
		}
		mesh.vertices = vertices;
		mesh.RecalculateNormals ();
		mesh.RecalculateBounds ();
	}

	public void Pull(Mesh mesh, Vector3 position, float power, float Radius){
		
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;

		for (int i = 0; i < vertices.Length; i++) {
			float difference = Vector3.Distance (vertices [i], position);
			if (difference < Radius) {
				List<int> ans=find_same_vertices(mesh,vertices[i]);
				Debug.Log (ans.Count);
				for (int j = 0; j < ans.Count; j++) {
					
					float rho = power * weight (vertices [ans [j]], position);
					vertices [ans [j]] = vertices [ans [j]] + normals [i] * rho;
				}
			}
		}
		mesh.vertices = vertices;
		mesh.RecalculateNormals ();
		mesh.RecalculateBounds ();

	}
	public List<int> find_same_vertices(Mesh mesh,Vector3 vert){
		triangles = mesh.triangles;
		List<int> same = new List<int> ();
		Vector3[] vertices = mesh.vertices;
		for (int i = 0; i < triangles.Length; i++) {
			int index = triangles [i];
			Vector3 v = vertices[index];
			if (v == vert) {
				same.Add (index);
			}
		}
		return same;
	}







}


