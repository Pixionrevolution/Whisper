 Shader "Custom/Rim Light V2"
 {
     Properties
     {
     	 // Albedo	
         _MainTex("Texture", 2D) = "white" {}
         //Normal
         _Normal("Normal", 2D) = "bump"{}
         // Blue Color
         _RimColorOne("Rim Color One", Color) = (0.26,0.19,0.16,0.0)
         // Red Color
         _RimColorTwo("Rim Color Two", Color) = (0.26,0.19,0.16,0.0)
         // Intensity of Rim
         _RimPower("Rim Power", Range(0.0,10.0)) = 3.0
         // Rim switch blue to red
         [HideInInspector]
         _RimSwitch("Rim Switch Color", Range(0,1)) = 0
         // Blend two Color
         _Blend("Blend Color", Range(0,1)) = 0.0
     }
         SubShader
     {
         Tags{ "Queue"="Transparent" "RenderType"="Opaque" "IgnoreProjector"="True" }

 		LOD 100
        Cull Back // Don’t render polygons facing away from the viewer
       	Lighting On
        ZWrite On // Solid Object written to th depth buffer
      
	         CGPROGRAM

	  		// Physically based Standard lighting model, and enable shadows on all light types
	 		 #pragma surface surf Standard fullforwardshadows
	 		// #pragma target 3.0

	 		 //SPECULARE FONCTION
 		    half4 LightingSimpleSpecular (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) 
 		    {
		        half3 h = normalize (lightDir + viewDir);

		        half diff = max (0, dot (s.Normal, lightDir));

		        float nh = max (0, dot (s.Normal, h));
		        float spec = pow (nh, 48.0);

		        half4 c;
		        c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * atten;
		        c.a = s.Alpha;
		        return c;
	        }



			// Structure INPUT VERTEX 
		     struct Input
		     {
		         float2 uv_MainTex;
		         float2 uv_Normal;
		         //View direction 
		         float3 viewDir;
		         //World reflection vector
		         float3 worldRefl;

		         // Reflections
		         INTERNAL_DATA
		     };

		     //Variables  (textures)
		     sampler2D _MainTex;
		     sampler2D _Normal;
		  
		    //Variables (float)
		     float4 _RimColorOne;
		     float4 _RimColorTwo;
		     float _RimPower;
		     float _RimSwitch;
		     float _Blend;
			



		     // FONCTION texture&Color
		     void surf(Input IN, inout SurfaceOutputStandard o)
		     {

		     	// Assign 2DTXT
		         o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgba;
		         //Assign normrla
		         o.Normal = UnpackNormal (tex2D (_Normal, IN.uv_Normal));
		         // Rim Color 
		         half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
				
		        //Assign RimColor emissive
		        	// Blue
		        if(_RimSwitch == 0){
		           o.Emission = _RimColorOne.rgba * pow(rim, _RimPower);
		        }
		        	//Red
		        if(_RimSwitch == 1){
		         o.Emission = _RimColorTwo.rgba * pow(rim, _RimPower);
		       	}
		       	// Blend  ColorOne & ColorTwo
		       	else{
		       	o.Emission = lerp((_RimColorOne.rgba * pow(rim, _RimPower)), (_RimColorTwo.rgba * pow(rim, _RimPower)), _Blend);
		       	}


		     }

     	ENDCG
     }
         Fallback "Diffuse"
 }