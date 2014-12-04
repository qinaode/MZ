/**  智客知识管理平台。
* ZK_NT_MsgExtent.cs
*
* 功 能： N/A
* 类 名： ZK_NT_MsgExtent
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/3/5 14:54:08   N/A    初版
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
	/// ZK_NT_MsgExtent:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ZK_NT_MsgExtent
	{
		public ZK_NT_MsgExtent()
		{}
		#region Model
		private int _id;
		private string _extkey;
		private string _extvalue;
		private int? _sid;
		/// <summary>
		/// 标识
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 类型名
		/// </summary>
		public string extKey
		{
			set{ _extkey=value;}
			get{return _extkey;}
		}
		/// <summary>
		/// 类型值
		/// </summary>
		public string extValue
		{
			set{ _extvalue=value;}
			get{return _extvalue;}
		}
		/// <summary>
		/// 消息标识
		/// </summary>
		public int? SID
		{
			set{ _sid=value;}
			get{return _sid;}
		}
		#endregion Model

	}
}

