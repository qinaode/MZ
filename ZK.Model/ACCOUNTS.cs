/**  智客知识管理平台。
* ACCOUNTS.cs
*
* 功 能： N/A
* 类 名： ACCOUNTS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:32   N/A    初版
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
	/// ACCOUNTS:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ACCOUNTS
	{
		public ACCOUNTS()
		{}
		#region Model
		private int _userid;
		private decimal _randomvalue;
		private int _registed;
		private DateTime? _registertime;
		private string _registeripaddr;
		private string _section;
		private DateTime _createtime;
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
		public decimal RANDOMVALUE
		{
			set{ _randomvalue=value;}
			get{return _randomvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int REGISTED
		{
			set{ _registed=value;}
			get{return _registed;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? REGISTERTIME
		{
			set{ _registertime=value;}
			get{return _registertime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string REGISTERIPADDR
		{
			set{ _registeripaddr=value;}
			get{return _registeripaddr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SECTION
		{
			set{ _section=value;}
			get{return _section;}
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

