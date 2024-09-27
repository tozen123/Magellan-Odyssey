Shader "Custom/WaterWithCausticOffset"
{ 
    Properties
    {
        _WaterColor("Water Color", Color) = (0.0, 0.5, 1.0, 0.5)
        _WaveSpeed("Wave Speed", Range(0.1, 2.0)) = 0.3
        _WaveHeight("Wave Height", Range(0.1, 1.5)) = 0.1
        _WaveFrequency("Wave Frequency", Range(0.1, 2.0)) = 0.5
        _CausticTex("Caustics Texture", 2D) = "white" {}
        _MinCausticIntensity("Min Caustic Intensity", Range(0.0, 2.0)) = 0.5
        _MaxCausticIntensity("Max Caustic Intensity", Range(0.0, 2.0)) = 1.5
        _CausticAnimationSpeed("Caustic Animation Speed", Range(0.1, 5.0)) = 1.0
    }
        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 200

            CGPROGRAM
            #pragma surface surf Lambert vertex:vert alpha:blend

            fixed4 _WaterColor;
            float _WaveSpeed;
            float _WaveHeight;
            float _WaveFrequency;
            sampler2D _CausticTex;
            float _CausticSpeedX;
            float _CausticSpeedY;
            float _MinCausticIntensity;
            float _MaxCausticIntensity;
            float _CausticAnimationSpeed;

            struct Input
            {
                float2 uv_MainTex;
                float3 worldPos;
                float3 viewDir;
                float2 uv_CausticTex;
            };

            void vert(inout appdata_full v)
            {
                // Calculate wave motion
                float wave = cos((v.vertex.x * _WaveFrequency + v.vertex.z * _WaveFrequency) + _Time.y * _WaveSpeed) * _WaveHeight;
                v.vertex.y += wave;

                // No need to modify caustic offset in vertex shader
            }

            void surf(Input IN, inout SurfaceOutput o)
            {
                o.Albedo = _WaterColor.rgb;
                o.Alpha = _WaterColor.a;

                // Calculate animated caustic intensity
                float causticIntensity = lerp(_MinCausticIntensity, _MaxCausticIntensity, 0.5 * (1.0 + sin(_Time.y * _CausticAnimationSpeed)));

                // Adjust caustic texture coordinates with both X and Y speed
                float2 causticUV = IN.uv_CausticTex ;

                // Sample caustic texture with modified UV coordinates
                float4 caustic = tex2D(_CausticTex, causticUV * _WaveFrequency);
                caustic *= causticIntensity;
                o.Albedo += caustic.rgb;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
