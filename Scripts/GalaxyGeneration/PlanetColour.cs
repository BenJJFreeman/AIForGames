using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetColour{

	public static Gradient GetPlanetColour (PlanetType _type){
        switch (_type)
        {
            case PlanetType.Ice:
                return GetArticColour();
            case PlanetType.ColdBarren:
                return GetTundraColour();
            case PlanetType.Rock:
                return GetBarrenColour();
            case PlanetType.Ocean:
                return GetOceanColour();
            case PlanetType.Jungle:
                return GetTropicalColour();
            case PlanetType.Continental:
                return GetContinentalColour();
            case PlanetType.WetDesert:
                return GetSavannaColour();
            case PlanetType.DryDesert:
                return GetDesertColour();
            case PlanetType.HotBarren:
                return GetAridColour();
            case PlanetType.Lava:
                return GetMoltenColour();
            default:
                return GetContinentalColour ();
        }

        //          case  PlanetType.Continental:
        //	return
        //case  PlanetType.Ocean:
        //	return 
        //case  PlanetType.Tropical:
        //	return 
        //case  PlanetType.Alpine:
        //	return GetAlpineColour ();
        //case  PlanetType.Artic:
        //	return 
        //case  PlanetType.Tundra:
        //	return
        //case  PlanetType.Arid:
        //	return 
        //case  PlanetType.Desert:
        //	return 
        //case  PlanetType.Savanna:
        //	return 
        //case  PlanetType.Gaia:
        //	return GetGaiaColour ();
        //case  PlanetType.Barren:
        //	return 
        //case  PlanetType.Frozen:
        //	return GetFrozenColour ();
        //case  PlanetType.Molten:
        //	return 
        //case  PlanetType.Toxic:
        //	return GetNewColour ();
        //case  PlanetType.GasGiant:
        //	return GetAlienColour ();
        //default:
        //	return GetContinentalColour ();
        //}

    }

	public static Gradient GetCloudColour (){

		Gradient newG = new Gradient();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color (1.0f, 1.0f, 1.0f), 0.0f),
			new GradientColorKey (new Color (1.0f, 1.0f, 1.0f), 1.0f),
			//new GradientColorKey (new Color (1.0f, 0.84f, 0.56f), 0.51f),
			//new GradientColorKey (new Color (0.93f, 0.66f, 0.24f), 0.76f),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {
			new GradientAlphaKey (0.0f, 0.55f),
			new GradientAlphaKey (1.0f, 0.7f),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}

	// Wet
	public static Gradient GetContinentalColour (){

		Gradient newG = new Gradient();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color (0.12f, 0.33f, 0.95f), 0.38f),//water
			new GradientColorKey (new Color (0.83f, 0.76f, 0.66f), 0.45f),//sand
			new GradientColorKey (new Color (0.83f, 0.76f, 0.66f), 0.48f),//sand
			new GradientColorKey (new Color (0.18f, 0.34f, 0.24f), 0.50f),//greenArea
			new GradientColorKey (new Color (0.18f, 0.34f, 0.24f), 0.93f),//greenArea
			new GradientColorKey (new Color (0.61f, 0.60f, 0.56f), 0.96f),//mountain
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {				
			new GradientAlphaKey (1, 0),
			new GradientAlphaKey (1, 1),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}
	public static Gradient GetOceanColour (){

		Gradient newG = new Gradient();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color (0.094f, 0.19f, 0.44f), 0.80f),//water
			new GradientColorKey (new Color (0.18f, 0.72f, 0.17f), 0.80f),//greenArea
			new GradientColorKey (new Color (0.18f, 0.34f, 0.24f), 1.00f),//mountain
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {				
			new GradientAlphaKey (1, 0),
			new GradientAlphaKey (1, 1),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}
	public static Gradient GetTropicalColour (){

		Gradient newG = new Gradient();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color (0.094f, 0.19f, 0.44f), 0.40f),//water
			new GradientColorKey (new Color (0.18f, 0.72f, 0.17f), 0.44f),//greenArea
			new GradientColorKey (new Color (0.18f, 0.72f, 0.17f), 0.80f),//greenArea
			new GradientColorKey (new Color (0.18f, 0.34f, 0.24f), 1.00f),//mountain
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {				
			new GradientAlphaKey (1, 0),
			new GradientAlphaKey (1, 1),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}
	//

	//Frozen
	public static Gradient GetAlpineColour (){

		Gradient newG = new Gradient ();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color (0.412f, 0.50f, 0.37f), 0.52f),
			new GradientColorKey (new Color (1.0f, 1.0f, 1.0f), 0.56f),
			new GradientColorKey (new Color (1.0f, 1.0f, 1.0f), 0.59f),
			new GradientColorKey (new Color (0.67f, 0.71f, 0.71f), 0.64f),
			new GradientColorKey (new Color (0.67f, 0.71f, 0.71f), 0.75f),
			new GradientColorKey (new Color (0.412f, 0.50f, 0.37f), 0.78f),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {				
			new GradientAlphaKey (1, 0),
			new GradientAlphaKey (1, 1),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}
	public static Gradient GetArticColour (){

		Gradient newG = new Gradient ();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color (0.89f, 0.97f, 1.0f), 0.35f),
			new GradientColorKey (new Color (0.67f, 0.71f, 0.71f), 0.38f),
			new GradientColorKey (new Color (0.67f, 0.71f, 0.71f), 0.48f),
			new GradientColorKey (new Color (1.0f, 1.0f, 1.0f), 0.52f),
			new GradientColorKey (new Color (1.0f, 1.0f, 1.0f), 0.58f),
			new GradientColorKey (new Color (0.76f, 0.91f, 0.95f), 0.68f),
			new GradientColorKey (new Color (1.0f, 1.0f, 1.0f), 1.0f),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {				
			new GradientAlphaKey (1, 0),
			new GradientAlphaKey (1, 1),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}
	public static Gradient GetTundraColour (){

		Gradient newG = new Gradient ();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color (0.412f, 0.50f, 0.37f), 0.20f),
			new GradientColorKey (new Color (0.62f, 0.55f, 0.47f), 0.30f),
			new GradientColorKey (new Color (1.0f, 1.0f, 1.0f), 0.60f),
			new GradientColorKey (new Color (0.67f, 0.71f, 0.71f), 0.80f),
			new GradientColorKey (new Color (0.412f, 0.50f, 0.37f), 0.95f),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {				
			new GradientAlphaKey (1, 0),
			new GradientAlphaKey (1, 1),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}


	public static Gradient GetFrozenColour (){

		Gradient newG = new Gradient ();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color (0.89f, 0.97f, 1.0f), 0.48f),
			new GradientColorKey (new Color (1.0f, 1.0f, 1.0f), 0.52f),
			new GradientColorKey (new Color (1.0f, 1.0f, 1.0f), 0.58f),
			new GradientColorKey (new Color (0.76f, 0.91f, 0.95f), 0.68f),
			new GradientColorKey (new Color (1.0f, 1.0f, 1.0f), 1.0f),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {				
			new GradientAlphaKey (1, 0),
			new GradientAlphaKey (1, 1),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}


	//

	//Dry
	public static Gradient GetAridColour (){

		Gradient newG = new Gradient ();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color (0.91f, 0.85f, 0.32f), 0.0f),
			new GradientColorKey (new Color (0.93f, 0.66f, 0.32f), 0.13f),
			new GradientColorKey (new Color (0.93f, 0.66f, 0.32f), 0.60f),
			new GradientColorKey (new Color (0.61f,0.73f, 0.77f), 0.70f),
			new GradientColorKey (new Color (0.61f,0.73f, 0.77f), 0.80f),
			new GradientColorKey (new Color (0.88f, 0.67f, 0.41f), 0.90f),
			new GradientColorKey (new Color (0.05f, 0.24f, 0.05f), 0.95f),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {				
			new GradientAlphaKey (1, 0),
			new GradientAlphaKey (1, 1),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}
	public static Gradient GetDesertColour (){

		Gradient newG = new Gradient ();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color (0.91f, 0.85f, 0.32f), 0.2f),
			new GradientColorKey (new Color (0.93f, 0.66f, 0.32f), 0.25f),
			new GradientColorKey (new Color (1.0f, 0.84f, 0.56f), 0.3f),
			new GradientColorKey (new Color (0.93f, 0.66f, 0.24f), 0.45f),
			new GradientColorKey (new Color (0.93f, 0.66f, 0.24f), 0.85f),
			new GradientColorKey (new Color (0.05f, 0.24f, 0.05f), 0.95f),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {				
			new GradientAlphaKey (1, 0),
			new GradientAlphaKey (1, 1),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}
	public static Gradient GetSavannaColour (){

		Gradient newG = new Gradient ();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color (0.91f, 0.85f, 0.32f), 0.0f),
			new GradientColorKey (new Color (0.93f, 0.66f, 0.32f), 0.13f),
			new GradientColorKey (new Color (0.93f, 0.66f, 0.32f), 0.60f),
			new GradientColorKey (new Color (0.05f, 0.24f, 0.05f), 0.70f),
			new GradientColorKey (new Color (0.05f, 0.24f, 0.05f), 0.80f),
			new GradientColorKey (new Color (0.88f, 0.67f, 0.41f), 0.90f),
			new GradientColorKey (new Color (0.05f, 0.24f, 0.05f), 0.95f),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {				
			new GradientAlphaKey (1, 0),
			new GradientAlphaKey (1, 1),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}
	//



	public static Gradient GetBarrenColour (){

		Gradient newG = new Gradient();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color (0.41f, 0.40f, 0.40f), 0.14f),
			new GradientColorKey (new Color (0.68f, 0.66f, 0.66f), 0.20f),
			new GradientColorKey (new Color (0.66f, 0.64f, 0.64f), 0.49f),
			new GradientColorKey (new Color (0.49f, 0.49f, 0.49f), 0.55f),
			//new GradientColorKey (new Color (0, 0, 00), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {				
			new GradientAlphaKey (1, 0),
			new GradientAlphaKey (1, 1),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}

	public static Gradient GetMoltenColour (){

		Gradient newG = new Gradient ();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color (0.21f, 0.21f, 0.21f), 0.0f),
			new GradientColorKey (new Color (0.41f, 0.40f, 0.40f), 0.40f),
			new GradientColorKey (new Color (0.93f, 0.53f, 0.32f), 0.45f),
			new GradientColorKey (new Color (1.0f, 0.25f, 0.18f), 0.50f),
			new GradientColorKey (new Color (1.0f, 0.25f, 0.18f), 0.75f),
			new GradientColorKey (new Color (0.93f, 0.53f, 0.32f), 0.85f),
			//new GradientColorKey (new Color (0.41f, 0.40f, 0.40f), 0.48f),
			new GradientColorKey (new Color (0.21f, 0.21f, 0.21f), 1.0f),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {				
			new GradientAlphaKey (1, 0),
			new GradientAlphaKey (1, 1),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}

	public static Gradient GetGaiaColour (){

		Gradient newG = new Gradient ();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color (0.38f, 1.0f, 1.0f), 0.11f),
			new GradientColorKey (new Color (0.27f, 0.62f, 0.58f), 0.19f),
			new GradientColorKey (new Color (0.27f, 0.62f, 0.58f), 0.45f),
			new GradientColorKey (new Color (0.44f, 1.0f, 0.61f), 0.51f),
			new GradientColorKey (new Color (0.35f, 1.0f, 0.78f), 0.73f),
			new GradientColorKey (new Color (0.44f, 0.41f, 1.0f), 1.0f),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {				
			new GradientAlphaKey (1, 0),
			new GradientAlphaKey (1, 1),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}
	public static Gradient GetAlienColour (){

		Gradient newG = new Gradient ();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color (0.99f, 0.40f, 1.0f), 0.13f),
			new GradientColorKey (new Color (0.16f, 0.58f, 0.15f), 0.17f),
			new GradientColorKey (new Color (1.0f, 0.21f, 0.21f), 0.46f),
			new GradientColorKey (new Color (0.44f, 1.0f, 0.61f), 0.50f),
			new GradientColorKey (new Color (0.82f, 1.0f, 0.94f), 0.66f),
			new GradientColorKey (new Color (0.65f, 0.42f, 0.29f), 0.70f),
			//new GradientColorKey (new Color (0, 0, 0), 0),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {				
			new GradientAlphaKey (1, 0),
			new GradientAlphaKey (1, 1),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}

	public static Gradient GetNewColour (){

		Gradient newG = new Gradient ();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color ( 0.94902f, 0.93333f, 0.92941f ), 0.0f),
			new GradientColorKey (new Color ( 0.88235f, 0.38039f, 0.15686f ), 0.30f),
			new GradientColorKey (new Color ( 0.87451f, 0.80784f, 0.67451f ), 0.45f),
			new GradientColorKey (new Color ( 0.23529f, 0.10980f, 0.30196f ), 0.50f),
			new GradientColorKey (new Color ( 0.86275f, 0.90980f, 0.70196f ), 0.55f),
			new GradientColorKey (new Color ( 0.17647f, 0.50588f, 0.11373f ), 0.70f),
			new GradientColorKey (new Color ( 0.90588f, 0.93333f, 0.89804f ), 1.0f),
			//new GradientColorKey (new Color (0, 0, 0), 0),
		};
		GradientAlphaKey[] gak = new GradientAlphaKey[] {				
			new GradientAlphaKey (1, 0),
			new GradientAlphaKey (1, 1),
		};
		newG.SetKeys (gck, gak);
		return newG;
	}
}
