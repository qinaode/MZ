/**  智客知识管理平台。
* GROUPMEMBERS.cs
*
* 功 能： N/A
* 类 名： GROUPMEMBERS
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
	/// GROUPMEMBERS:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GROUPMEMBERS
	{
		public GROUPMEMBERS()
		{}
		#region Model
		private int _memberid;
		private int _groupid;
		private int _ismanager;
		private long _lastrecvmsgsid;
		private int _msghintsetting;
		private int _modifycardbymng;
		private string _card_name;
		private int _card_sex;
		private string _card_number;
		private string _card_email;
		private string _card_remark;
		private DateTime _jointime;
		/// <summary>
		/// 
		/// </summary>
		public int MEMBERID
		{
			set{ _memberid=value;}
			get{return _memberid;}
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
		public int ISMANAGER
		{
			set{ _ismanager=value;}
			get{return _ismanager;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long LASTRECVMSGSID
		{
			set{ _lastrecvmsgsid=value;}
			get{return _lastrecvmsgsid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int MSGHINTSETTING
		{
			set{ _msghintsetting=value;}
			get{return _msghintsetting;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int MODIFYCARDBYMNG
		{
			set{ _modifycardbymng=value;}
			get{return _modifycardbymng;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CARD_NAME
		{
			set{ _card_name=value;}
			get{return _card_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CARD_SEX
		{
			set{ _card_sex=value;}
			get{return _card_sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CARD_NUMBER
		{
			set{ _card_number=value;}
			get{return _card_number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CARD_EMAIL
		{
			set{ _card_email=value;}
			get{return _card_email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CARD_REMARK
		{
			set{ _card_remark=value;}
			get{return _card_remark;}
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

