/**  智客知识管理平台。
* miniyun_users.cs
*
* 功 能： N/A
* 类 名： miniyun_users
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/12 8:26:33   N/A    初版
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
	/// miniyun_users:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class miniyun_users
	{
		public miniyun_users()
		{}
		#region Model
		private int _id;
		private string _user_uuid;
		private string _user_name;
		private string _user_pass;
		private int _user_status=1;
		private string _salt;
		private DateTime _created_at;
		private DateTime _updated_at;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string user_uuid
		{
			set{ _user_uuid=value;}
			get{return _user_uuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string user_name
		{
			set{ _user_name=value;}
			get{return _user_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string user_pass
		{
			set{ _user_pass=value;}
			get{return _user_pass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int user_status
		{
			set{ _user_status=value;}
			get{return _user_status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string salt
		{
			set{ _salt=value;}
			get{return _salt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime created_at
		{
			set{ _created_at=value;}
			get{return _created_at;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime updated_at
		{
			set{ _updated_at=value;}
			get{return _updated_at;}
		}
		#endregion Model

	}
}

