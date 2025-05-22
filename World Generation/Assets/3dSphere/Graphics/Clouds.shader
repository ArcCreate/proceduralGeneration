Shader "Unlit/Clouds"
{
    Properties
    {
        _CloudColor ("Cloud Color", Color) = (1,1,1,1)
        _MainTex ("Noise Texture", 2D) = "white" {}
        _Speed ("Scroll Speed", Float) = 0.1
        _Scale ("Noise Scale", Float) = 1.0
        _CloudThickness ("Cloud Thickness", Float) = 0.2
        _AlphaMultiplier ("Alpha Multiplier", Float) = 0.6
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Back
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _CloudColor;
            float _Speed;
            float _Scale;
            float _CloudThickness;
            float _AlphaMultiplier;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            float hash(float3 p)
            {
                return frac(sin(dot(p, float3(12.9898, 78.233, 45.164))) * 43758.5453);
            }

            float noise(float3 p)
            {
                float3 i = floor(p);
                float3 f = frac(p);
                f = f * f * (3.0 - 2.0 * f); // smoothstep

                float n = lerp(
                    lerp(
                        lerp(hash(i + float3(0,0,0)), hash(i + float3(1,0,0)), f.x),
                        lerp(hash(i + float3(0,1,0)), hash(i + float3(1,1,0)), f.x),
                        f.y),
                    lerp(
                        lerp(hash(i + float3(0,0,1)), hash(i + float3(1,0,1)), f.x),
                        lerp(hash(i + float3(0,1,1)), hash(i + float3(1,1,1)), f.x),
                        f.y),
                    f.z);
                return n;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 p = i.worldPos * _Scale + _Time.y * _Speed;

                // Sample cloud density at multiple depths
                float d0 = noise(p);
                float d1 = noise(p + float3(0.1, 0.1, 0.1) * _CloudThickness);
                float d2 = noise(p - float3(0.1, 0.1, 0.1) * _CloudThickness);
                float d = (d0 + d1 + d2) / 3.0;

                float alpha = smoothstep(0.45, 0.65, d) * _AlphaMultiplier;

                // Optional: fade clouds near edges (rim fade)
                float3 viewDir = normalize(i.worldPos - _WorldSpaceCameraPos);
                //float rim = saturate(dot(viewDir, normalize(i.worldPos)));
                //alpha *= rim;

                return float4(_CloudColor.rgb, alpha);
            }
            ENDCG
        }
    }
}
