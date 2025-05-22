Shader "Unlit/Clouds"
{
    Properties
    {
        _CloudColor ("Cloud Color", Color) = (1,1,1,1)
        _MainTex ("Noise Texture", 2D) = "white" {}
        _Speed ("Scroll Speed", Float) = 0.1
        _Scale ("Noise Scale", Float) = 1.0
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

            // Simple hash function for pseudo-random value
            float hash(float3 p)
            {
                return frac(sin(dot(p, float3(12.9898, 78.233, 45.164))) * 43758.5453);
            }

            // Basic 3D value noise
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
                float3 p = i.worldPos * _Scale;
                p += _Time.y * _Speed; // Time.y is time in seconds
                float d = noise(p);
                float alpha = smoothstep(0.4, 0.7, d); // control cloud density
                return float4(_CloudColor.rgb, alpha);
            }
            ENDCG
        }
    }
}
