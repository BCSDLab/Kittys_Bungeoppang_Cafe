using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class CatText : ScriptableObject
{
	public List<CatTextEntity> Sheet1; // Replace 'EntityType' to an actual type that is serializable.
}
