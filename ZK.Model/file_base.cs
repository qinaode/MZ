/**  智客知识管理平台。
* file_base.cs
*
* 功 能： N/A
* 类 名： file_base
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/28 14:00:04   N/A    初版
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
	/// file_base:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class file_base
	{
		public file_base()
		{}
		#region Model
		private int _file_id;
		private string _file_name;
		private string _file_desc;
		private int _file_type;
		private int? _parent_file_id;
		private DateTime? _create_time;
		private DateTime _update_time= Convert.ToDateTime(DateTime.Now);
		private long _file_size;
		private string _file_path;
		private string _file_ext;
		private string _file_download;
		private int _channel_id;
		private int _is_dir;
		private int _owner_id;
		private int? _channel_group_id;
		private int _is_deleted=0;
		private int _download_count=0;
		private int _view_count=0;
		private int? _chapter_id;
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
		public string file_name
		{
			set{ _file_name=value;}
			get{return _file_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string file_desc
		{
			set{ _file_desc=value;}
			get{return _file_desc;}
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
		public int? parent_file_id
		{
			set{ _parent_file_id=value;}
			get{return _parent_file_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? create_time
		{
			set{ _create_time=value;}
			get{return _create_time;}
		}
		/// <summary>
		/// on update CURRENT_TIMESTAMP
		/// </summary>
		public DateTime update_time
		{
			set{ _update_time=value;}
			get{return _update_time;}
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
		public string file_ext
		{
			set{ _file_ext=value;}
			get{return _file_ext;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string file_download
		{
			set{ _file_download=value;}
			get{return _file_download;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int channel_id
		{
			set{ _channel_id=value;}
			get{return _channel_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int is_dir
		{
			set{ _is_dir=value;}
			get{return _is_dir;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int owner_id
		{
			set{ _owner_id=value;}
			get{return _owner_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? channel_group_id
		{
			set{ _channel_group_id=value;}
			get{return _channel_group_id;}
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
		public int download_count
		{
			set{ _download_count=value;}
			get{return _download_count;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int view_count
		{
			set{ _view_count=value;}
			get{return _view_count;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? chapter_id
		{
			set{ _chapter_id=value;}
			get{return _chapter_id;}
		}
		#endregion Model

	}
}

