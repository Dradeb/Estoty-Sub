using UnityEngine;
using System.Collections;

public class Fps : MonoBehaviour
{

	string label = "";
	string label2 = "";
	float count;
	float lowest = 100f; 
	IEnumerator Start()
	{
		yield return new WaitForSeconds(2);
		GUI.depth = 2;
		while (true)
		{
			if (Time.timeScale == 1)
			{
				yield return new WaitForSeconds(0.1f);
				count = (1 / Time.deltaTime);
				if (count < lowest)
					lowest = count;

				label2 = "Lowest :" + (Mathf.Round(lowest));
				label = "FPS :" + (Mathf.Round(count));
			}
			else
			{
				label = "Pause";
			}
			yield return new WaitForSeconds(0.001f);
		}
	}
	 private GUIStyle guiStyle = new GUIStyle(); 
	void OnGUI()
	{

		guiStyle.normal.textColor = Color.green;
		guiStyle.fontSize = 80;
		GUI.Label(new Rect(5, 40, 100, 100), label,guiStyle);
		GUI.Label(new Rect(5, 100, 100, 100), label2, guiStyle);
	}
}