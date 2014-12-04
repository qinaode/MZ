/**  智客知识管理平台。
* SYSMSGS.cs
*
* 功 能： N/A
* 类 名： SYSMSGS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:39   N/A    初版
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
	public partial class SYSMSGS
	{
		public SYSMSGS()
		{}
		#region Model
		private long _sid;
		private int _forusertype;
		private string _title;
		private string _content;
		private string _link;
		private int _sendto;
		private int _online;
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
		public int FORUSERTYPE
		{
			set{ _forusertype=value;}
			get{return _forusertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TITLE
		{
			set{ _title=value;}
			get{return _title;}
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
		public string LINK
		{
			set{ _link=value;}
			get{return _link;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SENDTO
		{
			set{ _sendto=value;}
			get{return _sendto;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ONLINE
		{
			set{ _online=value;}
			get{return _online;}
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

