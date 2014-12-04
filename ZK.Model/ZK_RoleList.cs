/**  智客知识管理平台。
* ZK_RoleList.cs
*
* 功 能： N/A
* 类 名： ZK_RoleList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/31 10:35:50   N/A    初版
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
	/// ZK_RoleList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ZK_RoleList
	{
		public ZK_RoleList()
		{}
		#region Model
		private int _roleid;
		private string _rolename;
		private string _roledesc;
		private int? _roleasc;
		private int? _roletype;
		/// <summary>
		/// 角色标识
		/// </summary>
		public int roleID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// 角色名称
		/// </summary>
		public string roleName
		{
			set{ _rolename=value;}
			get{return _rolename;}
		}
		/// <summary>
		/// 角色描述
		/// </summary>
		public string roleDesc
		{
			set{ _roledesc=value;}
			get{return _roledesc;}
		}
		/// <summary>
		/// 排序
		/// </summary>
		public int? roleASC
		{
			set{ _roleasc=value;}
			get{return _roleasc;}
		}
		/// <summary>
		/// 角色类别
		/// </summary>
		public int? roleType
		{
			set{ _roletype=value;}
			get{return _roletype;}
		}
		#endregion Model

	}
}

