// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:39061,y:33483,varname:node_2865,prsc:2|diff-791-OUT,spec-256-OUT,gloss-4550-OUT,normal-571-OUT,alpha-9785-OUT,disp-6047-OUT,tess-9182-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1689,x:29524,y:33755,ptovrint:False,ptlb:Sway Frequency,ptin:_SwayFrequency,varname:_SwayFrequency,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tau,id:7476,x:29524,y:33828,varname:node_7476,prsc:2;n:type:ShaderForge.SFN_Time,id:7573,x:29524,y:33947,varname:node_7573,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1410,x:29745,y:33937,varname:node_1410,prsc:2|A-1689-OUT,B-7476-OUT,C-7573-T;n:type:ShaderForge.SFN_Slider,id:4693,x:29721,y:33770,ptovrint:False,ptlb:Sway Intensity,ptin:_SwayIntensity,varname:_SwayIntensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Sin,id:7783,x:29931,y:33937,varname:node_7783,prsc:2|IN-1410-OUT;n:type:ShaderForge.SFN_Multiply,id:120,x:30134,y:33937,varname:node_120,prsc:2|A-4693-OUT,B-7783-OUT;n:type:ShaderForge.SFN_Add,id:6160,x:30315,y:33937,varname:node_6160,prsc:2|A-120-OUT,B-7573-T;n:type:ShaderForge.SFN_ObjectScale,id:2375,x:29828,y:34745,varname:node_2375,prsc:2,rcp:False;n:type:ShaderForge.SFN_Append,id:5826,x:30010,y:34755,varname:node_5826,prsc:2|A-2375-X,B-2375-Z;n:type:ShaderForge.SFN_Vector1,id:438,x:30010,y:34653,varname:node_438,prsc:2,v1:1;n:type:ShaderForge.SFN_SwitchProperty,id:1365,x:30309,y:34808,ptovrint:False,ptlb:Toggl Flow With Obj,ptin:_TogglFlowWithObj,varname:_TogglFlowWithObj,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-438-OUT,B-5826-OUT;n:type:ShaderForge.SFN_Slider,id:6916,x:30267,y:34401,ptovrint:False,ptlb:Flow Movement Horizontal,ptin:_FlowMovementHorizontal,varname:_FlowMovementHorizontal,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:1;n:type:ShaderForge.SFN_Slider,id:5226,x:30267,y:34527,ptovrint:False,ptlb:Flow Movement Vertical,ptin:_FlowMovementVertical,varname:_FlowMovementVertical,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:1;n:type:ShaderForge.SFN_Multiply,id:6584,x:30658,y:34457,varname:node_6584,prsc:2|A-6160-OUT,B-5226-OUT;n:type:ShaderForge.SFN_Multiply,id:4480,x:30876,y:34457,varname:node_4480,prsc:2|A-6160-OUT,B-6916-OUT;n:type:ShaderForge.SFN_TexCoord,id:7765,x:30309,y:34626,varname:node_7765,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:150,x:30658,y:34644,varname:node_150,prsc:2|A-7765-UVOUT,B-1365-OUT;n:type:ShaderForge.SFN_Panner,id:9794,x:30876,y:34644,varname:node_9794,prsc:2,spu:0,spv:1|UVIN-150-OUT,DIST-6584-OUT;n:type:ShaderForge.SFN_Panner,id:8157,x:31096,y:34644,varname:scrollingFlowUV,prsc:2,spu:1,spv:0|UVIN-9794-UVOUT,DIST-4480-OUT;n:type:ShaderForge.SFN_Tex2d,id:8395,x:31314,y:34644,ptovrint:False,ptlb:FlowMap,ptin:_FlowMap,varname:_FlowMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8157-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:316,x:31624,y:34644,varname:node_316,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-8395-RGB;n:type:ShaderForge.SFN_ValueProperty,id:8007,x:31603,y:35045,ptovrint:False,ptlb:Flow Speed,ptin:_FlowSpeed,varname:_FlowSpeed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:5502,x:31788,y:35032,varname:flowWithSpeed,prsc:2|A-6160-OUT,B-8007-OUT;n:type:ShaderForge.SFN_Vector1,id:5891,x:31788,y:35199,varname:node_5891,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:1186,x:31977,y:35089,varname:node_1186,prsc:2|A-5502-OUT,B-5891-OUT;n:type:ShaderForge.SFN_Vector1,id:4754,x:31920,y:34931,varname:node_4754,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:2710,x:32103,y:34647,varname:flowIntensity,prsc:2|A-4672-OUT,B-1204-OUT,C-4754-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1204,x:31920,y:34859,ptovrint:False,ptlb:Flow Power,ptin:_FlowPower,varname:_FlowPower,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Frac,id:3700,x:32234,y:34849,varname:node_3700,prsc:2|IN-1186-OUT;n:type:ShaderForge.SFN_Frac,id:6319,x:32234,y:34993,varname:FlowFrac,prsc:2|IN-5502-OUT;n:type:ShaderForge.SFN_Vector1,id:8315,x:32234,y:35169,varname:pointFive,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:3672,x:32467,y:34683,varname:node_3672,prsc:2|A-2710-OUT,B-6319-OUT;n:type:ShaderForge.SFN_Multiply,id:5270,x:32467,y:34809,varname:node_5270,prsc:2|A-2710-OUT,B-3700-OUT;n:type:ShaderForge.SFN_Subtract,id:2969,x:32467,y:34993,varname:node_2969,prsc:2|A-6319-OUT,B-8315-OUT;n:type:ShaderForge.SFN_Divide,id:5081,x:32648,y:34993,varname:node_5081,prsc:2|A-2969-OUT,B-8315-OUT;n:type:ShaderForge.SFN_ObjectScale,id:5371,x:32048,y:34307,varname:node_5371,prsc:2,rcp:False;n:type:ShaderForge.SFN_Append,id:5223,x:32251,y:34350,varname:node_5223,prsc:2|A-5371-X,B-5371-Z;n:type:ShaderForge.SFN_TexCoord,id:1797,x:32251,y:34485,varname:node_1797,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:3932,x:32467,y:34516,varname:scaledNormalUV,prsc:2|A-5223-OUT,B-1797-UVOUT;n:type:ShaderForge.SFN_Add,id:9508,x:32655,y:34589,varname:node_9508,prsc:2|A-3932-OUT,B-3672-OUT;n:type:ShaderForge.SFN_Add,id:1364,x:32655,y:34757,varname:node_1364,prsc:2|A-3932-OUT,B-5270-OUT;n:type:ShaderForge.SFN_Abs,id:2909,x:32825,y:34993,varname:node_2909,prsc:2|IN-5081-OUT;n:type:ShaderForge.SFN_Lerp,id:3362,x:33016,y:34796,varname:node_3362,prsc:2|A-9508-OUT,B-1364-OUT,T-2909-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5707,x:33005,y:34297,ptovrint:False,ptlb:NormalTilingDifference,ptin:_NormalTilingDifference,varname:_NormalTilingDifference,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1.4;n:type:ShaderForge.SFN_Multiply,id:2665,x:33284,y:34407,varname:node_2665,prsc:2|A-5707-OUT,B-3362-OUT;n:type:ShaderForge.SFN_Slider,id:4668,x:31251,y:32960,ptovrint:False,ptlb:Wave 1 X-Direction,ptin:_Wave1XDirection,varname:_Wave1XDirection,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:3081,x:31251,y:33094,ptovrint:False,ptlb:Wave 1 Z-Direction,ptin:_Wave1ZDirection,varname:_Wave1ZDirection,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:4347,x:31249,y:33214,ptovrint:False,ptlb:Wave 2 X-Direction,ptin:_Wave2XDirection,varname:_Wave2XDirection,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:9061,x:31249,y:33329,ptovrint:False,ptlb:Wave 2 Z-Direction,ptin:_Wave2ZDirection,varname:_Wave2ZDirection,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:161,x:31251,y:33458,ptovrint:False,ptlb:Wave 3 X-Direction,ptin:_Wave3XDirection,varname:_Wave3XDirection,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:3108,x:31251,y:33572,ptovrint:False,ptlb:Wave 3 Z-Direction,ptin:_Wave3ZDirection,varname:_Wave3ZDirection,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Append,id:9506,x:31642,y:33024,varname:node_9506,prsc:2|A-4668-OUT,B-3081-OUT;n:type:ShaderForge.SFN_Append,id:3832,x:31642,y:33259,varname:node_3832,prsc:2|A-4347-OUT,B-9061-OUT;n:type:ShaderForge.SFN_Append,id:2477,x:31642,y:33508,varname:node_2477,prsc:2|A-161-OUT,B-3108-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:2400,x:31242,y:33728,varname:node_2400,prsc:2;n:type:ShaderForge.SFN_Normalize,id:1206,x:31489,y:33803,varname:node_1206,prsc:2|IN-2400-XYZ;n:type:ShaderForge.SFN_ComponentMask,id:7274,x:31633,y:33699,varname:node_7274,prsc:2,cc1:0,cc2:2,cc3:-1,cc4:-1|IN-2400-XYZ;n:type:ShaderForge.SFN_Relay,id:76,x:31536,y:34157,varname:node_76,prsc:2|IN-6160-OUT;n:type:ShaderForge.SFN_Dot,id:8152,x:31956,y:33512,varname:node_8152,prsc:2,dt:0|A-2477-OUT,B-7274-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5295,x:31797,y:33358,ptovrint:False,ptlb:Wave Speed,ptin:_WaveSpeed,varname:_WaveSpeed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:6812,x:31797,y:33190,ptovrint:False,ptlb:Wave Spread,ptin:_WaveSpread,varname:_WaveSpread,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:4015,x:31797,y:32945,ptovrint:False,ptlb:Wave Height,ptin:_WaveHeight,varname:_WaveHeight,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Dot,id:8068,x:31956,y:33268,varname:node_8068,prsc:2,dt:0|A-3832-OUT,B-7274-OUT;n:type:ShaderForge.SFN_Dot,id:1177,x:31956,y:33019,varname:node_1177,prsc:2,dt:0|A-9506-OUT,B-7274-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3565,x:31797,y:32846,ptovrint:False,ptlb:Wave Sharpness,ptin:_WaveSharpness,varname:_WaveSharpness,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Code,id:7937,x:32301,y:32866,varname:node_7937,prsc:2,code:ZgBsAG8AYQB0ACAAcABoAGEAcwBlAEMAbwBuAHMAdAAgAD0AIABTAHAAZQBlAGQAIAAqACAAVwBhAHYAZQBsAGUAbgBnAHQAaAA7AAoAZgBsAG8AYQB0ACAAeABWAGEAbAAgAD0AIABTAHQAZQBlAHAAbgBlAHMAcwAgACoAIABBAG0AcABsAGkAdAB1AGQAZQAgACoAIABEAGkAcgBlAGMAdABpAG8AbgBWAGUAYwAuAHgAIAAqACAAYwBvAHMAKABXAGEAdgBlAGwAZQBuAGcAdABoACAAKgAgAEQAbwB0AFAAcgBvAGQAIAArACAAVABpAG0AZQAgACoAIABwAGgAYQBzAGUAQwBvAG4AcwB0ACkAOwAKAGYAbABvAGEAdAAgAHkAVgBhAGwAIAA9ACAAQQBtAHAAbABpAHQAdQBkAGUAIAAqACAAcwBpAG4AKABXAGEAdgBlAGwAZQBuAGcAdABoACAAKwAgAFQAaQBtAGUAIAAqACAAcABoAGEAcwBlAEMAbwBuAHMAdAApADsACgBmAGwAbwBhAHQAIAB6AFYAYQBsACAAPQAgAFMAdABlAGUAcABuAGUAcwBzACAAKgAgAEEAbQBwAGwAaQB0AHUAZABlACAAKgAgAEQAaQByAGUAYwB0AGkAbwBuAFYAZQBjAC4AeQAgACoAIABjAG8AcwAoAFcAYQB2AGUAbABlAG4AZwB0AGgAIAAqACAARABvAHQAUAByAG8AZAAgACsAIABUAGkAbQBlACAAKgAgAHAAaABhAHMAZQBDAG8AbgBzAHQAKQA7AAoAcgBlAHQAdQByAG4AIABmAGwAbwBhAHQAMwAoAHgAVgBhAGwALAAgAHkAVgBhAGwALAAgAHoAVgBhAGwAKQA7AA==,output:2,fname:Gerstner,width:743,height:188,input:0,input:0,input:0,input:0,input:0,input:0,input:1,input_1_label:Steepness,input_2_label:Amplitude,input_3_label:Wavelength,input_4_label:Speed,input_5_label:Time,input_6_label:DotProd,input_7_label:DirectionVec|A-3565-OUT,B-4015-OUT,C-6812-OUT,D-5295-OUT,E-76-OUT,F-1177-OUT,G-9506-OUT;n:type:ShaderForge.SFN_Code,id:607,x:32300,y:33229,varname:node_607,prsc:2,code:ZgBsAG8AYQB0ACAAcABoAGEAcwBlAEMAbwBuAHMAdAAgAD0AIABTAHAAZQBlAGQAIAAqACAAVwBhAHYAZQBsAGUAbgBnAHQAaAA7AAoAZgBsAG8AYQB0ACAAeABWAGEAbAAgAD0AIABTAHQAZQBlAHAAbgBlAHMAcwAgACoAIABBAG0AcABsAGkAdAB1AGQAZQAgACoAIABEAGkAcgBlAGMAdABpAG8AbgBWAGUAYwAuAHgAIAAqACAAYwBvAHMAKABXAGEAdgBlAGwAZQBuAGcAdABoACAAKgAgAEQAbwB0AFAAcgBvAGQAIAArACAAVABpAG0AZQAgACoAIABwAGgAYQBzAGUAQwBvAG4AcwB0ACkAOwAKAGYAbABvAGEAdAAgAHkAVgBhAGwAIAA9ACAAQQBtAHAAbABpAHQAdQBkAGUAIAAqACAAcwBpAG4AKABXAGEAdgBlAGwAZQBuAGcAdABoACAAKwAgAFQAaQBtAGUAIAAqACAAcABoAGEAcwBlAEMAbwBuAHMAdAApADsACgBmAGwAbwBhAHQAIAB6AFYAYQBsACAAPQAgAFMAdABlAGUAcABuAGUAcwBzACAAKgAgAEEAbQBwAGwAaQB0AHUAZABlACAAKgAgAEQAaQByAGUAYwB0AGkAbwBuAFYAZQBjAC4AeQAgACoAIABjAG8AcwAoAFcAYQB2AGUAbABlAG4AZwB0AGgAIAAqACAARABvAHQAUAByAG8AZAAgACsAIABUAGkAbQBlACAAKgAgAHAAaABhAHMAZQBDAG8AbgBzAHQAKQA7AAoAcgBlAHQAdQByAG4AIABmAGwAbwBhAHQAMwAoAHgAVgBhAGwALAAgAHkAVgBhAGwALAAgAHoAVgBhAGwAKQA7AA==,output:2,fname:Gerstner2,width:743,height:188,input:0,input:0,input:0,input:0,input:0,input:0,input:1,input_1_label:Steepness,input_2_label:Amplitude,input_3_label:Wavelength,input_4_label:Speed,input_5_label:Time,input_6_label:DotProd,input_7_label:DirectionVec|A-3565-OUT,B-4015-OUT,C-6812-OUT,D-5295-OUT,E-3531-OUT,F-8068-OUT,G-3832-OUT;n:type:ShaderForge.SFN_Code,id:4592,x:32308,y:33525,varname:node_4592,prsc:2,code:ZgBsAG8AYQB0ACAAcABoAGEAcwBlAEMAbwBuAHMAdAAgAD0AIABTAHAAZQBlAGQAIAAqACAAVwBhAHYAZQBsAGUAbgBnAHQAaAA7AAoAZgBsAG8AYQB0ACAAeABWAGEAbAAgAD0AIABTAHQAZQBlAHAAbgBlAHMAcwAgACoAIABBAG0AcABsAGkAdAB1AGQAZQAgACoAIABEAGkAcgBlAGMAdABpAG8AbgBWAGUAYwAuAHgAIAAqACAAYwBvAHMAKABXAGEAdgBlAGwAZQBuAGcAdABoACAAKgAgAEQAbwB0AFAAcgBvAGQAIAArACAAVABpAG0AZQAgACoAIABwAGgAYQBzAGUAQwBvAG4AcwB0ACkAOwAKAGYAbABvAGEAdAAgAHkAVgBhAGwAIAA9ACAAQQBtAHAAbABpAHQAdQBkAGUAIAAqACAAcwBpAG4AKABXAGEAdgBlAGwAZQBuAGcAdABoACAAKwAgAFQAaQBtAGUAIAAqACAAcABoAGEAcwBlAEMAbwBuAHMAdAApADsACgBmAGwAbwBhAHQAIAB6AFYAYQBsACAAPQAgAFMAdABlAGUAcABuAGUAcwBzACAAKgAgAEEAbQBwAGwAaQB0AHUAZABlACAAKgAgAEQAaQByAGUAYwB0AGkAbwBuAFYAZQBjAC4AeQAgACoAIABjAG8AcwAoAFcAYQB2AGUAbABlAG4AZwB0AGgAIAAqACAARABvAHQAUAByAG8AZAAgACsAIABUAGkAbQBlACAAKgAgAHAAaABhAHMAZQBDAG8AbgBzAHQAKQA7AAoAcgBlAHQAdQByAG4AIABmAGwAbwBhAHQAMwAoAHgAVgBhAGwALAAgAHkAVgBhAGwALAAgAHoAVgBhAGwAKQA7AA==,output:2,fname:Gerstner3,width:743,height:188,input:0,input:0,input:0,input:0,input:0,input:0,input:1,input_1_label:Steepness,input_2_label:Amplitude,input_3_label:Wavelength,input_4_label:Speed,input_5_label:Time,input_6_label:DotProd,input_7_label:DirectionVec|A-3565-OUT,B-4015-OUT,C-6812-OUT,D-5295-OUT,E-6260-OUT,F-8152-OUT,G-2477-OUT;n:type:ShaderForge.SFN_Add,id:3531,x:32030,y:33844,varname:node_3531,prsc:2|A-76-OUT,B-6022-OUT;n:type:ShaderForge.SFN_Add,id:6260,x:32018,y:34065,varname:node_6260,prsc:2|A-76-OUT,B-5225-OUT;n:type:ShaderForge.SFN_Vector1,id:6022,x:31864,y:33923,varname:node_6022,prsc:2,v1:2;n:type:ShaderForge.SFN_Vector1,id:5225,x:31792,y:34122,varname:node_5225,prsc:2,v1:4;n:type:ShaderForge.SFN_Relay,id:6554,x:32954,y:33958,varname:node_6554,prsc:2|IN-76-OUT;n:type:ShaderForge.SFN_Slider,id:1660,x:33724,y:34510,ptovrint:False,ptlb:Normal 1 Horizontal Speed,ptin:_Normal1HorizontalSpeed,varname:node_1660,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:418,x:33726,y:34636,ptovrint:False,ptlb:Normal 1 Vertical Speed,ptin:_Normal1VerticalSpeed,varname:_Normal1HorizontalSpeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_ValueProperty,id:5581,x:33612,y:34758,ptovrint:False,ptlb:NormalScrollSpeed,ptin:_NormalScrollSpeed,varname:node_5581,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:3963,x:33848,y:34737,varname:node_3963,prsc:2|A-6554-OUT,B-5581-OUT;n:type:ShaderForge.SFN_Slider,id:5321,x:33728,y:35028,ptovrint:False,ptlb:Normal 2 Vertical Speed,ptin:_Normal2VerticalSpeed,varname:_Normal1VerticalSpeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:7248,x:34132,y:34486,varname:node_7248,prsc:2|A-1660-OUT,B-3963-OUT;n:type:ShaderForge.SFN_Multiply,id:2960,x:34136,y:34615,varname:node_2960,prsc:2|A-418-OUT,B-3963-OUT;n:type:ShaderForge.SFN_Multiply,id:3467,x:34138,y:34906,varname:node_3467,prsc:2|A-3963-OUT,B-7035-OUT;n:type:ShaderForge.SFN_Multiply,id:6499,x:34139,y:35085,varname:node_6499,prsc:2|A-3963-OUT,B-5321-OUT;n:type:ShaderForge.SFN_Panner,id:1423,x:34369,y:34605,varname:node_1423,prsc:2,spu:1,spv:0|UVIN-2665-OUT,DIST-2960-OUT;n:type:ShaderForge.SFN_Panner,id:3028,x:34585,y:34605,varname:node_3028,prsc:2,spu:0,spv:1|UVIN-1423-UVOUT,DIST-7248-OUT;n:type:ShaderForge.SFN_Panner,id:7195,x:34382,y:35053,varname:node_7195,prsc:2,spu:1,spv:0|UVIN-3362-OUT,DIST-6499-OUT;n:type:ShaderForge.SFN_Panner,id:1135,x:34585,y:35053,varname:node_1135,prsc:2,spu:0,spv:1|UVIN-7195-UVOUT,DIST-3467-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:6686,x:34585,y:34825,ptovrint:False,ptlb:Normal Map,ptin:_NormalMap,varname:node_6686,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:7777,x:34834,y:34717,varname:node_7777,prsc:2,ntxv:0,isnm:False|UVIN-3028-UVOUT,TEX-6686-TEX;n:type:ShaderForge.SFN_Blend,id:571,x:35056,y:34826,varname:node_571,prsc:2,blmd:5,clmp:True|SRC-7777-RGB,DST-146-RGB;n:type:ShaderForge.SFN_ComponentMask,id:1421,x:35326,y:34686,varname:node_1421,prsc:2,cc1:2,cc2:-1,cc3:-1,cc4:-1|IN-571-OUT;n:type:ShaderForge.SFN_Relay,id:9879,x:32842,y:32672,varname:node_9879,prsc:2|IN-4015-OUT;n:type:ShaderForge.SFN_Relay,id:3786,x:33749,y:33117,varname:node_3786,prsc:2|IN-9879-OUT;n:type:ShaderForge.SFN_Add,id:8700,x:33720,y:33326,varname:node_8700,prsc:2|A-7937-OUT,B-607-OUT,C-4592-OUT;n:type:ShaderForge.SFN_Vector1,id:3220,x:33720,y:33200,varname:node_3220,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Multiply,id:1095,x:33926,y:33132,varname:node_1095,prsc:2|A-3786-OUT,B-3220-OUT;n:type:ShaderForge.SFN_Divide,id:4398,x:33930,y:33427,varname:node_4398,prsc:2|A-8700-OUT,B-9617-OUT;n:type:ShaderForge.SFN_Vector1,id:9617,x:33720,y:33495,varname:node_9617,prsc:2,v1:2;n:type:ShaderForge.SFN_Negate,id:2650,x:34132,y:33198,varname:node_2650,prsc:2|IN-1095-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:6361,x:34397,y:33203,varname:node_6361,prsc:2|IN-4398-OUT,IMIN-2650-OUT,IMAX-1095-OUT,OMIN-3402-OUT,OMAX-121-OUT;n:type:ShaderForge.SFN_RemapRange,id:4672,x:31920,y:34647,varname:node_4672,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-316-OUT;n:type:ShaderForge.SFN_Vector1,id:3402,x:34132,y:33363,varname:node_3402,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:121,x:34132,y:33427,varname:node_121,prsc:2,v1:1;n:type:ShaderForge.SFN_ComponentMask,id:2511,x:34602,y:33212,varname:node_2511,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-6361-OUT;n:type:ShaderForge.SFN_ViewPosition,id:6663,x:34035,y:33915,varname:node_6663,prsc:2;n:type:ShaderForge.SFN_FragmentPosition,id:3513,x:34035,y:33788,varname:node_3513,prsc:2;n:type:ShaderForge.SFN_Distance,id:2357,x:34256,y:33803,varname:node_2357,prsc:2|A-3513-XYZ,B-6663-XYZ;n:type:ShaderForge.SFN_ValueProperty,id:5314,x:34256,y:33726,ptovrint:False,ptlb:Tesselation Max,ptin:_TesselationMax,varname:node_5314,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_ValueProperty,id:5899,x:34256,y:33636,ptovrint:False,ptlb:Tesselation Min,ptin:_TesselationMin,varname:_TesselationMax_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:4577,x:34559,y:33909,ptovrint:False,ptlb:Far Tesselation Amount,ptin:_FarTesselationAmount,varname:node_4577,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:8495,x:34559,y:34005,ptovrint:False,ptlb:Far Tesselation Curve,ptin:_FarTesselationCurve,varname:node_8495,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:50;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:3519,x:34831,y:33467,varname:node_3519,prsc:2|IN-2511-OUT,IMIN-3402-OUT,IMAX-121-OUT,OMIN-5899-OUT,OMAX-5314-OUT;n:type:ShaderForge.SFN_If,id:9067,x:35081,y:33589,varname:node_9067,prsc:2|A-2357-OUT,B-8495-OUT,GT-4577-OUT,EQ-4577-OUT,LT-3519-OUT;n:type:ShaderForge.SFN_Relay,id:3792,x:36017,y:33588,varname:node_3792,prsc:2|IN-571-OUT;n:type:ShaderForge.SFN_Time,id:948,x:32661,y:32234,varname:node_948,prsc:2;n:type:ShaderForge.SFN_Slider,id:4445,x:32504,y:32397,ptovrint:False,ptlb:Foam Time Speed,ptin:_FoamTimeSpeed,varname:node_4445,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.009579911,max:1;n:type:ShaderForge.SFN_Multiply,id:7649,x:32853,y:32276,varname:node_7649,prsc:2|A-948-T,B-4445-OUT;n:type:ShaderForge.SFN_Sin,id:8283,x:33029,y:32276,varname:node_8283,prsc:2|IN-7649-OUT;n:type:ShaderForge.SFN_ValueProperty,id:193,x:33029,y:32187,ptovrint:False,ptlb:Foam Vertical Speed,ptin:_FoamVerticalSpeed,varname:node_193,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:5391,x:33029,y:32447,ptovrint:False,ptlb:Foam Horizontal Speed,ptin:_FoamHorizontalSpeed,varname:node_5391,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:5696,x:33295,y:32359,varname:node_5696,prsc:2|A-8283-OUT,B-5391-OUT;n:type:ShaderForge.SFN_Multiply,id:9800,x:33482,y:32226,varname:node_9800,prsc:2|A-193-OUT,B-8283-OUT;n:type:ShaderForge.SFN_Panner,id:84,x:33514,y:32359,varname:node_84,prsc:2,spu:1,spv:0|UVIN-2665-OUT,DIST-5696-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4173,x:33027,y:32070,ptovrint:False,ptlb:Depth,ptin:_Depth,varname:node_4173,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Slider,id:9511,x:32948,y:31874,ptovrint:False,ptlb:Foam Spread,ptin:_FoamSpread,varname:node_9511,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Reciprocal,id:7148,x:33317,y:31881,varname:node_7148,prsc:2|IN-9511-OUT;n:type:ShaderForge.SFN_Panner,id:2696,x:33705,y:32359,varname:node_2696,prsc:2,spu:1,spv:1|UVIN-84-UVOUT,DIST-9800-OUT;n:type:ShaderForge.SFN_Slider,id:1699,x:33419,y:32139,ptovrint:False,ptlb:Opacity Minimum,ptin:_OpacityMinimum,varname:node_1699,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.25,max:1;n:type:ShaderForge.SFN_Clamp,id:9872,x:33874,y:32175,varname:node_9872,prsc:2|IN-7414-OUT,MIN-1699-OUT,MAX-3883-OUT;n:type:ShaderForge.SFN_Vector1,id:3883,x:33697,y:32268,varname:node_3883,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:8884,x:33548,y:31921,varname:node_8884,prsc:2|A-7148-OUT,B-7414-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2016,x:33548,y:31836,ptovrint:False,ptlb:Foam Falloff,ptin:_FoamFalloff,varname:node_2016,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Power,id:8606,x:33771,y:31862,varname:node_8606,prsc:2|VAL-8884-OUT,EXP-2016-OUT;n:type:ShaderForge.SFN_Clamp01,id:9869,x:33980,y:31862,varname:node_9869,prsc:2|IN-8606-OUT;n:type:ShaderForge.SFN_OneMinus,id:9573,x:34279,y:31735,varname:node_9573,prsc:2|IN-9869-OUT;n:type:ShaderForge.SFN_ObjectScale,id:6377,x:34050,y:31509,varname:node_6377,prsc:2,rcp:False;n:type:ShaderForge.SFN_Add,id:7376,x:34279,y:31509,varname:node_7376,prsc:2|A-6377-X,B-6377-Z;n:type:ShaderForge.SFN_ValueProperty,id:3599,x:34279,y:31422,ptovrint:False,ptlb:Edge-Line Width,ptin:_EdgeLineWidth,varname:node_3599,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:9029,x:34511,y:31509,varname:node_9029,prsc:2|A-3599-OUT,B-7376-OUT,C-9242-OUT;n:type:ShaderForge.SFN_Vector1,id:9242,x:34279,y:31642,varname:node_9242,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:8846,x:34704,y:31509,varname:node_8846,prsc:2|A-9029-OUT,B-9573-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9588,x:34704,y:31681,ptovrint:False,ptlb:FoamOutlineFalloff,ptin:_FoamOutlineFalloff,varname:node_9588,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Power,id:9099,x:34933,y:31509,varname:node_9099,prsc:2|VAL-8846-OUT,EXP-9588-OUT;n:type:ShaderForge.SFN_Add,id:9031,x:35139,y:31509,varname:node_9031,prsc:2|A-9099-OUT,B-1428-OUT;n:type:ShaderForge.SFN_Tex2d,id:9518,x:34068,y:32329,ptovrint:False,ptlb:Foam,ptin:_Foam,varname:node_9518,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2696-UVOUT;n:type:ShaderForge.SFN_Multiply,id:1428,x:34488,y:32172,varname:node_1428,prsc:2|A-9573-OUT,B-9518-RGB;n:type:ShaderForge.SFN_Slider,id:7770,x:34982,y:31720,ptovrint:False,ptlb:Foam Intensity,ptin:_FoamIntensity,varname:node_7770,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:5599,x:35367,y:31649,varname:node_5599,prsc:2|A-9031-OUT,B-7770-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5563,x:35603,y:31772,varname:node_5563,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-5599-OUT;n:type:ShaderForge.SFN_Add,id:3713,x:35792,y:31916,varname:node_3713,prsc:2|A-5563-OUT,B-9872-OUT;n:type:ShaderForge.SFN_Relay,id:9785,x:36534,y:32125,varname:node_9785,prsc:2|IN-3713-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1739,x:34725,y:32297,ptovrint:False,ptlb:Whitecap Scroll Speed Horizontal,ptin:_WhitecapScrollSpeedHorizontal,varname:node_1739,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:5211,x:34725,y:32418,ptovrint:False,ptlb:Whitecap Scroll Speed Vertical,ptin:_WhitecapScrollSpeedVertical,varname:node_5211,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:7589,x:34930,y:32351,varname:node_7589,prsc:2|A-1739-OUT,B-5211-OUT;n:type:ShaderForge.SFN_Multiply,id:4323,x:35147,y:32351,varname:node_4323,prsc:2|A-7589-OUT,B-7880-OUT;n:type:ShaderForge.SFN_Relay,id:7880,x:34097,y:32511,varname:node_7880,prsc:2|IN-6554-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:8022,x:34497,y:32821,varname:node_8022,prsc:2;n:type:ShaderForge.SFN_ViewPosition,id:4441,x:34497,y:32952,varname:node_4441,prsc:2;n:type:ShaderForge.SFN_Distance,id:6589,x:34724,y:32888,varname:node_6589,prsc:2|A-8022-XYZ,B-4441-XYZ;n:type:ShaderForge.SFN_ValueProperty,id:268,x:34724,y:33045,ptovrint:False,ptlb:Foam Transition Distance,ptin:_FoamTransitionDistance,varname:node_268,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:100;n:type:ShaderForge.SFN_Subtract,id:958,x:34895,y:32888,varname:node_958,prsc:2|A-6589-OUT,B-268-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7341,x:34895,y:33078,ptovrint:False,ptlb:Foam Transition Fade,ptin:_FoamTransitionFade,varname:node_7341,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:25;n:type:ShaderForge.SFN_Divide,id:3825,x:35060,y:32888,varname:node_3825,prsc:2|A-958-OUT,B-7341-OUT;n:type:ShaderForge.SFN_Clamp01,id:3856,x:35228,y:32888,varname:node_3856,prsc:2|IN-3825-OUT;n:type:ShaderForge.SFN_TexCoord,id:6261,x:35039,y:32663,varname:node_6261,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:2952,x:35367,y:32681,varname:node_2952,prsc:2|A-4323-OUT,B-6261-UVOUT;n:type:ShaderForge.SFN_Tex2dAsset,id:8751,x:35367,y:32507,ptovrint:False,ptlb:Whitecap,ptin:_Whitecap,varname:node_8751,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:1225,x:35364,y:32812,varname:node_1225,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:4522,x:35565,y:32757,varname:node_4522,prsc:2|A-2952-OUT,B-1225-OUT;n:type:ShaderForge.SFN_Tex2d,id:9286,x:35787,y:32716,varname:node_9286,prsc:2,ntxv:0,isnm:False|UVIN-4522-OUT,TEX-8751-TEX;n:type:ShaderForge.SFN_Tex2d,id:9648,x:35787,y:32461,varname:node_9648,prsc:2,ntxv:0,isnm:False|UVIN-2952-OUT,TEX-8751-TEX;n:type:ShaderForge.SFN_Lerp,id:693,x:36034,y:32499,varname:node_693,prsc:2|A-9648-RGB,B-9286-RGB,T-3856-OUT;n:type:ShaderForge.SFN_Lerp,id:7856,x:36034,y:32676,varname:node_7856,prsc:2|A-9648-A,B-9286-A,T-3856-OUT;n:type:ShaderForge.SFN_Color,id:2087,x:35810,y:32098,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_2087,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.5,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:9185,x:35810,y:32276,ptovrint:False,ptlb:Lightend Color,ptin:_LightendColor,varname:node_9185,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:8448,x:36146,y:32164,cmnt: Final Color,varname:node_8448,prsc:2|A-2087-RGB,B-9185-RGB,T-2511-OUT;n:type:ShaderForge.SFN_ViewVector,id:1700,x:35953,y:32822,varname:node_1700,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:6755,x:35953,y:32948,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:163,x:36161,y:32883,varname:node_163,prsc:2,dt:3|A-1700-OUT,B-6755-OUT;n:type:ShaderForge.SFN_Slider,id:5888,x:36211,y:33054,ptovrint:False,ptlb:Reflection Tint,ptin:_ReflectionTint,varname:node_5888,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:8391,x:36555,y:32922,varname:node_8391,prsc:2|A-163-OUT,B-5888-OUT;n:type:ShaderForge.SFN_Vector3,id:1151,x:36555,y:33048,varname:node_1151,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_Multiply,id:3994,x:36555,y:32750,varname:node_3994,prsc:2|A-7856-OUT,B-8419-OUT;n:type:ShaderForge.SFN_Slider,id:9618,x:35618,y:33264,ptovrint:False,ptlb:Whitecap Power,ptin:_WhitecapPower,varname:node_9618,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:6;n:type:ShaderForge.SFN_Power,id:8419,x:35964,y:33247,varname:node_8419,prsc:2|VAL-2511-OUT,EXP-9618-OUT;n:type:ShaderForge.SFN_Lerp,id:5328,x:36829,y:32897,varname:node_5328,prsc:2|A-8448-OUT,B-1151-OUT,T-8391-OUT;n:type:ShaderForge.SFN_Slider,id:3013,x:36705,y:32524,ptovrint:False,ptlb:Reflection Max,ptin:_ReflectionMax,varname:node_3013,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.75,max:1;n:type:ShaderForge.SFN_Slider,id:1438,x:36705,y:32378,ptovrint:False,ptlb:Reflection Min,ptin:_ReflectionMin,varname:node_1438,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.25,max:1;n:type:ShaderForge.SFN_OneMinus,id:7173,x:37060,y:32375,varname:node_7173,prsc:2|IN-1438-OUT;n:type:ShaderForge.SFN_OneMinus,id:379,x:37060,y:32521,varname:node_379,prsc:2|IN-3013-OUT;n:type:ShaderForge.SFN_ScreenPos,id:8785,x:36829,y:33072,varname:node_8785,prsc:2,sctp:2;n:type:ShaderForge.SFN_ComponentMask,id:8525,x:36829,y:33233,varname:node_8525,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-3792-OUT;n:type:ShaderForge.SFN_Slider,id:6768,x:36672,y:33405,ptovrint:False,ptlb:Reflection Distortion Intensity,ptin:_ReflectionDistortionIntensity,varname:node_6768,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:7534,x:37067,y:33233,varname:node_7534,prsc:2|A-8525-OUT,B-6768-OUT;n:type:ShaderForge.SFN_Add,id:3905,x:37251,y:33077,varname:node_3905,prsc:2|A-8785-UVOUT,B-7534-OUT;n:type:ShaderForge.SFN_Tex2d,id:7011,x:37460,y:33077,ptovrint:False,ptlb:Reflection Texture,ptin:_ReflectionTexture,varname:node_7011,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:1,isnm:False|UVIN-3905-OUT;n:type:ShaderForge.SFN_Multiply,id:5059,x:37747,y:32927,cmnt: Reflection Image,varname:node_5059,prsc:2|A-5328-OUT,B-7011-RGB;n:type:ShaderForge.SFN_Relay,id:7034,x:38068,y:32982,varname:node_7034,prsc:2|IN-3994-OUT;n:type:ShaderForge.SFN_Lerp,id:5140,x:38038,y:32835,varname:node_5140,prsc:2|A-5059-OUT,B-8448-OUT,T-2617-OUT;n:type:ShaderForge.SFN_Vector1,id:9665,x:37227,y:32302,varname:node_9665,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:6756,x:37227,y:32231,varname:node_6756,prsc:2,v1:0;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:2617,x:37489,y:32308,varname:node_2617,prsc:2|IN-163-OUT,IMIN-6756-OUT,IMAX-9665-OUT,OMIN-379-OUT,OMAX-7173-OUT;n:type:ShaderForge.SFN_Lerp,id:7728,x:38279,y:32677,varname:node_7728,prsc:2|A-693-OUT,B-5140-OUT,T-3994-OUT;n:type:ShaderForge.SFN_ComponentMask,id:9126,x:36141,y:34486,varname:node_9126,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-571-OUT;n:type:ShaderForge.SFN_Relay,id:2825,x:36079,y:34772,varname:node_2825,prsc:2|IN-7414-OUT;n:type:ShaderForge.SFN_DepthBlend,id:7414,x:33218,y:32070,varname:node_7414,prsc:2|DIST-4173-OUT;n:type:ShaderForge.SFN_Vector1,id:256,x:38838,y:33485,varname:node_256,prsc:2,v1:0;n:type:ShaderForge.SFN_Clamp01,id:4550,x:38573,y:33560,varname:node_4550,prsc:2|IN-812-OUT;n:type:ShaderForge.SFN_Subtract,id:812,x:38396,y:33560,varname:node_812,prsc:2|A-5428-OUT,B-7034-OUT;n:type:ShaderForge.SFN_Slider,id:5428,x:38040,y:33560,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_5428,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.9,max:1;n:type:ShaderForge.SFN_Add,id:791,x:38633,y:32650,varname:node_791,prsc:2|A-5599-OUT,B-7728-OUT;n:type:ShaderForge.SFN_Relay,id:6047,x:38894,y:33878,varname:node_6047,prsc:2|IN-4398-OUT;n:type:ShaderForge.SFN_Relay,id:9182,x:38894,y:33928,varname:node_9182,prsc:2|IN-9067-OUT;n:type:ShaderForge.SFN_Tex2d,id:146,x:34821,y:34919,varname:node_146,prsc:2,ntxv:0,isnm:False|UVIN-1135-UVOUT,TEX-6686-TEX;n:type:ShaderForge.SFN_Slider,id:7035,x:33728,y:34909,ptovrint:False,ptlb:Normal 2 Horiz Speed,ptin:_Normal2HorizSpeed,varname:node_7035,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:8091,x:35984,y:34661,ptovrint:False,ptlb:Refraction Intensity,ptin:_RefractionIntensity,varname:node_8091,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:1689-4693-1365-6916-5226-8395-8007-1204-5707-1660-418-5321-5581-6686-4173-4668-3081-4347-9061-161-3108-5295-6812-4015-3565-1739-5211-268-7341-8751-9618-5428-2087-9185-5888-3013-1438-6768-7011-4445-193-5391-9511-2016-3599-9588-9518-7770-1699-7035-8091-5314-5899-4577-8495;pass:END;sub:END;*/

Shader "Shader Forge/WaterShader" {
    Properties {
        _SwayFrequency ("Sway Frequency", Float ) = 1
        _SwayIntensity ("Sway Intensity", Range(0, 1)) = 0.5
        [MaterialToggle] _TogglFlowWithObj ("Toggl Flow With Obj", Float ) = 1
        _FlowMovementHorizontal ("Flow Movement Horizontal", Range(0, 1)) = 0.1
        _FlowMovementVertical ("Flow Movement Vertical", Range(0, 1)) = 0.1
        _FlowMap ("FlowMap", 2D) = "white" {}
        _FlowSpeed ("Flow Speed", Float ) = 1
        _FlowPower ("Flow Power", Float ) = 0.5
        _NormalTilingDifference ("NormalTilingDifference", Float ) = 1.4
        _Normal1HorizontalSpeed ("Normal 1 Horizontal Speed", Range(-1, 1)) = 0
        _Normal1VerticalSpeed ("Normal 1 Vertical Speed", Range(-1, 1)) = 0
        _Normal2VerticalSpeed ("Normal 2 Vertical Speed", Range(-1, 1)) = 0
        _NormalScrollSpeed ("NormalScrollSpeed", Float ) = 0.2
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _Depth ("Depth", Float ) = 0
        _Wave1XDirection ("Wave 1 X-Direction", Range(-1, 1)) = 0
        _Wave1ZDirection ("Wave 1 Z-Direction", Range(-1, 1)) = 0
        _Wave2XDirection ("Wave 2 X-Direction", Range(-1, 1)) = 0
        _Wave2ZDirection ("Wave 2 Z-Direction", Range(-1, 1)) = 0
        _Wave3XDirection ("Wave 3 X-Direction", Range(-1, 1)) = 0
        _Wave3ZDirection ("Wave 3 Z-Direction", Range(-1, 1)) = 0
        _WaveSpeed ("Wave Speed", Float ) = 1
        _WaveSpread ("Wave Spread", Float ) = 1
        _WaveHeight ("Wave Height", Float ) = 1
        _WaveSharpness ("Wave Sharpness", Float ) = 1
        _WhitecapScrollSpeedHorizontal ("Whitecap Scroll Speed Horizontal", Float ) = 0
        _WhitecapScrollSpeedVertical ("Whitecap Scroll Speed Vertical", Float ) = 0
        _FoamTransitionDistance ("Foam Transition Distance", Float ) = 100
        _FoamTransitionFade ("Foam Transition Fade", Float ) = 25
        _Whitecap ("Whitecap", 2D) = "white" {}
        _WhitecapPower ("Whitecap Power", Range(0, 6)) = 0.5
        _Gloss ("Gloss", Range(0, 1)) = 0.9
        _Color ("Color", Color) = (0,0.5,1,1)
        _LightendColor ("Lightend Color", Color) = (0.5,0.5,0.5,1)
        _ReflectionTint ("Reflection Tint", Range(0, 1)) = 0
        _ReflectionMax ("Reflection Max", Range(0, 1)) = 0.75
        _ReflectionMin ("Reflection Min", Range(0, 1)) = 0.25
        _ReflectionDistortionIntensity ("Reflection Distortion Intensity", Range(0, 1)) = 0
        _ReflectionTexture ("Reflection Texture", 2D) = "gray" {}
        _FoamTimeSpeed ("Foam Time Speed", Range(0, 1)) = 0.009579911
        _FoamVerticalSpeed ("Foam Vertical Speed", Float ) = 0
        _FoamHorizontalSpeed ("Foam Horizontal Speed", Float ) = 0
        _FoamSpread ("Foam Spread", Range(0, 1)) = 1
        _FoamFalloff ("Foam Falloff", Float ) = 0.2
        _EdgeLineWidth ("Edge-Line Width", Float ) = 0
        _FoamOutlineFalloff ("FoamOutlineFalloff", Float ) = 1
        _Foam ("Foam", 2D) = "white" {}
        _FoamIntensity ("Foam Intensity", Range(0, 1)) = 1
        _OpacityMinimum ("Opacity Minimum", Range(0, 1)) = 0.25
        _Normal2HorizSpeed ("Normal 2 Horiz Speed", Range(0, 1)) = 0
        _RefractionIntensity ("Refraction Intensity", Range(0, 1)) = 0
        _TesselationMax ("Tesselation Max", Float ) = 5
        _TesselationMin ("Tesselation Min", Float ) = 2
        _FarTesselationAmount ("Far Tesselation Amount", Float ) = 2
        _FarTesselationCurve ("Far Tesselation Curve", Float ) = 50
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform sampler2D _CameraDepthTexture;
            uniform float _SwayFrequency;
            uniform float _SwayIntensity;
            uniform fixed _TogglFlowWithObj;
            uniform float _FlowMovementHorizontal;
            uniform float _FlowMovementVertical;
            uniform sampler2D _FlowMap; uniform float4 _FlowMap_ST;
            uniform float _FlowSpeed;
            uniform float _FlowPower;
            uniform float _NormalTilingDifference;
            uniform float _Wave1XDirection;
            uniform float _Wave1ZDirection;
            uniform float _Wave2XDirection;
            uniform float _Wave2ZDirection;
            uniform float _Wave3XDirection;
            uniform float _Wave3ZDirection;
            uniform float _WaveSpeed;
            uniform float _WaveSpread;
            uniform float _WaveHeight;
            uniform float _WaveSharpness;
            float3 Gerstner( float Steepness , float Amplitude , float Wavelength , float Speed , float Time , float DotProd , float2 DirectionVec ){
            float phaseConst = Speed * Wavelength;
            float xVal = Steepness * Amplitude * DirectionVec.x * cos(Wavelength * DotProd + Time * phaseConst);
            float yVal = Amplitude * sin(Wavelength + Time * phaseConst);
            float zVal = Steepness * Amplitude * DirectionVec.y * cos(Wavelength * DotProd + Time * phaseConst);
            return float3(xVal, yVal, zVal);
            }
            
            float3 Gerstner2( float Steepness , float Amplitude , float Wavelength , float Speed , float Time , float DotProd , float2 DirectionVec ){
            float phaseConst = Speed * Wavelength;
            float xVal = Steepness * Amplitude * DirectionVec.x * cos(Wavelength * DotProd + Time * phaseConst);
            float yVal = Amplitude * sin(Wavelength + Time * phaseConst);
            float zVal = Steepness * Amplitude * DirectionVec.y * cos(Wavelength * DotProd + Time * phaseConst);
            return float3(xVal, yVal, zVal);
            }
            
            float3 Gerstner3( float Steepness , float Amplitude , float Wavelength , float Speed , float Time , float DotProd , float2 DirectionVec ){
            float phaseConst = Speed * Wavelength;
            float xVal = Steepness * Amplitude * DirectionVec.x * cos(Wavelength * DotProd + Time * phaseConst);
            float yVal = Amplitude * sin(Wavelength + Time * phaseConst);
            float zVal = Steepness * Amplitude * DirectionVec.y * cos(Wavelength * DotProd + Time * phaseConst);
            return float3(xVal, yVal, zVal);
            }
            
            uniform float _Normal1HorizontalSpeed;
            uniform float _Normal1VerticalSpeed;
            uniform float _NormalScrollSpeed;
            uniform float _Normal2VerticalSpeed;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _TesselationMax;
            uniform float _TesselationMin;
            uniform float _FarTesselationAmount;
            uniform float _FarTesselationCurve;
            uniform float _FoamTimeSpeed;
            uniform float _FoamVerticalSpeed;
            uniform float _FoamHorizontalSpeed;
            uniform float _Depth;
            uniform float _FoamSpread;
            uniform float _OpacityMinimum;
            uniform float _FoamFalloff;
            uniform float _EdgeLineWidth;
            uniform float _FoamOutlineFalloff;
            uniform sampler2D _Foam; uniform float4 _Foam_ST;
            uniform float _FoamIntensity;
            uniform float _WhitecapScrollSpeedHorizontal;
            uniform float _WhitecapScrollSpeedVertical;
            uniform float _FoamTransitionDistance;
            uniform float _FoamTransitionFade;
            uniform sampler2D _Whitecap; uniform float4 _Whitecap_ST;
            uniform float4 _Color;
            uniform float4 _LightendColor;
            uniform float _ReflectionTint;
            uniform float _WhitecapPower;
            uniform float _ReflectionMax;
            uniform float _ReflectionMin;
            uniform float _ReflectionDistortionIntensity;
            uniform sampler2D _ReflectionTexture; uniform float4 _ReflectionTexture_ST;
            uniform float _Gloss;
            uniform float _Normal2HorizSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 projPos : TEXCOORD7;
                UNITY_FOG_COORDS(8)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD9;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                    float2 texcoord1 : TEXCOORD1;
                    float2 texcoord2 : TEXCOORD2;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    o.texcoord1 = v.texcoord1;
                    o.texcoord2 = v.texcoord2;
                    return o;
                }
                void displacement (inout VertexInput v){
                    float4 node_7573 = _Time;
                    float node_6160 = ((_SwayIntensity*sin((_SwayFrequency*6.28318530718*node_7573.g)))+node_7573.g);
                    float node_76 = node_6160;
                    float2 node_9506 = float2(_Wave1XDirection,_Wave1ZDirection);
                    float2 node_7274 = mul(unity_ObjectToWorld, v.vertex).rgb.rb;
                    float2 node_3832 = float2(_Wave2XDirection,_Wave2ZDirection);
                    float2 node_2477 = float2(_Wave3XDirection,_Wave3ZDirection);
                    float3 node_4398 = ((Gerstner( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , node_76 , dot(node_9506,node_7274) , node_9506 )+Gerstner2( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+2.0) , dot(node_3832,node_7274) , node_3832 )+Gerstner3( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+4.0) , dot(node_2477,node_7274) , node_2477 ))/2.0);
                    v.vertex.xyz += node_4398;
                }
                float Tessellation(TessVertex v){
                    float node_9067_if_leA = step(distance(mul(unity_ObjectToWorld, v.vertex).rgb,_WorldSpaceCameraPos),_FarTesselationCurve);
                    float node_9067_if_leB = step(_FarTesselationCurve,distance(mul(unity_ObjectToWorld, v.vertex).rgb,_WorldSpaceCameraPos));
                    float4 node_7573 = _Time;
                    float node_6160 = ((_SwayIntensity*sin((_SwayFrequency*6.28318530718*node_7573.g)))+node_7573.g);
                    float node_76 = node_6160;
                    float2 node_9506 = float2(_Wave1XDirection,_Wave1ZDirection);
                    float2 node_7274 = mul(unity_ObjectToWorld, v.vertex).rgb.rb;
                    float2 node_3832 = float2(_Wave2XDirection,_Wave2ZDirection);
                    float2 node_2477 = float2(_Wave3XDirection,_Wave3ZDirection);
                    float3 node_4398 = ((Gerstner( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , node_76 , dot(node_9506,node_7274) , node_9506 )+Gerstner2( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+2.0) , dot(node_3832,node_7274) , node_3832 )+Gerstner3( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+4.0) , dot(node_2477,node_7274) , node_2477 ))/2.0);
                    float node_1095 = (_WaveHeight*1.5);
                    float node_2650 = (-1*node_1095);
                    float node_3402 = 0.0;
                    float node_121 = 1.0;
                    float node_2511 = (node_3402 + ( (node_4398 - node_2650) * (node_121 - node_3402) ) / (node_1095 - node_2650)).g;
                    return lerp((node_9067_if_leA*(_TesselationMin + ( (node_2511 - node_3402) * (_TesselationMax - _TesselationMin) ) / (node_121 - node_3402)))+(node_9067_if_leB*_FarTesselationAmount),_FarTesselationAmount,node_9067_if_leA*node_9067_if_leB);
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    v.texcoord1 = vi[0].texcoord1*bary.x + vi[1].texcoord1*bary.y + vi[2].texcoord1*bary.z;
                    displacement(v);
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 node_7573 = _Time;
                float node_6160 = ((_SwayIntensity*sin((_SwayFrequency*6.28318530718*node_7573.g)))+node_7573.g);
                float node_76 = node_6160;
                float node_6554 = node_76;
                float node_3963 = (node_6554*_NormalScrollSpeed);
                float2 scaledNormalUV = (float2(objScale.r,objScale.b)*i.uv0);
                float2 scrollingFlowUV = (((i.uv0*lerp( 1.0, float2(objScale.r,objScale.b), _TogglFlowWithObj ))+(node_6160*_FlowMovementVertical)*float2(0,1))+(node_6160*_FlowMovementHorizontal)*float2(1,0));
                float4 _FlowMap_var = tex2D(_FlowMap,TRANSFORM_TEX(scrollingFlowUV, _FlowMap));
                float2 flowIntensity = ((_FlowMap_var.rgb.rg*2.0+-1.0)*_FlowPower*(-1.0));
                float flowWithSpeed = (node_6160*_FlowSpeed);
                float FlowFrac = frac(flowWithSpeed);
                float pointFive = 0.5;
                float2 node_3362 = lerp((scaledNormalUV+(flowIntensity*FlowFrac)),(scaledNormalUV+(flowIntensity*frac((flowWithSpeed+0.5)))),abs(((FlowFrac-pointFive)/pointFive)));
                float2 node_2665 = (_NormalTilingDifference*node_3362);
                float2 node_3028 = ((node_2665+(_Normal1VerticalSpeed*node_3963)*float2(1,0))+(_Normal1HorizontalSpeed*node_3963)*float2(0,1));
                float3 node_7777 = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_3028, _NormalMap)));
                float2 node_1135 = ((node_3362+(node_3963*_Normal2VerticalSpeed)*float2(1,0))+(node_3963*_Normal2HorizSpeed)*float2(0,1));
                float3 node_146 = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_1135, _NormalMap)));
                float3 node_571 = saturate(max(node_7777.rgb,node_146.rgb));
                float3 normalLocal = node_571;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float2 node_2952 = ((float2(_WhitecapScrollSpeedHorizontal,_WhitecapScrollSpeedVertical)*node_6554)+i.uv0);
                float4 node_9648 = tex2D(_Whitecap,TRANSFORM_TEX(node_2952, _Whitecap));
                float2 node_4522 = (node_2952*0.5);
                float4 node_9286 = tex2D(_Whitecap,TRANSFORM_TEX(node_4522, _Whitecap));
                float node_3856 = saturate(((distance(i.posWorld.rgb,_WorldSpaceCameraPos)-_FoamTransitionDistance)/_FoamTransitionFade));
                float2 node_9506 = float2(_Wave1XDirection,_Wave1ZDirection);
                float2 node_7274 = i.posWorld.rgb.rb;
                float2 node_3832 = float2(_Wave2XDirection,_Wave2ZDirection);
                float2 node_2477 = float2(_Wave3XDirection,_Wave3ZDirection);
                float3 node_4398 = ((Gerstner( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , node_76 , dot(node_9506,node_7274) , node_9506 )+Gerstner2( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+2.0) , dot(node_3832,node_7274) , node_3832 )+Gerstner3( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+4.0) , dot(node_2477,node_7274) , node_2477 ))/2.0);
                float node_1095 = (_WaveHeight*1.5);
                float node_2650 = (-1*node_1095);
                float node_3402 = 0.0;
                float node_121 = 1.0;
                float node_2511 = (node_3402 + ( (node_4398 - node_2650) * (node_121 - node_3402) ) / (node_1095 - node_2650)).g;
                float node_3994 = (lerp(node_9648.a,node_9286.a,node_3856)*pow(node_2511,_WhitecapPower));
                float gloss = saturate((_Gloss-node_3994));
                float perceptualRoughness = 1.0 - saturate((_Gloss-node_3994));
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = 0.0;
                float specularMonochrome;
                float node_7414 = saturate((sceneZ-partZ)/_Depth);
                float node_9573 = (1.0 - saturate(pow(((1.0 / _FoamSpread)*node_7414),_FoamFalloff)));
                float4 node_948 = _Time;
                float node_8283 = sin((node_948.g*_FoamTimeSpeed));
                float2 node_2696 = ((node_2665+(node_8283*_FoamHorizontalSpeed)*float2(1,0))+(_FoamVerticalSpeed*node_8283)*float2(1,1));
                float4 _Foam_var = tex2D(_Foam,TRANSFORM_TEX(node_2696, _Foam));
                float3 node_5599 = ((pow(((_EdgeLineWidth*(objScale.r+objScale.b)*0.5)+node_9573),_FoamOutlineFalloff)+(node_9573*_Foam_var.rgb))*_FoamIntensity);
                float3 node_8448 = lerp(_Color.rgb,_LightendColor.rgb,node_2511); //  Final Color
                float node_163 = abs(dot(viewDirection,i.normalDir));
                float2 node_3905 = (sceneUVs.rg+(node_571.rg*_ReflectionDistortionIntensity));
                float4 _ReflectionTexture_var = tex2D(_ReflectionTexture,TRANSFORM_TEX(node_3905, _ReflectionTexture));
                float node_6756 = 0.0;
                float node_379 = (1.0 - _ReflectionMax);
                float3 diffuseColor = (node_5599+lerp(lerp(node_9648.rgb,node_9286.rgb,node_3856),lerp((lerp(node_8448,float3(1,1,1),(node_163*_ReflectionTint))*_ReflectionTexture_var.rgb),node_8448,(node_379 + ( (node_163 - node_6756) * ((1.0 - _ReflectionMin) - node_379) ) / (1.0 - node_6756))),node_3994)); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,(node_5599.r+clamp(node_7414,_OpacityMinimum,1.0)));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform sampler2D _CameraDepthTexture;
            uniform float _SwayFrequency;
            uniform float _SwayIntensity;
            uniform fixed _TogglFlowWithObj;
            uniform float _FlowMovementHorizontal;
            uniform float _FlowMovementVertical;
            uniform sampler2D _FlowMap; uniform float4 _FlowMap_ST;
            uniform float _FlowSpeed;
            uniform float _FlowPower;
            uniform float _NormalTilingDifference;
            uniform float _Wave1XDirection;
            uniform float _Wave1ZDirection;
            uniform float _Wave2XDirection;
            uniform float _Wave2ZDirection;
            uniform float _Wave3XDirection;
            uniform float _Wave3ZDirection;
            uniform float _WaveSpeed;
            uniform float _WaveSpread;
            uniform float _WaveHeight;
            uniform float _WaveSharpness;
            float3 Gerstner( float Steepness , float Amplitude , float Wavelength , float Speed , float Time , float DotProd , float2 DirectionVec ){
            float phaseConst = Speed * Wavelength;
            float xVal = Steepness * Amplitude * DirectionVec.x * cos(Wavelength * DotProd + Time * phaseConst);
            float yVal = Amplitude * sin(Wavelength + Time * phaseConst);
            float zVal = Steepness * Amplitude * DirectionVec.y * cos(Wavelength * DotProd + Time * phaseConst);
            return float3(xVal, yVal, zVal);
            }
            
            float3 Gerstner2( float Steepness , float Amplitude , float Wavelength , float Speed , float Time , float DotProd , float2 DirectionVec ){
            float phaseConst = Speed * Wavelength;
            float xVal = Steepness * Amplitude * DirectionVec.x * cos(Wavelength * DotProd + Time * phaseConst);
            float yVal = Amplitude * sin(Wavelength + Time * phaseConst);
            float zVal = Steepness * Amplitude * DirectionVec.y * cos(Wavelength * DotProd + Time * phaseConst);
            return float3(xVal, yVal, zVal);
            }
            
            float3 Gerstner3( float Steepness , float Amplitude , float Wavelength , float Speed , float Time , float DotProd , float2 DirectionVec ){
            float phaseConst = Speed * Wavelength;
            float xVal = Steepness * Amplitude * DirectionVec.x * cos(Wavelength * DotProd + Time * phaseConst);
            float yVal = Amplitude * sin(Wavelength + Time * phaseConst);
            float zVal = Steepness * Amplitude * DirectionVec.y * cos(Wavelength * DotProd + Time * phaseConst);
            return float3(xVal, yVal, zVal);
            }
            
            uniform float _Normal1HorizontalSpeed;
            uniform float _Normal1VerticalSpeed;
            uniform float _NormalScrollSpeed;
            uniform float _Normal2VerticalSpeed;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _TesselationMax;
            uniform float _TesselationMin;
            uniform float _FarTesselationAmount;
            uniform float _FarTesselationCurve;
            uniform float _FoamTimeSpeed;
            uniform float _FoamVerticalSpeed;
            uniform float _FoamHorizontalSpeed;
            uniform float _Depth;
            uniform float _FoamSpread;
            uniform float _OpacityMinimum;
            uniform float _FoamFalloff;
            uniform float _EdgeLineWidth;
            uniform float _FoamOutlineFalloff;
            uniform sampler2D _Foam; uniform float4 _Foam_ST;
            uniform float _FoamIntensity;
            uniform float _WhitecapScrollSpeedHorizontal;
            uniform float _WhitecapScrollSpeedVertical;
            uniform float _FoamTransitionDistance;
            uniform float _FoamTransitionFade;
            uniform sampler2D _Whitecap; uniform float4 _Whitecap_ST;
            uniform float4 _Color;
            uniform float4 _LightendColor;
            uniform float _ReflectionTint;
            uniform float _WhitecapPower;
            uniform float _ReflectionMax;
            uniform float _ReflectionMin;
            uniform float _ReflectionDistortionIntensity;
            uniform sampler2D _ReflectionTexture; uniform float4 _ReflectionTexture_ST;
            uniform float _Gloss;
            uniform float _Normal2HorizSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 projPos : TEXCOORD7;
                LIGHTING_COORDS(8,9)
                UNITY_FOG_COORDS(10)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                    float2 texcoord1 : TEXCOORD1;
                    float2 texcoord2 : TEXCOORD2;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    o.texcoord1 = v.texcoord1;
                    o.texcoord2 = v.texcoord2;
                    return o;
                }
                void displacement (inout VertexInput v){
                    float4 node_7573 = _Time;
                    float node_6160 = ((_SwayIntensity*sin((_SwayFrequency*6.28318530718*node_7573.g)))+node_7573.g);
                    float node_76 = node_6160;
                    float2 node_9506 = float2(_Wave1XDirection,_Wave1ZDirection);
                    float2 node_7274 = mul(unity_ObjectToWorld, v.vertex).rgb.rb;
                    float2 node_3832 = float2(_Wave2XDirection,_Wave2ZDirection);
                    float2 node_2477 = float2(_Wave3XDirection,_Wave3ZDirection);
                    float3 node_4398 = ((Gerstner( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , node_76 , dot(node_9506,node_7274) , node_9506 )+Gerstner2( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+2.0) , dot(node_3832,node_7274) , node_3832 )+Gerstner3( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+4.0) , dot(node_2477,node_7274) , node_2477 ))/2.0);
                    v.vertex.xyz += node_4398;
                }
                float Tessellation(TessVertex v){
                    float node_9067_if_leA = step(distance(mul(unity_ObjectToWorld, v.vertex).rgb,_WorldSpaceCameraPos),_FarTesselationCurve);
                    float node_9067_if_leB = step(_FarTesselationCurve,distance(mul(unity_ObjectToWorld, v.vertex).rgb,_WorldSpaceCameraPos));
                    float4 node_7573 = _Time;
                    float node_6160 = ((_SwayIntensity*sin((_SwayFrequency*6.28318530718*node_7573.g)))+node_7573.g);
                    float node_76 = node_6160;
                    float2 node_9506 = float2(_Wave1XDirection,_Wave1ZDirection);
                    float2 node_7274 = mul(unity_ObjectToWorld, v.vertex).rgb.rb;
                    float2 node_3832 = float2(_Wave2XDirection,_Wave2ZDirection);
                    float2 node_2477 = float2(_Wave3XDirection,_Wave3ZDirection);
                    float3 node_4398 = ((Gerstner( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , node_76 , dot(node_9506,node_7274) , node_9506 )+Gerstner2( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+2.0) , dot(node_3832,node_7274) , node_3832 )+Gerstner3( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+4.0) , dot(node_2477,node_7274) , node_2477 ))/2.0);
                    float node_1095 = (_WaveHeight*1.5);
                    float node_2650 = (-1*node_1095);
                    float node_3402 = 0.0;
                    float node_121 = 1.0;
                    float node_2511 = (node_3402 + ( (node_4398 - node_2650) * (node_121 - node_3402) ) / (node_1095 - node_2650)).g;
                    return lerp((node_9067_if_leA*(_TesselationMin + ( (node_2511 - node_3402) * (_TesselationMax - _TesselationMin) ) / (node_121 - node_3402)))+(node_9067_if_leB*_FarTesselationAmount),_FarTesselationAmount,node_9067_if_leA*node_9067_if_leB);
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    v.texcoord1 = vi[0].texcoord1*bary.x + vi[1].texcoord1*bary.y + vi[2].texcoord1*bary.z;
                    displacement(v);
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 node_7573 = _Time;
                float node_6160 = ((_SwayIntensity*sin((_SwayFrequency*6.28318530718*node_7573.g)))+node_7573.g);
                float node_76 = node_6160;
                float node_6554 = node_76;
                float node_3963 = (node_6554*_NormalScrollSpeed);
                float2 scaledNormalUV = (float2(objScale.r,objScale.b)*i.uv0);
                float2 scrollingFlowUV = (((i.uv0*lerp( 1.0, float2(objScale.r,objScale.b), _TogglFlowWithObj ))+(node_6160*_FlowMovementVertical)*float2(0,1))+(node_6160*_FlowMovementHorizontal)*float2(1,0));
                float4 _FlowMap_var = tex2D(_FlowMap,TRANSFORM_TEX(scrollingFlowUV, _FlowMap));
                float2 flowIntensity = ((_FlowMap_var.rgb.rg*2.0+-1.0)*_FlowPower*(-1.0));
                float flowWithSpeed = (node_6160*_FlowSpeed);
                float FlowFrac = frac(flowWithSpeed);
                float pointFive = 0.5;
                float2 node_3362 = lerp((scaledNormalUV+(flowIntensity*FlowFrac)),(scaledNormalUV+(flowIntensity*frac((flowWithSpeed+0.5)))),abs(((FlowFrac-pointFive)/pointFive)));
                float2 node_2665 = (_NormalTilingDifference*node_3362);
                float2 node_3028 = ((node_2665+(_Normal1VerticalSpeed*node_3963)*float2(1,0))+(_Normal1HorizontalSpeed*node_3963)*float2(0,1));
                float3 node_7777 = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_3028, _NormalMap)));
                float2 node_1135 = ((node_3362+(node_3963*_Normal2VerticalSpeed)*float2(1,0))+(node_3963*_Normal2HorizSpeed)*float2(0,1));
                float3 node_146 = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_1135, _NormalMap)));
                float3 node_571 = saturate(max(node_7777.rgb,node_146.rgb));
                float3 normalLocal = node_571;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float2 node_2952 = ((float2(_WhitecapScrollSpeedHorizontal,_WhitecapScrollSpeedVertical)*node_6554)+i.uv0);
                float4 node_9648 = tex2D(_Whitecap,TRANSFORM_TEX(node_2952, _Whitecap));
                float2 node_4522 = (node_2952*0.5);
                float4 node_9286 = tex2D(_Whitecap,TRANSFORM_TEX(node_4522, _Whitecap));
                float node_3856 = saturate(((distance(i.posWorld.rgb,_WorldSpaceCameraPos)-_FoamTransitionDistance)/_FoamTransitionFade));
                float2 node_9506 = float2(_Wave1XDirection,_Wave1ZDirection);
                float2 node_7274 = i.posWorld.rgb.rb;
                float2 node_3832 = float2(_Wave2XDirection,_Wave2ZDirection);
                float2 node_2477 = float2(_Wave3XDirection,_Wave3ZDirection);
                float3 node_4398 = ((Gerstner( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , node_76 , dot(node_9506,node_7274) , node_9506 )+Gerstner2( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+2.0) , dot(node_3832,node_7274) , node_3832 )+Gerstner3( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+4.0) , dot(node_2477,node_7274) , node_2477 ))/2.0);
                float node_1095 = (_WaveHeight*1.5);
                float node_2650 = (-1*node_1095);
                float node_3402 = 0.0;
                float node_121 = 1.0;
                float node_2511 = (node_3402 + ( (node_4398 - node_2650) * (node_121 - node_3402) ) / (node_1095 - node_2650)).g;
                float node_3994 = (lerp(node_9648.a,node_9286.a,node_3856)*pow(node_2511,_WhitecapPower));
                float gloss = saturate((_Gloss-node_3994));
                float perceptualRoughness = 1.0 - saturate((_Gloss-node_3994));
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = 0.0;
                float specularMonochrome;
                float node_7414 = saturate((sceneZ-partZ)/_Depth);
                float node_9573 = (1.0 - saturate(pow(((1.0 / _FoamSpread)*node_7414),_FoamFalloff)));
                float4 node_948 = _Time;
                float node_8283 = sin((node_948.g*_FoamTimeSpeed));
                float2 node_2696 = ((node_2665+(node_8283*_FoamHorizontalSpeed)*float2(1,0))+(_FoamVerticalSpeed*node_8283)*float2(1,1));
                float4 _Foam_var = tex2D(_Foam,TRANSFORM_TEX(node_2696, _Foam));
                float3 node_5599 = ((pow(((_EdgeLineWidth*(objScale.r+objScale.b)*0.5)+node_9573),_FoamOutlineFalloff)+(node_9573*_Foam_var.rgb))*_FoamIntensity);
                float3 node_8448 = lerp(_Color.rgb,_LightendColor.rgb,node_2511); //  Final Color
                float node_163 = abs(dot(viewDirection,i.normalDir));
                float2 node_3905 = (sceneUVs.rg+(node_571.rg*_ReflectionDistortionIntensity));
                float4 _ReflectionTexture_var = tex2D(_ReflectionTexture,TRANSFORM_TEX(node_3905, _ReflectionTexture));
                float node_6756 = 0.0;
                float node_379 = (1.0 - _ReflectionMax);
                float3 diffuseColor = (node_5599+lerp(lerp(node_9648.rgb,node_9286.rgb,node_3856),lerp((lerp(node_8448,float3(1,1,1),(node_163*_ReflectionTint))*_ReflectionTexture_var.rgb),node_8448,(node_379 + ( (node_163 - node_6756) * ((1.0 - _ReflectionMin) - node_379) ) / (1.0 - node_6756))),node_3994)); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * (node_5599.r+clamp(node_7414,_OpacityMinimum,1.0)),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform float _SwayFrequency;
            uniform float _SwayIntensity;
            uniform float _Wave1XDirection;
            uniform float _Wave1ZDirection;
            uniform float _Wave2XDirection;
            uniform float _Wave2ZDirection;
            uniform float _Wave3XDirection;
            uniform float _Wave3ZDirection;
            uniform float _WaveSpeed;
            uniform float _WaveSpread;
            uniform float _WaveHeight;
            uniform float _WaveSharpness;
            float3 Gerstner( float Steepness , float Amplitude , float Wavelength , float Speed , float Time , float DotProd , float2 DirectionVec ){
            float phaseConst = Speed * Wavelength;
            float xVal = Steepness * Amplitude * DirectionVec.x * cos(Wavelength * DotProd + Time * phaseConst);
            float yVal = Amplitude * sin(Wavelength + Time * phaseConst);
            float zVal = Steepness * Amplitude * DirectionVec.y * cos(Wavelength * DotProd + Time * phaseConst);
            return float3(xVal, yVal, zVal);
            }
            
            float3 Gerstner2( float Steepness , float Amplitude , float Wavelength , float Speed , float Time , float DotProd , float2 DirectionVec ){
            float phaseConst = Speed * Wavelength;
            float xVal = Steepness * Amplitude * DirectionVec.x * cos(Wavelength * DotProd + Time * phaseConst);
            float yVal = Amplitude * sin(Wavelength + Time * phaseConst);
            float zVal = Steepness * Amplitude * DirectionVec.y * cos(Wavelength * DotProd + Time * phaseConst);
            return float3(xVal, yVal, zVal);
            }
            
            float3 Gerstner3( float Steepness , float Amplitude , float Wavelength , float Speed , float Time , float DotProd , float2 DirectionVec ){
            float phaseConst = Speed * Wavelength;
            float xVal = Steepness * Amplitude * DirectionVec.x * cos(Wavelength * DotProd + Time * phaseConst);
            float yVal = Amplitude * sin(Wavelength + Time * phaseConst);
            float zVal = Steepness * Amplitude * DirectionVec.y * cos(Wavelength * DotProd + Time * phaseConst);
            return float3(xVal, yVal, zVal);
            }
            
            uniform float _TesselationMax;
            uniform float _TesselationMin;
            uniform float _FarTesselationAmount;
            uniform float _FarTesselationCurve;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord1 : TEXCOORD1;
                    float2 texcoord2 : TEXCOORD2;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord1 = v.texcoord1;
                    o.texcoord2 = v.texcoord2;
                    return o;
                }
                void displacement (inout VertexInput v){
                    float4 node_7573 = _Time;
                    float node_6160 = ((_SwayIntensity*sin((_SwayFrequency*6.28318530718*node_7573.g)))+node_7573.g);
                    float node_76 = node_6160;
                    float2 node_9506 = float2(_Wave1XDirection,_Wave1ZDirection);
                    float2 node_7274 = mul(unity_ObjectToWorld, v.vertex).rgb.rb;
                    float2 node_3832 = float2(_Wave2XDirection,_Wave2ZDirection);
                    float2 node_2477 = float2(_Wave3XDirection,_Wave3ZDirection);
                    float3 node_4398 = ((Gerstner( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , node_76 , dot(node_9506,node_7274) , node_9506 )+Gerstner2( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+2.0) , dot(node_3832,node_7274) , node_3832 )+Gerstner3( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+4.0) , dot(node_2477,node_7274) , node_2477 ))/2.0);
                    v.vertex.xyz += node_4398;
                }
                float Tessellation(TessVertex v){
                    float node_9067_if_leA = step(distance(mul(unity_ObjectToWorld, v.vertex).rgb,_WorldSpaceCameraPos),_FarTesselationCurve);
                    float node_9067_if_leB = step(_FarTesselationCurve,distance(mul(unity_ObjectToWorld, v.vertex).rgb,_WorldSpaceCameraPos));
                    float4 node_7573 = _Time;
                    float node_6160 = ((_SwayIntensity*sin((_SwayFrequency*6.28318530718*node_7573.g)))+node_7573.g);
                    float node_76 = node_6160;
                    float2 node_9506 = float2(_Wave1XDirection,_Wave1ZDirection);
                    float2 node_7274 = mul(unity_ObjectToWorld, v.vertex).rgb.rb;
                    float2 node_3832 = float2(_Wave2XDirection,_Wave2ZDirection);
                    float2 node_2477 = float2(_Wave3XDirection,_Wave3ZDirection);
                    float3 node_4398 = ((Gerstner( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , node_76 , dot(node_9506,node_7274) , node_9506 )+Gerstner2( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+2.0) , dot(node_3832,node_7274) , node_3832 )+Gerstner3( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+4.0) , dot(node_2477,node_7274) , node_2477 ))/2.0);
                    float node_1095 = (_WaveHeight*1.5);
                    float node_2650 = (-1*node_1095);
                    float node_3402 = 0.0;
                    float node_121 = 1.0;
                    float node_2511 = (node_3402 + ( (node_4398 - node_2650) * (node_121 - node_3402) ) / (node_1095 - node_2650)).g;
                    return lerp((node_9067_if_leA*(_TesselationMin + ( (node_2511 - node_3402) * (_TesselationMax - _TesselationMin) ) / (node_121 - node_3402)))+(node_9067_if_leB*_FarTesselationAmount),_FarTesselationAmount,node_9067_if_leA*node_9067_if_leB);
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord1 = vi[0].texcoord1*bary.x + vi[1].texcoord1*bary.y + vi[2].texcoord1*bary.z;
                    displacement(v);
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform sampler2D _CameraDepthTexture;
            uniform float _SwayFrequency;
            uniform float _SwayIntensity;
            uniform fixed _TogglFlowWithObj;
            uniform float _FlowMovementHorizontal;
            uniform float _FlowMovementVertical;
            uniform sampler2D _FlowMap; uniform float4 _FlowMap_ST;
            uniform float _FlowSpeed;
            uniform float _FlowPower;
            uniform float _NormalTilingDifference;
            uniform float _Wave1XDirection;
            uniform float _Wave1ZDirection;
            uniform float _Wave2XDirection;
            uniform float _Wave2ZDirection;
            uniform float _Wave3XDirection;
            uniform float _Wave3ZDirection;
            uniform float _WaveSpeed;
            uniform float _WaveSpread;
            uniform float _WaveHeight;
            uniform float _WaveSharpness;
            float3 Gerstner( float Steepness , float Amplitude , float Wavelength , float Speed , float Time , float DotProd , float2 DirectionVec ){
            float phaseConst = Speed * Wavelength;
            float xVal = Steepness * Amplitude * DirectionVec.x * cos(Wavelength * DotProd + Time * phaseConst);
            float yVal = Amplitude * sin(Wavelength + Time * phaseConst);
            float zVal = Steepness * Amplitude * DirectionVec.y * cos(Wavelength * DotProd + Time * phaseConst);
            return float3(xVal, yVal, zVal);
            }
            
            float3 Gerstner2( float Steepness , float Amplitude , float Wavelength , float Speed , float Time , float DotProd , float2 DirectionVec ){
            float phaseConst = Speed * Wavelength;
            float xVal = Steepness * Amplitude * DirectionVec.x * cos(Wavelength * DotProd + Time * phaseConst);
            float yVal = Amplitude * sin(Wavelength + Time * phaseConst);
            float zVal = Steepness * Amplitude * DirectionVec.y * cos(Wavelength * DotProd + Time * phaseConst);
            return float3(xVal, yVal, zVal);
            }
            
            float3 Gerstner3( float Steepness , float Amplitude , float Wavelength , float Speed , float Time , float DotProd , float2 DirectionVec ){
            float phaseConst = Speed * Wavelength;
            float xVal = Steepness * Amplitude * DirectionVec.x * cos(Wavelength * DotProd + Time * phaseConst);
            float yVal = Amplitude * sin(Wavelength + Time * phaseConst);
            float zVal = Steepness * Amplitude * DirectionVec.y * cos(Wavelength * DotProd + Time * phaseConst);
            return float3(xVal, yVal, zVal);
            }
            
            uniform float _Normal1HorizontalSpeed;
            uniform float _Normal1VerticalSpeed;
            uniform float _NormalScrollSpeed;
            uniform float _Normal2VerticalSpeed;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _TesselationMax;
            uniform float _TesselationMin;
            uniform float _FarTesselationAmount;
            uniform float _FarTesselationCurve;
            uniform float _FoamTimeSpeed;
            uniform float _FoamVerticalSpeed;
            uniform float _FoamHorizontalSpeed;
            uniform float _Depth;
            uniform float _FoamSpread;
            uniform float _FoamFalloff;
            uniform float _EdgeLineWidth;
            uniform float _FoamOutlineFalloff;
            uniform sampler2D _Foam; uniform float4 _Foam_ST;
            uniform float _FoamIntensity;
            uniform float _WhitecapScrollSpeedHorizontal;
            uniform float _WhitecapScrollSpeedVertical;
            uniform float _FoamTransitionDistance;
            uniform float _FoamTransitionFade;
            uniform sampler2D _Whitecap; uniform float4 _Whitecap_ST;
            uniform float4 _Color;
            uniform float4 _LightendColor;
            uniform float _ReflectionTint;
            uniform float _WhitecapPower;
            uniform float _ReflectionMax;
            uniform float _ReflectionMin;
            uniform float _ReflectionDistortionIntensity;
            uniform sampler2D _ReflectionTexture; uniform float4 _ReflectionTexture_ST;
            uniform float _Gloss;
            uniform float _Normal2HorizSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float4 projPos : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                    float2 texcoord0 : TEXCOORD0;
                    float2 texcoord1 : TEXCOORD1;
                    float2 texcoord2 : TEXCOORD2;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    o.texcoord0 = v.texcoord0;
                    o.texcoord1 = v.texcoord1;
                    o.texcoord2 = v.texcoord2;
                    return o;
                }
                void displacement (inout VertexInput v){
                    float4 node_7573 = _Time;
                    float node_6160 = ((_SwayIntensity*sin((_SwayFrequency*6.28318530718*node_7573.g)))+node_7573.g);
                    float node_76 = node_6160;
                    float2 node_9506 = float2(_Wave1XDirection,_Wave1ZDirection);
                    float2 node_7274 = mul(unity_ObjectToWorld, v.vertex).rgb.rb;
                    float2 node_3832 = float2(_Wave2XDirection,_Wave2ZDirection);
                    float2 node_2477 = float2(_Wave3XDirection,_Wave3ZDirection);
                    float3 node_4398 = ((Gerstner( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , node_76 , dot(node_9506,node_7274) , node_9506 )+Gerstner2( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+2.0) , dot(node_3832,node_7274) , node_3832 )+Gerstner3( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+4.0) , dot(node_2477,node_7274) , node_2477 ))/2.0);
                    v.vertex.xyz += node_4398;
                }
                float Tessellation(TessVertex v){
                    float node_9067_if_leA = step(distance(mul(unity_ObjectToWorld, v.vertex).rgb,_WorldSpaceCameraPos),_FarTesselationCurve);
                    float node_9067_if_leB = step(_FarTesselationCurve,distance(mul(unity_ObjectToWorld, v.vertex).rgb,_WorldSpaceCameraPos));
                    float4 node_7573 = _Time;
                    float node_6160 = ((_SwayIntensity*sin((_SwayFrequency*6.28318530718*node_7573.g)))+node_7573.g);
                    float node_76 = node_6160;
                    float2 node_9506 = float2(_Wave1XDirection,_Wave1ZDirection);
                    float2 node_7274 = mul(unity_ObjectToWorld, v.vertex).rgb.rb;
                    float2 node_3832 = float2(_Wave2XDirection,_Wave2ZDirection);
                    float2 node_2477 = float2(_Wave3XDirection,_Wave3ZDirection);
                    float3 node_4398 = ((Gerstner( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , node_76 , dot(node_9506,node_7274) , node_9506 )+Gerstner2( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+2.0) , dot(node_3832,node_7274) , node_3832 )+Gerstner3( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+4.0) , dot(node_2477,node_7274) , node_2477 ))/2.0);
                    float node_1095 = (_WaveHeight*1.5);
                    float node_2650 = (-1*node_1095);
                    float node_3402 = 0.0;
                    float node_121 = 1.0;
                    float node_2511 = (node_3402 + ( (node_4398 - node_2650) * (node_121 - node_3402) ) / (node_1095 - node_2650)).g;
                    return lerp((node_9067_if_leA*(_TesselationMin + ( (node_2511 - node_3402) * (_TesselationMax - _TesselationMin) ) / (node_121 - node_3402)))+(node_9067_if_leB*_FarTesselationAmount),_FarTesselationAmount,node_9067_if_leA*node_9067_if_leB);
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    v.texcoord0 = vi[0].texcoord0*bary.x + vi[1].texcoord0*bary.y + vi[2].texcoord0*bary.z;
                    v.texcoord1 = vi[0].texcoord1*bary.x + vi[1].texcoord1*bary.y + vi[2].texcoord1*bary.z;
                    displacement(v);
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : SV_Target {
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float node_7414 = saturate((sceneZ-partZ)/_Depth);
                float node_9573 = (1.0 - saturate(pow(((1.0 / _FoamSpread)*node_7414),_FoamFalloff)));
                float4 node_948 = _Time;
                float node_8283 = sin((node_948.g*_FoamTimeSpeed));
                float2 scaledNormalUV = (float2(objScale.r,objScale.b)*i.uv0);
                float4 node_7573 = _Time;
                float node_6160 = ((_SwayIntensity*sin((_SwayFrequency*6.28318530718*node_7573.g)))+node_7573.g);
                float2 scrollingFlowUV = (((i.uv0*lerp( 1.0, float2(objScale.r,objScale.b), _TogglFlowWithObj ))+(node_6160*_FlowMovementVertical)*float2(0,1))+(node_6160*_FlowMovementHorizontal)*float2(1,0));
                float4 _FlowMap_var = tex2D(_FlowMap,TRANSFORM_TEX(scrollingFlowUV, _FlowMap));
                float2 flowIntensity = ((_FlowMap_var.rgb.rg*2.0+-1.0)*_FlowPower*(-1.0));
                float flowWithSpeed = (node_6160*_FlowSpeed);
                float FlowFrac = frac(flowWithSpeed);
                float pointFive = 0.5;
                float2 node_3362 = lerp((scaledNormalUV+(flowIntensity*FlowFrac)),(scaledNormalUV+(flowIntensity*frac((flowWithSpeed+0.5)))),abs(((FlowFrac-pointFive)/pointFive)));
                float2 node_2665 = (_NormalTilingDifference*node_3362);
                float2 node_2696 = ((node_2665+(node_8283*_FoamHorizontalSpeed)*float2(1,0))+(_FoamVerticalSpeed*node_8283)*float2(1,1));
                float4 _Foam_var = tex2D(_Foam,TRANSFORM_TEX(node_2696, _Foam));
                float3 node_5599 = ((pow(((_EdgeLineWidth*(objScale.r+objScale.b)*0.5)+node_9573),_FoamOutlineFalloff)+(node_9573*_Foam_var.rgb))*_FoamIntensity);
                float node_76 = node_6160;
                float node_6554 = node_76;
                float2 node_2952 = ((float2(_WhitecapScrollSpeedHorizontal,_WhitecapScrollSpeedVertical)*node_6554)+i.uv0);
                float4 node_9648 = tex2D(_Whitecap,TRANSFORM_TEX(node_2952, _Whitecap));
                float2 node_4522 = (node_2952*0.5);
                float4 node_9286 = tex2D(_Whitecap,TRANSFORM_TEX(node_4522, _Whitecap));
                float node_3856 = saturate(((distance(i.posWorld.rgb,_WorldSpaceCameraPos)-_FoamTransitionDistance)/_FoamTransitionFade));
                float2 node_9506 = float2(_Wave1XDirection,_Wave1ZDirection);
                float2 node_7274 = i.posWorld.rgb.rb;
                float2 node_3832 = float2(_Wave2XDirection,_Wave2ZDirection);
                float2 node_2477 = float2(_Wave3XDirection,_Wave3ZDirection);
                float3 node_4398 = ((Gerstner( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , node_76 , dot(node_9506,node_7274) , node_9506 )+Gerstner2( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+2.0) , dot(node_3832,node_7274) , node_3832 )+Gerstner3( _WaveSharpness , _WaveHeight , _WaveSpread , _WaveSpeed , (node_76+4.0) , dot(node_2477,node_7274) , node_2477 ))/2.0);
                float node_1095 = (_WaveHeight*1.5);
                float node_2650 = (-1*node_1095);
                float node_3402 = 0.0;
                float node_121 = 1.0;
                float node_2511 = (node_3402 + ( (node_4398 - node_2650) * (node_121 - node_3402) ) / (node_1095 - node_2650)).g;
                float3 node_8448 = lerp(_Color.rgb,_LightendColor.rgb,node_2511); //  Final Color
                float node_163 = abs(dot(viewDirection,i.normalDir));
                float node_3963 = (node_6554*_NormalScrollSpeed);
                float2 node_3028 = ((node_2665+(_Normal1VerticalSpeed*node_3963)*float2(1,0))+(_Normal1HorizontalSpeed*node_3963)*float2(0,1));
                float3 node_7777 = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_3028, _NormalMap)));
                float2 node_1135 = ((node_3362+(node_3963*_Normal2VerticalSpeed)*float2(1,0))+(node_3963*_Normal2HorizSpeed)*float2(0,1));
                float3 node_146 = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_1135, _NormalMap)));
                float3 node_571 = saturate(max(node_7777.rgb,node_146.rgb));
                float2 node_3905 = (sceneUVs.rg+(node_571.rg*_ReflectionDistortionIntensity));
                float4 _ReflectionTexture_var = tex2D(_ReflectionTexture,TRANSFORM_TEX(node_3905, _ReflectionTexture));
                float node_6756 = 0.0;
                float node_379 = (1.0 - _ReflectionMax);
                float node_3994 = (lerp(node_9648.a,node_9286.a,node_3856)*pow(node_2511,_WhitecapPower));
                float3 diffColor = (node_5599+lerp(lerp(node_9648.rgb,node_9286.rgb,node_3856),lerp((lerp(node_8448,float3(1,1,1),(node_163*_ReflectionTint))*_ReflectionTexture_var.rgb),node_8448,(node_379 + ( (node_163 - node_6756) * ((1.0 - _ReflectionMin) - node_379) ) / (1.0 - node_6756))),node_3994));
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0.0, specColor, specularMonochrome );
                float roughness = 1.0 - saturate((_Gloss-node_3994));
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
