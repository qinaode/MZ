/**  智客知识管理平台。
* miniyun_user_metas.cs
*
* 功 能： N/A
* 类 名： miniyun_user_metas
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/12 8:26:21   N/A    初版
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
	/// miniyun_user_metas:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class miniyun_user_metas
	{
		public miniyun_user_metas()
		{}
		#region Model
		private int _id;
		private int _user_id;
		private string _meta_key;
		private string _meta_value;
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
		public int user_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string meta_key
		{
			set{ _meta_key=value;}
			get{return _meta_key;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string meta_value
		{
			set{ _meta_value=value;}
			get{return _meta_value;}
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

