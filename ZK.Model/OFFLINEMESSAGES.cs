/**  智客知识管理平台。
* OFFLINEMESSAGES.cs
*
* 功 能： N/A
* 类 名： OFFLINEMESSAGES
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
	/// OFFLINEMESSAGES:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OFFLINEMESSAGES
	{
		public OFFLINEMESSAGES()
		{}
		#region Model
		private string _msgid;
		private int _userid;
		private int _msgtype;
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
		public string MSGID
		{
			set{ _msgid=value;}
			get{return _msgid;}
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
		public int MSGTYPE
		{
			set{ _msgtype=value;}
			get{return _msgtype;}
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

