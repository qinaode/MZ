/**  智客知识管理平台。
* DEPARTUSERS.cs
*
* 功 能： N/A
* 类 名： DEPARTUSERS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/1 10:24:49   N/A    初版
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
	/// DEPARTUSERS:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DEPARTUSERS
	{
		public DEPARTUSERS()
		{}
		#region Model
		private int _userid;
		private int _departid;
		private string _departname;
		/// <summary>
		/// 
		/// </summary>
		public int USERID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DEPARTID
		{
			set{ _departid=value;}
			get{return _departid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DEPARTNAME
		{
			set{ _departname=value;}
			get{return _departname;}
		}
		#endregion Model

	}
}

