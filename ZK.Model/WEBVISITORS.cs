/**  智客知识管理平台。
* WEBVISITORS.cs
*
* 功 能： N/A
* 类 名： WEBVISITORS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:41   N/A    初版
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
	public partial class WEBVISITORS
	{
		public WEBVISITORS()
		{}
		#region Model
		private int _visitorid;
		private string _visitorcode;
		private int _userid;
		private string _srcurl;
		private string _clientipaddr;
		private string _clientlocation;
		private string _clientos;
		private string _webbrowser;
		private string _remarkname;
		private string _remarktext;
		private int _flag;
		private int _isactive;
		private int _logintimes;
		private DateTime _lastlogintime;
		private int _sendmsgs;
		private int _recvmsgs;
		private int _leavemsgs;
		private DateTime _createtime;
		/// <summary>
		/// 
		/// </summary>
		public int VISITORID
		{
			set{ _visitorid=value;}
			get{return _visitorid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string VISITORCODE
		{
			set{ _visitorcode=value;}
			get{return _visitorcode;}
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
		public string SRCURL
		{
			set{ _srcurl=value;}
			get{return _srcurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CLIENTIPADDR
		{
			set{ _clientipaddr=value;}
			get{return _clientipaddr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CLIENTLOCATION
		{
			set{ _clientlocation=value;}
			get{return _clientlocation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CLIENTOS
		{
			set{ _clientos=value;}
			get{return _clientos;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WEBBROWSER
		{
			set{ _webbrowser=value;}
			get{return _webbrowser;}
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
		public string REMARKTEXT
		{
			set{ _remarktext=value;}
			get{return _remarktext;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int FLAG
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ISACTIVE
		{
			set{ _isactive=value;}
			get{return _isactive;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int LOGINTIMES
		{
			set{ _logintimes=value;}
			get{return _logintimes;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime LASTLOGINTIME
		{
			set{ _lastlogintime=value;}
			get{return _lastlogintime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SENDMSGS
		{
			set{ _sendmsgs=value;}
			get{return _sendmsgs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int RECVMSGS
		{
			set{ _recvmsgs=value;}
			get{return _recvmsgs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int LEAVEMSGS
		{
			set{ _leavemsgs=value;}
			get{return _leavemsgs;}
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

