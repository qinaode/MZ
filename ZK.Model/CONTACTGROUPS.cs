/**  智客知识管理平台。
* CONTACTGROUPS.cs
*
* 功 能： N/A
* 类 名： CONTACTGROUPS
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
	/// CONTACTGROUPS:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CONTACTGROUPS
	{
		public CONTACTGROUPS()
		{}
		#region Model
		private int _contactgroupid;
		private int _userid;
		private string _contactgroupname;
		private int _hidewhenonline;
		private int _onlinewhenhide;
		private int _remindmewhenlogin;
		private int _nomessage;
		private int _ordervalue;
		private DateTime _createtime;
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
		public int USERID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CONTACTGROUPNAME
		{
			set{ _contactgroupname=value;}
			get{return _contactgroupname;}
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
		public int ORDERVALUE
		{
			set{ _ordervalue=value;}
			get{return _ordervalue;}
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

