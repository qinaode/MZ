/**  智客知识管理平台。
* ADMINS.cs
*
* 功 能： N/A
* 类 名： ADMINS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:33   N/A    初版
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
	/// ADMINS:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ADMINS
	{
		public ADMINS()
		{}
		#region Model
		private string _adminname;
		private string _adminpwd;
		private string _description;
		private int _logined;
		private int _islock;
		private DateTime? _lastlogintime;
		private DateTime _createtime;
		/// <summary>
		/// 
		/// </summary>
		public string ADMINNAME
		{
			set{ _adminname=value;}
			get{return _adminname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ADMINPWD
		{
			set{ _adminpwd=value;}
			get{return _adminpwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DESCRIPTION
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int LOGINED
		{
			set{ _logined=value;}
			get{return _logined;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ISLOCK
		{
			set{ _islock=value;}
			get{return _islock;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LASTLOGINTIME
		{
			set{ _lastlogintime=value;}
			get{return _lastlogintime;}
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

