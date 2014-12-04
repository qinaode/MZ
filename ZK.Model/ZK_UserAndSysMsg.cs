/**  智客知识管理平台。
* ZK_UserAndSysMsg.cs
*
* 功 能： N/A
* 类 名： ZK_UserAndSysMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/26 11:22:42   N/A    初版
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
	/// ZK_UserAndSysMsg:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ZK_UserAndSysMsg
	{
		public ZK_UserAndSysMsg()
		{}
		#region Model
		private int _id;
		private int? _sysmsgid;
		private int? _userid;
		private bool _issee;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? sysMsgID
		{
			set{ _sysmsgid=value;}
			get{return _sysmsgid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? userID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool isSee
		{
			set{ _issee=value;}
			get{return _issee;}
		}
		#endregion Model

	}
}

