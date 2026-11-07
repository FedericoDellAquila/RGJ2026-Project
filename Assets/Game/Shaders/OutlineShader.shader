Shader "Custom/SpriteOutlineURP"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1, 1, 1, 1)
        _OutlineWidth ("Outline Width", Range(0, 10)) = 1
    }

    SubShader
    {
        Tags 
        { 
            "Queue" = "Transparent" 
            "RenderType" = "Transparent" 
            "RenderPipeline" = "UniversalPipeline"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            Name "SpriteOutline"
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex   : POSITION;
                float2 uv       : TEXCOORD0;
                float4 color    : COLOR;
            };

            struct v2f
            {
                float4 pos      : SV_POSITION;
                float2 uv       : TEXCOORD0;
                float4 color    : COLOR;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_TexelSize;
            
            float4 _OutlineColor;
            float _OutlineWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv) * i.color;

                if (texColor.a < 0.1)
                {
                    float w = _OutlineWidth * _MainTex_TexelSize.x;
                    float h = _OutlineWidth * _MainTex_TexelSize.y;

                    float alphaSum = 0;
                    alphaSum += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv + float2(w, 0)).a;
                    alphaSum += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv + float2(-w, 0)).a;
                    alphaSum += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv + float2(0, h)).a;
                    alphaSum += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv + float2(0, -h)).a;
                    alphaSum += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv + float2(w, h)).a;
                    alphaSum += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv + float2(-w, -h)).a;
                    alphaSum += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv + float2(-w, h)).a;
                    alphaSum += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv + float2(w, -h)).a;

                    if (alphaSum > 0.05)
                    {
                        return float4(_OutlineColor.rgb, _OutlineColor.a);
                    }
                }

                return texColor;
            }
            ENDHLSL
        }
    }
}