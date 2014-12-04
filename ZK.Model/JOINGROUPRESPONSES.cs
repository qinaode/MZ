/**  智客知识管理平台。
* JOINGROUPRESPONSES.cs
*
* 功 能： N/A
* 类 名： JOINGROUPRESPONSES
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:35   N/A    初版
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
	/// JOINGROUPRESPONSES:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class JOINGROUPRESPONSES
	{
		public JOINGROUPRESPONSES()
		{}
		#region Model
		private long _sid;
		private int _userid;
		private int _groupid;
		private string _content;
		private int _agree;
		private int _sender;
		private DateTime _sendtime;
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
		public int GROUPID
		{
			set{ _groupid=value;}
			get{return _groupid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CONTENT
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int AGREE
		{
			set{ _agree=value;}
			get{return _agree;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SENDER
		{
			set{ _sender=value;}
			get{return _sender;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime SENDTIME
		{
			set{ _sendtime=value;}
			get{return _sendtime;}
		}
		#endregion Model

	}
}

