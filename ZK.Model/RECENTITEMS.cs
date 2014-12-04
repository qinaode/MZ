/**  智客知识管理平台。
* RECENTITEMS.cs
*
* 功 能： N/A
* 类 名： RECENTITEMS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:38   N/A    初版
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
	/// RECENTITEMS:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RECENTITEMS
	{
		public RECENTITEMS()
		{}
		#region Model
		private long _sid;
		private int _userid;
		private int _itemtype;
		private string _itemvalue;
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
		public int USERID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ITEMTYPE
		{
			set{ _itemtype=value;}
			get{return _itemtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ITEMVALUE
		{
			set{ _itemvalue=value;}
			get{return _itemvalue;}
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

