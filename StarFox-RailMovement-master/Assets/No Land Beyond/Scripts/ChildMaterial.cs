using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMaterial : MonoBehaviour
{
	public Material wall_material;
	private List<Material> material_list; 
    // Start is called before the first frame update
    void Start()
    {
    	material_list.Add(wall_material);
       	int num_children = transform.childCount;
 		for(int i = 0; i < num_children; i++)
 		{
     		GameObject child = transform.GetChild(i).gameObject;
     		child.GetComponent<Renderer>().materials = material_list.ToArray();
 		}  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
