/**  智客知识管理平台。
* miniyun_share_files.cs
*
* 功 能： N/A
* 类 名： miniyun_share_files
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/24 20:30:17   N/A    初版
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
	/// miniyun_share_files:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class miniyun_share_files
	{
		public miniyun_share_files()
		{}
		#region Model
		private int _id;
		private string _share_key;
		private int _file_id;
		private int _expires;
		private string _password= "-1";
		private int _down_count=0;
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
		public string share_key
		{
			set{ _share_key=value;}
			get{return _share_key;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int file_id
		{
			set{ _file_id=value;}
			get{return _file_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int expires
		{
			set{ _expires=value;}
			get{return _expires;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int down_count
		{
			set{ _down_count=value;}
			get{return _down_count;}
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

