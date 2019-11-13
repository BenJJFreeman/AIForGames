using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarColour {

    public static Gradient GetStarColour(char _type)
    {
        switch (_type)
        {
            case 'B':
                return GetClassBColour();
            case 'A':
                return GetClassAColour();
            case 'F':
                return GetClassFColour();
            case 'G':
                return GetClassGColour();
            case 'K':
                return GetClassKColour();
            case 'M':
                return GetClassMColour();
            default:
                return GetClassAColour();
        }

    }

    public static Gradient GetClassBColour (){

		Gradient newG = new Gradient();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color ( 0.94902f, 0.93333f, 0.92941f ), 0.03f),
			new GradientColorKey (new Color ( 0.53725f, 0.31373f, 0.92549f ), 0.14f),
			new GradientColorKey (new Color ( 0.32941f, 0.91765f, 1.00000f ), 0.50f),
			new GradientColorKey (new Color ( 0.53725f, 0.31373f, 0.92549f ), 0.80f),
			new GradientColorKey (new Color ( 0.94902f, 0.93333f, 0.92941f ), 0.97f),
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
	public static Gradient GetClassAColour (){

		Gradient newG = new Gradient();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color ( 0.94902f, 0.93333f, 0.92941f ), 0.03f),
			new GradientColorKey (new Color ( 0.32941f, 0.91765f, 1.00000f ), 0.14f),
			new GradientColorKey (new Color ( 0.15294f, 0.42353f, 0.95686f ), 0.50f),
			new GradientColorKey (new Color ( 0.55686f, 0.87059f, 1.00000f ), 0.80f),
			new GradientColorKey (new Color ( 0.94902f, 0.93333f, 0.92941f ), 0.97f),
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
	public static Gradient GetClassFColour (){

		Gradient newG = new Gradient();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color ( 0.94902f, 0.93333f, 0.92941f ), 0.03f),
			new GradientColorKey (new Color ( 0.32941f, 0.91765f, 1.00000f ), 0.50f),
			new GradientColorKey (new Color ( 0.94902f, 0.93333f, 0.92941f ), 0.97f),
			//new GradientColorKey (new Color ( 0.55686f, 0.87059f, 1.00000f ), 0.80f),
			//new GradientColorKey (new Color ( 0.94902f, 0.93333f, 0.92941f ), 0.97f),
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
	public static Gradient GetClassGColour (){

		Gradient newG = new Gradient();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color ( 1.00000f, 0.72549f, 0.43529f ), 0.0f),
			new GradientColorKey (new Color ( 0.95686f, 0.91765f, 0.35686f ), 0.25f),
			new GradientColorKey (new Color ( 1.00000f, 0.72549f, 0.43529f ), 0.50f),
			new GradientColorKey (new Color ( 0.95686f, 0.91765f, 0.35686f ), 0.75f),
			new GradientColorKey (new Color ( 1.00000f, 0.72549f, 0.43529f ), 1.0f),
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
	public static Gradient GetClassKColour (){

		Gradient newG = new Gradient();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color ( 0.99216f, 0.32157f, 0.12549f ), 0.30f),
			new GradientColorKey (new Color ( 0.95686f, 0.91765f, 0.35686f ), 0.45f),
			new GradientColorKey (new Color ( 0.50196f, 0.25098f, 0.11373f ), 0.50f),
			new GradientColorKey (new Color ( 0.95686f, 0.91765f, 0.35686f ), 0.55f),
			new GradientColorKey (new Color ( 0.99216f, 0.32157f, 0.12549f ), 0.70f),
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
	public static Gradient GetClassMColour (){

		Gradient newG = new Gradient();
		GradientColorKey[] gck = new GradientColorKey[] {				
			new GradientColorKey (new Color ( 1.00000f, 0.28235f, 0.11765f ), 0.55f),
			new GradientColorKey (new Color ( 0.93333f, 0.89020f, 0.32157f ), 0.60f),
			new GradientColorKey (new Color ( 1.00000f, 0.28235f, 0.11765f ), 0.65f),
			new GradientColorKey (new Color ( 0.93333f, 0.89020f, 0.32157f ), 0.70f),
			new GradientColorKey (new Color ( 1.00000f, 0.28235f, 0.11765f ), 0.75f),
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
}
