Shader "Custom/ScreenCutoutPortal"
{
    Properties
    {
        _MainTex ("Render Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent" 
            "RenderType"="Transparent" 
        }
        
        // Optimisation : On désactive l'écriture Z si vous voulez un effet de "trou" pur 
        // ou on laisse ZWrite On pour qu'il soit bien occlus par les objets devant.
        ZWrite On
        Cull Back
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.pos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Division perspective pour obtenir les coordonnées d'écran normalisées (0 à 1)
                float2 screenUV = i.screenPos.xy / i.screenPos.w;
                return tex2D(_MainTex, screenUV);
            }
            ENDCG
        }
    }
}