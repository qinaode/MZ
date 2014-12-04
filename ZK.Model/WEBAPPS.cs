/**  智客知识管理平台。
* WEBAPPS.cs
*
* 功 能： N/A
* 类 名： WEBAPPS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:40   N/A    初版
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
	public partial class WEBAPPS
	{
		public WEBAPPS()
		{}
		#region Model
		private int _appid;
		private int _forusertype;
		private string _appname;
		private string _category;
		private string _introduction;
		private string _appimage;
		private string _appurl;
		private int _method;
		private string _postdata;
		private int? _popup;
		private int _clientwebbrowser;
		private int _webbrowserwidth;
		private int _webbrowserheight;
		private int _shortcut;
		private int _ordervalue;
		private DateTime _createtime;
		/// <summary>
		/// 
		/// </summary>
		public int APPID
		{
			set{ _appid=value;}
			get{return _appid;}
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
		public string APPNAME
		{
			set{ _appname=value;}
			get{return _appname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CATEGORY
		{
			set{ _category=value;}
			get{return _category;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string INTRODUCTION
		{
			set{ _introduction=value;}
			get{return _introduction;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string APPIMAGE
		{
			set{ _appimage=value;}
			get{return _appimage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string APPURL
		{
			set{ _appurl=value;}
			get{return _appurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int METHOD
		{
			set{ _method=value;}
			get{return _method;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string POSTDATA
		{
			set{ _postdata=value;}
			get{return _postdata;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? POPUP
		{
			set{ _popup=value;}
			get{return _popup;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CLIENTWEBBROWSER
		{
			set{ _clientwebbrowser=value;}
			get{return _clientwebbrowser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int WEBBROWSERWIDTH
		{
			set{ _webbrowserwidth=value;}
			get{return _webbrowserwidth;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int WEBBROWSERHEIGHT
		{
			set{ _webbrowserheight=value;}
			get{return _webbrowserheight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SHORTCUT
		{
			set{ _shortcut=value;}
			get{return _shortcut;}
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

