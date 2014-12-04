/**  智客知识管理平台。
* CONTACTS.cs
*
* 功 能： N/A
* 类 名： CONTACTS
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
	/// CONTACTS:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CONTACTS
	{
		public CONTACTS()
		{}
		#region Model
		private int _contactid;
		private int _userid;
		private int _contactgroupid;
		private string _remarkname;
		private int _hidewhenonline;
		private int _onlinewhenhide;
		private int _remindmewhenlogin;
		private int _nomessage;
		private DateTime _jointime;
		/// <summary>
		/// 
		/// </summary>
		public int CONTACTID
		{
			set{ _contactid=value;}
			get{return _contactid;}
		}
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
		public int CONTACTGROUPID
		{
			set{ _contactgroupid=value;}
			get{return _contactgroupid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string REMARKNAME
		{
			set{ _remarkname=value;}
			get{return _remarkname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int HIDEWHENONLINE
		{
			set{ _hidewhenonline=value;}
			get{return _hidewhenonline;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ONLINEWHENHIDE
		{
			set{ _onlinewhenhide=value;}
			get{return _onlinewhenhide;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int REMINDMEWHENLOGIN
		{
			set{ _remindmewhenlogin=value;}
			get{return _remindmewhenlogin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int NOMESSAGE
		{
			set{ _nomessage=value;}
			get{return _nomessage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime JOINTIME
		{
			set{ _jointime=value;}
			get{return _jointime;}
		}
		#endregion Model

	}
}

