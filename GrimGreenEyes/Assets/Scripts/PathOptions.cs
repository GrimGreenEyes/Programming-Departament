using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PathOptions : MonoBehaviour
{
    // Start is called before the first frame update

    //public GameObject[] pathOpt;
  //  public Dictionary<GameObject,bool> opt = new Dictionary<GameObject, bool>();

	[SerializeField] public SubClass[] myArray;

	public GameObject navigationController;
	public bool firstItem;
	public GameObject path;
	public LineRenderer line;

	public List<LineRenderer> lines;

    //Saving Game
    public GameObject glovalVar;

	public bool isLast;


	//public bool isLast;


	private void Start()
	{
		navigationController = GameObject.Find("NavigationController");
        glovalVar = GameObject.Find("GlobalAttributes");

		if (isLast)
		{
			gameObject.GetComponent<Image>().color = Color.red;
		}

        if (firstItem)
		{
			navigationController.GetComponent<Navigation>().actualNode = this.gameObject;
			glovalVar.GetComponent<GlobalVar>().actualNode = this.gameObject;
		}

		if (firstItem)
		{
			Vector3 thisPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1);
			GameObject.Find("PLAYER").transform.position = thisPos;
			bool cont = true;
			var copy = myArray;
			var itNode = gameObject;
			line.SetPosition(0, gameObject.transform.position);
			line.SetPosition(1, gameObject.transform.position);

			while (cont)
			{
				if (copy.Length > 0)
				{
					//lines.Add(line);
					//Instantiate(path, itNode.transform.position, copy[0].node.transform.position)
					Vector3 itNodePos = new Vector3(itNode.transform.position.x, itNode.transform.position.y, -0f);
					Vector3 copyNodePos = new Vector3(copy[0].node.transform.position.x, copy[0].node.transform.position.y, -0f);
					//lines[lines.Count-1].SetPosition(0, itNodePos);
					//lines[lines.Count-1].SetPosition(1, copyNodePos);
					copy[0].node.GetComponent<PathOptions>().line.SetPosition(0, itNodePos);
					copy[0].node.GetComponent<PathOptions>().line.SetPosition(1, copyNodePos);

					Debug.Log(copy[0].node.name);
					itNode = copy[0].node;
					copy = copy[0].node.GetComponent<PathOptions>().myArray;
					Debug.Log("line added");
					//Debug.Log(lines.Count);
				}
				else
					cont = false;
			}
			//}
			/*if (isLast)
			{
				Array.Resize(ref myArray, myArray.Length + 1);
				SubClass testt = new SubClass();
				myArray[1] = testt;
			}*/
		}
	}

	private void OnDrawGizmos()
    {
		foreach(var x in myArray)
        {
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(this.transform.position, x.node.transform.position);
		}
	}
    public void SetValue(int index, SubClass subClass)
	{

		// Perform any validation checks here.
		myArray[index] = subClass;
	}
	public SubClass GetValue(int index)
	{
		// Perform any validation checks here.
		return myArray[index];
	}

	public bool getGameobjects(GameObject node)
    {
		//Check if node is in myArray and bool isup
		foreach (var x in myArray){
			if (x.node.GetInstanceID() == node.GetInstanceID()){
				if (x.isUp == true)
					return true;
			}
        }
		return false;
    }

	public void navigate(GameObject thisNode)
	{
		if(navigationController == null)
		{
            navigationController = GameObject.Find("NavigationController");
        }
        navigationController.GetComponent<Navigation>().moveToNode(thisNode);
		if(glovalVar.GetComponent<GlobalVar>().actualNode.GetComponent<PathOptions>().getGameobjects(thisNode))
		StartCoroutine(goMatch());
	}


	public IEnumerator goMatch()
	{
		Debug.Log("GOMATCH");
		yield return new WaitForSeconds(1.0f);
		glovalVar.GetComponent<MainController>().loadScreen("CombatScene");

        if (isLast)
        {
			glovalVar.GetComponent<GlobalVar>().loadNewScene();
        }


	}
}
[System.Serializable]
public class SubClass
{
	public GameObject node;
	public bool isUp;
}

