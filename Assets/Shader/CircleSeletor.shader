// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/CircleSeletor"
{
    Properties
    {
        _BoundColor("Bound Color", Color) = (1,1,1,1)
        _BgColor("Background Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _BoundWidth("BoundWidth", float) = 10
        _ComponentWidth("ComponentWidth", float) = 100
    }

    SubShader
    {
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag Lambert alpha
            // make fog work
            #pragma multi_compile_fog
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _BoundWidth;
            fixed4 _BoundColor;
            fixed4 _BgColor;
            float _ComponentWidth;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                    float4 vertex : SV_POSITION;
            };

            float4 _MainTex_ST;
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float antialias(float w, float d, float r) {
                return 1 - (d - r - w / 2) / (2 * w);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 c = tex2D(_MainTex,i.uv);
            float x = i.uv.x;
            float y = i.uv.y;
            float dis = sqrt(pow((0.5 - x), 2) + pow((0.5 - y), 2));
            if (dis > 0.5) {
                c.a = 0;
                discard;
            }
            else {
                float innerRadius = (_ComponentWidth * 0.5 - _BoundWidth) / _ComponentWidth;
                if (dis > innerRadius) {
                    c = _BoundColor;
                    //c.a = c.a*antialias(_BoundWidth, dis, innerRadius);
                }
                else {
                    c = _BgColor;
                }
            }
            return c;
            }

                ENDCG
        }
        GrabPass{
            "_MainTex2"
        }
        Pass
        {
                Blend One zero
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
            };
            struct v2f 
            {
                float4 pos : SV_POSITION;
                fixed4 color : COLOR;
                float4 scrPos : TEXCOORD0;
            };

            float4 _MainTex_ST;
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);    
                o.scrPos = ComputeScreenPos(o.pos);
                o.color = v.color;
                return o;
            }

            sampler2D _MainTex2;
            float4 _MainTex2_TexelSize;

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = (i.scrPos.xy / i.scrPos.w);
                fixed4 c = tex2D(_MainTex2, uv );
                fixed4 up = tex2D(_MainTex2, uv + fixed2(0, _MainTex2_TexelSize.y));
                fixed4 down = tex2D(_MainTex2, uv - fixed2(0, _MainTex2_TexelSize.y));
                fixed4 left = tex2D(_MainTex2, uv - fixed2(_MainTex2_TexelSize.x, 0));
                fixed4 right = tex2D(_MainTex2, uv + fixed2(_MainTex2_TexelSize.x, 0));

                c.rgb = (c.rgb + up.rgb + down.rgb + left.rgb + right.rgb) / 5;
                c.a = (c.a + up.a + down.a + left.a + right.a) / 5;

                return c;
            }
                ENDCG

        }
    }
}