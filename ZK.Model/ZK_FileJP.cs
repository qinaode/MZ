/**  智客知识管理平台。
* ZK_FileJP.cs
*
* 功 能： N/A
* 类 名： ZK_FileJP
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/15 13:49:32   N/A    初版
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
	/// ZK_FileJP:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ZK_FileJP
	{
		public ZK_FileJP()
		{}
		#region Model
		private int _id;
		private int? _typeid;
		private int? _fileid;
		private string _filename;
		private int? _filetype;
		private string _imageurl;

		private int? _sortnum;

		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}

       
		/// <summary>
		/// 
		/// </summary>
		public int? typeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? fileID
		{
			set{ _fileid=value;}
			get{return _fileid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fileName
		{
			set{ _filename=value;}
			get{return _filename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? fileType
		{
			set{ _filetype=value;}
			get{return _filetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string imageURL
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? sortNum
		{
			set{ _sortnum=value;}
			get{return _sortnum;}
		}
		#endregion Model

	}
}

