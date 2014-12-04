/**  智客知识管理平台。
* ACCOUNTSECTIONS.cs
*
* 功 能： N/A
* 类 名： ACCOUNTSECTIONS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:33   N/A    初版
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
	/// ACCOUNTSECTIONS:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ACCOUNTSECTIONS
	{
		public ACCOUNTSECTIONS()
		{}
		#region Model
		private int _beginuserid;
		private int _enduserid;
		private DateTime _createtime;
		/// <summary>
		/// 
		/// </summary>
		public int BEGINUSERID
		{
			set{ _beginuserid=value;}
			get{return _beginuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ENDUSERID
		{
			set{ _enduserid=value;}
			get{return _enduserid;}
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

