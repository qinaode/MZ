/**  智客知识管理平台。
* GROUPS.cs
*
* 功 能： N/A
* 类 名： GROUPS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:34   N/A    初版
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
	/// GROUPS:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GROUPS
	{
		public GROUPS()
		{}
		#region Model
		private int _groupid;
		private string _groupname;
		private string _introduction;
		private string _notice;
		private int _joinsetting;
		private int _creatorid;
		private int _ownerid;
		private int _ownerusertype;
		private DateTime _createtime;
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
		public string GROUPNAME
		{
			set{ _groupname=value;}
			get{return _groupname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string INTRODUCTION
		{
			set{ _introduction=value;}
			get{return _introduction;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NOTICE
		{
			set{ _notice=value;}
			get{return _notice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int JOINSETTING
		{
			set{ _joinsetting=value;}
			get{return _joinsetting;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CREATORID
		{
			set{ _creatorid=value;}
			get{return _creatorid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int OWNERID
		{
			set{ _ownerid=value;}
			get{return _ownerid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int OWNERUSERTYPE
		{
			set{ _ownerusertype=value;}
			get{return _ownerusertype;}
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

