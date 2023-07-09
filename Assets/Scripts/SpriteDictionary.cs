using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sprite Dictionary")]
public class SpriteDictionary : ScriptableObject {
	[SerializeField] public List<JobDefinition> dictionaries;
	
	public JobDefinition GetJob(PrintJobEnum jobType) {
		return dictionaries.Find(dict => dict.key == jobType);
	}
}
