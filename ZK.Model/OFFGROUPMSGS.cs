/**  智客知识管理平台。
* OFFGROUPMSGS.cs
*
* 功 能： N/A
* 类 名： OFFGROUPMSGS
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
	/// OFFGROUPMSGS:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OFFGROUPMSGS
	{
		public OFFGROUPMSGS()
		{}
		#region Model
		private long _sid;
		private int _groupid;
		private string _msgid;
		private int _msglevel;
		private string _content;
		private string _font;
		private int _sender_userid;
		private string _sender_nickname;
		private string _sender_actualname;
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
		public int MSGLEVEL
		{
			set{ _msglevel=value;}
			get{return _msglevel;}
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
		public string FONT
		{
			set{ _font=value;}
			get{return _font;}
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
		public string SENDER_NICKNAME
		{
			set{ _sender_nickname=value;}
			get{return _sender_nickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SENDER_ACTUALNAME
		{
			set{ _sender_actualname=value;}
			get{return _sender_actualname;}
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

