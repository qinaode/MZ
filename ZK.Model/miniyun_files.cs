﻿/**  智客知识管理平台。
* miniyun_files.cs
*
* 功 能： N/A
* 类 名： miniyun_files
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/12 8:33:57   N/A    初版
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
	/// miniyun_files:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class miniyun_files
	{
		public miniyun_files()
		{}
		#region Model
		private int _id;
		private int _user_id;
		private int _file_type;
		private int _parent_file_id;
		private long _file_create_time;
		private long _file_update_time;
		private string _file_name;
		private int _version_id;
		private long _file_size;
		private string _file_path;
		private string _event_uuid;
		private int _is_deleted=0;
		private string _mime_type;
		private DateTime _created_at;
		private DateTime _updated_at;
		private int _sort=0;
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
		public int file_type
		{
			set{ _file_type=value;}
			get{return _file_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int parent_file_id
		{
			set{ _parent_file_id=value;}
			get{return _parent_file_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long file_create_time
		{
			set{ _file_create_time=value;}
			get{return _file_create_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long file_update_time
		{
			set{ _file_update_time=value;}
			get{return _file_update_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string file_name
		{
			set{ _file_name=value;}
			get{return _file_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int version_id
		{
			set{ _version_id=value;}
			get{return _version_id;}
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
		public string file_path
		{
			set{ _file_path=value;}
			get{return _file_path;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string event_uuid
		{
			set{ _event_uuid=value;}
			get{return _event_uuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int is_deleted
		{
			set{ _is_deleted=value;}
			get{return _is_deleted;}
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
		/// <summary>
		/// 
		/// </summary>
		public int sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		#endregion Model

	}
}

