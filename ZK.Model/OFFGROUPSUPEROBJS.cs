﻿/**  智客知识管理平台。
* OFFGROUPSUPEROBJS.cs
*
* 功 能： N/A
* 类 名： OFFGROUPSUPEROBJS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:36   N/A    初版
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
	/// OFFGROUPSUPEROBJS:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OFFGROUPSUPEROBJS
	{
		public OFFGROUPSUPEROBJS()
		{}
		#region Model
		private long _sid;
		private int _groupid;
		private string _msgid;
		private int _fromuserid;
		private string _superobjcode;
		private string _localfilename;
		private DateTime _createtime;
		/// <summary>
		/// 
		/// </summary>
		public long SID
		{
			set{ _sid=value;}
			get{return _sid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int GROUPID
		{
			set{ _groupid=value;}
			get{return _groupid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MSGID
		{
			set{ _msgid=value;}
			get{return _msgid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int FROMUSERID
		{
			set{ _fromuserid=value;}
			get{return _fromuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SUPEROBJCODE
		{
			set{ _superobjcode=value;}
			get{return _superobjcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LOCALFILENAME
		{
			set{ _localfilename=value;}
			get{return _localfilename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CREATETIME
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

