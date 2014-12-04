/**  智客知识管理平台。
* WEBVISITORTOTAL.cs
*
* 功 能： N/A
* 类 名： WEBVISITORTOTAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:41   N/A    初版
*
* Copyright (c) 2012 BeiJing HaoLian Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：北京浩联教育科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace ZK.Model
{
	/// <summary>
	/// 1
	/// </summary>
	[Serializable]
	public partial class WEBVISITORTOTAL
	{
		public WEBVISITORTOTAL()
		{}
		#region Model
		private string _keyname;
		private int _keyvalue;
		/// <summary>
		/// 
		/// </summary>
		public string KEYNAME
		{
			set{ _keyname=value;}
			get{return _keyname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int KEYVALUE
		{
			set{ _keyvalue=value;}
			get{return _keyvalue;}
		}
		#endregion Model

	}
}

