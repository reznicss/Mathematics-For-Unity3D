Shader "Custom/ToonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _OutlineWidth ("Outline Width", Range(0.01, 1)) = 0.01
        _OutLineColor ("OutLine Color", Color) = (0.5,0.5,0.5,1)

        _RampStart ("交界起始 RampStart", Range(0.1, 1)) = 0.3
        _RampSize ("交界大小 RampSize", Range(0, 1)) = 0.1
        [IntRange] _RampStep("交界段数 RampStep", Range(1,10)) = 1
        _RampSmooth ("交界柔和度 RampSmooth", Range(0.01, 1)) = 0.1
        _DarkColor ("暗面 DarkColor", Color) = (0.4, 0.4, 0.4, 1)
        _LightColor ("亮面 LightColor", Color) = (0.8, 0.8, 0.8, 1)

        _SpecPow("SpecPow 光泽度", Range(0, 1)) = 0.1
        _SpecularColor ("SpecularColor 高光", Color) = (1.0, 1.0, 1.0, 1)
        _SpecIntensity("SpecIntensity 高光强度", Range(0, 1)) = 0
        _SpecSmooth("SpecSmooth 高光柔和度", Range(0, 0.5)) = 0.1

        _RimColor ("RimColor 边缘光", Color) = (1.0, 1.0, 1.0, 1)
        _RimThreshold("RimThreshold 边缘光阈值", Range(0, 1)) = 0.45
        _RimSmooth("RimSmooth 边缘光柔和度", Range(0, 0.5)) = 0.1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal: NORMAL; // 计算光照需要用到模型法线
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                // 计算光照需要用到法线和世界位置
                float3 worldNormal: TEXCOORD1;
                float3 worldPos:TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _RampStart;
            float _RampSize;
            float _RampStep;
            float _RampSmooth;
            float3 _DarkColor;
            float3 _LightColor;

            float _SpecPow;
            float3 _SpecularColor;
            float _SpecIntensity;
            float _SpecSmooth;

            float3 _RimColor;
            float _RimThreshold;
            float _RimSmooth;

            float linearstep(float min, float max, float t)
            {
                return saturate((t - min) / (max - min));
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                // 向下传输这些数据
                o.worldNormal = normalize(UnityObjectToWorldNormal(v.normal));
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                //------------------------ 漫反射 ------------------------
                // 得到顶点法线
                float3 normal = normalize(i.worldNormal);
                // 得到光照方向
                float3 worldLightDir = UnityWorldSpaceLightDir(i.worldPos);
                // NoL代表表面接受的能量大小
                float NoL = dot(i.worldNormal, worldLightDir);
                // 计算half-lambert亮度值
                float halfLambert = NoL * 0.5 + 0.5;

                //------------------------ 高光 ------------------------
                // 得到视向量
                float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
                // 计算half向量, 使用Blinn-phone计算高光
                float3 halfDir = normalize(viewDir + worldLightDir);
                // 计算NoH用于计算高光
                float NoH = dot(normal, halfDir);
                // 计算高光亮度值
                float blinnPhone = pow(max(0, NoH), _SpecPow * 128.0);
                // 计算高光色彩
                float3 specularColor = smoothstep(0.7 - _SpecSmooth / 2, 0.7 + _SpecSmooth / 2, blinnPhone)
                    * _SpecularColor * _SpecIntensity;

                //------------------------ 边缘光 ------------------------
                // 计算NoV用于计算边缘光
                float NoV = dot(i.worldNormal, viewDir);
                // 计算边缘光亮度值
                float rim = (1 - max(0, NoV)) * NoL;
                // 计算边缘光颜色
                float3 rimColor = smoothstep(_RimThreshold - _RimSmooth / 2, _RimThreshold + _RimSmooth / 2, rim) *
                    _RimColor;

                //------------------------ 色阶 ------------------------
                // 通过亮度值计算线性ramp
                float ramp = linearstep(_RampStart, _RampStart + _RampSize, halfLambert);
                float step = ramp * _RampStep; // 使每个色阶大小为1, 方便计算
                float gridStep = floor(step); // 得到当前所处的色阶
                float smoothStep = smoothstep(gridStep, gridStep + _RampSmooth, step) + gridStep;
                ramp = smoothStep / _RampStep; // 回到原来的空间
                // 得到最终的ramp色彩
                float3 rampColor = lerp(_DarkColor, _LightColor, ramp);
                rampColor *= col;

                // 混合颜色
                float3 finalColor = saturate(rampColor + specularColor + rimColor);
                return float4(finalColor, 1);
            }
            ENDCG
        }

        Pass
        {
            Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                // 法线
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            // 线条宽度
            float _OutlineWidth;
            // 线条颜色
            float4 _OutLineColor;

            v2f vert(appdata v)
            {
                v2f o;
                float4 newVertex = float4(v.vertex.xyz + normalize(v.normal) * _OutlineWidth * 0.05, 1);
                o.vertex = UnityObjectToClipPos(newVertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _OutLineColor;
            }
            ENDCG
        }
    }
    fallback"Diffuse"
}