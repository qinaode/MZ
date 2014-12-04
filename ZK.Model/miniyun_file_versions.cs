/**  智客知识管理平台。
* miniyun_file_versions.cs
*
* 功 能： N/A
* 类 名： miniyun_file_versions
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/24 20:30:15   N/A    初版
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
	/// miniyun_file_versions:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class miniyun_file_versions
	{
		public miniyun_file_versions()
		{}
		#region Model
		private int _id;
		private string _file_signature;
		private long _file_size;
		private string _block_ids;
		private int _ref_count;
		private string _mime_type;
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
		public string file_signature
		{
			set{ _file_signature=value;}
			get{return _file_signature;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long file_size
		{
			set{ _file_size=value;}
			get{return _file_size;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string block_ids
		{
			set{ _block_ids=value;}
			get{return _block_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ref_count
		{
			set{ _ref_count=value;}
			get{return _ref_count;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mime_type
		{
			set{ _mime_type=value;}
			get{return _mime_type;}
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

