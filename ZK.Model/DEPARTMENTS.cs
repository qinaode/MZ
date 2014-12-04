/**  智客知识管理平台。
* DEPARTMENTS.cs
*
* 功 能： N/A
* 类 名： DEPARTMENTS
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
	/// DEPARTMENTS:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DEPARTMENTS
	{
		public DEPARTMENTS()
		{}
		#region Model
		private int _departid;
		private string _departname;
		private int _ordervalue;
		private int _parentdepartid;
		private DateTime _createtime;
		/// <summary>
		/// 
		/// </summary>
		public int DEPARTID
		{
			set{ _departid=value;}
			get{return _departid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DEPARTNAME
		{
			set{ _departname=value;}
			get{return _departname;}
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
		public int PARENTDEPARTID
		{
			set{ _parentdepartid=value;}
			get{return _parentdepartid;}
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

