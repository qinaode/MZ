﻿/**  智客知识管理平台。
* OFFLINEFILES.cs
*
* 功 能： N/A
* 类 名： OFFLINEFILES
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:37   N/A    初版
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
	/// OFFLINEFILES:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OFFLINEFILES
	{
		public OFFLINEFILES()
		{}
		#region Model
		private string _sid;
		private int _sender_userid;
		private int _receiver_userid;
		private string _filenameordir;
		private int _isdir;
		private int _allfilecount;
		private long _allfilesize;
		private DateTime _createtime;
		/// <summary>
		/// 
		/// </summary>
		public string SID
		{
			set{ _sid=value;}
			get{return _sid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SENDER_USERID
		{
			set{ _sender_userid=value;}
			get{return _sender_userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int RECEIVER_USERID
		{
			set{ _receiver_userid=value;}
			get{return _receiver_userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FILENAMEORDIR
		{
			set{ _filenameordir=value;}
			get{return _filenameordir;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ISDIR
		{
			set{ _isdir=value;}
			get{return _isdir;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ALLFILECOUNT
		{
			set{ _allfilecount=value;}
			get{return _allfilecount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ALLFILESIZE
		{
			set{ _allfilesize=value;}
			get{return _allfilesize;}
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

