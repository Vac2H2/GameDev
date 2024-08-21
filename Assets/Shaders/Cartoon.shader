Shader "Custom/ToonShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _ShadingSteps ("Shading Steps", Range(1, 5)) = 3
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        float _ShadingSteps;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 viewDir;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            float intensity = dot(normalize(IN.worldNormal), normalize(IN.viewDir));
            
            // Quantize the intensity to create distinct shading bands
            intensity = floor(intensity * _ShadingSteps) / _ShadingSteps;
            intensity = saturate(intensity); // Ensure it stays within [0, 1]

            o.Albedo = c.rgb * intensity;
            o.Alpha = c.a;
        }
        ENDCG
    }

    FallBack "Diffuse"
}
