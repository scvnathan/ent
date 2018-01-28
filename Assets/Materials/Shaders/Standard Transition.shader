// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Standard Transition"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_MainAlbedo("Main Albedo", 2D) = "white" {}
		_MainNormal("Main Normal", 2D) = "white" {}
		_MainRough("Main Rough", 2D) = "white" {}
		_TransitionValue("TransitionValue", Float) = 0
		_Alpha("Alpha", 2D) = "white" {}
		_NormalIntensity("Normal Intensity", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _MainNormal;
		uniform float4 _MainNormal_ST;
		uniform float _NormalIntensity;
		uniform sampler2D _MainAlbedo;
		uniform float4 _MainAlbedo_ST;
		uniform sampler2D _MainRough;
		uniform float4 _MainRough_ST;
		uniform sampler2D _Alpha;
		uniform float4 _Alpha_ST;
		uniform float _TransitionValue;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainNormal = i.uv_texcoord * _MainNormal_ST.xy + _MainNormal_ST.zw;
			o.Normal = saturate( ( tex2D( _MainNormal, uv_MainNormal ) * _NormalIntensity ) ).rgb;
			float2 uv_MainAlbedo = i.uv_texcoord * _MainAlbedo_ST.xy + _MainAlbedo_ST.zw;
			o.Albedo = tex2D( _MainAlbedo, uv_MainAlbedo ).rgb;
			float2 uv_MainRough = i.uv_texcoord * _MainRough_ST.xy + _MainRough_ST.zw;
			o.Smoothness = tex2D( _MainRough, uv_MainRough ).r;
			o.Alpha = 1;
			float2 uv_Alpha = i.uv_texcoord * _Alpha_ST.xy + _Alpha_ST.zw;
			clip( ( tex2D( _Alpha, uv_Alpha ) * _TransitionValue ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14301
29;148;1474;750;1584.789;562.1189;1.55165;True;True
Node;AmplifyShaderEditor.SamplerNode;5;-776.8762,44.02144;Float;True;Property;_MainNormal;Main Normal;2;0;Create;True;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;16;-526.0376,-138.9404;Float;False;Property;_NormalIntensity;Normal Intensity;6;0;Create;True;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-228.0376,-166.9404;Float;False;2;2;0;COLOR;0.0;False;1;FLOAT;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;12;-1616.562,-205.2542;Float;True;Property;_Alpha;Alpha;5;0;Create;True;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-1450.645,170.6451;Float;False;Property;_TransitionValue;TransitionValue;4;0;Create;True;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;17;-88.0376,-98.94043;Float;False;1;0;COLOR;0.0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;2;-771.1709,-503.2534;Float;True;Property;_MainAlbedo;Main Albedo;1;0;Create;True;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-746.6011,575.8208;Float;True;Property;_MainRough;Main Rough;3;0;Create;True;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-1253.604,-54.45908;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;143,-174;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Standard Transition;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;False;0;Custom;0.5;True;True;0;False;Transparent;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;15;0;5;0
WireConnection;15;1;16;0
WireConnection;17;0;15;0
WireConnection;11;0;12;0
WireConnection;11;1;4;0
WireConnection;0;0;2;0
WireConnection;0;1;17;0
WireConnection;0;4;8;0
WireConnection;0;10;11;0
ASEEND*/
//CHKSM=82091D6A1ECB70923F4782F6FDE59BAA37B4860D